using System.Runtime.CompilerServices;

namespace DocumentoHelper;

public readonly partial struct CNPJ
{
    public readonly string Value;
    public readonly bool IsValid;

    public CNPJ(string value)
    {
        Value = value;

        Span<int> digits = stackalloc int[14];

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
    private static bool ValidaDigito(ReadOnlySpan<int> cnpj)
    {
        ReadOnlySpan<int> zipper = stackalloc int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        return CriaDigitoVerificador(cnpj, zipper.Slice(1)) == cnpj[12]
            && CriaDigitoVerificador(cnpj, zipper) == cnpj[13];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal static int CriaDigitoVerificador(ReadOnlySpan<int> cnpj, ReadOnlySpan<int> zipper)
    {
        var total =
            cnpj[0] * zipper[0] +
            cnpj[1] * zipper[1] +
            cnpj[2] * zipper[2] +
            cnpj[3] * zipper[3] +
            cnpj[4] * zipper[4] +
            cnpj[5] * zipper[5] +
            cnpj[6] * zipper[6] +
            cnpj[7] * zipper[7] +
            cnpj[8] * zipper[8] +
            cnpj[9] * zipper[9] +
            cnpj[10] * zipper[10] +
            cnpj[11] * zipper[11] +
            (zipper.Length != 13 ? 0 : cnpj[12] * zipper[12]);

        total %= 11;
        return total < 2 ? 0 : 11 - total;
    }
}