using App.DataAccess.Data.Repository.IRepository;

namespace App.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Steps = new StepRepository(dbContext);
            Items = new ItemRepository(dbContext);
        }

        public IStepRepository Steps
        {
            get;
            private set;
        }
        public IItemRepository Items
        {
            get;
            private set;
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}