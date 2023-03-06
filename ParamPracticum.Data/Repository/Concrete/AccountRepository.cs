using ParamPracticum.Data.Context;
using ParamPracticum.Data.Models;
using ParamPracticum.Data.Repository.Abstract;

namespace ParamPracticum.Data.Repository.Concrete
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext Context) : base(Context)
        {

        }
    }
}
