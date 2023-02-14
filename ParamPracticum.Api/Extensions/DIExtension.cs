using ParamPracticum.Api.CustomService;
using ParamPracticum.Data.Uow;

namespace ParamPracticum.Api.Extensions
{
    public static class DIExtension
    {
        public static void AddServiceDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
