using System.Windows;

namespace ContactsAppUI
{
    /// <summary>
    /// Вью-модель окна "About".
    /// </summary>
    public class AboutWindowVM
    {
        /// <summary>
        /// Команда перехода на почту.
        /// </summary>
        private RelayCommand _goToMailCommand;

        /// <summary>
        /// Команда перехода в GitHub.
        /// </summary>
        private RelayCommand _goToGitCommand;

        /// <summary>
        /// Поле команды перехода на почту.
        /// </summary>
        private RelayCommand _cancelCommand;

        /// <summary>
        /// Свойство команды перехода на почту.
        /// </summary>
        public RelayCommand GoToMailCommand
        {
            get
            {
                return _goToMailCommand ??
                    (_goToMailCommand = new RelayCommand(obj =>
                    {
                        System.Diagnostics.Process.Start("mailto:vdm.working@gmail.com");
                    }));
            }
        }

        /// <summary>
        /// Свойство команды перехода на GitHub.
        /// </summary>
        public RelayCommand GoToGitCommand
        {
            get
            {
                return _goToGitCommand ??
                   (_goToGitCommand = new RelayCommand(obj =>
                   {
                       System.Diagnostics.Process.Start("https://github.com/vadim777white/ContactsApp");
                   }));
            }
        }

        /// <summary>
        /// Команда закрытия окна добавления/редактирования
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                    (_cancelCommand = new RelayCommand(obj =>
                    {
                        ((Window)obj).DialogResult = false;
                        ((Window)obj).Close();
                    }));
            }
        }
    }
}
