using System.Runtime.CompilerServices;

public readonly struct CPFImp9
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp9(string value)
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
}
