using Aurum.Data.Entities;

namespace Aurum.Data.Seeders.DataReaders
{
    public class ExpenseReader : CsvDataReader<Dictionary<string, Dictionary<string, List<string>>>>
    {
        private CsvDataReader<Dictionary<string, List<string>>> _expenseSubCategoryReader;
        public ExpenseReader(string fileName, CsvDataReader<Dictionary<string, List<string>>> expenseSubCategoryReader) : base(fileName)
        {
            _expenseSubCategoryReader = expenseSubCategoryReader;
        }

        public override List<Dictionary<string, Dictionary<string, List<string>>>> Read()
        {
            var expenses = new List<Dictionary<string, Dictionary<string, List<string>>>>();
            expenses.Add(ReadByCategory());
            return expenses;
        }
        private Dictionary<string, Dictionary<string, List<string>>> ReadByCategory()
        {
            var expenseSubCategoriesByCategories = _expenseSubCategoryReader.Read().FirstOrDefault();
            Dictionary<string, Dictionary<string, List<string>>> expensesBySubByCategory = new();

            var items = ReadRawItems();
            int itemCatIndex = 0;
            int itemSubCatIndex = 0;

            foreach (var catSubCat in expenseSubCategoriesByCategories)
            {
                var curCatItems = items[itemCatIndex];

                var catName = catSubCat.Key;
                var subCatList = catSubCat.Value;
                Dictionary<string, List<string>> labelsBySubCat = new();

                foreach (var subCatName in subCatList)
                {
                    List<string> labels = new();
                    var curSubCatItems = curCatItems[itemSubCatIndex];
                    foreach (var item in curSubCatItems)
                    {
                        labels.Add(item);
                    }

                    labelsBySubCat.Add(subCatName, labels);
                    itemSubCatIndex++;
                }
                expensesBySubByCategory.Add(catName, labelsBySubCat);

                itemSubCatIndex = 0;
                itemCatIndex++;
            }

            return expensesBySubByCategory;
        }

        private List<List<List<string>>> ReadRawItems()
        {
            int itemPerSubCategory = 4;

            List<List<List<string>>> itemsByCategories = new();
            //  using (_reader)
            //{
            string line;
            while ((line = _reader.ReadLine()) != null)
            {
                var data = line.Split(",");

                var itemsPerOneCat = new List<List<string>>();

                for (int i = 0; i < data.Length; i += itemPerSubCategory)
                {
                    var itemsPerSubCat = new List<string>();
                    for (int j = 0; j < itemPerSubCategory; j++)
                    {
                        itemsPerSubCat.Add(data[i + j]);
                    }
                    itemsPerOneCat.Add(new List<string>(itemsPerSubCat));
                    itemsPerSubCat.Clear();
                }

                itemsByCategories.Add(new List<List<string>>(itemsPerOneCat));
            }
            return itemsByCategories;
            //      }

        }
    }
}
