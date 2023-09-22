using Base.Shared.Date;

namespace Base.Shared.Date;

public static class PersianDateTimeExtensions
{
    public static string ToPersianDateTimeString(this System.DateTime dateTime)
    {
        var persianDateTime =
            new PersianDateTime(dateTime: dateTime);

        var result =
            persianDateTime.ToString();

        return result;
    }
    public static string ToPersianDateString(this System.DateTime dateTime)
    {
        var persianDate =
            new PersianDate(dateTime: dateTime);

        var result =
            persianDate.ToString();

        return result;
    }
}
