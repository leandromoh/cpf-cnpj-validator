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

    public static bool Validate(string value)
    {
        Span<int> digits = stackalloc int[11];

        return Utils.TryWriteNumbers(digits, value)
            && CriaDigitoVerificador(digits, true) == digits[9]
            && CriaDigitoVerificador(digits, false) == digits[10];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador(ReadOnlySpan<int> cpf, bool skipFirst)
    {
        var i = 0;

        var total =
            (skipFirst ? 0 : cpf[i++] * 11) +
            cpf[i++] * 10 +
            cpf[i++] * 9 +
            cpf[i++] * 8 +
            cpf[i++] * 7 +
            cpf[i++] * 6 +
            cpf[i++] * 5 +
            cpf[i++] * 4 +
            cpf[i++] * 3 +
            cpf[i++] * 2;

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }

    public static string GenerateUnformatted()
    {
        Span<char> dest = stackalloc char[11];
        Utils.GenerateImpl(dest, CriaDigitoVerificador);
        return new string(dest);
    }
}