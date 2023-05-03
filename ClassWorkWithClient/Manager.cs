using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassWorkWithClient;

namespace ClassWorkWithClient
{
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

        public static void SetPassportData(string newPassportData, Client client)
        {
            client.passportData = newPassportData;
            client.OnPropertyChanged(nameof(client.passportData));
        }
    }
}
