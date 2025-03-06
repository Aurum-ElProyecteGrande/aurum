namespace Aurum.Data.Seeders.DataReaders
{
    public abstract class CsvDataReader<T> where T : class
    {
        protected StreamReader _reader;
        public CsvDataReader(string fileName, IConfiguration config)
        {
            string _csvFilePath = Path.Combine(config["RawSeedDataPath"] ?? "", fileName);
            _reader = new StreamReader(_csvFilePath);
        }
        public abstract IEnumerable<T> Read();
    }
}
