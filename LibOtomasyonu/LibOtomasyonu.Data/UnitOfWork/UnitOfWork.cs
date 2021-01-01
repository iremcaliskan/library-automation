using LibOtomasyonu.Data.Repositories;
using System;

namespace LibOtomasyonu.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork //interface implement edildi
    {
        private readonly Context _context;

        public UnitOfWork() { //Yapıcı method, UnitOfWork nesnesi oluşturulup constructure'da Context bağlantısı alındı

            _context = new Context(); //cons daki nesneyi yeni contexte atadım, Context sınıfı oluşturulur

        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context); //Context bağlantısı gönderimi
        }

         public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception){ throw; }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing) {

            if (!this.disposed) 
            {
                if (disposing)
                    _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
