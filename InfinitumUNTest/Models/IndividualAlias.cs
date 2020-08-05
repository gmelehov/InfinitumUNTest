using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;




namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Возможный псевдоним личности.
  /// <see cref="XElement"/> INDIVIDUAL_ALIAS.
  /// </summary>
  public class IndividualAlias : Alias
  {
    public IndividualAlias()
    {

    }
    public IndividualAlias(IndividualItem individual, XElement xelem) : this()
    {
      if(individual != null)
      {
        IndividualItem = individual;
        IndividualId = individual.Id;
      };
      FillCommonInfo(xelem);
      if(xelem != null && xelem.Name.LocalName == "INDIVIDUAL_ALIAS")
      {
        BirthDate = xelem.Element(S("DATE_OF_BIRTH"))?.Value;
        BirthCity = xelem.Element(S("CITY_OF_BIRTH"))?.Value;
        BirthCountry = xelem.Element(S("COUNTRY_OF_BIRTH"))?.Value;
        Note = xelem.Element(S("NOTE"))?.Value;
      };
    }










    /// <summary>
    /// Значение <see cref="XElement"/> DATE_OF_BIRTH.
    /// </summary>
    public string BirthDate { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> CITY_OF_BIRTH.
    /// </summary>
    public string BirthCity { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> COUNTRY_OF_BIRTH.
    /// </summary>
    public string BirthCountry { get; set; }

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
