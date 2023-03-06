using AutoMapper;
using ParamPracticum.Data.Models;
using ParamPracticum.Data.Repository.Abstract;
using ParamPracticum.Data.Uow;
using ParamPracticum.Dto.Dtos;
using ParamPracticum.Service.Abstract;

namespace ParamPracticum.Service.Concrete
{
    public class AccounService : BaseService<AccountDto, Account>, IAccountService
    {
        private readonly IGenericRepository<Account> _repository;
        public AccounService(IGenericRepository<Account> genericRepository, IMapper mapper, IUnitOfWork unitOfWork): base(genericRepository, mapper, unitOfWork)
        {
            _repository = genericRepository;   
        }
    }
}
