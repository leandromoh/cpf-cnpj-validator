using System;
using System.Collections.Generic;
using System.Linq;
using static DocumentoHelper.Common;

namespace DocumentoHelper
{
    public static class CNPJ
    {
        static int[] cnpjZipper = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        static IEnumerable<int>[] cnpjZipperList = new[] { cnpjZipper.Skip(1), cnpjZipper };

        public static bool IsValid(this long cnpj) => IsValid(cnpj.ToString());

        public static bool IsValid(this string cnpj) => IsValid(cnpj.ToIntArray());

        public static bool IsValid(this int[] cnpj)
            => (cnpj ?? throw new ArgumentNullException(nameof(cnpj))).Length == 14 && cnpjZipperList.All(xs => Valid(cnpj, xs));

        public static string Format(this string str) => long.TryParse(str, out var number) ? Format(number) : null;

        public static string Format(this long number) => number.ToString(@"00\.000\.000\/0000\-00");

        public static long Generate() =>
            ConcatCheckDigits(NumerosRandom(12, 0, 9)).Aggregate(0L, (acc, x) => acc * 10 + x);

        private static IEnumerable<int> ConcatCheckDigits(IEnumerable<int> xs) =>
            xs.Concat(CalculateCheckDigits(xs));

        public static IEnumerable<int> CalculateCheckDigits(IEnumerable<int> xs) =>
            Common.CalculateCheckDigits(xs, cnpjZipperList);
    }
}