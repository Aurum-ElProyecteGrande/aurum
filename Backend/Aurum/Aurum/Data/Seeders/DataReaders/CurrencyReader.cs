using Aurum.Data.Entities;
using Aurum.Repositories.CurrencyRepository;

namespace Aurum.Data.Seeders.DataReaders
{
    public class CurrencyReader : CsvDataReader<Currency>
    { 
        public CurrencyReader(string fileName) : base(fileName) { }

        public override List<Currency> Read()
        { 
            List<Currency> currencies = new();

            using (_reader)
            {
                string line;
                while ((line = _reader.ReadLine()) != null)
                {
                    var data = line.Split(",");
                    currencies.Add(new Currency() { Name = data[0], CurrencyCode = data[1], Symbol = data[2] });
                }
            }
            return currencies;
        }
    }
}
