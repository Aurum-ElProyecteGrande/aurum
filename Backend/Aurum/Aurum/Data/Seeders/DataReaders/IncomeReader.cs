using Aurum.Data.Entities;
using System.Reflection.PortableExecutable;

namespace Aurum.Data.Seeders.DataReaders
{
    public class IncomeReader : CsvDataReader<Dictionary<string, List<string>>>
    {
        public IncomeReader(string fileName) : base(fileName) { }

        public override List<Dictionary<string, List<string>>> Read()
        {
            var incomes = new List<Dictionary<string, List<string>>>();
            incomes.Add(ReadByCategory());
            return incomes;
        }
        private Dictionary<string, List<string>> ReadByCategory()
        {
            Dictionary<string, List<string>> incomesByCategory = new()
            {
                {"Salary", new List<string>() },
                {"Freelance Work", new List<string>()},
                {"Business Profits", new List<string>() },
                {"Investments", new List<string>() },
                {"Rental", new List<string>() },
                {"Government", new List<string>() },
                {"Gifts", new List<string>() },
                {"SideHustles", new List<string>() },
                {"Bonuses", new List<string>() },
                {"Royalties", new List<string>()}
            };

            using (_reader)
            {
                string line;
                while ((line = _reader.ReadLine()) != null)
                {
                    var data = line.Split(",");
                    incomesByCategory["Salary"].Add(data[0]);
                    incomesByCategory["Freelance Work"].Add(data[1]);
                    incomesByCategory["Business Profits"].Add(data[2]);
                    incomesByCategory["Investments"].Add(data[3]);
                    incomesByCategory["Rental"].Add(data[4]);
                    incomesByCategory["Government"].Add(data[5]);
                    incomesByCategory["Gifts"].Add(data[6]);
                    incomesByCategory["SideHustles"].Add(data[7]);
                    incomesByCategory["Bonuses"].Add(data[8]);
                    incomesByCategory["Royalties"].Add(data[9]);
                }
            }
            return incomesByCategory;
        }
    }
}