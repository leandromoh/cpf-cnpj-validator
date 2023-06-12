using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace DocumentoHelper.Test;

public class CNPJTest
{
    [Theory]
    [MemberData(nameof(GenerateCNPJSource))]
    public void Validate_generated_values(string doc) =>
        CNPJ.Validate(doc).Should().BeTrue();

    [Theory]
    
    // formatted

    [InlineData("06.352.066/0001-27")]
    [InlineData("11.744.636/0001-64")]
    [InlineData("25.713.061/0001-27")]
    [InlineData("31.547.776/0001-50")]
    [InlineData("49.131.124/0001-03")]
    [InlineData("54.458.515/0001-69")]
    [InlineData("68.872.146/0001-60")]
    [InlineData("78.644.026/0001-60")]
    [InlineData("84.717.824/0001-77")]
    [InlineData("93.141.967/0001-74")]

    // unformatted

    [InlineData("06352066000127")]
    [InlineData("11744636000164")]
    [InlineData("25713061000127")]
    [InlineData("31547776000150")]
    [InlineData("49131124000103")]
    [InlineData("54458515000169")]
    [InlineData("68872146000160")]
    [InlineData("78644026000160")]
    [InlineData("84717824000177")]
    [InlineData("93141967000174")]
    public void Validate(string doc) =>
        CNPJ.Validate(doc).Should().BeTrue();

    public static IEnumerable<object[]> GenerateCNPJSource() =>
        Enumerable
        .Range(0, 1000)
        .Select(_ => new[] { CNPJ.GenerateUnformatted() });
}
