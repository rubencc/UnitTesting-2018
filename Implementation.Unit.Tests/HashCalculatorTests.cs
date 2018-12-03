using FluentAssertions;
using Implementations;
using Interfaces;
using NSubstitute;
using Xunit;

namespace Implementation.Unit.Tests
{
    [Trait("Unit Test", "HashCalculator")]
    public class HashCalculatorTests
    {

        [Fact(DisplayName = "Calculate hash MD5")]
        public void Calculate_Hash_MD5()
        {
            HashProvider hashProvider = new HashProvider();
            HashCalculator hashCalculator = new HashCalculator(hashProvider); 

            string expected = "6825270045572556";
            string input = "string to hash";

            string result = hashCalculator.Hash(input);

            result.Should().Be(expected);
        }

        [Fact(DisplayName = "Check byte to string")]
        public void Check_byte_to_string()
        {
            IHashProvider hashProvider = Substitute.For<IHashProvider>();
            HashCalculator hashCalculator = new HashCalculator(hashProvider); 
            

            string expected = "6825270045572556";
            string input = "string to hash";

            byte[] computed = new byte[]{106,248,194,223,82,112,219,192,196,85,122,37,91,166,255,253};

            hashProvider.ComputeHash(Arg.Any<byte[]>()).Returns(x => computed);

            string result = hashCalculator.Hash(input);

            result.Should().Be(expected);

            hashProvider.Received(1).ComputeHash(Arg.Any<byte[]>());
        }
    }
}
