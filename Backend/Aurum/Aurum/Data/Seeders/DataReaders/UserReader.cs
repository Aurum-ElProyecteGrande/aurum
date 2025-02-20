using Aurum.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection.PortableExecutable;

namespace Aurum.Data.Seeders.DataReaders
{
    public class UserReader : CsvDataReader<IdentityUser>
    {
        public UserReader(string fileName, UserManager<IdentityUser> userManager) : base(fileName) { }

        public override List<IdentityUser> Read()
        {
            List<IdentityUser> users = new();

            using (_reader)
            {
                string line;
                while ((line = _reader.ReadLine()) != null)
                {
                    var data = line.Split(",");
                    users.Add(new() { UserName = data[0], Email = data[1] });
                }
            }
            return users;
        }
    }
}
