namespace DocumentoHelper.Test
{
    using NUnit.Framework;
    using DocumentoHelper;
    using System.Linq;
    using System.Collections.Generic;

    [TestFixture]
    public class CNPJTest
    {

        [TestCaseSource(nameof(GenerateCNPJSource))]
        public void GenerateCNPJ(long cnpj) => Assert.IsTrue(CNPJ.IsValid(cnpj));
		
        public static IEnumerable<object> GenerateCNPJSource() =>
            Enumerable.Range(0, 100).Select(_ => CNPJ.Generate()).Cast<object>();

        [Test]
        public void CalculateCheckDigits() => 
            Assert.AreEqual(new[] { 2,1 }, CNPJ.CalculateCheckDigits(new[] { 4,4,2,6,2,6,3,7,0,0,0,1 }));

        [Test]
        public void IsValidString() => Assert.IsTrue(CNPJ.IsValid("44.262.637/0001-21"));

        [Test]
        public void IsValidLong() => Assert.IsTrue(CNPJ.IsValid(44262637000121));

        [Test]
        public void IsValidIntArray() =>  Assert.IsTrue(CNPJ.IsValid(new[] { 4,4,2,6,2,6,3,7,0,0,0,1,2,1 }));

        [Test]
        public void FormatString() => Assert.AreEqual(CNPJ.Format("44262637000121"), "44.262.637/0001-21");

        [Test]
        public void FormatLong() => Assert.AreEqual(CNPJ.Format(44262637000121), "44.262.637/0001-21");
    }
}
