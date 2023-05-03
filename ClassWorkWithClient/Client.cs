using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassWorkWithClient
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
}