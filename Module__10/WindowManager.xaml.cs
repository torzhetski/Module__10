using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
using ClassWorkWithClient;

namespace Module__10
{
    /// <summary>
    /// Логика взаимодействия для WindowManager.xaml
    /// </summary>
    public partial class WindowManager : Window
    {
        Manger manager;
        ObservableCollection<int> whatChanged;

        public WindowManager()
        {
            InitializeComponent();
            manager = new Manger();
            ClientSpisok.ItemsSource = manager.clients;
            whatChanged = new ObservableCollection<int>();
        }

        private void ClientSpisok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientSpisok.SelectedIndex >= 0)
            {
                ButtonChange.Visibility = Visibility.Visible;
                PhoneNumber.Visibility = Visibility.Visible;
                if (manager.clients[ClientSpisok.SelectedIndex].WhoChanged == string.Empty ||
                manager.clients[ClientSpisok.SelectedIndex].WhoChanged == null ||
                    manager.clients[ClientSpisok.SelectedIndex].WhoChanged == "")
                {
                    WhoChanged.Visibility = Visibility.Collapsed;
                    DateOfChange.Visibility = Visibility.Collapsed;
                    Title.Visibility = Visibility.Collapsed;
                    WhatChanged.Visibility = Visibility.Collapsed;
                }
                else
                {
                    WhoChanged.Visibility = Visibility.Visible;
                    DateOfChange.Visibility = Visibility.Visible;
                    Title.Visibility = Visibility.Visible;
                    WhatChanged.Visibility = Visibility.Visible;
                }
            }
            else
                ButtonChange.Visibility = Visibility.Collapsed;
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            ButtonChange.Visibility = Visibility.Collapsed;
            ChangeInfo.Visibility = Visibility.Visible;
            ClientSpisok.IsEnabled = false;
            ButtonBack.IsEnabled = false;
            ButtonAdd.IsEnabled = false;
            ButtonDelete.IsEnabled = false;
            
        }

        private void ButtonDiscard_Click(object sender, RoutedEventArgs e)
        {
            ButtonChange.Visibility = Visibility.Visible;
            ChangeInfo.Visibility= Visibility.Collapsed;
            ClientSpisok.IsEnabled = true;
            ButtonBack.IsEnabled = true;
            ButtonAdd.IsEnabled = true;
            ButtonDelete.IsEnabled = true;
            
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ChangeInfo.Visibility = Visibility.Collapsed;
            ButtonChange.Visibility = Visibility.Visible;
            ClientSpisok.IsEnabled = true;
            ButtonBack.IsEnabled = true;
            ButtonDelete.IsEnabled = true;
            ButtonAdd.IsEnabled = true;
            WhoChanged.Visibility = Visibility.Visible;
            DateOfChange.Visibility = Visibility.Visible;
            Title.Visibility = Visibility.Visible;
            WhatChanged.Visibility = Visibility.Visible;
            Manger.SetPhoneNumber(NewPhone.Text, manager.clients[ClientSpisok.SelectedIndex]);
            Manger.SetFirstName(NewFirstName.Text, manager.clients[ClientSpisok.SelectedIndex]);
            Manger.SetLastName(NewLastName.Text, manager.clients[ClientSpisok.SelectedIndex]);
            Manger.SetFatherName(NewFatherName.Text, manager.clients[ClientSpisok.SelectedIndex]);
            Manger.SetPassportData(NewPassportData.Text, manager.clients[ClientSpisok.SelectedIndex]);
            Manger.SetChangeValues(manager.clients[ClientSpisok.SelectedIndex], false, whatChanged);
            using (StreamWriter sw = new("D:/ Client.json", false))
                foreach (var item in manager.clients)
                    sw.WriteLine(JsonConvert.SerializeObject(item));
            whatChanged.Clear();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            manager.DeleteClient(manager.clients[ClientSpisok.SelectedIndex]);
            Title.Visibility= Visibility.Collapsed;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            FullInfo.Visibility = Visibility.Collapsed;
            AddClientInfo.Visibility = Visibility.Visible;  
            ButtonAdd.IsEnabled = false;
            ButtonBack.IsEnabled = false;
            ButtonDelete.IsEnabled = false;
            ClientSpisok.IsEnabled = false;
        }

        private void Add_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = String.Empty;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {

            if (AddName.Text == string.Empty || AddName.Text == "Введите имя..." ||
                AddLastName.Text == string.Empty || AddLastName.Text == "Введите фамилию..." ||
                AddFatherName.Text == string.Empty || AddFatherName.Text == "Введите отчество..." ||
                AddPhoneNumber.Text == string.Empty || AddPhoneNumber.Text == "Введите номер телефона..."
                )
                MessageBox.Show("Все поля кроме пасспортных данных должны быть заполнены");
            else
            {
                if (AddPassportData.Text == "Введите пасспортные данные...")
                    AddPassportData.Text = string.Empty;
                Client client = new(AddName.Text, AddLastName.Text, AddFatherName.Text, AddPhoneNumber.Text, AddPassportData.Text);
                manager.AddClient(client);
                AddClientInfo.Visibility = Visibility.Collapsed;
                FullInfo.Visibility = Visibility.Visible;
                ButtonAdd.IsEnabled = true;
                ButtonBack.IsEnabled = true;
                ButtonDelete.IsEnabled = true;
                ClientSpisok.IsEnabled = true;
                AddName.Text = "Введите имя...";
                AddLastName.Text = "Введите фамилию...";
                AddFatherName.Text = "Введите отчество...";
                AddPhoneNumber.Text = "Введите номер телефона...";
                AddPassportData.Text = "Введите пасспортные данные...";
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            FullInfo.Visibility = Visibility.Visible;
            AddClientInfo.Visibility = Visibility.Collapsed;
            ButtonAdd.IsEnabled = true;
            ButtonBack.IsEnabled = true;
            ButtonDelete.IsEnabled = true;
            ClientSpisok.IsEnabled = true;
            AddName.Text = "Введите имя...";
            AddLastName.Text = "Введите фамилию...";
            AddFatherName.Text = "Введите отчество...";
            AddPhoneNumber.Text = "Введите номер телефона...";
            AddPassportData.Text = "Введите пасспортные данные...";
        }

        private void NewPhone_GotFocus(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < whatChanged.Count(); i++)
                if (whatChanged[i] == 1)
                    whatChanged.Remove(whatChanged[i]);
            whatChanged.Add(1);
        }

        private void NewFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < whatChanged.Count(); i++)
                if (whatChanged[i] == 2)
                    whatChanged.Remove(whatChanged[i]);
            whatChanged.Add(2);
        }

        private void NewLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < whatChanged.Count(); i++)
                if (whatChanged[i] == 3)
                    whatChanged.Remove(whatChanged[i]);
            whatChanged.Add(3);
        }

        private void NewFatherName_GotFocus(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < whatChanged.Count(); i++)
                if (whatChanged[i] == 4)
                    whatChanged.Remove(whatChanged[i]);
            whatChanged.Add(4);
        }

        private void NewPassportData_GotFocus(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < whatChanged.Count(); i++)
                if (whatChanged[i] == 5)
                    whatChanged.Remove(whatChanged[i]);
            whatChanged.Add(5);
        }
    }
}
