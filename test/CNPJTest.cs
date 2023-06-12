using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace DocumentoHelper.Test;

public class CNPJTest
{
    [Theory]
    [MemberData(nameof(GenerateCNPJSource))]
    public void Validate(string doc) =>
        CNPJ.Validate(doc).Should().BeTrue();

    public static IEnumerable<object[]> GenerateCNPJSource() =>
        Enumerable
        .Range(0, 1000)
        .Select(_ => new[] { CNPJ.GenerateUnformatted() });
}
