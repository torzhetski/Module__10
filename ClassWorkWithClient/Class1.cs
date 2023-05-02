using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkWithClientLibrary
{
    public class Client : INotifyPropertyChanged
    {

        internal string firstName;
        internal string lastName;
        internal string fatherName;
        internal string phoneNumber;
        internal string passportData;
        internal DateTime dateOfChange;
        internal string whoChanged;
        internal ObservableCollection<string> whatChanged = new();
        public string FirstName
        {
            get { return firstName; }
        }
        public string LastName
        {
            get { return lastName; }
        }
        public string FatherName
        {
            get { return fatherName; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
        }
        public string PassportData
        {
            get { return passportData; }
        }
        public DateTime DateOfChange
        {
            get { return dateOfChange; }
        }
        public string WhoChanged
        {
            get { return whoChanged; }
        }
        public ObservableCollection<string> WhatChanged
        {
            get { return whatChanged; }
        }


        [JsonConstructor]
        public Client(string firstname, string lastname, string fathername, string phonenumber, string passportdata, DateTime dateofchange, string whochanged, ObservableCollection<string> whatchanged)
        {
            firstName = firstname;
            lastName = lastname;
            phoneNumber = phonenumber;
            fatherName = fathername;
            passportData = passportdata;
            whoChanged = whochanged;
            dateOfChange = dateofchange;
            whatChanged = whatchanged;
        }
        public Client(string firstName, string lastName, string fatherName, string phoneNumber, string passportdata) : this(firstName, lastName, fatherName, phoneNumber, passportdata, DateTime.MinValue, string.Empty, new() ) { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public abstract class Employer
    {
        public ObservableCollection<Client> clients = new();
        public Employer()
        {
            using (StreamReader sr = new StreamReader("D:/ Client.json"))
            {
                while (!sr.EndOfStream)
                {
                    string json = sr.ReadLine();
                    Client client = JsonConvert.DeserializeObject<Client>(json);
                    clients.Add(client);
                }
            }
        }
        public static void SetPhoneNumber(string newPhone, Client client)
        {
            if (newPhone == string.Empty || newPhone == "")
                MessageBox.Show("Это поле должно быть заполнено");
            else
            {
                client.phoneNumber = newPhone;
                client.OnPropertyChanged(nameof(client.phoneNumber));
            }
        }
        public static void SetChangeValues(Client client, bool who, ObservableCollection<int> whatchanged)
        {
            client.whatChanged.Clear();
            client.dateOfChange = DateTime.Now;
            client.whoChanged = who ? "Консультант" : "Менеджер";
            foreach (var item in whatchanged)
                switch (item)
                {
                    case 1:
                        client.whatChanged.Add("Номер телефона");
                        break;

                    case 2:
                        client.whatChanged.Add("Имя");
                        break;
                        
                    case 3:
                        client.whatChanged.Add("Фамилия");
                        break;

                    case 4:
                        client.whatChanged.Add("Отчество");
                        break;

                    case 5:
                        client.whatChanged.Add("Пасспортные данные");
                        break;      
                }
            
            client.OnPropertyChanged(nameof(client.dateOfChange));
            client.OnPropertyChanged(nameof(client.whoChanged));
            client.OnPropertyChanged(nameof(client.whatChanged));
        }
    }

    public interface Imanager 
    {
        public  void DeleteClient(Client client);
        public void AddClient(Client client);
        public static abstract void SetFirstName(string newFirstName, Client client);
        public static abstract void SetLastName(string newLastName, Client client);
        public static abstract void SetFatherName(string newFatherName, Client client);
        public static abstract void SetPassportData(string newPassportData, Client client);
    }

    public class Consultant : Employer
    {
        public Consultant() : base() { }
    }



    public class Manger : Employer, Imanager
    {
        public Manger() : base() { }

        public void AddClient(Client client)
        {
            clients.Add(client);
            using (StreamWriter sw = new("D:/ Client.json", false))
                foreach (var item in clients)
                    sw.WriteLine(JsonConvert.SerializeObject(item));
        }

        public void DeleteClient(Client client)
        {
            clients.Remove(client);
            using (StreamWriter sw = new("D:/ Client.json", false))
                foreach (var item in clients)
                    sw.WriteLine(JsonConvert.SerializeObject(item));
        }

        public static void SetFatherName(string newFatherName, Client client)
        {
            if (newFatherName == string.Empty || newFatherName == "")
                MessageBox.Show("Это поле должно быть заполнено");
            else
            {
                client.fatherName = newFatherName;
                client.OnPropertyChanged(nameof(client.fatherName));
            }
        }

        public static void SetFirstName(string newFirstName, Client client)
        {
            if (newFirstName == string.Empty || newFirstName == "")
                MessageBox.Show("Это поле должно быть заполнено");
            else
            {
                client.firstName = newFirstName;
                client.OnPropertyChanged(nameof(client.firstName));
            }
        }

        public static void SetLastName(string newLastName, Client client)
        {
            if (newLastName == string.Empty || newLastName == "")
                MessageBox.Show("Это поле должно быть заполнено");
            else
            {
                client.lastName = newLastName;
                client.OnPropertyChanged(nameof(client.lastName));
            }
        }

        public  static void SetPassportData(string newPassportData, Client client)
        {            
                client.passportData = newPassportData;
                client.OnPropertyChanged(nameof(client.passportData));
        }
    }
}