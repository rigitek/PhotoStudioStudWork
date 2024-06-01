using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    /// Логика взаимодействия для AddPhotosessionWindow.xaml
    /// </summary>
    public partial class AddPhotosessionWindow : Window
    {
        PhotoStudioContext db = new PhotoStudioContext();

        //создание листов, позже применим
        List<Client> clients;
        List<Photographer> photographers;
        List<TypeOfPhotoSession> typeOfPhotoSessions;

        public PhotoSession PhotoSession { get; set; }

        public AddPhotosessionWindow()
        {
            InitializeComponent();
            this.Loaded += AddPhotosessionWindow_Loaded;

            Complete.IsEnabled = false;
        }
        public AddPhotosessionWindow(PhotoSession photoSession)
        {
            InitializeComponent();
            this.Loaded += AddPhotosessionWindow_Loaded;

            PhotoSession = photoSession;

            //инициализация комобоксов
            clientsComboBox.SelectedIndex = PhotoSession.Client.Id - 1;
            photographersComboBox.SelectedIndex = PhotoSession.Photographer.Id - 1;
            typeComboBox.SelectedIndex = PhotoSession.TypeOfPhotoSession.Id - 1;

            //выключаем комбобоксы для изменения
            clientsComboBox.IsEnabled = false;
            photographersComboBox.IsEnabled = false;
            typeComboBox.IsEnabled = false;

            //инициализация часа
            Hour.Text= PhotoSession.DateAndTime.Hour.ToString();
            //инициализация минут
            Minute.Text = PhotoSession.DateAndTime.Minute.ToString();

            DataContext = PhotoSession;
        }

        private void AddPhotosessionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка данных из бд
            db.Clients.Load();
            db.Photographers.Load();
            db.TypeOfPhotoSessions.Load();

            //передача данных в лист
            clients = db.Clients.ToList();
            photographers = db.Photographers.ToList();
            typeOfPhotoSessions = db.TypeOfPhotoSessions.ToList();

            //передача листов в комбобокс
            clientsComboBox.ItemsSource = clients;
            photographersComboBox.ItemsSource = photographers;
            typeComboBox.ItemsSource = typeOfPhotoSessions;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            //если объект не пуст
            if (PhotoSession != null)
            {
                //если час или минута не пустые
                if(Hour.Text !="" || Minute.Text != "")
                {
                    //обнуление времени
                    TimeSpan timeTemp = new TimeSpan(PhotoSession.DateAndTime.Hour, PhotoSession.DateAndTime.Minute, 0);
                    PhotoSession.DateAndTime = PhotoSession.DateAndTime-timeTemp;

                    //присвоение нового времени
                    TimeSpan timeSpan = new TimeSpan(int.Parse(Hour.Text), int.Parse(Minute.Text), 0);
                    PhotoSession.DateAndTime= PhotoSession.DateAndTime.Add(timeSpan);
                }
                DialogResult = true;
            }
            else
            {
                //передача выбранных объектов
                Client client = clientsComboBox.SelectedItem as Client;
                Photographer photographer = photographersComboBox.SelectedItem as Photographer;
                TypeOfPhotoSession typeOfPhotoSession = typeComboBox.SelectedItem as TypeOfPhotoSession;

                //запись даты и времени
                DateTime DatePicker = Date.SelectedDate.Value;
                TimeSpan timeSpan = new TimeSpan(int.Parse(Hour.Text), int.Parse(Minute.Text), 0);
                DatePicker = DatePicker.Add(timeSpan);

                //если какой то объект не выбран, то ничего не происходит
                if (client == null) return;
                if (photographer == null) return;
                if (typeOfPhotoSession == null) return;

                //создание нового объекта
                PhotoSession photoSession = new PhotoSession
                {
                    DateAndTime = DatePicker,
                    Time = Time.Text,
                    Location = Location.Text,
                    Price = Convert.ToDouble(Price.Text),
                    Complete = false,
                    Client = client,
                    Photographer = photographer,
                    TypeOfPhotoSession = typeOfPhotoSession
                };

                
                db.Clients.Attach(client);
                db.Photographers.Attach(photographer);
                db.TypeOfPhotoSessions.Attach(typeOfPhotoSession);

                //добавление новго объекта в бд
                db.PhotoSessions.Add(photoSession);
                //сохранение бд
                db.SaveChanges();

                DialogResult = true;
            }
        }

      
    }
}
