namespace Base.AspCore.Infrastructure.Settings;

public class CultureSettings : object
{
    // **********
    public string? DefaultCultureName { get; set; }
    // **********

    // **********
    public string[]? SupportedCultureNames { get; set; }
    // **********
}
