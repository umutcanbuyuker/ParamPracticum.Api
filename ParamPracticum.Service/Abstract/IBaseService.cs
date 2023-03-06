using ParamPracticum.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamPracticum.Service.Abstract
{
    public interface IBaseService<Dto,TEntity>
    {
        Task<BaseResponse<Dto>> GetByIdAsync(int id);
        Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync();
        Task<BaseResponse<Dto>> InsertAsync(Dto insertResource);
        Task<BaseResponse<Dto>> RemoveAsync(int id);
        Task<BaseResponse<Dto>> UpdateAsync(int id, Dto updateResource);

    }
}
