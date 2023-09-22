using Base.AspCore.Infrastructure.Settings;

namespace Base.AspCore.Infrastructure.Settings;

public class IconSettings : object
{
    public static readonly string KeyName = nameof(IconSettings);

    public IconSettings()
    {
        UserIcons = new UserIcons();

        TableIcons = new TableIcons();

        SharedIcons = new SharedIcons();
    }

    public UserIcons UserIcons { get; set; }

    public TableIcons TableIcons { get; set; }

    public SharedIcons SharedIcons { get; set; }
}
