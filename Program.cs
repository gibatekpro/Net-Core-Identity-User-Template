builder.Services.AddDbContext<DefaultDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
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
        ValidIssuer = builder.Configuration["AppConfig:JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["AppConfig:JwtSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AppConfig:JwtSettings:Key"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//Run this to seed the database
//Use it to create a user, roles and assign roles to the user
//await DbSeeder.SeedAsync(app.Services);
app.Run();
