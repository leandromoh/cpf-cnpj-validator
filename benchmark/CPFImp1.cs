using System.Runtime.CompilerServices;

public readonly struct CPFImp1
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp1(string value)
    {
        Value = value;

        Span<int> digits = stackalloc int[11];

        if (Utils.TryWriteNumbers(digits, value) is false)
        {
            IsValid = false;
            return;
        }

        ReadOnlySpan<int> zipper = stackalloc int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        IsValid =
            ValidaDigito(digits, zipper.Slice(1)) &&
            ValidaDigito(digits, zipper);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static bool ValidaDigito(ReadOnlySpan<int> cpf, ReadOnlySpan<int> zipper)
    {
        return CriaDigitoVerificador(cpf, zipper) == cpf[zipper.Length];
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