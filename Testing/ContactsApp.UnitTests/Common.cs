using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Вспомогательный класс для тестирования класса ProjectManager.
    /// </summary>
    class Common
    {
        public static string DataFolderForTest()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(location) + @"\TestData";
        }

        public static string PathFile()
        {
            return PathDirectory() + @"Contacts.json";
        }

        public static string PathDirectory()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\ContactsApp\";
        }

        public static Project PrepareProject()
        {
            var project = new Project();
            var phoneNumber = new PhoneNumber()
            {
                Number = 78005553535
            };
            project.Contacts.Add(new Contact()
            {
                Name = "BName",
                Surname = "BSurname",
                BirthDate = new DateTime(1923, 02, 15),
                VkId = "B434234",
                Email = "Bhogger@bk.com",
                PhoneNumber = phoneNumber
            });
            project.Contacts.Add(new Contact()
            {
                Name = "CName",
                Surname = "CSurname",
                BirthDate = new DateTime(1923, 11, 15),
                VkId = "C434234",
                Email = "Chogger@bk.com",
                PhoneNumber = phoneNumber
            });
            project.Contacts.Add(new Contact()
            {
                Name = "AName",
                Surname = "ASurname",
                BirthDate = new DateTime(1923, 02, 15),
                VkId = "A434234",
                Email = "Ahogger@bk.com",
                PhoneNumber = phoneNumber
            });

            return project;
        }
    }
}
