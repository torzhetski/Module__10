using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassWorkWithClient;

namespace ClassWorkWithClient
{

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
}

