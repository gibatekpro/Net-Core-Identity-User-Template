builder.Services.AddDbContext<DefaultDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DefaultDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));

// Configure For Dependency Injection. 
//Uses Scrutor. check README for Docs
builder.Services.Scan(scan => scan
    .FromAssemblyOf<EmailService>() //Use any known interface from the project
    .AddClasses(classes => classes.Where(type =>
        type.Name.EndsWith("Service") || 
        type.Name.EndsWith("Repo") || 
        type.Name.EndsWith("Repository") || 
        type.Name.EndsWith("Handler")))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

//Register Authentication method
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AppConfig:Jwt:Issuer"],
        ValidAudience = builder.Configuration["AppConfig:Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AppConfig:Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
