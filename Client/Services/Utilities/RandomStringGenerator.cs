using System;

namespace TaskPlanner.Client.Services.Utilities
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public string Next(int length = 16)
        {
            var stringChars = new char[length];
            var random = new Random();
            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = Chars[random.Next(Chars.Length)];
            }

            return new string(stringChars);
        }
    }
}
