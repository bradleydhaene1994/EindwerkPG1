using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Enumerations;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class Customer
    {
        public Customer(string firstName, string lastName, string municipality, string street, DateTime birthDate, string houseNumber, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Municipality = municipality;
            Street = street;
            BirthDate = birthDate;
            HouseNumber = houseNumber;
            Gender = gender;
        }

        public Customer(int id, string firstName, string lastName, string municipality, string street, DateTime birthDate, string houseNumber, Gender? gender)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Municipality = municipality;
            Street = street;
            BirthDate = birthDate;
            HouseNumber = houseNumber;
            Gender = gender;
        }
        private int _id;
        public int Id
        {
            get => _id;
            private set
            {
                _id = value;
            }
        }
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("First name cannot be empty.");
                else _firstName = value;
            }
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Last name cannot be empty.");
                else _lastName = value;
            }
        }
        private string _municipality;
        public string Municipality
        {
            get => _municipality;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Municipality cannot be empty.");
                else _municipality = value;
            }
        }
        private string _street;
        public string Street
        {
            get => _street;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Street cannot be empty.");
                else _street = value;
            }
        }
        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            private set
            {
                if (value > DateTime.Today) throw new CustomerException("BirthDate cannot be in the future.");
                else _birthDate = value;
            }
        }
        private string _houseNumber;
        public string HouseNumber
        {
            get => _houseNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Housenumber cannot be empty.");
                else _houseNumber = value;
            }
        }
        private Gender? _gender;
        public Gender? Gender
        {
            get => _gender;
            set
            {
                _gender = value;
            }
        }
    }
}
