using System.Runtime.CompilerServices;

namespace DocumentoHelper;

public readonly struct CPF
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPF(string value)
    {
        Value = value;
        IsValid = Validate(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool Validate(string value)
    {
        Span<int> digits2 = stackalloc int[12];
        var digits = digits2.Slice(1);
        return Utils.TryWriteNumbers(digits, value)
            && CriaDigitoVerificador(digits2) == digits[9]
            && CriaDigitoVerificador(digits) == digits[10];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador(ReadOnlySpan<int> cpf)
    {
        var total =
            cpf[0] * 11 +
            cpf[1] * 10 +
            cpf[2] * 9 +
            cpf[3] * 8 +
            cpf[4] * 7 +
            cpf[5] * 6 +
            cpf[6] * 5 +
            cpf[7] * 4 +
            cpf[8] * 3 +
            cpf[9] * 2;

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }

    public static string Generate()
    {
        Span<int> digits2 = stackalloc int[12];
        var digits = digits2.Slice(1);

        Utils.GenerateImpl(digits, 9);
        digits[9] = CriaDigitoVerificador(digits2);
        digits[10] = CriaDigitoVerificador(digits);
        Span<char> chars = stackalloc char[11];
        Utils.Cast(digits, chars);
        return new string(chars);
    }

    public static string GenerateFormatted()
    {
        Span<int> digits2 = stackalloc int[12];
        var digits = digits2.Slice(1);

        Utils.GenerateImpl(digits, 9);
        digits[9] = CriaDigitoVerificador(digits2);
        digits[10] = CriaDigitoVerificador(digits);
        Span<char> chars = stackalloc char[14];

        Utils.Cast(digits.Slice(0, 3), chars.Slice(0, 3));
        chars[3] = '.';
        Utils.Cast(digits.Slice(3, 3), chars.Slice(4, 3));
        chars[7] = '.';
        Utils.Cast(digits.Slice(6, 3), chars.Slice(8, 3));
        chars[11] = '-';
        Utils.Cast(digits.Slice(9, 2), chars.Slice(12, 2));
        return new string(chars);
    }
}