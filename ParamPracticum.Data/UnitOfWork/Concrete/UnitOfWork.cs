using ParamPracticum.Data.Context;
using ParamPracticum.Data.Models;
using ParamPracticum.Data.Repository.Abstract;
using ParamPracticum.Data.Repository.Concrete;

namespace ParamPracticum.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private bool disposed;

        public IGenericRepository<Account> AccountRepository { get; private set; }

        public IGenericRepository<Person> PersonRepository { get; private set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            AccountRepository = new GenericRepository<Account>(dbContext);
            PersonRepository = new GenericRepository<Person>(dbContext);

        }

        public async Task CompleteAsync()
        {
            var dbContextTransaction = dbContext.Database.BeginTransaction();
            {
                try
                {
                    dbContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //logging
                    dbContextTransaction.Rollback();
                }
            }
        }



        /* DBcontext'i dispose etmemiz lazım yani carbageCollecter'ın devreye girip memory'de kalan kullanılmayan verileri temizlemesi lazım. Dependency injection ile controoler'lara register ettiğimiz interfaceler her bir api çağırıldığında instance oluşturacak. Singleton olarak eklersek tek bir scope üzerinden çalışır. Örneğin transit olarak dispose etmezsek sürekli instance oluşturur ve memory şişmeye başlar. Dispose edilmeyen nesneler buna sebep olur.*/
        
        protected virtual void Clean(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        { 
            Clean(true);
            GC.SuppressFinalize(this);
        }
    }
}
