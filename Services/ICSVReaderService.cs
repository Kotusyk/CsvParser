using App.Model;

namespace AnnaMelnyk_TestTask.Services
{
    public interface ICSVReaderService
    {
        public IEnumerable<User> ReadCSV<T>(Stream file);
    }
}
    