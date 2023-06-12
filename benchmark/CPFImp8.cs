using System.Runtime.CompilerServices;

public readonly struct CPFImp8
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp8(string value)
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
    internal static int CriaDigitoVerificador(ReadOnlySpan<int> cpf, int i)
    {
        var total =
            (i == 0 ? cpf[i++] * 11 : ++i) +
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
