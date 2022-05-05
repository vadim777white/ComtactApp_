using ContactsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ContactsAppUI
{
    /// <summary>
    /// Логика взаимодействия для ContactManagerWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public Contact Contact { get; set; }

        public ContactWindow(Contact contact)
        {
            Contact = contact;
            InitializeComponent();
            DataContext = new ContactWindowVM(Contact);
        }
    }
}
