using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace DocumentoHelper.Test;

public class CPFTest
{
    [Theory]
    [MemberData(nameof(GenerateCPFSource))]
    public void Validate(string doc) =>
        CPF.Validate(doc).Should().BeTrue();

    public static IEnumerable<object[]> GenerateCPFSource() =>
        Enumerable
        .Range(0, 1000)
        .Select(_ => new[] { CPF.GenerateUnformatted() });
}
