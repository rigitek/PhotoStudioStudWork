﻿using Microsoft.EntityFrameworkCore;
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

            clientsComboBox.SelectedIndex = PhotoSession.Client.Id - 1;
            photographersComboBox.SelectedIndex = PhotoSession.Photographer.Id - 1;
            typeComboBox.SelectedIndex = PhotoSession.TypeOfPhotoSession.Id - 1;

            clientsComboBox.IsEnabled = false;
            photographersComboBox.IsEnabled = false;
            typeComboBox.IsEnabled = false;

            Hour.Text= PhotoSession.DateAndTime.Hour.ToString();
         
            Minute.Text = PhotoSession.DateAndTime.Minute.ToString();

            DataContext = PhotoSession;
        }

        private void AddPhotosessionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Clients.Load();
            db.Photographers.Load();
            db.TypeOfPhotoSessions.Load();

            clients = db.Clients.ToList();
            photographers = db.Photographers.ToList();
            typeOfPhotoSessions = db.TypeOfPhotoSessions.ToList();

            clientsComboBox.ItemsSource = clients;
            photographersComboBox.ItemsSource = photographers;
            typeComboBox.ItemsSource = typeOfPhotoSessions;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {

            if (PhotoSession != null)
            {
                if(Hour.Text !="" || Minute.Text != "")
                {
                    
                    TimeSpan timeTemp = new TimeSpan(PhotoSession.DateAndTime.Hour, PhotoSession.DateAndTime.Minute, 0);
                    PhotoSession.DateAndTime = PhotoSession.DateAndTime-timeTemp;

                    TimeSpan timeSpan = new TimeSpan(int.Parse(Hour.Text), int.Parse(Minute.Text), 0);
                    PhotoSession.DateAndTime= PhotoSession.DateAndTime.Add(timeSpan);
                }
                DialogResult = true;
            }
            else
            {
                Client client = clientsComboBox.SelectedItem as Client;
                Photographer photographer = photographersComboBox.SelectedItem as Photographer;
                TypeOfPhotoSession typeOfPhotoSession = typeComboBox.SelectedItem as TypeOfPhotoSession;

                DateTime DatePicker = Date.SelectedDate.Value;
                TimeSpan timeSpan = new TimeSpan(int.Parse(Hour.Text), int.Parse(Minute.Text), 0);
                DatePicker = DatePicker.Add(timeSpan);

                if (client == null) return;
                if (photographer == null) return;
                if (typeOfPhotoSession == null) return;

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
                db.PhotoSessions.Add(photoSession);
                db.SaveChanges();

                DialogResult = true;
            }
        }

      
    }
}
