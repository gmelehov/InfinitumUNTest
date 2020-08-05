using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;




namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Запись в журнале логов.
  /// </summary>
  public class LogEntry
  {
    public LogEntry()
    {

    }











    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Момент создания записи.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Тип записи.
    /// </summary>
    [Required, MaxLength(15)]
    public LogLevel Level { get; set; }

    /// <summary>
    /// Текст сообщения.
    /// </summary>
    [MaxLength(250)]
    public string Content { get; set; }

  }  
}
