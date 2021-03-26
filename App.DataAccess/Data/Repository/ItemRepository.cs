using App.DataAccess.Data.Repository.IRepository;
using App.Models;
using System.Linq;
using System.Threading.Tasks;

namespace App.DataAccess.Data.Repository
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        void IItemRepository.Update(Item item)
        {
            var itemFromDb = dbContext.Items.FirstOrDefault(i => i.Id == item.Id);
            if (itemFromDb != null)
            {
                itemFromDb.Title = item.Title;
                itemFromDb.Description = item.Description;
                dbContext.Items.Update(itemFromDb);
                dbContext.SaveChanges();
            }
        }
    }
}