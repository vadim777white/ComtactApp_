using ContactsApp;
using System.ComponentModel;

namespace ContactsAppUI
{
    /// <summary>
    /// Класс для манипуляций над валидируемым номером телефона.
    /// </summary>
    public class PhoneNumberVM : ModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Номер телефона.
        /// </summary>
        private long _number;

        /// <summary>
        /// Автосвойство состояния поля номера телефона контакта.
        /// </summary>
        public bool NumberIsChecked { get; set; }

        /// <summary>
        /// Свойство ошибки.
        /// </summary>
        public string Error { get => null; }

        /// <summary>
        /// Свойство номера телефона.
        /// Устанавливает значение номера в случае, если оно начинается с цифры 7 и его длина равна 11.
        /// </summary>
        public long Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
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
                    case "Number":
                        if (Number == 0)
                        {
                            if (NumberIsChecked) error = "Phone number is unset";
                        }
                        else if (Number.ToString().Length != 11)
                        {
                            error = "Phone number length must be 11";
                        }
                        else if (Number.ToString()[0] != '7')
                        {
                            error = "Phone number must start with 7";
                        }
                        break;
                }

                return error;
            }
        }
    }
}
