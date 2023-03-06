using ParamPracticum.Data.Models;
using ParamPracticum.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamPracticum.Service.Abstract
{
    public interface IPersonService : IBaseService<PersonDto,Person>
    {

    }
}
