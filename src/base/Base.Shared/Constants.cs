namespace Base.Shared
{
    public static class Constants
    {
        static Constants()
        {
        }
        public static class CategoryType
        {
            public const byte Category = 1;
            public const byte SubCategory = 2;
            //public const byte DepartmentCategory = 3;
            //public const byte SubDepartmentCategory = 4;
        }
        public static class FolderPath
        {
            public static string ProductPath = "images\\products";
            public static string SlidersPath = "images\\Sliders";
        }
        public static class ImageExtension
        {
            public const string Jpeg = ".jpeg";
            public const string Jpg = ".jpg";
            public const string Png = ".png";
            public const string Webp = ".webp";
        }
        public static class Page
        {
            static Page()
            {
            }

            public const int PageSize = 8;
            public static int PageSizeInWeb = 8;
        }
        public static class Logger
        {
            static Logger()
            {
            }

            public const string ErrorMessage = "Error Message: {Message}";
        }

        public static class Format
        {
            static Format()
            {
            }
            public const string Date = "yyyy/MM/dd";
            public const string Time = "HH:mm:ss";
            public const string DateTime = "yyyy/MM/dd - HH:mm:ss";
        }

        public static class FixedLength
        {
            static FixedLength()
            {
            }

            public const int NationalCode = 10;
            public const int CellPhoneNumber = 14;
            public const int DatabasePassword = 44;
        }

        public static class MinLength
        {
            static MinLength()
            {
            }

            public const int Password = 8;
            public const int CellPhoneNumberVerificationKey = 6;
        }

        public static class MaxLength
        {
            static MaxLength()
            {
            }

            public const int IP = 15;

            public const int Name = 150;
            public const int Title = 50;

            public const int Username = 20;
            public const int FullName = 50;
            public const int Password = 20;

            public const int EmailAddress = 254;

            public const int CellPhoneNumberVerificationKey = 10;
        }
        public static class RoleId
        {
            public const int Minimum = 1;
            public const int Maximum = 100;
        }

        /// <summary>
        /// https://regex101.com/
        /// </summary>
        public static class RegularExpression
        {
            static RegularExpression()
            {
            }

            public const string NationalCode =
                @"^\d{10}$";

            public const string Username =
                @"^[a-zA-Z0-9_]$";

            public const string CellPhoneNumber =
                @"^00989\d{9}";

            public const string AToZDigitsUnderline =
                @"^[a-zA-Z][a-zA-Z0-9_]*$";

            public const string EmailAddress =
                @"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9_.-]+$";

            public const string Password =
                //	@"^[a-zA-Z0-9_]$";
                "^(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+])[A-Za-z\\d][A-Za-z\\d!@#$%^&*()_+]{1,20}$";


            public const string IP =
                @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

            public const string Url =
                @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";
        }

        public static class Minimum
        {
            static Minimum()
            {
            }

            public const int Ordering = 1;
        }

        public static class Maximum
        {
            static Maximum()
            {
            }

            public const int Ordering = 100_000;
        }
    }
}
