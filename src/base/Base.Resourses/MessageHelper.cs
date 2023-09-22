using Base.Resourses;
namespace Base.Resourses;

public static class ResoursMessageHelper
{
    public static string NotFind(this string message)
    {
        return string.Format(Messages.NotFind, message);
    }
}
