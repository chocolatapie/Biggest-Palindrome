using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var timerCallback = new TimerCallback(DisplayMessagesToUserInConsole);
            var statusChecker = new StatusChecker();
            var timer = new Timer(timerCallback, statusChecker, 0, 2500);

            var biggestPalindromeULong = GetBiggestPalindrome();

            statusChecker.WasPrimeNumberFound = true;
            timer.Dispose();

            Console.WriteLine(biggestPalindromeULong);
            Console.ReadKey();
        }

        /// <summary>
        /// Callback for timer, responsible for displaying messages for user in console
        /// </summary>
        public static void DisplayMessagesToUserInConsole(object obj)
        {
            var currentStatusChecker = (StatusChecker) obj;

            if (currentStatusChecker.StatusMessage == null)
                currentStatusChecker.StatusMessage = new StringBuilder("Please, stand by");

            currentStatusChecker.StatusMessage?.Append(".");

            if (currentStatusChecker.WasPrimeNumberFound == false)
                Console.WriteLine(currentStatusChecker.StatusMessage.ToString());
        }

        /// <summary>
        /// Finds biggest palindrome
        /// </summary>
        public static ulong GetBiggestPalindrome()
        {
            ulong biggestPalindrome = 0;
            var primeNumbers = GetPrimeNumbersFromSieveOfSundaram();
            var primeNumbersLength = primeNumbers.Length;

            for (int i = 0; i < primeNumbersLength; i++)
            {
                for (int j = 0; j < primeNumbersLength; j++)
                {
                    ulong productOfTwoPrimes = primeNumbers[i] * primeNumbers[j];
                    if (IsNumberPalindrome(productOfTwoPrimes) && (productOfTwoPrimes > biggestPalindrome))
                    {
                        biggestPalindrome = productOfTwoPrimes;
                    }
                }
            }
            return biggestPalindrome;
        }

        /// <summary>
        /// Checks if input number is a palindrome 
        /// </summary>
        public static bool IsNumberPalindrome(ulong input)
        {
            var numbersAsCharArray = input.ToString().ToCharArray();
            var arrayLength = numbersAsCharArray.Length;

            for (int i = 0; i < arrayLength / 2; i++)
            {
                if (numbersAsCharArray[i] != numbersAsCharArray[(arrayLength - 1) - i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Finds all prime numbers till upper bound using sieve of Sundaram
        /// </summary>
        public static ulong[] GetPrimeNumbersFromSieveOfSundaram()
        {
            uint upperBound = 100000;

            List<ulong> listOfPrimes = new List<ulong>();
            uint nNew = (upperBound - 2) / 2;
            bool[] marked = new bool[nNew + 2];

            for (ulong i = 1; i <= nNew; i++)
            {
                for (ulong j = i; (i + j + 2 * i * j) <= nNew; j++)
                    marked[i + j + 2 * i * j] = true;
            }
            for (ulong i = 0; i <= nNew; i++)
            {
                if (marked[i] == false)
                    listOfPrimes.Add(2 * i + 1);
            }

            return listOfPrimes.ToArray();
        }
    }
}
