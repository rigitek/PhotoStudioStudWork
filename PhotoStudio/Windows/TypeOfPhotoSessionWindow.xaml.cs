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
            //загруззка данных из бд
            db.TypeOfPhotoSessions.Load();
            //передача данных в контекст
            DataContext = db.TypeOfPhotoSessions.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //создание обьекта окна добавление с передачей в конструктор нового объекта
            AddTypeOfPhotoSessionWindow AddTypeOfPhotoSessionWindow = new AddTypeOfPhotoSessionWindow(new TypeOfPhotoSession());

            //если при закрытии окна будет тру
            if (AddTypeOfPhotoSessionWindow.ShowDialog() == true)
            {
                //создаем новый объект
                TypeOfPhotoSession TypeOfPhotoSession = AddTypeOfPhotoSessionWindow.TypeOfPhotoSession;
                //добавляем новй объект в бд
                db.TypeOfPhotoSessions.Add(TypeOfPhotoSession);
                //сохраняем его в бд
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //передаем выбранный объект из страницы
            TypeOfPhotoSession? typeOfPhotoSession = typeOfPhotoSessionsList.SelectedItem as TypeOfPhotoSession;
            //если не выбран то нчиего не происходит
            if (typeOfPhotoSession is null) return;

            //создаем объект окна и передаем данные выбранного объекта
            AddTypeOfPhotoSessionWindow AddTypeOfPhotoSessionWindow = new AddTypeOfPhotoSessionWindow(new TypeOfPhotoSession
            {
                Id = typeOfPhotoSession.Id,
                Title = typeOfPhotoSession.Title,
                Visible = typeOfPhotoSession.Visible
            });


            //если при закрытии будет тру
            if (AddTypeOfPhotoSessionWindow.ShowDialog() == true)
            {
                // получаем измененный объект и находим его в бд
                typeOfPhotoSession = db.TypeOfPhotoSessions.Find(AddTypeOfPhotoSessionWindow.TypeOfPhotoSession.Id);
                //если нашли
                if (typeOfPhotoSession != null)
                {
                    //присваиваем новые свойства 
                    typeOfPhotoSession.Title = AddTypeOfPhotoSessionWindow.TypeOfPhotoSession.Title;
                    typeOfPhotoSession.Visible = AddTypeOfPhotoSessionWindow.TypeOfPhotoSession.Visible;
                    //сохраняем изменения
                    db.SaveChanges();
                    //обновляем список
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
            //сохранение
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
