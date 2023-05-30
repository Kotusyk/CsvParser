using CsvHelper;
using System.Globalization;
using App.Data;
using App.Model;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using Microsoft.SqlServer.Server;

namespace AnnaMelnyk_TestTask.Services
{
    public class CSVReaderService : ICSVReaderService
    {
        private readonly TaskDBContext _context;
        public CSVReaderService(TaskDBContext context)
        {
            _context = context;
        }
        public IEnumerable<User> ReadCSV<T>(Stream file)
        {
                var reader = new StreamReader(file);
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true, 
                };
                var csv = new CsvReader(reader, config);
            
            var records = csv.GetRecords<User>();
            foreach (var record in records)
            {
                _context.Users.Add(record);
            }
            _context.SaveChanges();

            return records;
            
        }
    }
}
