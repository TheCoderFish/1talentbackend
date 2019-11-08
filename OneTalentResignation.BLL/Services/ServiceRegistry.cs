using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneTalentResignation.BLL.Interface;
using OneTalentResignation.BLL.ResignationBLL;
using OneTalentResignation.DAL.ResignationProcess.Interface;
using OneTalentResignation.DAL.ResignationProcess.ResignationDAL;

namespace OneTalentResignation.BLL.Repository
{
    public static class ServiceRegistry
    {
        public static void AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IApprovalResignationBLL, ApprovalResignationBLL>();
            services.AddScoped<IApprovalResignationDAL, DatabaseQuery>();
            services.AddScoped<IEmployeeResignationDAL, EmployeeResignationDAL>();
            services.AddScoped<ICalculations, Calculations>();
            services.AddScoped<EmployeeResignationBLL>();
            services.AddScoped<EmployeeResignationDatabaseCalls>();
            services.AddTransient<IConcernEmployees, ConcernEmployees>();
            services.AddDbContext<DAL.Context.ResignationContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection")));


        }
    }
}
