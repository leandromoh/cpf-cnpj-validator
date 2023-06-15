using System.Runtime.CompilerServices;

public readonly struct CPFImp11
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp11(string value)
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
}
