using AutoMapper;
using ParamPracticum.Base;
using ParamPracticum.Data.Repository.Abstract;
using ParamPracticum.Data.Uow;
using ParamPracticum.Service.Abstract;
using Serilog;

namespace ParamPracticum.Service.Concrete
{
    //base servis instance kimse almasın ve kimse kolay kullanmasın

    public abstract class BaseService<Dto,Entity> : IBaseService <Dto,Entity> where Entity : class
    {
        private readonly IGenericRepository<Entity> genericRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public BaseService(IGenericRepository<Entity> genericRepository,IMapper mapper,IUnitOfWork unitOfWork) 
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public virtual async Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync()
        {
            // Get list record from DB
            var tempEntity = await genericRepository.GetAllAsync();
            // Mapping Entity to Resource
            var result = mapper.Map<IEnumerable<Entity>, IEnumerable<Dto>>(tempEntity);

            return new BaseResponse<IEnumerable<Dto>>(result);
        }

        public virtual async Task<BaseResponse<Dto>> GetByIdAsync(int id)
        {
            var tempEntity = await genericRepository.GetByIdAsync(id);
            // Mapping Entity to Resource
            var result = mapper.Map<Entity, Dto>(tempEntity);

            return new BaseResponse<Dto>(result);
        }

        public virtual async Task<BaseResponse<Dto>> InsertAsync(Dto insertResource)
        {
            try
            {
                // Mapping Resource to Entity
                var tempEntity = mapper.Map<Dto, Entity>(insertResource);

                await genericRepository.InsertAsync(tempEntity);
                await unitOfWork.CompleteAsync();

                var mapped = mapper.Map<Entity, Dto>(tempEntity);

                return new BaseResponse<Dto>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Saving_Error");
                return new BaseResponse<Dto>("Saving_Error");
            }
        }

        public virtual async Task<BaseResponse<Dto>> RemoveAsync(int id)
        {
            try
            {
                // Validate Id is existent
                var tempEntity = await genericRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return new BaseResponse<Dto>("Id_NoData");

                genericRepository.RemoveAsync(tempEntity);
                await unitOfWork.CompleteAsync();

                return new BaseResponse<Dto>(mapper.Map<Entity, Dto>(tempEntity));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Deleting_Error");
                return new BaseResponse<Dto>("Deleting_Error");
            }
        }

        public virtual async Task<BaseResponse<Dto>> UpdateAsync(int id, Dto updateResource)
        {
            try
            {
                // Validate Id is existent
                var tempEntity = await genericRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return new BaseResponse<Dto>("NoData");
                // Update infomation
                var mapped = mapper.Map(updateResource, tempEntity);

                genericRepository.Update(mapped);
                await unitOfWork.CompleteAsync();

                // Mapping
                var resource = mapper.Map<Entity, Dto>(mapped);

                return new BaseResponse<Dto>(resource);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Updating_Error");
                return new BaseResponse<Dto>("Updating_Error");
            }
        }

    }
}
