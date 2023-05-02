using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkWithClientLibrary;
using Newtonsoft.Json;
using System.IO;

namespace Module__10
{
    /// <summary>
    /// Логика взаимодействия для WindowConsultant.xaml
    /// </summary>
    public partial class WindowConsultant : Window
    {
        Consultant consultant;
        ObservableCollection<int> whatChanged;

        public WindowConsultant()
        {
            InitializeComponent();
            //Client client = new("Имя 1", "Фамилия 1", "Отчество 1", "+375447630991");
            //Client client1 = new("Имя 2", "Фамилия 2", "Отчество 2", "+375447630992");
            //Client client2 = new("Имя 3", "Фамилия 3", "Отчество 3", "+375447630993");
            //using (StreamWriter sw = new("D:/ Client.json", false))
            //{
            //    sw.WriteLine(JsonConvert.SerializeObject(client));
            //    sw.WriteLine(JsonConvert.SerializeObject(client1));
            //    sw.WriteLine(JsonConvert.SerializeObject(client2));
            //}
            consultant = new Consultant();
            ClientSpisok.ItemsSource = consultant.clients;
            whatChanged = new ObservableCollection<int>();
        }

        private void ClientSpisok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonChangePhoneNumber.Visibility = Visibility.Visible;
            PhoneNumber.Visibility = Visibility.Visible;
            
            if (consultant.clients[ClientSpisok.SelectedIndex].WhoChanged == string.Empty ||
                consultant.clients[ClientSpisok.SelectedIndex].WhoChanged == null ||
                consultant.clients[ClientSpisok.SelectedIndex].WhoChanged == "")
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
            if (!(consultant.clients[ClientSpisok.SelectedIndex].PassportData == null ||
                consultant.clients[ClientSpisok.SelectedIndex].PassportData == string.Empty))
                PassportData.Text = "Паспортные данные: ************";
            else
                PassportData.Text = "Паспортные данные:" + string.Empty;
            
        }

        private void ButtonChangePhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            ButtonChangePhoneNumber.Visibility = Visibility.Collapsed;
            ButtonSave.Visibility = Visibility.Visible;
            ButtonDiscard.Visibility = Visibility.Visible;
            ClientSpisok.IsEnabled = false;
            ButtonBack.IsEnabled = false;
            NewPhone.Visibility = Visibility.Visible;
           
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonDiscard.Visibility = Visibility.Collapsed;
            ButtonChangePhoneNumber.Visibility = Visibility.Visible;
            ClientSpisok.IsEnabled = true;
            ButtonBack.IsEnabled = true;
            NewPhone.Visibility = Visibility.Collapsed;
            WhoChanged.Visibility = Visibility.Visible;
            DateOfChange.Visibility = Visibility.Visible;
            Title.Visibility = Visibility.Visible;
            WhatChanged.Visibility = Visibility.Visible;
            Consultant.SetPhoneNumber(NewPhone.Text, consultant.clients[ClientSpisok.SelectedIndex]);
            Consultant.SetChangeValues(consultant.clients[ClientSpisok.SelectedIndex], true, whatChanged);
            using (StreamWriter sw = new("D:/ Client.json", false))
                foreach (var item in consultant.clients)
                    sw.WriteLine(JsonConvert.SerializeObject(item));
            whatChanged.Clear();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void NewPhone_GotFocus(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < whatChanged.Count(); i++)
                if (whatChanged[i] == 1)
                    whatChanged.Remove(whatChanged[i]);
            whatChanged.Add(1);
        }

        private void ButtonDiscard_Click(object sender, RoutedEventArgs e)
        {
            ButtonChangePhoneNumber.Visibility = Visibility.Visible;
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonDiscard.Visibility = Visibility.Collapsed;
            ClientSpisok.IsEnabled = true;
            ButtonBack.IsEnabled = true;
            NewPhone.Visibility = Visibility.Collapsed;
        }
    }
}
