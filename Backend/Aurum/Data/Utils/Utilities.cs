using Microsoft.Identity.Client;

namespace Aurum.Data.Utils
{
    public static class Utilities
    {
        public static Random rand = new();

        public static int GetRandomTenTousands(int min, int max)
        {
            int n = rand.Next(min, max);
            int tenN = n / 10000;
            n = tenN * 10000;
            return n;
        }

        public static int GetRandomThousands(int min, int max)
        {
            int n = rand.Next(min, max);
            int tenN = n / 1000;
            n = tenN * 1000;
            return n;
        }
        public static int GetRandomHundreds(int min, int max)
        {
            int n = rand.Next(min, max);
            int tenN = n / 100;
            n = tenN * 100;
            return n;
        }

        public static int GetRandomTens(int min, int max)
        {
            int n = rand.Next(min, max);
            int tenN = n / 10;
            n = tenN * 10;
            return n;
        }

        public static int GetRandom(int min, int max)
        {
            int n = rand.Next(min, max);
            return n;
        }
        public static T GetRandomElement<T>(List<T> collection)
        {
            var i = rand.Next(0, collection.Count());
            return collection[i];
        }

        public static bool IsIncomeToday()
        {
            return rand.Next(0, 100) > 65;
        }
    }
}
