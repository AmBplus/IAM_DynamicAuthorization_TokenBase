namespace Base.AspCore.Infrastructure.Settings;

public class ToastSettings : object
{
    public string ToasterError = nameof(ToasterError);
    public string ToasterInformation = nameof(ToasterInformation);
    public string ToasterSuccess = nameof(ToasterSuccess);
    public string ToasterWarning = nameof(ToasterWarning);

    public ToastSettings()
    {
        DelayStep = 1000;
        InitialDelay = 5000;

        Style =
            "top-25 start-10 p-3 opacity-50";
    }

    // **********
    public string Style { get; set; }
    // **********

    // **********
    public int DelayStep { get; set; }
    // **********

    // **********
    public int InitialDelay { get; set; }
    // **********
}
