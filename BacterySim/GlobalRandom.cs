using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BacterySim
{
    public static class GlobalRandom
    {
        private static readonly Random Random = new Random();

        public static int Next()
        {
            return Random.Next();
        }

        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public static double NextDouble()
        {
            return Random.NextDouble();
        }

        public static void NextBytes(byte[] buffer)
        {
            Random.NextBytes(buffer);
        }

        public static Vector NextDirection()
        {
            var vec = new Vector(NextDouble(), NextDouble());
            vec.Normalize();
            return vec;
        }
    }
}
