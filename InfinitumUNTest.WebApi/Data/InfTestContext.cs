using InfinitumUNTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.WebApi.Data
{
  /// <summary>
  /// Контекст базы данных.
  /// </summary>
  public class InfTestContext : DbContext
  {
    public InfTestContext()
    {
      Database.EnsureCreated();
    }
    public InfTestContext(DbContextOptions<InfTestContext> options) : base(options)
    {
      Database.EnsureCreated();
    }









    public DbSet<ConsolidatedList> ConsolidatedLists { get; set; }
    public DbSet<EntityItem> EntityItems { get; set; }
    public DbSet<IndividualItem> IndividualItems { get; set; }
    public DbSet<LogEntry> LogEntries { get; set; }








    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
      builder.UseLazyLoadingProxies().UseSqlite("Data Source=InfTest.db");
      //builder.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Port=5432;Database=InfTestDb");
      builder.EnableDetailedErrors();
      builder.EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder mb)
    {
      mb.Entity<EntityItem>(ent =>
      {
        ent.HasOne(e => e.ConsolidatedList).WithMany(w => w.EntitiesList).HasForeignKey(f => f.ConsListId);
      });
      mb.Entity<IndividualItem>(ent =>
      {
        ent.HasOne(e => e.ConsolidatedList).WithMany(w => w.IndividualsList).HasForeignKey(f => f.ConsListId);
      });
      mb.Entity<LogEntry>(ent =>
      {
        ent.Property(e => e.Level).HasConversion(new EnumToStringConverter<LogLevel>());
      });
    }








    /// <summary>
    /// Проверяет существование в базе данных перечня,
    /// содержащегося в указанном XML-документе.
    /// Возвращает <see langword="true"/>, если в БД уже имеется перечень с указанным состоянием.
    /// Возвращает <see langword="false"/>, если в БД не имеется перечня с указанным состоянием.
    /// Возвращает <see langword="null"/>, если в указанном XML-документе отсутствует атрибут dateGenerated.
    /// </summary>
    /// <param name="xdoc">XML-документ с перечнем.</param>
    /// <returns></returns>
    public bool? HasThisGenerationDate(XDocument xdoc)
    {
      var dateGenerated = ParseXmlAttrDateTime(xdoc.Root, "dateGenerated");
      return dateGenerated != null ? ConsolidatedLists.Any(a => a.GeneratedOn == dateGenerated) as bool? : null;
    }

  }
}
