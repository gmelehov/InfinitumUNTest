using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Организация.
  /// <see cref="XElement"/> ENTITY.
  /// </summary>
  public class EntityItem : ListItem
  {
    public EntityItem() : base()
    {
      EntityAliases = new List<EntityAlias>();
    }
    public EntityItem(ConsolidatedList consolidated, XElement xelem) : this()
    {
      if(consolidated != null)
      {
        ConsolidatedList = consolidated;
        ConsListId = consolidated.Id;
      };
      if(xelem != null && xelem.Name.LocalName == "ENTITY")
      {
        FillCommonInfo(xelem);

        SubmittedOn = ParseXmlDateTime(xelem.Element(S("SUBMITTED_ON")));

        EntityAliases = xelem.Elements(S("ENTITY_ALIAS")).Select(s => new EntityAlias(this, s)).Where(w => w != null && w.IsCorrect()).ToList();
        Addresses = xelem.Elements(S("ENTITY_ADDRESS")).Select(s => new Address(this, s)).Where(w => w != null && w.IsCorrect()).ToList();

        FillLastDayUpdates(xelem.Element(S("LAST_DAY_UPDATED")));
      };
    }









    /// <summary>
    /// Значение <see cref="XElement"/> SUBMITTED_ON.
    /// </summary>
    public DateTime? SubmittedOn { get; set; }





    

    /// <summary>
    /// Сведения об известных псевдонимах организации.
    /// Коллекция <see cref="XElement"/> ENTITY_ALIAS.
    /// </summary>
    public virtual List<EntityAlias> EntityAliases { get; set; }









    /// <summary>
    /// Внешний ключ.
    /// </summary>
    public int ConsListId { get; set; }

    /// <summary>
    /// Ссылка на родительский перечень ООН.
    /// </summary>
    [ForeignKey("ConsListId")]
    public virtual ConsolidatedList ConsolidatedList { get; set; }
  }
}
