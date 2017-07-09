namespace DocumentoHelper.Test
{
    using NUnit.Framework;
    using DocumentoHelper;
    using System.Linq;

    [TestFixture]
    public class CPFTest
    {

       [Test]
       public void GenerateCPF() => Assert.IsTrue(CPF.IsValid(CPF.Generate()));

        [Test]
        public void CalculateCheckDigits() => 
            Assert.AreEqual(new[] { 1,9 }, CPF.CalculateCheckDigits(new[] { 6,3,2,1,9,9,1,0,3 }));

        [Test]
        public void IsValidString() => Assert.IsTrue(CPF.IsValid("632.199.103-19"));

        [Test]
        public void IsValidLong() => Assert.IsTrue(CPF.IsValid(63219910319));

        [Test]
        public void IsValidIntArray() =>  Assert.IsTrue(CPF.IsValid(new[] { 6,3,2,1,9,9,1,0,3,1,9 }));

        [Test]
        public void FormatString() => Assert.AreEqual(CPF.Format("63219910319"), "632.199.103-19");

        [Test]
        public void FormatLong() => Assert.AreEqual(CPF.Format(63219910319), "632.199.103-19");
    }
}
