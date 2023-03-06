using Microsoft.AspNetCore.Mvc;
using ParamPracticum.Base;
using ParamPracticum.Dto.Dtos;
using ParamPracticum.Service.Abstract;
using Serilog;

namespace ParamPracticum.Api.Controllers
{
    [Route("param/api/[controller]")]
    [ApiController]
    public class PersonContoller : ControllerBase
    {
        private readonly IPersonService service;

        public PersonContoller(IPersonService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<PersonDto>>> Get()
        {
            Log.Debug("PersonContoller.Get");
            var persons = await service.GetAllAsync();
            return persons;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<PersonDto>> GetById(int id)
        {
            Log.Debug("PersonContoller.GetById");
            var person = await service.GetByIdAsync(id);
            return person;
        }

        [HttpPost]
        public async Task<BaseResponse<PersonDto>> Post([FromBody] PersonDto dto)
        {
            Log.Debug("PersonContoller.Post");

            dto.CreatedAt = DateTime.Now;
            dto.CreatedBy = "SystemUser";

            var person = await service.InsertAsync(dto);
            return person;
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse<PersonDto>> Put(int id, [FromBody] PersonDto dto)
        {
            Log.Debug("PersonContoller.Put");
            var person = await service.UpdateAsync(id, dto);
            return person;
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse<PersonDto>> Delete(int id)
        {
            Log.Debug("PersonContoller.Delete");
            var person = await service.RemoveAsync(id);
            return person;
        }
    }
}
