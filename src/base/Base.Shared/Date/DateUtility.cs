namespace Base.Shared.Date;

public static class DateUtility
{
    public static DateTime ToDateTimeNow(this DateTime date)
    {
        return DateTime.UtcNow;
    }
  
    public static DateTime DateTimeNow { get { return DateTime.UtcNow; } } 
}
