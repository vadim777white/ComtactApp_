using System;
using System.Collections.ObjectModel;
using System.Linq;


namespace ContactsApp
{
    /// <summary>
    /// Класс логики проекта.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Динамическая коллекция контактов проекта.
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        /// <summary>
        /// Метод сортировки контактов по фамилии
        /// </summary>
        /// <param name="contactsToSort">Список контактов для сортировки</param>
        /// <returns>Отсортированный список контактов</returns>
        public ObservableCollection<Contact> SortContactsBySurname (ObservableCollection<Contact> contactsToSort)
        {
            return new ObservableCollection<Contact>(contactsToSort.OrderBy(i => i.Surname));
        }

        /// <summary>
        /// Метод поиска контакта по подстроке, включающий в себя сортировку результатов
        /// </summary>
        /// <param name="searchKey">Ключевое слово для поиска</param>
        /// <returns>Отсотрированный результат поиска контактов</returns>
        public ObservableCollection<Contact> SortContactsBySurname(string searchKey, Project project)
        {
            var searchResult = new ObservableCollection<Contact>(project.Contacts.Where(contact => 
                contact.Surname.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase) ||
                contact.Name.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            ));

            return SortContactsBySurname(searchResult);
        }

        public ObservableCollection<Contact> FindBirthdayContacts(DateTime dateToCompare)
        {
            var birthdayContacts = new ObservableCollection<Contact>(Contacts.Where(contact =>
                contact.BirthDate.Month == dateToCompare.Month &&
                contact.BirthDate.Day == dateToCompare.Day
            ));

            return SortContactsBySurname(birthdayContacts);
        }
    }
}
