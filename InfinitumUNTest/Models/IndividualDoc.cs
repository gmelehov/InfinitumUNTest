using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Известный документ личности.
  /// <see cref="XElement"/> INDIVIDUAL_DOCUMENT.
  /// </summary>
  public class IndividualDoc
  {
    public IndividualDoc()
    {

    }
    public IndividualDoc(IndividualItem item, XElement xelem) : this()
    {
      if (item != null)
      {
        IndividualItem = item;
      };
      if (xelem != null && xelem.Name.LocalName == "INDIVIDUAL_DOCUMENT")
      {
        Type = xelem.Element(S("TYPE_OF_DOCUMENT"))?.Value;
        OtherType = xelem.Element(S("TYPE_OF_DOCUMENT2"))?.Value;
        Number = xelem.Element(S("NUMBER"))?.Value;
        IssuingCountry = xelem.Element(S("ISSUING_COUNTRY"))?.Value;
        DateOfIssue = xelem.Element(S("DATE_OF_ISSUE"))?.Value;
        CityOfIssue = xelem.Element(S("CITY_OF_ISSUE"))?.Value;
        CountryOfIssue = xelem.Element(S("COUNTRY_OF_ISSUE"))?.Value;
        Note = xelem.Element(S("NOTE"))?.Value;
      };
    }









    /// <summary>
    /// Проверяет корректность созданного при парсинге XML-данных экземпляра.
    /// Возвращает <see langword="false"/>, если дочерние элементы 
    /// <see cref="XElement"/> INDIVIDUAL_DOCUMENT содержали пустые значения.
    /// </summary>
    /// <returns></returns>
    public bool IsCorrect() => !(
      string.IsNullOrWhiteSpace(Type) && string.IsNullOrWhiteSpace(OtherType) && string.IsNullOrWhiteSpace(Number)
      && 
      string.IsNullOrWhiteSpace(IssuingCountry) && string.IsNullOrWhiteSpace(DateOfIssue) && string.IsNullOrWhiteSpace(CityOfIssue)
      &&
      string.IsNullOrWhiteSpace(CountryOfIssue) && string.IsNullOrWhiteSpace(Note)
      );












    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> TYPE_OF_DOCUMENT.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> TYPE_OF_DOCUMENT2.
    /// </summary>
    public string OtherType { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> NUMBER.
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> ISSUING_COUNTRY.
    /// </summary>
    public string IssuingCountry { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> DATE_OF_ISSUE.
    /// </summary>
    public string DateOfIssue { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> CITY_OF_ISSUE.
    /// </summary>
    public string CityOfIssue { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> COUNTRY_OF_ISSUE.
    /// </summary>
    public string CountryOfIssue { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> NOTE.
    /// </summary>
    public string Note { get; set; }










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
