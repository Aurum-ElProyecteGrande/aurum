using Aurum.Data.Context;
using Aurum.Data.Entities;

namespace Aurum.Data.Seeders.DataGenerators
{
    public class ExpenseSubCategoryGenerator
    {
        private AurumContext _context;
        private Dictionary<string, List<string>> _subsByCats;
        public ExpenseSubCategoryGenerator(AurumContext context, Dictionary<string, List<string>> expenseSubCategoriesByCategory)
        {
            _context = context;
            _subsByCats = expenseSubCategoriesByCategory;
        }

        public List<ExpenseSubCategory> GenerateExpenseSubCategories()
        {
            var actualCategories = GetActualCategories(_subsByCats);

            List<ExpenseSubCategory> expenseSubCategories = new();

            foreach (var kvp in actualCategories)
            {
                var cat = kvp.Key;
                foreach (var subCat in kvp.Value)
                {
                    expenseSubCategories.Add(new ExpenseSubCategory() { ExpenseCategoryId = GetExpenseCatId(cat), Name = subCat, IsBase = true });
                }
            }

            return expenseSubCategories;
        }
        private int GetExpenseCatId(string catName) =>
            _context.ExpenseCategories.FirstOrDefault(ec => ec.Name == catName).ExpenseCategoryId;


        private Dictionary<string, List<string>> GetActualCategories(Dictionary<string, List<string>> expenseSubCatsByCat)
        {
            Dictionary<string, List<string>> actualCategories = new();

            foreach (var cat in _context.ExpenseCategories)
            {
                if (expenseSubCatsByCat.ContainsKey(cat.Name))
                {
                    actualCategories.Add(cat.Name, expenseSubCatsByCat[cat.Name]);
                }
            }

            return actualCategories;
        }
    }
}
