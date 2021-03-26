using System;
using System.Collections.Generic;
using System.Text;

namespace App.DataAccess.Data.Repository.IRepository
{
   public interface IUnitOfWork:IDisposable
    {
        IStepRepository Steps { get; }
        IItemRepository  Items { get; }
        void Save();
    }
}
