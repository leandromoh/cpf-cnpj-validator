using System;
using System.Collections.Generic;
using System.Linq;
using static DocumentoHelper.Common;

namespace DocumentoHelper
{					
    public static class CPF
    {
        static int[] cpfZipper = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        static IEnumerable<int>[] cpfZipperList = new[] { cpfZipper.Skip(1), cpfZipper };

        public static bool IsValid(this long cpf) => IsValid(cpf.ToString());

        public static bool IsValid(this string cpf) => IsValid(cpf.ToIntArray());

        public static bool IsValid(this int[] cpf) => 
            (cpf ?? throw new ArgumentNullException(nameof(cpf))).Length == 11 && cpfZipperList.All(xs => Valid(cpf, xs));

        public static string Format(this string str) => long.TryParse(str, out var number) ? Format(number) : null;

        public static string Format(this long number) => number.ToString(@"000\.000\.000\-00");

        public static long Generate() => 
            ConcatCheckDigits(NumerosRandom(9, 0, 9)).Aggregate(0L, (acc, x) => acc * 10 + x);

        private static IEnumerable<int> ConcatCheckDigits(IEnumerable<int> xs) => 
            xs.Concat(CalculateCheckDigits(xs));

        public static IEnumerable<int> CalculateCheckDigits(IEnumerable<int> xs) => 
            Common.CalculateCheckDigits(xs, cpfZipperList);
    }
}