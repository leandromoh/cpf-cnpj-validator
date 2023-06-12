using System.Runtime.CompilerServices;

public readonly struct CPFImp7
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp7(string value)
    {
        Value = value;
        IsValid = Validate(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool Validate(string value)
    {
        Span<int> digits = stackalloc int[11];

        return Utils.TryWriteNumbers(digits, value) 
            && CriaDigitoVerificador(digits, -1) == digits[9]
            && CriaDigitoVerificador(digits, 0) == digits[10];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador(ReadOnlySpan<int> cpf, int offset)
    {
        var total =
            (offset == 0 ? cpf[0 + offset] * 11 : 0) +
            cpf[1 + offset] * 10 +
            cpf[2 + offset] * 9 +
            cpf[3 + offset] * 8 +
            cpf[4 + offset] * 7 +
            cpf[5 + offset] * 6 +
            cpf[6 + offset] * 5 +
            cpf[7 + offset] * 4 +
            cpf[8 + offset] * 3 +
            cpf[9 + offset] * 2;

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }
}
