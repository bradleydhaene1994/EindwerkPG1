using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class SimulationData
    {
        public SimulationData(string client, DateTime dateCreated)
        {
            Client = client;
            DateCreated = dateCreated;
        }

        public SimulationData(int id, string client, DateTime dateCreated)
        {
            Id = id;
            Client = client;
            DateCreated = dateCreated;
        }
        private int _id;
        public int Id
        {
            get => _id;
            private set
            {
                if (value <= 0) throw new SimulationException("ID <= 0");
            }
        }
        private string _client;
        public string Client
        {
            get => _client;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new SimulationException("Client cannot be empty.");
                else _client = value;
            }
        }
        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated;
            private set
            {
                if (value > DateTime.Now) throw new SimulationException("Date created cannot be in the future");
                else _dateCreated = value;
            }
        }
    }
}
