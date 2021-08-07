namespace ChatMe.Domain.Extensions.String
{
    using ChatMe.Domain.Exceptions;
    using System;

    public static class StringToDoubles
    {
        public static double ToDouble(this string text)
        {
            Throw.When<NullReferenceException>(string.IsNullOrEmpty(text), string.Empty);

            return Convert.ToDouble(text);
        }
    }
}
