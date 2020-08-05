using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// <see cref="XElement"/> VALUE, вложенный в элементы 
  /// <see cref="XElement"/> TITLE, <see cref="XElement"/> DESIGNATION, <see cref="XElement"/> NATIONALITY.
  /// </summary>
  public abstract class StringValue
  {
    public StringValue()
    {

    }
    public StringValue(IndividualItem item, XElement xelem) : this()
    {
      if(item != null)
      {
        IndividualItem = item;
        IndividualId = item.Id;
      };
      if (xelem != null && xelem.Name.LocalName == "VALUE")
      {
        Value = xelem.Value;
      };
    }










    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> VALUE.
    /// </summary>
    public string Value { get; set; }










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
