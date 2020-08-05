using InfinitumUNTest.Models;
using InfinitumUNTest.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;




namespace InfinitumUNTest.WebApi.Services
{
  /// <summary>
  /// Сервис логгирования.
  /// </summary>
  public class InfLoggerService : IInfLoggerService
  {


    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public LogEntry LogError(string content) => new LogEntry { Level = LogLevel.Error, CreatedOn = DateTime.Now, Content = content };

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public LogEntry LogInfo(string content) => new LogEntry { Level = LogLevel.Information, CreatedOn = DateTime.Now, Content = content };

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public LogEntry LogWarning(string content) => new LogEntry { Level = LogLevel.Warning, CreatedOn = DateTime.Now, Content = content };

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="entry"></param>
    public void Log([FromServices]InfTestContext ctx, LogEntry entry)
    {
      if(ctx != null && entry != null)
      {
        bool hasEntry = ctx.LogEntries.Any(a => a.Id == entry.Id);
        if (!hasEntry)
        {
          ctx.LogEntries.Add(entry);
          ctx.SaveChanges();
        };
      };
    }

  }
}
