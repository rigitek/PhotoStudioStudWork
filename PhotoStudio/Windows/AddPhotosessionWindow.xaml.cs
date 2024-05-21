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
        }
        public AddPhotosessionWindow(PhotoSession photoSession)
        {
            InitializeComponent();
            PhotoSession = photoSession;
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

            DialogResult = true;
        }
    }
}
