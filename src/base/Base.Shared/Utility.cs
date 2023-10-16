namespace Base.Shared
{
    public static class Utility
    {
        static Utility()
        {
        }
        public static string GetPropertyName<T>(System.Linq.Expressions.Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression.Body is System.Linq.Expressions.MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            throw new ArgumentException("Expression is not a valid property expression.");
        }
        public static class Page
        {
            public static long PageSize { get; set; } = 20;
        }
  

        public static string? FixText(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            text =
                text.Trim();

            while (text.Contains("  "))
            {
                text =
                    text.Replace("  ", " ");
            }

            return text;
        }

        public static string? RemoveSpaces(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            text =
                text.Trim();

            text = text.Replace(oldValue: " ", newValue: string.Empty);

            return text;
        }

        public static string? RemoveSpacesAndMakeTextCaseInsensitive(string? text)
        {
            text = RemoveSpaces(text: text);

            if (string.IsNullOrWhiteSpace(value: text))
            {
                return text;
            }

            text = text.ToLower();

            return text;
        }

    }
}
