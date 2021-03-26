using App.Models;
using System.Threading.Tasks;

namespace App.DataAccess.Data.Repository.IRepository
{
    public interface IItemRepository : IRepository<Item>
    {
        void Update(Item item);
    }
}