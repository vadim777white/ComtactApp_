using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Класс тестирования класса ProjectManager.
    /// </summary>
    [TestFixture]
    public class ProjectManagerTests
    {
        
        [Test]
        public void SaveToFile_CreateFolder_FolderExist()
        {
            //Setup
            var project = Common.PrepareProject();
            var testDataFolder = Common.DataFolderForTest() + "CreateTest";
            var testFileName = testDataFolder + @"CreateFolderTest";
            
            if (Directory.Exists(testDataFolder))
            {
                Directory.Delete(testDataFolder);
            }

            //Act
            ProjectManager.SaveToFile(project, testFileName, testDataFolder);

            //Assert
            Assert.IsTrue(Directory.Exists(testDataFolder));
        }

        
        [Test]
        public void LoadFromFile_InCorrectFile_ReturnEmptyProject()
        {
            //Setup
            var testDataFolder = Common.DataFolderForTest();
            var testFileName = testDataFolder + @"\defectiveProject.json";

            //Act
            var actualProject = ProjectManager.LoadFromFile(testFileName);

            //Assert
            Assert.IsEmpty(actualProject.Contacts);
        }

        [Test]
        public void LoadFromFile_InCorrectPath_ReturnEmptyProject()
        {
            //Setup
            var testFileName = Common.DataFolderForTest() + "wrong way";

            //Act
            var actualProject = ProjectManager.LoadFromFile(testFileName);

            //Assert
            Assert.IsEmpty(actualProject.Contacts);
        }

        [Test]
        public void LoadFromFile_InCorrectProject_FileSavedCorrectly()
        {
            //SetUp
            var sourceProject = Common.PrepareProject();
            var testDataFolder = Common.DataFolderForTest();
            var testFileName = testDataFolder + @"\defectiveProject.json";
            
            if (File.Exists(testFileName))
            {
                File.Delete(testFileName);
            }
            
            sourceProject.Contacts.Add(new Contact());
            
            if (!Directory.Exists(testDataFolder))
            {
                Directory.CreateDirectory(testDataFolder);
            }
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(testFileName))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, sourceProject);
            }

            //Act
            var actualProject = ProjectManager.LoadFromFile(testFileName);

            //Assert
            Assert.IsEmpty(actualProject.Contacts);
        }

        [Test]
        public void PathFile_GoodFilePath_ReturnSamePath()
        {
            //Setup
            var expectedPath = Common.PathFile();

            //Act
            var actualPath = ProjectManager.PathFile();

            //Assert
            Assert.AreEqual(expectedPath, actualPath);
        }

        [Test]
        public void PathDirectory_GoodDirectoryPath_ReturnSameDirectory()
        {
            //Setup
            var expectedPath = Common.PathDirectory();

            //Act
            var actualPath = ProjectManager.PathDirectory();

            //Assert
            Assert.AreEqual(expectedPath, actualPath);
        }
    }
}
