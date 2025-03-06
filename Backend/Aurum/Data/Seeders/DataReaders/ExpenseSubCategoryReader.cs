using Aurum.Data.Entities;
using System.Reflection.PortableExecutable;

namespace Aurum.Data.Seeders.DataReaders
{
    public class ExpenseSubCategoryReader : CsvDataReader<Dictionary<string, List<string>>>
    {
        private CsvDataReader<ExpenseCategory> _expenseCategoryReader;
        public ExpenseSubCategoryReader(string fileName, CsvDataReader<ExpenseCategory> expenseCategoryReader, IConfiguration config) : base(fileName, config)
        {
            _expenseCategoryReader = expenseCategoryReader;
        }

        public override List<Dictionary<string, List<string>>> Read()
        {
            var expenseSubCategories = new List<Dictionary<string, List<string>>>();
            expenseSubCategories.Add(ReadByCategory());
            return expenseSubCategories;
        }
        private Dictionary<string, List<string>> ReadByCategory()
        {
            var expenseCategories = _expenseCategoryReader.Read().ToList();
            Dictionary<string, List<string>> expenseSubCategoriesByCategory = new();

            foreach (var expenseCategory in expenseCategories)
            {
                expenseSubCategoriesByCategory.Add(expenseCategory.Name, new List<string>());
            }


            using (_reader)
            {
                string line;
                while ((line = _reader.ReadLine()) != null)
                {
                    var data = line.Split(",");

                    for (int i = 0; i < data.Length; i++)
                    {
                        expenseSubCategoriesByCategory[expenseCategories[i].Name].Add(data[i]);
                    }
                }
            }
            return expenseSubCategoriesByCategory;
        }
    }
}