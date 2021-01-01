using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LibOtomasyonu.Data.Repositories
{
    public interface IRepository<T> where T:class //Generic yapıldı
    {
        List<T> GetAll(); //Sınıflar içerisinde GetAll() methodu olucak, tüm verileri çeken
        List<T> GetAll(Expression<Func<T, bool>> predicate); //Linq desteği sunarak Expressionları kullanılıyor
        T GetById(int id); //id ye göre veri çekmek için bu metoda ulaşılarak çekilecek
        T Get(Expression<Func<T, bool>> predicate); //Farklı koşullara göre veri çekilecekse bu method

        T Add(T entity); //Veri eklemek istenildiğinde
        T Update(T entity);
        T Delete(T entity);
        void Delete(int id); //id ye göre veri silme
        int Count(Expression<Func<T, bool>> filter = null); //Filtre boşta gelebilir, param gelirse filtrelenip çekilecek.
    }
}
