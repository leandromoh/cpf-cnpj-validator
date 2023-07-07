using System.Runtime.CompilerServices;

namespace DocumentoHelper;

// Permanent Account Number
public readonly struct PAN
{
    public readonly string Value;
    public readonly bool IsValid;

    public PAN(string value)
    {
        Value = value;
        IsValid = Validate(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool Validate(string value)
    {
        Span<char> text = stackalloc char[10];

        return Utils.TryWriteAlphanumeric(text, value)
            && ValidateText(text);
    }

    private static bool ValidateText(ReadOnlySpan<char> text)
    {
        return
            // The first three letters are sequence of alphabets, from AAA to ZZZ.
            char.IsAsciiLetter(text[0]) &&
            char.IsAsciiLetter(text[1]) &&
            char.IsAsciiLetter(text[2]) &&

            // The fourth character specifies about the holder of the card. Each holder is uniquely defined as below:
            (text[3] is
            // A - Association of Persons (AOP)
            'a' or 'A' or
            // B - Body of Individuals (BOI)
            'b' or 'B' or
            // C - Company
            'c' or 'C' or
            // F - Firm
            'f' or 'F' or
            // G - Government
            'g' or 'G' or
            // H - HUF (Hindu Undivided Family)
            'h' or 'H' or
            // L - Local Authority
            'l' or 'L' or
            // J - Artificial Judicial Person
            'j' or 'J' or
            // P - Individual
            'p' or 'P' or
            // T - AOP(Trust)
            't' or 'T') &&

            // The fifth character represents the first character of PAN holder's name
            char.IsAsciiLetter(text[4]) &&

            // The next four-characters should be any digit
            char.IsAsciiDigit(text[5]) &&
            char.IsAsciiDigit(text[6]) &&
            char.IsAsciiDigit(text[7]) &&
            char.IsAsciiDigit(text[8]) &&

            // The last (tenth) character is an alphabetic check digit
            char.IsAsciiLetter(text[9]);
    }
}