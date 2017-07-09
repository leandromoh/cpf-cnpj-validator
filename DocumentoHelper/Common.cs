using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentoHelper
{
    public static partial class Common
    {
        internal static readonly Random random = new Random();
        
        internal static int CalculaDigitoVerificador(int n) => n < 2 ? 0 : 11 - n;
        
        internal static int CalculaSoma(IEnumerable<int> xs, IEnumerable<int> ys) => 
            xs.Zip(ys, (x, y) => x * y).Sum();

        internal static int CriaDigitoVerificador(IEnumerable<int> xs, IEnumerable<int> ys) => 
            CalculaDigitoVerificador(CalculaSoma(xs, ys) % 11);
    
        internal static bool Valid(IEnumerable<int> xs, IEnumerable<int> ys) => 
            CriaDigitoVerificador(xs, ys) == xs.ElementAt(ys.Count());

        internal static int[] ToIntArray(this string str) => str.Where(char.IsDigit).Select(x => x - '0').ToArray();

        internal static IEnumerable<int> NumerosRandom(int count, int min, int max) => 
            Enumerable.Range(0, count).Select(_ => random.Next(min, max -1)).ToList();

        internal static IEnumerable<int> CalculateCheckDigits(IEnumerable<int> xs, IEnumerable<IEnumerable<int>> zipper) => 
            zipper.Aggregate(xs, (a,b) => a.Concat(new[]{ CriaDigitoVerificador(b, a) })).Skip(xs.Count());
    }
}