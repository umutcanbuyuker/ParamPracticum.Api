using Microsoft.EntityFrameworkCore;
using ParamPracticum.Base.Jwt;
using ParamPracticum.Data.Context;

namespace ParamPracticum.Api.Extensions
{
    public static class DbContextExtension
    {
        public static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration)
        {


            // Postgresql veya Mssql kullanım seçeneği için:

            var dbtype = configuration.GetConnectionString("DbType");
            if (dbtype == "SQL")
            {
                var dbConfig = configuration.GetConnectionString("baglanti");
                services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(dbConfig));
            }
            //else if (dbtype == "PostgreSql")
            //{
            //    var dbConfig = configuration.GetConnectionString("baglanti");
            //    services.AddDbContext<AppDbContext>(options => options
            //    .UseNpgsql(dbConfig));
            //}
        }
    }
}
