using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// <see cref="XElement"/> LAST_DAY_UPDATED/VALUE.
  /// </summary>
  public class LastDayUpdate
  {
    public LastDayUpdate()
    {

    }
    public LastDayUpdate(ListItem item, XElement xelem) : this()
    {
      if(item != null)
      {
        Item = item;
      };
      if(xelem != null && xelem.Name.LocalName == "VALUE")
      {
        Value = ParseXmlDateTime(xelem);
      };
    }













    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Значение даты.
    /// Значение <see cref="XElement"/> VALUE.
    /// </summary>
    public DateTime? Value { get; set; }









    



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
