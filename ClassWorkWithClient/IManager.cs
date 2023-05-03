using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassWorkWithClient;

namespace ClassWorkWithClient
{
    public interface Imanager
    {
        public void DeleteClient(Client client);
        public void AddClient(Client client);
        public static abstract void SetFirstName(string newFirstName, Client client);
        public static abstract void SetLastName(string newLastName, Client client);
        public static abstract void SetFatherName(string newFatherName, Client client);
        public static abstract void SetPassportData(string newPassportData, Client client);
    }
}
