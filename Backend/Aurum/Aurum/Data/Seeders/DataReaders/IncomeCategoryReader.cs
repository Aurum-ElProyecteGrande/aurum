using Aurum.Data.Entities;
using System.Reflection.PortableExecutable;

namespace Aurum.Data.Seeders.DataReaders
{
    public class IncomeCategoryReader : CsvDataReader<IncomeCategory>
    {
        public IncomeCategoryReader(string fileName) : base(fileName) { }

        public override List<IncomeCategory> Read()
        {
            List<IncomeCategory> incomeCategories = new();

            using (_reader)
            {
                string line;
                while ((line = _reader.ReadLine()) != null)
                {
                    incomeCategories.Add(new IncomeCategory() { Name = line });
                }
            }
            return incomeCategories;
        }
    }
}
