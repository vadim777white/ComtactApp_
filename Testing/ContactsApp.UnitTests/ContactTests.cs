using NUnit.Framework;
using System;

namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Класс тестирования класса Contact.
    /// </summary>
    [TestFixture]
    public class ContactTests
    {
        [Test]
        public void Name_GoodValue_ReturnsSameName()
        {
            //Setup
            var contact = new Contact();
            const string sourceName = "Ivan";
            var expectedName = sourceName;

            //Act
            contact.Name = sourceName;
            var actualName = contact.Name;

            //Assert
            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void Name_ChangeNameRegister_ReturnsRegisterChangedName()
        {
            //Setup
            var contact = new Contact();
            const string sourceName = "ivan";
            const string expectedName = "Ivan";

            //Act
            contact.Name = sourceName;
            var actualName = contact.Name;

            //Assert
            Assert.AreEqual(expectedName, actualName);
        }

        [TestCase("")]
        [TestCase("012345678901234567890123456789012345678901234567890123456789")]
        public void Name_BadValue_ThrowsException(string wrongName)
        {
            //SetUp
            var contact = new Contact();
            
            //Assert
            Assert.Throws<ArgumentException>
            (
                //Act
                () => contact.Name = wrongName
            );
        }

        [Test]
        public void Surname_GoodValue_ReturnsSameSurname()
        {
            //Setup
            var contact = new Contact();
            const string sourceSurname = "Sergeev";
            var expectedSurname = sourceSurname;

            //Act
            contact.Surname = sourceSurname;
            var actualSurname = contact.Surname;

            //Assert
            Assert.AreEqual(expectedSurname, actualSurname);
        }

        [Test]
        public void Surname_ChangeNameRegister_ReturnsRegisterChangedName()
        {
            //Setup
            var contact = new Contact();
            const string sourceSurname = "sergeev";
            const string expectedSurname = "Sergeev";

            //Act
            contact.Surname = sourceSurname;
            var actualSurname = contact.Surname;

            //Assert
            Assert.AreEqual(expectedSurname, actualSurname);
        }

        [TestCase("")]
        [TestCase("012345678901234567890123456789012345678901234567890123456789")]
        public void Surname_BadValue_ThrowsException(string wrongSurname)
        {
            //SetUp
            var contact = new Contact();
            
            //Assert
            Assert.Throws<ArgumentException>
            (
                //Act
                () => contact.Surname = wrongSurname
            );
        }

        [Test]
        public void PhoneNumber_GoodValue_ReturnsSamePhoneNumber()
        {
            //Setup
            var contact = new Contact();
            var sourcePhoneNumber = 78005553535;
            var phoneNumber = new PhoneNumber();
            var expectedPhoneNumber = sourcePhoneNumber;
            phoneNumber.Number = sourcePhoneNumber;

            //Act
            contact.PhoneNumber = phoneNumber;
            var actualPhoneNumber = contact.PhoneNumber.Number;

            //Assert
            Assert.AreEqual(expectedPhoneNumber, actualPhoneNumber);
        }

        [Test]
        public void Email_GoodValue_ReturnsSameEmail()
        {
            //Setup
            var contact = new Contact();
            const string sourceEmail = "email@mail.ru";
            const string expectedEmail = sourceEmail;

            //Act
            contact.Email = sourceEmail;
            var actualEmail = contact.Email;

            //Assert
            Assert.AreEqual(expectedEmail, actualEmail);
        }

        [TestCase("")]
        [TestCase("012345678901234567890123456789012345678901234567890123456789")]
        public void Email_BadValue_ThrowsException(string wrongEmail)
        {
            //SetUp
            var contact = new Contact();
            
            //Assert
            Assert.Throws<ArgumentException>
            (
                //Act
                () => contact.Email = wrongEmail
            );
        }

        [Test]
        public void VkId_GoodValue_ReturnsSameVkId()
        {
            //Setup
            var contact = new Contact();
            const string sourceVkId = "id/12341223432";
            const string expectedVkId = sourceVkId;

            //Act
            contact.VkId = sourceVkId;
            var actualVkId = contact.VkId;

            //Assert
            Assert.AreEqual(expectedVkId, actualVkId);
        }

        [TestCase("")]
        [TestCase("012345678901234567890123456789012345678901234567890123456789")]
        public void VkId_BadValue_ThrowsException(string wrongVkId)
        {
            //SetUp
            var contact = new Contact();
            
            //Assert
            Assert.Throws<ArgumentException>
            (
                //Act
                () => contact.VkId = wrongVkId
            );
        }

        [Test]
        public void BirthDate_GoodValue_ReturnsSameBirthDate()
        {
            //Setup
            var contact = new Contact();
            var sourceBirthDate = DateTime.Today;
            var expectedBirthDate = sourceBirthDate;

            //Act
            contact.BirthDate = sourceBirthDate;
            var actualBirthDate = contact.BirthDate;

            //Assert
            Assert.AreEqual(expectedBirthDate, actualBirthDate);
        }

        [TestCase("01.01.2022")]
        [TestCase("01.01.1800")]


        [Test]
        public void Clone_GoodValue_ReturnsSameObject()
        {
            //Setup
            var contact = new Contact()
            {
                Surname = "Сидоров",
                Name = "Анатолий",
                BirthDate = new DateTime(1990, 02, 13),
                PhoneNumber = new PhoneNumber() { Number = 72344231132 },
                Email = "smth@mail.ru",
                VkId = "id/12312233"
            };

            var expectedContact = contact;

            //Act
            var actualContact = expectedContact.Clone() as Contact;

            //Assert
            Assert.AreEqual(expectedContact, actualContact);
        }
    }
}
