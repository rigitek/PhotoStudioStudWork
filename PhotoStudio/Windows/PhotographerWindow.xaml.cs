using Microsoft.EntityFrameworkCore;
using PhotoStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для PhotographerWindow.xaml
    /// </summary>
    public partial class PhotographerWindow : Window
    {
        PhotoStudioContext db = new PhotoStudioContext();
        public PhotographerWindow()
        {
            InitializeComponent();
            this.Loaded += PhotographerWindow_Loaded;
        }

        private void PhotographerWindow_Loaded(object sender, RoutedEventArgs e)
        {

            db.Photographers.Load();
            DataContext = db.Photographers.Local.ToObservableCollection();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddPhotographerWindow AddPhotographerWindow = new AddPhotographerWindow(new Photographer());

            //AddHumanWindow.Show();
            if (AddPhotographerWindow.ShowDialog() == true)
            {
                Photographer Photographer = AddPhotographerWindow.Photographer;
                db.Photographers.Add(Photographer);
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Photographer? photographer = photographersList.SelectedItem as Photographer;
            if (photographer is null) return;

            AddPhotographerWindow AddPhotographerWindow = new AddPhotographerWindow(new Photographer
            {
                Id = photographer.Id,
                FirstName = photographer.FirstName,
                LastName = photographer.LastName,
                PhoneNumber = photographer.PhoneNumber
            });


            if (AddPhotographerWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                photographer = db.Photographers.Find(AddPhotographerWindow.Photographer.Id);
                if (photographer != null)
                {
                    photographer.FirstName = AddPhotographerWindow.Photographer.FirstName;
                    photographer.LastName = AddPhotographerWindow.Photographer.LastName;
                    photographer.PhoneNumber = AddPhotographerWindow.Photographer.PhoneNumber;
                    db.SaveChanges();
                    photographersList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Photographer? photographer = photographersList.SelectedItem as Photographer;
            // если ни одного объекта не выделено, выходим
            if (photographer is null) return;
            db.Photographers.Remove(photographer);
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
