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
            //загрузка данных
            db.Clients.Load();
            //передача данных в контекст
            DataContext = db.Clients.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //создание обьекта окна добавление с передачей в конструктор нового клиента
            AddClientWindow AddClientWindow = new AddClientWindow(new Client());

            //если при закрытии окна будет тру
            if (AddClientWindow.ShowDialog() == true)
            {
                //создаем новый объект
                Client Client = AddClientWindow.Client;
                //добавляем новй объект в бд
                db.Clients.Add(Client);
                //сохраняем его в бд
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //передаем выбранный объект из страницы
            Client? client = clientsList.SelectedItem as Client;
            //если не выбран то нчиего не происходит
            if (client is null) return;

            //создаем объект окна и передаем данные выбранного объекта
            AddClientWindow AddClientWindow = new AddClientWindow(new Client
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber
            });

            //если при закрытии будет тру
            if (AddClientWindow.ShowDialog() == true)
            {
                // получаем измененный объект и находим его в бд
                client = db.Clients.Find(AddClientWindow.Client.Id);
                //если нашли
                if (client != null)
                {
                    //присваиваем новые свойства 
                    client.FirstName = AddClientWindow.Client.FirstName;
                    client.LastName = AddClientWindow.Client.LastName;
                    client.PhoneNumber = AddClientWindow.Client.PhoneNumber;
                    //сохраняем изменения
                    db.SaveChanges();
                    //обновляем список
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
            //если выделено то удаялем
            db.Clients.Remove(client);
            //сохраняем изменения
            db.SaveChanges();
        }

        //назад в меню отправляемся
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
