using System.Runtime.CompilerServices;

delegate int SpanInt(ReadOnlySpan<int> span);

public class Utils
{
    private static readonly Random _random = new();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool TryWriteNumbers(Span<int> digits, string value)
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
    internal static void GenerateImpl(Span<char> dest, SpanInt criaDigito1, SpanInt criaDigito2)
    {
        Span<int> digits = stackalloc int[dest.Length];
        var len = dest.Length - 2;
        for (var i = 0; i < len; i++)
            digits[i] = _random.Next(0, 9);

        digits[len] = criaDigito1(digits);
        digits[len + 1] = criaDigito2(digits);

        for (var i = 0; i < digits.Length; i++)
            dest[i] = (char)(digits[i] + '0');
    }
}