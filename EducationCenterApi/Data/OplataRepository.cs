using EducationCenterApi.Models;

namespace EducationCenterApi.Data
{
    public interface IOplataRepository
    {
        Task<List<Oplata>> GetAllAsync();
        Task<Oplata?> GetByIdAsync(int id);
        Task<Oplata> AddAsync(Oplata oplata);
        Task<bool> DeleteAsync(int id);
    }

    public class InMemoryOplataRepository : IOplataRepository
    {
        private List<Oplata> _items = new()
        {
            new() { Id = 1, RoditelId = 10, RebenokId = 20, KolichestvoZanyatiy = 4, Summa = 4000 },
            new() { Id = 2, RoditelId = 11, RebenokId = 21, KolichestvoZanyatiy = 8, Summa = 8000, Status = "Оплачено" },
            new() { Id = 3, RoditelId = 12, RebenokId = 22, KolichestvoZanyatiy = 2, Summa = 2000 }
        };
        private int _nextId = 4;

        public Task<List<Oplata>> GetAllAsync() => Task.FromResult(_items.ToList());
        public Task<Oplata?> GetByIdAsync(int id) => Task.FromResult(_items.FirstOrDefault(o => o.Id == id));

        public Task<Oplata> AddAsync(Oplata oplata)
        {
            oplata.Id = _nextId++;
            _items.Add(oplata);
            return Task.FromResult(oplata);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var item = _items.FirstOrDefault(o => o.Id == id);
            if (item == null) return Task.FromResult(false);
            _items.Remove(item);
            return Task.FromResult(true);
        }
    }
}