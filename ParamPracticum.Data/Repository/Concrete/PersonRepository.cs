using ParamPracticum.Data.Context;
using ParamPracticum.Data.Models;
using ParamPracticum.Data.Repository.Abstract;

namespace ParamPracticum.Data.Repository.Concrete
{
    public class PersonRepository  : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext Context): base(Context)
        {

        }
    }
}
