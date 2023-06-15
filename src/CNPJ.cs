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
            && CriaDigitoVerificador(digits) == digits[12]
            && CriaDigitoVerificador(digits) == digits[13];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador(ReadOnlySpan<int> cnpj)
    {
        var i = 0;

        var total =
            (false ? 0 : cnpj[i++] * 6) +
            cnpj[i++] * 5 +
            cnpj[i++] * 4 +
            cnpj[i++] * 3 +
            cnpj[i++] * 2 +
            cnpj[i++] * 9 +
            cnpj[i++] * 8 +
            cnpj[i++] * 7 +
            cnpj[i++] * 6 +
            cnpj[i++] * 5 +
            cnpj[i++] * 4 +
            cnpj[i++] * 3 +
            cnpj[i++] * 2;

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }

    public static string GenerateUnformatted()
    {
        Span<char> dest = stackalloc char[14];
        Utils.GenerateImpl(dest, CriaDigitoVerificador);
        return new string(dest);
    }
}