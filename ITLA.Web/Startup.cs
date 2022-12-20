using ITLA.BLL.Contracts;
using ITLA.BLL.Services;
using ITLA.DAL.Context;
using ITLA.DAL.Interfaces;
using ITLA.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLA.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection service)
        {
            string conntString = this.Configuration.GetConnectionString("ITLAContext");
            // Context //
            service.AddDbContext<ITLAContext>(options => options.UseSqlServer(conntString));

            // Student Dependencies //
            //services.AddStudentDependency();

            // General Dependencies
            service.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

            //Repositories
            service.AddScoped<IStudentRepository, StudentRepository>();
            service.AddScoped<IInstructorsRepository, InstructorsRepository>();
            service.AddScoped<ICourseRepository, CourseRepository>();
            service.AddScoped<IDepartmentRepository, DepartmentRepository>();

            //Services(BL)//
            service.AddTransient<IStudentService, StudentService>();
            service.AddTransient<IInstructorService, InstructorService>();
            service.AddTransient<ICourseService, CourseService>();
            service.AddTransient<IDepartmentService, DepartmentService>();


            service.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
