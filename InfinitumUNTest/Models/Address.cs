using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using System.Xml.Serialization;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Адрес личности/организации.
  /// <see cref="XElement"/> INDIVIDUAL_ADDRESS (ENTITY_ADDRESS).
  /// </summary>
  public class Address
  {
    public Address()
    {

    }
    public Address(ListItem item, XElement xelem) : this()
    {
      if(item != null)
      {
        Item = item;
        ItemId = item.Id;
      };
      if(xelem != null && (xelem.Name.LocalName == "INDIVIDUAL_ADDRESS" || xelem.Name.LocalName == "ENTITY_ADDRESS"))
      {
        Street = xelem.Element(S("STREET"))?.Value;
        City = xelem.Element(S("CITY"))?.Value;
        Province = xelem.Element(S("STATE_PROVINCE"))?.Value;
        ZipCode = xelem.Element(S("ZIP_CODE"))?.Value;
        Country = xelem.Element(S("COUNTRY"))?.Value;
        Note = xelem.Element(S("NOTE"))?.Value;
      };
    }







    /// <summary>
    /// Проверяет корректность созданного при парсинге XML-данных экземпляра.
    /// Возвращает <see langword="false"/>, если дочерние элементы 
    /// XML-элемента INDIVIDUAL_ADDRESS (ENTITY_ADDRESS) содержали пустые значения.
    /// </summary>
    /// <returns></returns>
    public bool IsCorrect() => !(
      string.IsNullOrWhiteSpace(Street) && string.IsNullOrWhiteSpace(City) 
      && 
      string.IsNullOrWhiteSpace(Province) && string.IsNullOrWhiteSpace(ZipCode) 
      && 
      string.IsNullOrWhiteSpace(Country) && string.IsNullOrWhiteSpace(Note)
      );









    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> STREET.
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> CITY.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> STATE_PROVINCE.
    /// </summary>
    public string Province { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> ZIP_CODE.
    /// </summary>
    public string ZipCode { get; set; }

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
    public int ItemId { get; set; }

    /// <summary>
    /// Ссылка на родительскую сущность (личность/организацию).
    /// </summary>
    [ForeignKey("ItemId")]
    public virtual ListItem Item { get; set; }

  }
}
