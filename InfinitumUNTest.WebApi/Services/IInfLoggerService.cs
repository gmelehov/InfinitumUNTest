using InfinitumUNTest.Models;
using InfinitumUNTest.WebApi.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;




namespace InfinitumUNTest.WebApi.Services
{
  /// <summary>
  /// Интерфейс сервиса логгирования.
  /// </summary>
  public interface IInfLoggerService
  {

    /// <summary>
    /// Возвращает запись об ошибке с указанным содержимым.
    /// </summary>
    /// <param name="content">Текстовое содержимое записи.</param>
    /// <returns></returns>
    LogEntry LogError(string content);

    /// <summary>
    /// Возвращает информационную запись с указанным содержимым.
    /// </summary>
    /// <param name="content">Текстовое содержимое записи.</param>
    /// <returns></returns>
    LogEntry LogInfo(string content);

    /// <summary>
    /// Возвращает предупредительную запись с указанным содержимым.
    /// </summary>
    /// <param name="content">Текстовое содержимое записи.</param>
    /// <returns></returns>
    LogEntry LogWarning(string content);

    /// <summary>
    /// Сохраняет указанную запись в базу данных.
    /// </summary>
    /// <param name="ctx">Контекст базы данных.</param>
    /// <param name="entry">Запись в журнал логов.</param>
    void Log(InfTestContext ctx, LogEntry entry);

  }
}
