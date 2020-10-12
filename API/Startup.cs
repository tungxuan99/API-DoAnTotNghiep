using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Helper;
using DAL.Helper.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddControllers();
            services.AddTransient<IDatabaseHelper, DatabaseHelper>();
            services.AddTransient<ITinTucRepository, TinTucRepository>();
            services.AddTransient<ITinTucBusiness, TinTucBusiness>();
            services.AddTransient<IHocSinhRepository, HocSinhRepository>();
            services.AddTransient<IHocSinhBusiness, HocSinhBusiness>();
            services.AddTransient<IDiemDanhRepository, DiemDanhRepository>();
            services.AddTransient<IDiemDanhBusiness, DiemDanhBusiness>();
            services.AddTransient<ICTDiemDanhRepository, CTDiemDanhRepository>();
            services.AddTransient<ICTDiemDanhBusiness, CTDiemDanhBusiness>();
            services.AddTransient<IDiemRepository, DiemRepository>();
            services.AddTransient<IDiemBusiness, DiemBusiness>();
            services.AddTransient<IGiaoVienRepository, GiaoVienRepository>();
            services.AddTransient<IGiaoVienBusiness, GiaoVienBusiness>();
            services.AddTransient<ILopHocRepository, LopHocRepository>();
            services.AddTransient<ILopHocBusiness, LopHocBusiness>();
            services.AddTransient<IMonHocRepository, MonHocRepository>();
            services.AddTransient<IMonHocBusiness, MonHocBusiness>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUsersBusiness, UsersBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseApiMiddleware();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHttpsRedirection();
        }
    }
}
