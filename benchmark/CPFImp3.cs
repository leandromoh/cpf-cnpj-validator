using System.Runtime.CompilerServices;

public readonly struct CPFImp3
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp3(string value)
    {
        Value = value;
        IsValid = Validate(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool Validate(string value)
    {
        Span<int> digits = stackalloc int[11];

        return Utils.TryWriteNumbers(digits, value) 
            && CriaDigitoVerificador1(digits) == digits[9]
            && CriaDigitoVerificador2(digits) == digits[10];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador1(ReadOnlySpan<int> cpf)
    {
        var total =
            cpf[0] * 10 +
            cpf[1] * 9 +
            cpf[2] * 8 +
            cpf[3] * 7 +
            cpf[4] * 6 +
            cpf[5] * 5 +
            cpf[6] * 4 +
            cpf[7] * 3 +
            cpf[8] * 2;

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador2(ReadOnlySpan<int> cpf)
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
}
