using System.Runtime.CompilerServices;

public readonly struct CPFImp2
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp2(string value)
    {
        Value = value;

        Span<int> digits = stackalloc int[11];

        IsValid = Utils.TryWriteNumbers(digits, value) && ValidaDigito(digits);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static bool ValidaDigito(ReadOnlySpan<int> cpf)
    {
        ReadOnlySpan<int> zipper = stackalloc int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        return CriaDigitoVerificador(cpf, zipper.Slice(1)) == cpf[9]
            && CriaDigitoVerificador(cpf, zipper) == cpf[10];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static int CriaDigitoVerificador(ReadOnlySpan<int> cpf, ReadOnlySpan<int> zipper)
    {
        var total = 0;
        var length = zipper.Length;
        for (var i = 0; i < length; i++)
            total += cpf[i] * zipper[i];

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }
}