using System.Runtime.CompilerServices;

namespace DocumentoHelper;

public readonly partial struct CPF
{
    public readonly string Value;
    public readonly bool IsValid;

    public CPF(string value)
    {
        Value = value;

        Span<int> digits = stackalloc int[11];

        IsValid = TryWriteNumbers(digits, value) && ValidaDigito(digits);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static bool TryWriteNumbers(Span<int> digits, string value)
    {
        var written = 0;
        foreach (var x in value)
        {
            if (char.IsDigit(x))
            {
                if (written == digits.Length)
                    return false;

                digits[written++] = x - '0';
            }
        }

        return written == digits.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static bool ValidaDigito(ReadOnlySpan<int> cpf)
    {
        ReadOnlySpan<int> zipper = stackalloc int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        return CriaDigitoVerificador(cpf, zipper.Slice(1)) == cpf[9]
            && CriaDigitoVerificador(cpf, zipper) == cpf[10];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador(ReadOnlySpan<int> cpf, ReadOnlySpan<int> zipper)
    {
        var total =
            cpf[0] * zipper[0] +
            cpf[1] * zipper[1] +
            cpf[2] * zipper[2] +
            cpf[3] * zipper[3] +
            cpf[4] * zipper[4] +
            cpf[5] * zipper[5] +
            cpf[6] * zipper[6] +
            cpf[7] * zipper[7] +
            cpf[8] * zipper[8] +
            (zipper.Length != 10 ? 0 : cpf[9] * zipper[9]);

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }
}