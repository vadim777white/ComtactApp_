using System;

namespace ContactsApp
{
    /// <summary>
    /// Класс номера телефона.
    /// </summary>
    public class PhoneNumber : ModelBase
    {
        /// <summary>
        /// Номер телефона.
        /// </summary>
        private long _number;

        /// <summary>
        /// Свойство номера телефона.
        /// Устанавливает значение номера в случае, если оно начинается с цифры 7 и его длина равна 11.
        /// </summary>
        public long Number
        {
            get => _number;
            set
            {
                if (value.ToString().Length != 11)
                {
                    throw new ArgumentException("Phone number length must be 11");
                }

                if (value.ToString()[0] != '7')
                {
                    throw new ArgumentException("Phone number must start with 7");
                }

                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
    }
}
