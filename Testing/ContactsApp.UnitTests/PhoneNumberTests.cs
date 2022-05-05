using System;
using NUnit.Framework;

namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Класс тестирования класса PhoneNumber.
    /// </summary>
    [TestFixture]
    public class PhoneNumberTests
    {
        [Test]
        public void PhoneNumber_GoodPhoneNumber_ReturnsSamePhoneNumber()
        {
            //Setup
            var phoneNumber = new PhoneNumber();
            var sourcePhoneNumber = 79996198754;
            var expectedPhoneNumber = sourcePhoneNumber;

            //Act
            phoneNumber.Number = sourcePhoneNumber;
            var actualPhoneNumber = phoneNumber.Number;

            //Assert
            Assert.AreEqual(expectedPhoneNumber, actualPhoneNumber);
        }

        [TestCase(87776665544)]
        [TestCase(877766655477777777)]
        public void PhoneNumber_BadPhoneNumber_ThrowsException(long wrongPhoneNumber)
        {
            //Setup
            var phoneNumber = new PhoneNumber();
            //Assert
            Assert.Throws<ArgumentException>
            (
                //Act
                () => phoneNumber.Number = wrongPhoneNumber
            );
        }
    }
}