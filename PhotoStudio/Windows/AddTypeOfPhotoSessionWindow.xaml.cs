using PhotoStudio.Models;
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

namespace PhotoStudio.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddTypeOfPhotoSessionWindow.xaml
    /// </summary>
    public partial class AddTypeOfPhotoSessionWindow : Window
    {
        public TypeOfPhotoSession TypeOfPhotoSession { get; set; }
        public AddTypeOfPhotoSessionWindow(TypeOfPhotoSession typeOfPhotoSession)
        {
            InitializeComponent();

            TypeOfPhotoSession = typeOfPhotoSession;
            DataContext = TypeOfPhotoSession;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
        }
    }
}
