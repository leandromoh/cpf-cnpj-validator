using System.Runtime.CompilerServices;

public readonly struct CPFImp4
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPFImp4(string value)
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
        var total1 = 0;
        var total2 = 0;
        var length = 9; //zipper.Length - 1;
        var i = 0;
        var zipper = 11;

        for (; i < length; i++)
        {
            total2 += cpf[i] * zipper--;
            total1 += cpf[i] * zipper;
        }

        total2 += cpf[i] * zipper; //last zipper digit

        total1 %= 11;
        total2 %= 11;

        return (total1 < 2 ? 0 : 11 - total1, 
                total2 < 2 ? 0 : 11 - total2);
    }
}
