namespace ChatMe.Domain.Exceptions
{
    using System;
    using System.Net;

    public static class Throw
    {
        public static void When<T>(bool condition, string message) where T : Exception, new()
        {
            if (!condition) return;

            throw (T)Activator.CreateInstance(typeof(T), message);
        }

        public static void When<T>(bool condition, string message, HttpStatusCode code) where T : Exception, new()
        {
            if (!condition) return;

            throw (T)Activator.CreateInstance(typeof(T), code, message);
        }
    }
}
