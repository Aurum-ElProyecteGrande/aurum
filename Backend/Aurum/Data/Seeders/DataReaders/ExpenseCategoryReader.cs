using Aurum.Data.Entities;
using System.Reflection.PortableExecutable;

namespace Aurum.Data.Seeders.DataReaders
{
    public class ExpenseCategoryReader : CsvDataReader<ExpenseCategory>
    {
        public ExpenseCategoryReader(string fileName) : base(fileName) { }

        public override List<ExpenseCategory> Read()
        {
            List<ExpenseCategory> expenseCategories = new();

            using (_reader)
            {
                string line;
                while ((line = _reader.ReadLine()) != null)
                {
                    expenseCategories.Add(new ExpenseCategory() { Name = line });
                }
            }
            return expenseCategories;
        }
    }
}
