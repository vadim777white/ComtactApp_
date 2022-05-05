using ContactsApp;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace ContactsAppUI
{
    /// <summary>
    /// Класс для манипуляций над валидируемым контактом.
    /// </summary>
    public class ContactVM : ModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Фамилия контакта.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Имя контакта.
        /// </summary>
        private string _name;

        /// <summary>
        /// Дата рождения контакта.
        /// </summary>
        private DateTime _birthDate;

        /// <summary>
        /// Адрес электронной почты контакта.
        /// </summary>
        private string _email;

        /// <summary>
        /// Идентификатор Вконтакте контакта.
        /// </summary>
        private string _vkId;

        /// <summary>
        /// Номер контакта
        /// </summary>
        private PhoneNumberVM _phoneNumber;

        /// <summary>
        /// Состояние поля отображения фамилии контакта.
        /// </summary>
        private bool _surnameChecked;

        /// <summary>
        /// Состояние поля отображения имени контакта.
        /// </summary>
        private bool _nameChecked;

        /// <summary>
        /// Состояние поля отображения дня рождения контакта.
        /// </summary>
        private bool _birthDateChecked;

        /// <summary>
        /// Состояние поля отображения номера телефона контакта.
        /// </summary>
        private bool _phoneNumberChecked;

        /// <summary>
        /// Состояние поля отображения электронной почты контакта.
        /// </summary>
        private bool _emailChecked;

        /// <summary>
        /// Состояние поля отображения Вконтакте контакта.
        /// </summary>
        private bool _vkIdChecked;

        /// <summary>
        /// Паттерн имени и фамилии.
        /// </summary>
        private const string RegexName = "^[а-яА-Яa-zA-Z]+(-[а-яА-Яa-zA-Z]+)?$";

        /// <summary>
        /// Паттерн емейла.
        /// </summary>
        private const string RegexEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        /// <summary>
        /// Паттерн айди вк.
        /// </summary>
        private const string RegexVkId = "^[a-zA-Z0-9]+(_[a-zA-Z0-9]+)?$";

        /// <summary>
        /// Свойство ошибки.
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// Номер телефона контакта.
        /// </summary>
        public PhoneNumberVM PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        /// <summary>
        /// Свойство Фамилии контакта.
        /// </summary>
        public string Surname
        {
            get => _surname;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _surname = char.ToUpper(value.First()) + value.Substring(1);
                }
                else
                {
                    _surname = value;
                }
                OnPropertyChanged(nameof(Surname));
            }
        }

        /// <summary>
        /// Свойство имени контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _name = char.ToUpper(value[0]) + value.Substring(1);
                }
                else
                {
                    _name = value;
                }
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Свойство электронной почты контакта.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        /// <summary>
        /// Свойство идентификатора Вконтакте контакта.
        /// </summary>
        public string VkId
        {
            get => _vkId;
            set
            {
                _vkId = value;
                OnPropertyChanged(nameof(VkId));
            }
        }

        /// <summary>
        /// Свойство даты рождения контакта.
        /// </summary>
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        /// <summary>
        /// Конструктор класса по умолчанию.
        /// </summary>
        public ContactVM() { }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="contact">Валидируемый контакт</param>
        public ContactVM(Contact contact)
        {
            Surname = contact.Surname;
            Name = contact.Name;
            BirthDate = contact.BirthDate;
            PhoneNumber = new PhoneNumberVM() { Number = contact.PhoneNumber.Number };
            Email = contact.Email;
            VkId = contact.VkId;
        }

        /// <summary>
        /// Реализация IDataErrorInfo
        /// </summary>
        /// <param name="propertyName">Изменяемое свойство</param>
        /// <returns>Ошибка (при наличии)</returns>
        public string this[string propertyName]
        {
            get
            {
                string error = null;

                switch (propertyName)
                {
                    case "Surname":
                        if (string.IsNullOrWhiteSpace(Surname))
                        {
                            if (_surnameChecked) error = "Surname is unset";
                        }
                        else if (!Regex.IsMatch(Surname, RegexName))
                        {
                            error = "The surname should contain only Russian and Latin letters";
                        }
                        else if (Surname.Length > 50)
                        {
                            error = "Length of surname can't exceed 50 characters";
                        }
                        break;
                    case "Name":
                        if (string.IsNullOrWhiteSpace(Name))
                        {
                            if (_nameChecked) error = "Name is unset";
                        }
                        else if (!Regex.IsMatch(Name, RegexName))
                        {
                            error = "The name should contain only Russian and Latin letters";
                        }
                        else if (Name.Length > 50)
                        {
                            error = "Length of name can't exceed 50 characters";
                        }
                        break;
                    case "BirthDate":
                        if (BirthDate >= DateTime.Now)
                        {
                            if (_birthDateChecked) error = "Birth date can't exceed current date";
                        }
                        else if (BirthDate.Year < 1900)
                        {
                            error = "Birth date can't be less then 1900 year";
                        }
                        break;
                    case "PhoneNumber":
                        if (PhoneNumber == null && _phoneNumberChecked)
                        {
                            error = "Phone number is unset";
                        }
                        break;
                    case "Email":
                        if (string.IsNullOrWhiteSpace(Email))
                        {
                            if (_emailChecked) error = "Email is unset";
                        }
                        else if (!Regex.IsMatch(Email, RegexEmail))
                        {
                            error = "The e-mail should contain Latin letters, \"@\" and \".\"";
                        }
                        else if (Email.Length > 50)
                        {
                            error = "Length of E-mail can't exceed 50 characters";
                        }
                        break;
                    case "VkId":
                        if (string.IsNullOrWhiteSpace(VkId))
                        {
                            if (_vkIdChecked) error = "Vk Id is unset";
                        }
                        else if (!Regex.IsMatch(VkId, RegexVkId))
                        {
                            error = "The e-mail should contain Latin letters";
                        }
                        else if (VkId.Length > 30)
                        {
                            error = "Length of Vk Id can't exceed 30 characters";
                        }
                        break;
                }
                Error = error;
                
                return error;
            }
        }

        /// <summary>
        /// Метод установки состояния checked полям валидируемого контакта.
        /// </summary>
        /// <param name="fieldName">Имя поля</param>
        public void SetChecked(string fieldName)
        {
            switch (fieldName)
            {
                case "SurnameTextBox":
                    _surnameChecked = true;
                    break;
                case "NameTextBox":
                    _nameChecked = true;
                    break;
                case "BirthDatePicker":
                    _birthDateChecked = true;
                    break;
                case "PhoneNumberTextBox":
                    _phoneNumberChecked = true;
                    PhoneNumber.NumberIsChecked = true;
                    break;
                case "EmailTextBox":
                    _emailChecked = true;
                    break;
                case "VkIdTextBox":
                    _vkIdChecked = true;
                    break;
            }
        }
    }
}
