using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для TypeOfPhotoSessionWindow.xaml
    /// </summary>
    public partial class TypeOfPhotoSessionWindow : Window
    {
        PhotoStudioContext db = new PhotoStudioContext();
        public TypeOfPhotoSessionWindow()
        {
            InitializeComponent();
            this.Loaded += TypeOfPhotoSessionWindow_Loaded;
        }

        private void TypeOfPhotoSessionWindow_Loaded(object sender, RoutedEventArgs e)
        {

            db.TypeOfPhotoSessions.Load();
            DataContext = db.TypeOfPhotoSessions.Local.ToObservableCollection();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddTypeOfPhotoSessionWindow AddTypeOfPhotoSessionWindow = new AddTypeOfPhotoSessionWindow(new TypeOfPhotoSession());

            //AddHumanWindow.Show();
            if (AddTypeOfPhotoSessionWindow.ShowDialog() == true)
            {
                TypeOfPhotoSession TypeOfPhotoSession = AddTypeOfPhotoSessionWindow.TypeOfPhotoSession;
                db.TypeOfPhotoSessions.Add(TypeOfPhotoSession);
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            TypeOfPhotoSession? typeOfPhotoSession = typeOfPhotoSessionsList.SelectedItem as TypeOfPhotoSession;
            if (typeOfPhotoSession is null) return;

            AddTypeOfPhotoSessionWindow AddTypeOfPhotoSessionWindow = new AddTypeOfPhotoSessionWindow(new TypeOfPhotoSession
            {
                Id = typeOfPhotoSession.Id,
                Title = typeOfPhotoSession.Title,
                Visible = typeOfPhotoSession.Visible
            });


            if (AddTypeOfPhotoSessionWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                typeOfPhotoSession = db.TypeOfPhotoSessions.Find(AddTypeOfPhotoSessionWindow.TypeOfPhotoSession.Id);
                if (typeOfPhotoSession != null)
                {
                    typeOfPhotoSession.Title = AddTypeOfPhotoSessionWindow.TypeOfPhotoSession.Title;
                    typeOfPhotoSession.Visible = AddTypeOfPhotoSessionWindow.TypeOfPhotoSession.Visible;
                    db.SaveChanges();
                    typeOfPhotoSessionsList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            TypeOfPhotoSession? typeOfPhotoSession = typeOfPhotoSessionsList.SelectedItem as TypeOfPhotoSession;
            // если ни одного объекта не выделено, выходим
            if (typeOfPhotoSession is null) return;
            db.TypeOfPhotoSessions.Remove(typeOfPhotoSession);
            db.SaveChanges();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
