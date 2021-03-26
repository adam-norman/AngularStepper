using App.Models;
using System.Threading.Tasks;

namespace App.DataAccess.Data.Repository.IRepository
{
    public interface IStepRepository : IRepository<Step>
    {
        Task Update(Step step);
    }
}