﻿using PhotoStudio.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //открытие меню клиентов
        private void Client_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            this.Close();
            clientWindow.Show();
        }


        //открытие меню фотографов
        private void Photographer_Click(object sender, RoutedEventArgs e)
        {
            PhotographerWindow photographerWindow = new PhotographerWindow();
            this.Close();
            photographerWindow.Show();
        }

        //открытие меню фотосессий
        private void Photosession_Click(object sender, RoutedEventArgs e)
        {
            PhotosessionWindow photosessionWindow = new PhotosessionWindow();
            this.Close();
            photosessionWindow.Show();
        }

        //открытие меню типа фотосессий
        private void TypeOfPhotoSession_Click(object sender, RoutedEventArgs e)
        {
            TypeOfPhotoSessionWindow typeOfPhotoSessionWindow = new TypeOfPhotoSessionWindow();
            this.Close();
            typeOfPhotoSessionWindow.Show();
        }
    }

}