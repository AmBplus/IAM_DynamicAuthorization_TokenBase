
namespace Base.Infrastructure.Extensions
{

    public static class ExtensionsInFrameworkInfrastructureException
    {
        public static void HandleNotFindEntityInRepositoryException<T>(this T? entity) where T : class
        {
            if (entity == null)
            {
                throw new Exception(typeof(T).Name);
            }
        }
        public static void HandleNotFindEntityInRepositoryException<T>(this T? entity, string message) where T : class
        {
            if (entity == null)
            {
                throw new Exception(message);
            }
        }
        public static void HandleNullEntityInRepositoryException<T>(this T? entity) where T : class
        {
            if (entity == null)
            {
                throw new NullReferenceException(typeof(T).Name);
            }
        }
        public static void HandleNullEntityInRepositoryException<T>(this T? entity, string message) where T : class
        {
            if (entity == null)
            {
                throw new NullReferenceException(message);
            }
        }
    }
}