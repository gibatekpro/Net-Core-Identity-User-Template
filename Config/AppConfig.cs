public class AppConfig
{
    public JwtSettings Jwt { get; set; }
    public EmailSettings EmailSettings { get; set; }
}

public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public int ExpireHours { get; set; }
}

public class EmailSettings
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
}