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
            Settings.SECRET = builder.Configuration["JWT:Key"];
            Settings.ISSUER = builder.Configuration["JWT:Issuer"];
            Settings.AUDIENCE = builder.Configuration["JWT:Audience"];

            builder.Services.AddDbContext<PixAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PixAPIConnectionString")));

            #region Services
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped<UsuarioService>();
            #endregion

            return builder;
        }
    }
}
