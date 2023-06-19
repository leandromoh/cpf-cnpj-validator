using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace DocumentoHelper.Test;

public class CPFTest
{
    [Theory]

    // formatted

    [InlineData("084.664.028-70")]
    [InlineData("120.862.205-61")]
    [InlineData("272.681.426-33")]
    [InlineData("386.785.420-39")]
    [InlineData("402.822.021-10")]
    [InlineData("521.267.394-14")]
    [InlineData("655.265.520-71")]
    [InlineData("780.984.401-68")]
    [InlineData("868.961.825-20")]
    [InlineData("977.486.519-73")]

    // unformatted

    [InlineData("08466402870")]
    [InlineData("12086220561")]
    [InlineData("27268142633")]
    [InlineData("38678542039")]
    [InlineData("40282202110")]
    [InlineData("52126739414")]
    [InlineData("65526552071")]
    [InlineData("78098440168")]
    [InlineData("86896182520")]
    [InlineData("97748651973")]
    public void Validate(string doc) =>
        CPF.Validate(doc).Should().BeTrue();

    [Theory]
    [InlineData("084@664_028#70")]
    [InlineData("120+862!205(61")]
    public void Validate_ignores_non_digits(string doc) =>
        CPF.Validate(doc).Should().BeTrue();

    [Theory]
    [InlineData("--------084@664_028#70.............")]
    [InlineData("()()()@@120+862!!!!205(61)(*&¨#@ %¨)")]
    public void Validate_has_no_limit_for_string_length(string doc) =>
        CPF.Validate(doc).Should().BeTrue();

    [Theory]
    [MemberData(nameof(GenerateSource))]
    public void Validate_generate_values(string doc)
    {
        doc.Should().MatchRegex(@"^\d{11}$");
        CPF.Validate(doc).Should().BeTrue();
    }

    public static IEnumerable<object[]> GenerateSource() =>
        Enumerable
        .Range(0, 1000)
        .Select(_ => new[] { CPF.Generate() });

    [Theory]
    [MemberData(nameof(GenerateFormattedSource))]
    public void Validate_generate_formatted_values(string doc)
    {
        doc.Should().MatchRegex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        CPF.Validate(doc).Should().BeTrue();
    }

    public static IEnumerable<object[]> GenerateFormattedSource() =>
        Enumerable
        .Range(0, 1000)
        .Select(_ => new[] { CPF.GenerateFormatted() });
}
