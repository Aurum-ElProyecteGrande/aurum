using Aurum.Data.Entities;
using System.Reflection.PortableExecutable;

namespace Aurum.Data.Seeders.DataReaders
{
    public class IncomeReader : CsvDataReader<Dictionary<string, List<string>>>
    {
        private CsvDataReader<IncomeCategory> _incomeCategoryReader;
        public IncomeReader(string fileName, CsvDataReader<IncomeCategory> incomeCategoryReader) : base(fileName)
        {
            _incomeCategoryReader = incomeCategoryReader;
        }

        public override List<Dictionary<string, List<string>>> Read()
        {
            var incomes = new List<Dictionary<string, List<string>>>();
            incomes.Add(ReadByCategory());
            return incomes;
        }
        private Dictionary<string, List<string>> ReadByCategory()
        {
            var incomeCategories = _incomeCategoryReader.Read().ToList();
            Dictionary<string, List<string>> incomesByCategory = new();

            foreach (var incomeCategory in incomeCategories)
            {
                incomesByCategory.Add(incomeCategory.Name, new List<string>());
            }


            using (_reader)
            {
                string line;
                while ((line = _reader.ReadLine()) != null)
                {
                    var data = line.Split(",");

                    for (int i = 0; i < data.Length; i++)
                    {
                        incomesByCategory[incomeCategories[i].Name].Add(data[i]);
                    }
                }
            }
            return incomesByCategory;
        }
    }
}
