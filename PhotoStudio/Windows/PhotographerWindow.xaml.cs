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
            //загруззка данных из бд
            db.Photographers.Load();
            //передача данных в контекст
            DataContext = db.Photographers.Local.ToObservableCollection();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //создание обьекта окна добавление с передачей в конструктор нового объекта
            AddPhotographerWindow AddPhotographerWindow = new AddPhotographerWindow(new Photographer());

            //если при закрытии окна будет тру
            if (AddPhotographerWindow.ShowDialog() == true)
            {
                //создаем новый объект
                Photographer Photographer = AddPhotographerWindow.Photographer;
                //добавляем новй объект в бд
                db.Photographers.Add(Photographer);
                //сохраняем его в бд
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //передаем выбранный объект из страницы
            Photographer? photographer = photographersList.SelectedItem as Photographer;
            //если не выбран то нчиего не происходит
            if (photographer is null) return;

            //создаем объект окна и передаем данные выбранного объекта
            AddPhotographerWindow AddPhotographerWindow = new AddPhotographerWindow(new Photographer
            {
                Id = photographer.Id,
                FirstName = photographer.FirstName,
                LastName = photographer.LastName,
                PhoneNumber = photographer.PhoneNumber
            });

            //если при закрытии будет тру
            if (AddPhotographerWindow.ShowDialog() == true)
            {
                // получаем измененный объект и находим его в бд
                photographer = db.Photographers.Find(AddPhotographerWindow.Photographer.Id);
                //если нашли
                if (photographer != null)
                {
                    //присваиваем новые свойства 
                    photographer.FirstName = AddPhotographerWindow.Photographer.FirstName;
                    photographer.LastName = AddPhotographerWindow.Photographer.LastName;
                    photographer.PhoneNumber = AddPhotographerWindow.Photographer.PhoneNumber;
                    //сохраняем изменения
                    db.SaveChanges();
                    //обновляем список
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
            //если выделено то удаялем
            db.Photographers.Remove(photographer);
            //сохраняем изменения
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
