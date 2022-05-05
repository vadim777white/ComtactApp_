using Newtonsoft.Json;
using System;
using System.IO;

namespace ContactsApp
{
    /// <summary>
    /// Класс менеджера проекта.
    /// </summary>
    public static class ProjectManager
    {
        /// <summary>
        /// Путь по умолчанию, по которому сохраняется файл.
        /// </summary>
        public static string PathFile()
        {
            return PathDirectory() + @"Contacts.json";
        }

        /// <summary>
        /// Путь по умолчанию, по которому создается папка для файла.
        /// </summary>
        public static string PathDirectory()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\ContactsApp\";
        }

        /// <summary>
        /// Метод сохранения данных в файл.
        /// </summary>
        /// <param name="data">Данные для сериализации</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="directoryPath">Путь до папки</param>
        public static void SaveToFile(Project data, string filePath, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(filePath))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// Метод загрузки данных путем десериализации.
        /// </summary>
        public static Project LoadFromFile(string filepath)
        {
            Project project;
            if (!File.Exists(filepath))
            {
                return new Project();
            }
            var serializer = new JsonSerializer();
            try
            {
                using (var sr = new StreamReader(filepath))
                using (var reader = new JsonTextReader(sr))
                    project = serializer.Deserialize<Project>(reader);
            }
            catch
            {
                return new Project();
            }
            
            return project;
        }
    }
}
