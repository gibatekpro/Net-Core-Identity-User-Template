public class AppConfig
{
    public JwtSettings JwtSettings { get; set; } = null!;
    public EmailSettings EmailSettings { get; set; } = null!;
}

public class JwtSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public int ExpireHours { get; set; }
}

public class EmailSettings
{
    public string SmtpServer { get; set; } = null!;
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; } = null!;
    public string SmtpPassword { get; set; } = null!;
}