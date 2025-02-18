namespace Aurum.Data.Seeders.DataReaders
{
    public abstract class CsvDataReader<T> where T : class
    {
        protected StreamReader _reader;

        public CsvDataReader(string fileName)
        {
            string _csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "raw-seeding-data", fileName);
            _reader = new StreamReader(_csvFilePath);
        }

        public abstract IEnumerable<T> Read();

    }
}
