using System.Runtime.CompilerServices;

namespace DocumentoHelper;

public readonly struct CNPJ
{
    public readonly string Value;
    public readonly bool IsValid;

    public CNPJ(string value)
    {
        Value = value;
        IsValid = Validate(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool Validate(string value)
    {
        Span<int> digits2 = stackalloc int[15];
        var digits = digits2.Slice(1);

        return Utils.TryWriteNumbers(digits, value)
            && CriaDigitoVerificador(digits2) == digits[12]
            && CriaDigitoVerificador(digits) == digits[13];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador(ReadOnlySpan<int> cnpj)
    {
        var total =
            cnpj[0] * 6 +
            cnpj[1] * 5 +
            cnpj[2] * 4 +
            cnpj[3] * 3 +
            cnpj[4] * 2 +
            cnpj[5] * 9 +
            cnpj[6] * 8 +
            cnpj[7] * 7 +
            cnpj[8] * 6 +
            cnpj[9] * 5 +
            cnpj[10] * 4 +
            cnpj[11] * 3 +
            cnpj[12] * 2;

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }

    public static string Generate()
    {
        Span<int> digits2 = stackalloc int[15];
        var digits = digits2.Slice(1);

        Utils.GenerateImpl(digits, 8);
        digits[8] = 0;
        digits[9] = 0;
        digits[10] = 0;
        digits[11] = 1;
        digits[12] = CriaDigitoVerificador(digits2);
        digits[13] = CriaDigitoVerificador(digits);
        Span<char> chars = stackalloc char[14];
        Utils.Cast(digits, chars);
        return new string(chars);
    }

    public static string GenerateFormatted()
    {
        Span<int> digits2 = stackalloc int[15];
        var digits = digits2.Slice(1);

        Utils.GenerateImpl(digits, 8);
        digits[8] = 0;
        digits[9] = 0;
        digits[10] = 0;
        digits[11] = 1;
        digits[12] = CriaDigitoVerificador(digits2);
        digits[13] = CriaDigitoVerificador(digits);
        Span<char> chars = stackalloc char[18];

        Utils.Cast(digits.Slice(0, 2), chars.Slice(0, 2));
        chars[2] = '.';
        Utils.Cast(digits.Slice(2, 3), chars.Slice(3, 3));
        chars[6] = '.';
        Utils.Cast(digits.Slice(5, 3), chars.Slice(7, 3));
        chars[10] = '/';
        Utils.Cast(digits.Slice(8, 4), chars.Slice(11, 4));
        chars[15] = '-';
        Utils.Cast(digits.Slice(12, 2), chars.Slice(16, 2));
        return new string(chars);
    }
}