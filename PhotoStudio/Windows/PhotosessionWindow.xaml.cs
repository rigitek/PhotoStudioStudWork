using Microsoft.EntityFrameworkCore;
using PhotoStudio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для PhotosessionWindow.xaml
    /// </summary>
    public partial class PhotosessionWindow : Window
    {
        PhotoStudioContext db = new PhotoStudioContext();
        public PhotosessionWindow()
        {
            InitializeComponent();
            this.Loaded += PhotosessionWindow_Loaded;
        }

        private void PhotosessionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загруззка данных из бд
            db.Clients.Load();
            db.Photographers.Load();
            db.PhotoSessions.Load();
            db.TypeOfPhotoSessions.Load();

            //передача данных в контекст
            DataContext = db.PhotoSessions.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddPhotosessionWindow AddPhotosessionWindow = new AddPhotosessionWindow();
            if (AddPhotosessionWindow.ShowDialog() == true)
            {
                //загруззка данных из бд
                db.PhotoSessions.Load();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //передаем выбранный объект из страницы
            PhotoSession? photoSession = photosessionsList.SelectedItem as PhotoSession;
            //если не выбран то нчиего не происходит
            if (photoSession is null) return;

            //создаем объект окна и передаем данные выбранного объекта
            AddPhotosessionWindow AddPhotosessionWindow = new AddPhotosessionWindow(new PhotoSession
            {
                Id = photoSession.Id,
                DateAndTime = photoSession.DateAndTime,
                
                Price = photoSession.Price,
                Location = photoSession.Location,
                Complete=photoSession.Complete,
                Time = photoSession.Time,
                TypeOfPhotoSession = photoSession.TypeOfPhotoSession,
                Client = photoSession.Client,
                Photographer = photoSession.Photographer
            });

            //если при закрытии будет тру
            if (AddPhotosessionWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                photoSession = db.PhotoSessions.Find(AddPhotosessionWindow.PhotoSession.Id);
                //если нашли
                if (photoSession != null)
                {
                    //присваиваем новые свойства 
                    photoSession.DateAndTime = AddPhotosessionWindow.PhotoSession.DateAndTime;
                    photoSession.Price = AddPhotosessionWindow.PhotoSession.Price;
                    photoSession.Location = AddPhotosessionWindow.PhotoSession.Location;
                    photoSession.Time = AddPhotosessionWindow.PhotoSession.Time;
                    photoSession.Complete= AddPhotosessionWindow.PhotoSession.Complete;
                    photoSession.TypeOfPhotoSession = AddPhotosessionWindow.PhotoSession.TypeOfPhotoSession;
                    photoSession.Client = AddPhotosessionWindow.PhotoSession.Client;
                    photoSession.Photographer = AddPhotosessionWindow.PhotoSession.Photographer;

                    //сохраняем изменения
                    db.SaveChanges();
                    //обновляем список
                    photosessionsList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            PhotoSession? photoSession = photosessionsList.SelectedItem as PhotoSession;
            // если ни одного объекта не выделено, выходим
            if (photoSession is null) return;
            //если выделено то удаялем
            db.PhotoSessions.Remove(photoSession);
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
