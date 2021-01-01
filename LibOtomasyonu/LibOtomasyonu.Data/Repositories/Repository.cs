using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace LibOtomasyonu.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class // koşul olarak sınıf, IRepo implement edildi
    {
        protected readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public Repository(Context context) { //Repository nesnesi oluşturulup constructure'da Context bağlantısı alındı

            _context = context; //cons daki nesneyi yani contexti oluşturulan contexte atadım, fielda aktarılır
            _dbSet = _context.Set<T>();

        }
        
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }


        public T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public T Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id); // id'yi bulup entity'e atılır
            if (entity == null) return; // kontrol
            Delete(entity);  // dolu ise sil
        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? _dbSet.Count() //Veri gönder null ile
                : _dbSet.Where(filter).Count(); //Boş değilse filtre eklenip veri gönder
        }
    }
}
