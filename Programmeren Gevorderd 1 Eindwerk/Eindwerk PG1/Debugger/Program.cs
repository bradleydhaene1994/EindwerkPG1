using CustomerSimulationDL.FileReaders;
using CustomerSimulationBL.Domein;
using CustomerSimulationDL.Repositories;

namespace Debugger
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connectionstring = "Data Source=BRADLEY\\SQLEXPRESS;Initial Catalog=SimulationCustomer;Integrated Security=True;Trust Server Certificate=True";
            string firstNamesMale = "C:\\Users\\dhaen\\EindwerkPG1\\SourceData\\Finland\\etunimitilasto-2025-08-13-dvv_miehet_ens.txt";
            string firstNamesFemale = "C:\\Users\\dhaen\\EindwerkPG1\\SourceData\\Finland\\etunimitilasto-2025-08-13-dvv_naiset_ens.txt";

            TxtFileReader reader = new TxtFileReader();

            IEnumerable<FirstName> firstNamesM = reader.ReadFirstNames(firstNamesMale);
            IEnumerable<FirstName> firstnamesF = reader.ReadFirstNames(firstNamesFemale);

            NameRepository repp = new NameRepository(connectionstring);

            repp.UploadFirstName(firstNamesM, 2);
            repp.UploadFirstName(firstnamesF, 2);

        }
    }
}
