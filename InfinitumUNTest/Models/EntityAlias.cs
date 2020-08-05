using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Возможный псевдоним организации.
  /// <see cref="XElement"/> ENTITY_ALIAS.
  /// </summary>
  public class EntityAlias : Alias
  {
    public EntityAlias()
    {

    }
    public EntityAlias(EntityItem item, XElement xelem) : this()
    {
      if(item != null)
      {
        EntityItem = item;
        EntityId = item.Id;
      };
      FillCommonInfo(xelem);
    }









    /// <summary>
    /// Внешний ключ.
    /// </summary>
    public int EntityId { get; set; }

    /// <summary>
    /// Ссылка на родительскую организацию.
    /// </summary>
    [ForeignKey("EntityId")]
    public virtual EntityItem EntityItem { get; set; }

  }
}
