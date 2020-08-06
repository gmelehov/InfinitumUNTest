using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InfinitumUNTest.WebApi.Data;
using InfinitumUNTest.WebApi.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;






namespace InfinitumUNTest.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }





    public IConfiguration Configuration { get; }

    
    
    
    
    /// <summary>
    /// ����������� �������� ����������.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {

      // ������������ �������� ���� ������ ����������
      services.AddDbContext<InfTestContext>(opt =>
      {
        // ���������� ������� �������� ��������� ������
        // ���������� ���������� SQLite �� ������� �����������, ������������ � appsettings.json
        //opt.UseLazyLoadingProxies().UseSqlite(Configuration.GetConnectionString("InfTestConn"));


        // ��� ������������� ���������� SqlServer �������� 
        // ��������� ���� ������ ����������� (��� ������� � appsettings.json),
        // ����������������� ������� ���� � ���������������� ������� ��� SQLite � PostgreSql.
        //opt.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("InfTestSql"));


        // ��� ������������� ���������� PostgreSql ����������������� ������� ����
        // � ���������������� ������� ��� SQLite � SqlServer.
        opt.UseLazyLoadingProxies().UseNpgsql(Configuration.GetConnectionString("InfTestPostgre"));
      });

      // ������������ ������ ������������
      services.AddInfTestLogger();


      services.AddControllers();
      services.AddMvc();

      // ���������� � ������������ �������� CORS
      services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
      {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        ;
      }));
    }





    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      };

      app.UseHttpsRedirection();
      app.UseCors("CorsPolicy");
      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
