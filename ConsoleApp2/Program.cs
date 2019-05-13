using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleApp2;

namespace PrimePolindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            TimerCallback tmCallback = new TimerCallback(TalkWithUser);
            StatusChecker statusChecker = new StatusChecker();
            Timer timer = new Timer(tmCallback, statusChecker, 0, 2500);

            ulong[] primeNumbers = GetPrimeNumbersFromSieveOfSundaram();
            ulong biggestPalindrome = GetBiggestPalindrome(primeNumbers);

            statusChecker.WasPrimeNumberFound = true;
            timer.Dispose();

            Console.WriteLine(biggestPalindrome);
            Console.ReadKey();
        }

        public static void TalkWithUser(object obj)
        {
            //Метод отправляющий обновляемое сообщение в консоль
            StatusChecker currentStatusChecker = (StatusChecker) obj;
            if (currentStatusChecker.PhraseForUser == null)
            {
                currentStatusChecker.PhraseForUser = new StringBuilder("Please, stand by");
            }
            if (currentStatusChecker.PhraseForUser != null)
            {
                currentStatusChecker.PhraseForUser.Append(".");
            }
            if (currentStatusChecker.WasPrimeNumberFound == false)
            {
                Console.WriteLine(currentStatusChecker.PhraseForUser.ToString());
            }
        }

        public static ulong GetBiggestPalindrome(ulong[] primeNumbers)
        {
            ulong biggestPalindrome = 0;
            int primeNumbersLength = primeNumbers.Length;
            for (int i = 0; i < primeNumbersLength; i++)
            {
                for (int j = 0; j < primeNumbersLength; j++)
                {
                    ulong productOfTwoPrimes = primeNumbers[i] * primeNumbers[j];
                    if (IsNumberIsPalindrome(productOfTwoPrimes) && (productOfTwoPrimes > biggestPalindrome))
                    {
                        biggestPalindrome = productOfTwoPrimes;
                    }
                }
            }
            return biggestPalindrome;
        }

        public static bool IsNumberIsPalindrome(ulong input)
        {
            //Разбиваем входное число на массив символов
            string inputNumberStr = input.ToString();
            char[] numbers = inputNumberStr.ToCharArray();
            int numLength = numbers.Length;

            //Сравниваем символы с начала строки с символами с конца
            for (int i = 0; i < numLength / 2; i++)
            {
                if (numbers[i] != numbers[(numLength - 1) - i])
                {
                    return false;
                }
            }
            return true;
        }

        public static ulong[] GetPrimeNumbersFromSieveOfSundaram()
        {
            //Нас интересуют простые 5-тизначные числа
            //Значит ищем их ДО первого шестизначного
            uint upperBound = 100000;

            //Для более быстрого поиска простых чисел исспользуем
            //Решето Сундарама (улучшенный вариант Эратосфена)
            List<ulong> listOfPrimes = new List<ulong>();
            uint nNew = (upperBound - 2) / 2;
            bool[] marked = new bool[nNew + 2];

            for (ulong i = 1; i <= nNew; i++)
            {
                for (ulong j = i; (i + j + 2 * i * j) <= nNew; j++)
                {
                    marked[i + j + 2 * i * j] = true;
                }
            }
            for (ulong i = 0; i <= nNew; i++)
            {
                if (marked[i] == false)
                {
                    listOfPrimes.Add(2 * i + 1);
                }
            }
            return listOfPrimes.ToArray();
        }
    }
}