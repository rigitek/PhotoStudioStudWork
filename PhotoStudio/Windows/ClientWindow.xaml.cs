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
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        PhotoStudioContext db = new PhotoStudioContext();
        public ClientWindow()
        {
            InitializeComponent();
            this.Loaded += ClientWindow_Loaded;
        }

        private void ClientWindow_Loaded(object sender, RoutedEventArgs e)
        {

            db.Clients.Load();
            DataContext = db.Clients.Local.ToObservableCollection();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow AddClientWindow = new AddClientWindow(new Client());

            //AddHumanWindow.Show();
            if (AddClientWindow.ShowDialog() == true)
            {
                Client Client = AddClientWindow.Client;
                db.Clients.Add(Client);
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Client? client = clientsList.SelectedItem as Client;
            if (client is null) return;

            AddClientWindow AddClientWindow = new AddClientWindow(new Client
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber
            });


            if (AddClientWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                client = db.Clients.Find(AddClientWindow.Client.Id);
                if (client != null)
                {
                    client.FirstName = AddClientWindow.Client.FirstName;
                    client.LastName = AddClientWindow.Client.LastName;
                    client.PhoneNumber = AddClientWindow.Client.PhoneNumber;
                    db.SaveChanges();
                    clientsList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Client? client = clientsList.SelectedItem as Client;
            // если ни одного объекта не выделено, выходим
            if (client is null) return;
            db.Clients.Remove(client);
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
