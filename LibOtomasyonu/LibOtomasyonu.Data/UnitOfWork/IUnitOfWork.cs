using LibOtomasyonu.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibOtomasyonu.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        //Bu method ile tek bir interface'den GetRepository() methoduna ulaşarak işlemleri halletmiş oluruz.

        int SaveChanges();
    }
}
