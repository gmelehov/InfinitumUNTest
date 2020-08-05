using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Известная/предполагаемая дата рождения личности.
  /// <see cref="XElement"/> INDIVIDUAL_DATE_OF_BIRTH.
  /// </summary>
  public class IndividualDOB
  {
    public IndividualDOB()
    {

    }
    public IndividualDOB(IndividualItem individual, XElement xelem) : this()
    {
      if(individual != null)
      {
        IndividualItem = individual;
      };
      if(xelem != null && xelem.Name.LocalName == "INDIVIDUAL_DATE_OF_BIRTH")
      {
        DateType = xelem.Element(S("TYPE_OF_DATE"))?.Value;
        Note = xelem.Element(S("NOTE"))?.Value;
        Date = ParseXmlDateTime(xelem.Element(S("DATE")));
        Year = ParseXmlShort(xelem.Element(S("YEAR")));
        FromYear = ParseXmlShort(xelem.Element(S("FROM_YEAR")));
        ToYear = ParseXmlShort(xelem.Element(S("TO_YEAR")));
      };
    }









    /// <summary>
    /// Проверяет корректность созданного при парсинге XML-данных экземпляра.
    /// Возвращает <see langword="false"/>, если дочерние элементы 
    /// <see cref="XElement"/> INDIVIDUAL_DATE_OF_BIRTH содержали пустые значения.
    /// </summary>
    /// <returns></returns>
    public bool IsCorrect() => !(
      string.IsNullOrWhiteSpace(DateType) && string.IsNullOrWhiteSpace(Note) 
      && 
      Date == null && Year == null && FromYear == null && ToYear == null
      );










    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> TYPE_OF_DATE.
    /// </summary>
    public string DateType { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> NOTE.
    /// </summary>
    public string Note { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> DATE.
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> YEAR.
    /// </summary>
    public short? Year { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> FROM_YEAR.
    /// </summary>
    public short? FromYear { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> TO_YEAR.
    /// </summary>
    public short? ToYear { get; set; }











    /// <summary>
    /// Внешний ключ.
    /// </summary>
    public int IndividualId { get; set; }

    /// <summary>
    /// Ссылка на родительскую личность.
    /// </summary>
    [ForeignKey("IndividualId")]
    public virtual IndividualItem IndividualItem { get; set; }
  }
}
