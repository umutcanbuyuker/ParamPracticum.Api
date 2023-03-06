using AutoMapper;
using ParamPracticum.Data.Models;
using ParamPracticum.Data.Repository.Abstract;
using ParamPracticum.Data.Uow;
using ParamPracticum.Dto.Dtos;
using ParamPracticum.Service.Abstract;

namespace ParamPracticum.Service.Concrete
{
    public class PersonService : BaseService<PersonDto,Person>, IPersonService
    {
        private readonly IGenericRepository<Person> _repository;    

        public PersonService(IGenericRepository<Person> genericRepository, IMapper mapper,IUnitOfWork unitOfWork) :base(genericRepository, mapper, unitOfWork)
        {
            this._repository = genericRepository;
        }

        //Extra metotlar veya base 
    }
}
