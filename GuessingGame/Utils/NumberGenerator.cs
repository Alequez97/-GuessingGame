using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Utils
{
    public static class NumberGenerator
    {

        public static string GenerateNumber(int size)
        {
            if (size < 1) return "";

            Random random = new Random();
            string newNumber = random.Next(1, 9).ToString();

            for (int i = 0; i < size - 1; i++)    //-1 because first number is already generated before
            {
                string s;
                do
                {
                    s = random.Next(0, 9).ToString();
                } while (newNumber.Contains(s));
                newNumber += s;
            }

            return newNumber;
        }

    }
}
