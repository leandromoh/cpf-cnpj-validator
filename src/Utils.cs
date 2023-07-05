using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("benchmark")]

internal class Utils
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
    public static void GenerateImpl(Span<int> digits, int len)
    {
        for (var i = 0; i < len; i++)
            digits[i] = _random.Next(0, 9);
    }

    public static void Cast(Span<int> digits, Span<char> chars)
    {
        if (chars.Length != digits.Length)
            return;

        for (var i = 0; i < digits.Length; i++)
            chars[i] = (char)(digits[i] + '0');
    }
}