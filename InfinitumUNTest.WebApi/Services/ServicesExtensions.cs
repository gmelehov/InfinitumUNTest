using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace InfinitumUNTest.WebApi.Services
{
  /// <summary>
  /// Расширения стандартных сервисов.
  /// </summary>
  public static class ServicesExtensions
  {


    /// <summary>
    /// Подключение сервиса логгирования.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns></returns>
    public static IServiceCollection AddInfTestLogger(this IServiceCollection services) => services.AddTransient<IInfLoggerService, InfLoggerService>();

  }
}
