using ParamPracticum.Data.Models;
using ParamPracticum.Data.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamPracticum.Data.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Account> AccountRepository { get; } //readonly çalışacak
        Task CompleteAsync(); // Transactionları commit edecek
    }
}
