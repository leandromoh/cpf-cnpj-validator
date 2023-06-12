using System.Runtime.CompilerServices;

public readonly struct CPFImp10
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp10(string value)
    {
        Value = value;
        IsValid = Validate(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
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
        var i = skipFirst ? -1 : 0;

        var total =
            (skipFirst ? 0 : cpf[i] * 11) +
            cpf[1 + i] * 10 +
            cpf[2 + i] * 9 +
            cpf[3 + i] * 8 +
            cpf[4 + i] * 7 +
            cpf[5 + i] * 6 +
            cpf[6 + i] * 5 +
            cpf[7 + i] * 4 +
            cpf[8 + i] * 3 +
            cpf[9 + i] * 2;

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }
}
