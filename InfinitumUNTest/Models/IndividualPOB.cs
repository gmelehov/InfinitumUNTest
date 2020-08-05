using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Известное/предполагаемое место рождения личности.
  /// <see cref="XElement"/> INDIVIDUAL_PLACE_OF_BIRTH.
  /// </summary>
  public class IndividualPOB
  {
    public IndividualPOB()
    {

    }
    public IndividualPOB(IndividualItem item, XElement xelem) : this()
    {
      if (item != null)
      {
        IndividualItem = item;
        IndividualId = item.Id;
      };
      if (xelem != null && xelem.Name.LocalName == "INDIVIDUAL_PLACE_OF_BIRTH")
      {
        City = xelem.Element(S("CITY"))?.Value;
        Province = xelem.Element(S("STATE_PROVINCE"))?.Value;
        Country = xelem.Element(S("COUNTRY"))?.Value;
        Note = xelem.Element(S("NOTE"))?.Value;
      };
    }










    /// <summary>
    /// Проверяет корректность созданного при парсинге XML-данных экземпляра.
    /// Возвращает <see langword="false"/>, если дочерние элементы 
    /// <see cref="XElement"/> INDIVIDUAL_PLACE_OF_BIRTH содержали пустые значения.
    /// </summary>
    /// <returns></returns>
    public bool IsCorrect() => !(
      string.IsNullOrWhiteSpace(City) && string.IsNullOrWhiteSpace(Province) 
      && 
      string.IsNullOrWhiteSpace(Country) && string.IsNullOrWhiteSpace(Note)
      );









    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> CITY.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> STATE_PROVINCE.
    /// </summary>
    public string Province { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> COUNTRY.
    /// </summary>
    public string Country { get; set; }

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
