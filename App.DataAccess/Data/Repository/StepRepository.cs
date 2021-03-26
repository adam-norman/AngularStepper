using App.DataAccess.Data.Repository.IRepository;
using App.Models;
using System.Linq;
using System.Threading.Tasks;

namespace App.DataAccess.Data.Repository
{
    public class StepRepository : Repository<Step>, IStepRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StepRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Update(Step step)
        {
            var stepFromDb = dbContext.Steps.FirstOrDefault(item => item.Id == step.Id);
            if (stepFromDb != null)
            {
                stepFromDb.Title = step.Title;
                dbContext.Steps.Update(stepFromDb);
              await  dbContext.SaveChangesAsync();
            }
        }
    }
}