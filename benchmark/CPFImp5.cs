using System.Runtime.CompilerServices;

public readonly struct CPFImp5
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp5(string value)
    {
        Value = value;

        Span<int> digits = stackalloc int[11];

        IsValid = Utils.TryWriteNumbers(digits, value) && ValidaDigito(digits);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static bool ValidaDigito(ReadOnlySpan<int> cpf)
    {
        return CriaDigitoVerificador(cpf) == (cpf[9], cpf[10]);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static (int, int) CriaDigitoVerificador(ReadOnlySpan<int> cpf)
    {
        ReadOnlySpan<int> zipper = stackalloc int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var total1 = 0;
        var total2 = 0;
        var length = 9; //zipper.Length - 1;
        var i = 0;

        for (; i < length; i++)
        {
            total1 += cpf[i] * zipper[i + 1];
            total2 += cpf[i] * zipper[i];
        }

        total2 += cpf[i] * zipper[i]; //last zipper digit

        total1 %= 11;
        total2 %= 11;

        return (total1 < 2 ? 0 : 11 - total1, 
                total2 < 2 ? 0 : 11 - total2);
    }
}
