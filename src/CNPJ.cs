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
        Span<int> digits = stackalloc int[14];

        return Utils.TryWriteNumbers(digits, value) 
            && CriaDigitoVerificador1(digits) == digits[12]
            && CriaDigitoVerificador2(digits) == digits[13];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador1(ReadOnlySpan<int> cnpj)
    {
        var total =
            cnpj[0] * 5 +
            cnpj[1] * 4 +
            cnpj[2] * 3 +
            cnpj[3] * 2 +
            cnpj[4] * 9 +
            cnpj[5] * 8 +
            cnpj[6] * 7 +
            cnpj[7] * 6 +
            cnpj[8] * 5 +
            cnpj[9] * 4 +
            cnpj[10] * 3 +
            cnpj[11] * 2;

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador2(ReadOnlySpan<int> cnpj)
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

    public static string GenerateUnformatted()
    {
        Span<char> dest = stackalloc char[14];
        Utils.GenerateImpl(dest, CriaDigitoVerificador1, CriaDigitoVerificador2);
        return new string(dest);
    }
}