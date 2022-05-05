using ContactsApp;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ContactsAppUI
{
    /// <summary>
    /// Вью-модель главного окна.
    /// </summary>
    public class MainWindowVM : ModelBase
    {
        /// <summary>
        /// Поле выбранного контакта.
        /// </summary>
        private Contact _selectedContact;

        /// <summary>
        /// Поле текущего отображаемого списка контактов.
        /// </summary>
        private ObservableCollection<Contact> _contacts;

        /// <summary>
        /// Поле ключевого слова для поиска контактов по фамилии/имени.
        /// </summary>
        private string _searchKey = "";

        /// <summary>
        /// Поле для хранения фамилий именинников.
        /// </summary>
        private string _birthdaySurnames;

        /// <summary>
        /// Поле видимости панели с именинниками
        /// </summary>
        private string _birthdayToday;

        #region Command declarations
        /// <summary>
        /// Команда редактирования контакта.
        /// </summary>
        private RelayCommand _editContactCommand;

        /// <summary>
        /// Команда добавления контакта.
        /// </summary>
        private RelayCommand _addContactCommand;

        /// <summary>
        /// Команда удаления контакта.
        /// </summary>
        private RelayCommand _deleteContactCommand;

        /// <summary>
        /// Команда закрытия главного окна приложенияю
        /// </summary>
        private RelayCommand _exitApplicationCommand;

        /// <summary>
        /// Команда открытия окна "About"
        /// </summary>
        private RelayCommand _showAboutCommand;
        #endregion

        /// <summary>
        /// Автосвойство проекта.
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// Свойство текущего отображаемого списка контактов.
        /// </summary>
        public ObservableCollection<Contact> Contacts 
        { 
            get => _contacts; 
            set
            {
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            } 
        }

        /// <summary>
        /// Свойство выбранного контакта.
        /// </summary>
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }

        /// <summary>
        /// Свойство ключевого слова для поиска контактов по фамилии/имени.
        /// </summary>
        public string SearchKey
        {
            get => _searchKey;
            set
            {
                _searchKey = value;
                UpdateCurrentList();
                OnPropertyChanged(nameof(SearchKey));
            }
        }

        /// <summary>
        /// Свойство для хранения фамилий именинников.
        /// </summary>
        public string BirthdaySurnames
        {
            get => _birthdaySurnames;
            private set
            {
                _birthdaySurnames = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Свойство видимости панели с именинниками
        /// </summary>
        public string BirthdayToday
        {
            get => _birthdayToday;
            set
            {
                _birthdayToday = value;
                OnPropertyChanged(nameof(BirthdayToday));
            }
        }

        /// <summary>
        /// Конструктор вью-модели главного окна.
        /// </summary>
        public MainWindowVM()
        {
            Project = ProjectManager.LoadFromFile(ProjectManager.PathFile());
            Contacts = Project.SortContactsBySurname(Project.Contacts);
            if (Contacts.Count > 0)
            {
                SelectedContact = Contacts.First();
            }
            UpdateCurrentList();
        }

        #region Command inplementations
        /// <summary>
        /// Свойство команды редактирования контакта.
        /// </summary>
        public RelayCommand EditContactCommand
        {
            get
            {
                return _editContactCommand ??
                    (_editContactCommand = new RelayCommand(obj =>
                    {
                        var contactWindow = new ContactWindow(SelectedContact.Clone() as Contact);
                        contactWindow.ShowDialog();
                        if (contactWindow.DialogResult == false) return;
                        SelectedContact.Surname = contactWindow.Contact.Surname;
                        SelectedContact.Name = contactWindow.Contact.Name;
                        SelectedContact.BirthDate = contactWindow.Contact.BirthDate;
                        SelectedContact.PhoneNumber = contactWindow.Contact.PhoneNumber;
                        SelectedContact.Email = contactWindow.Contact.Email;
                        SelectedContact.VkId = contactWindow.Contact.VkId;
                        ProjectManager.SaveToFile(Project, ProjectManager.PathFile(), ProjectManager.PathDirectory());
                        UpdateCurrentList();
                    }));
            }
        }

        /// <summary>
        /// Свойство команды вызова окна для добавления контакта.
        /// </summary>
        public RelayCommand AddContactCommand
        {
            get
            {
                return _addContactCommand ??
                    (_addContactCommand = new RelayCommand(obj =>
                    {
                        var contactWindow = new ContactWindow(new Contact());
                        contactWindow.ShowDialog();
                        if (contactWindow.DialogResult == false) return;
                        Project.Contacts.Add(contactWindow.Contact);
                        ProjectManager.SaveToFile(Project, ProjectManager.PathFile(), ProjectManager.PathDirectory());
                        SelectedContact = contactWindow.Contact;
                        UpdateCurrentList();
                    }));
            }
        }

        /// <summary>
        /// Свойство команды удаления контакта.
        /// </summary>
        public RelayCommand DeleteContactCommand
        {
            get
            {
                return _deleteContactCommand ??
                    (_deleteContactCommand = new RelayCommand(obj =>
                    {
                        if (MessageBox.Show(
                            $"Are you sure you want to delete {SelectedContact.Surname} ?",
                            "Delete confirmation", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                            return;
                        Project.Contacts.Remove(SelectedContact);
                        ProjectManager.SaveToFile(Project, ProjectManager.PathFile(), ProjectManager.PathDirectory());
                        UpdateCurrentList();
                        SelectedContact = Contacts.First();
                    }));
            }
        }

        /// <summary>
        /// Свойство команды закрытия главного окна.
        /// </summary>
        public RelayCommand ExitApplicationCommand
        {
            get
            {
                return _exitApplicationCommand ??
                    (_exitApplicationCommand = new RelayCommand(obj =>
                    {
                        ((Window)obj).Close();
                    }));
            }
        }

        /// <summary>
        /// Свойство команды открытия окна About.
        /// </summary>
        public RelayCommand ShowAboutCommand
        {
            get
            {
                return _showAboutCommand ??
                    (_showAboutCommand = new RelayCommand(obj =>
                    {
                        var aboutWindow = new AboutWindow();
                        aboutWindow.ShowDialog();
                    }));
            }
        }
        #endregion

        /// <summary>
        /// Метод получения и отображения списка именинников.
        /// </summary>
        private void GetBirthdaysContacts()
        {
            const string birthdayToday = "Сегодня день рождения:" + "\n";

            var birthdays = Project.FindBirthdayContacts(DateTime.Today);

            var birthdaySurnames = birthdays.Select(item => item.Surname).ToList();

            if (birthdaySurnames.Count > 0)
            {
                BirthdaySurnames = birthdayToday + 
                                   string.Join(", ", birthdaySurnames.ToArray());
                BirthdayToday = "Visible";
            }
            else
            {
                BirthdayToday = "Hidden";
            }
        }

        /// <summary>
        /// Метод обновления текущего списка контактов.
        /// </summary>
        private void UpdateCurrentList()
        {
            Contacts = Project.SortContactsBySurname(SearchKey, Project);
            GetBirthdaysContacts();
        }
    }
}
