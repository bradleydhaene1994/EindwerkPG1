using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Managers;

namespace CustomerSimulationBL.Services
{
    public class SimulationExportService
    {
        public void ExportStatisticsToTxt(SimulationExport export, string filePath, CountryVersion countryVersion)
        {
            using StreamWriter writer = new StreamWriter(filePath);

            //Simulation Data
            writer.WriteLine("=== SIMULATION DATA ===");
            writer.WriteLine($"Client name: {export.SimulationData.Client}");
            writer.WriteLine($"Date created: {export.SimulationData.DateCreated}");
            writer.WriteLine($"Country: {countryVersion.Country}");
            writer.WriteLine($"Year: {countryVersion.Year}");

            //Simulation Settings
            writer.WriteLine("=== SIMULATION SETTINGS");
            writer.WriteLine($"Total customers: {export.SimulationSettings.TotalCustomers}");
            writer.WriteLine($"Age range: {export.SimulationSettings.MinAge} - {export.SimulationSettings.MaxAge}");
            writer.WriteLine($"House number range: {export.SimulationSettings.MinNumber} - {export.SimulationSettings.MaxNumber}");
            writer.WriteLine($"House numbers can have letters: {export.SimulationSettings.HasLetters}");
            writer.WriteLine($"Percentage of house numbers that can have letters: {export.SimulationSettings.PercentageLetters}%");
            writer.WriteLine("Municipalities selected: ");
            if (export.SimulationSettings.SelectedMunicipalities == null)
            {
                writer.WriteLine("No municipalities specified");
            }
            else
            {
                foreach (var sel in export.SimulationSettings.SelectedMunicipalities)
                {
                    writer.WriteLine($"{sel.Municipality.Name}: {sel.Percentage}%");
                }
            }

            //General Statistics
            writer.WriteLine("--- GENERAL STATISTICS ---");
            writer.WriteLine($"Youngest Customer: {export.SimulationStatisticsResult.General.AgeYoungestCustomer}");
            writer.WriteLine($"Oldest Customer: {export.SimulationStatisticsResult.General.AgeOldestCustomer}");
            writer.WriteLine($"Average age at date of simumlation: {export.SimulationStatisticsResult.General.AverageAgeOnSimulationDate}");
            writer.WriteLine($"Average age at current duate: {export.SimulationStatisticsResult.General.AverageAgeOnCurrentDate}");

            //Customers per municipality
            writer.WriteLine("--- CUSTOMERS PER MUNICIPALITY ---");
            foreach (var stat in export.SimulationStatisticsResult.CustomersPerMunicipality)
            {
                writer.WriteLine($"{stat.Municipality.Name}: {stat.Count}");
            }

            //Streets per municipality
            writer.WriteLine("--- STREETS PER MUNICIPALITY ---");
            foreach (var stat in export.SimulationStatisticsResult.StreetsPerMunicipality)
            {
                writer.WriteLine($"{stat.Municipality.Name}: {stat.Count}");
            }

            //Name statistics
            writer.WriteLine("--- MALE NAMES ---");
            foreach (var mn in export.SimulationStatisticsResult.MaleNames)
            {
                writer.WriteLine($"{mn.Name}: {mn.Count}");
            }

            writer.WriteLine("--- FEMALE NAMES ---");
            foreach (var fn in export.SimulationStatisticsResult.FemaleNames)
            {
                writer.WriteLine($"{fn.Name}: {fn.Count}");
            }

            writer.WriteLine("--- LAST NAMES ---");
            foreach (var ln in export.SimulationStatisticsResult.LastNames)
            {
                writer.WriteLine($"{ln.Name}: {ln.Count}");
            }
        }
        public void ExportCustomerDataToTxt( SimulationData simulationData, List<CustomerDTO> customers, string filePath, CountryVersion countryVersion)
        {
            using StreamWriter writer = new(filePath);

            writer.WriteLine("FirstName;LastName;Municipality;Street;HouseNumber;BirthDate");

            foreach (var c in customers)
            {
                writer.WriteLine($"{c.FirstName};{c.LastName};{c.Municipality};{c.Street};{c.HouseNumber};{c.BirthDate}");
            }
        }
    }
}
