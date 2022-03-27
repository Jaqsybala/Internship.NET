using System;
using Week1.FractionTask;
using Xunit;

namespace UnitTests.Week1.FractionTask.Tests
{
    public class FractionTests
    {
        [Fact]
        public void Sum_1_2and2_3_Returns_1_17()
        {
            //arrange
            var firstExp = new Fraction(1, 2);
            var secondExp = new Fraction(2, 3);

            //act
            var res = Math.Round(Fraction.FromFractionToDouble(firstExp + secondExp), 2);

            //assert
            Assert.Equal(1.17, res);
        }

        [Fact]
        public void Difference_1_2and2_3_Returns_Minus0_17()
        {
            var firstExp = new Fraction(1, 2);
            var secondExp = new Fraction(2, 3);

            var res = Math.Round(Fraction.FromFractionToDouble(firstExp - secondExp), 2);

            Assert.Equal(-0.17, res);
        }

        [Fact]
        public void Multiplication_1_2and2_3_Returns_0_33()
        {
            var firstExp = new Fraction(1, 2);
            var secondExp = new Fraction(2, 3);

            var res = Math.Round(Fraction.FromFractionToDouble(firstExp * secondExp), 2);

            Assert.Equal(0.33, res);
        }

        [Fact]
        public void Division_1_2and2_3_Returns_0_75()
        {
            var firstExp = new Fraction(1, 2);
            var secondExp = new Fraction(2, 3);

            var res = Math.Round(Fraction.FromFractionToDouble(firstExp / secondExp), 2);

            Assert.Equal(0.75, res);
        }

        [Fact]
        public void Two_Objects_Are_Equal()
        {
            var firstObj = new Fraction(1, 2);
            var secondOnj = new Fraction(1, 2);

            var res = firstObj.Equals(secondOnj);

            Assert.True(res, $"{firstObj} and {secondOnj} are equal.");
        }

        [Fact]
        public void Two_Objects_Have_Same_Hash()
        {
            var firstObj = new Fraction(1, 2);
            var secondOnj = new Fraction(1, 2);

            var firstHash = firstObj.GetHashCode();
            var secondHash = secondOnj.GetHashCode();

            Assert.Equal(firstHash, secondHash);
        }
    }
}
