using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefuerzaUPT.Filters
{
    public static class RandomizerHelper
    {
        private static Random rng = new Random();

        /**
         * funcion para randomizar los valores de una lista
         * mas informacion: https://stackoverflow.com/questions/273313/randomize-a-listt
         */
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}