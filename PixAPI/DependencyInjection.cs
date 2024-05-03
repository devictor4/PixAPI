using Microsoft.EntityFrameworkCore;
using PixAPI.Business.Services;
using PixAPI.Business.Util;
using PixAPI.Repository.Context;

namespace PixAPI
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder ConfigureDI(this WebApplicationBuilder builder)
        {
            Settings.IS_DESENV = builder.Configuration["Ambiente"] == "2";

            builder.Services.AddDbContext<PixAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HydrosistemConnectionString")));

            builder.Services.AddScoped<UsuarioService>();

            return builder;
        }
    }
}
