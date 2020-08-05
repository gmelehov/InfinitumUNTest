using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;




namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Перечень ООН.
  /// <see cref="XElement"/> CONSOLIDATED_LIST.
  /// </summary>
  public class ConsolidatedList
  {
    public ConsolidatedList()
    {
      IndividualsList = new List<IndividualItem>();
      EntitiesList = new List<EntityItem>();
    }
    public ConsolidatedList(XDocument xdoc) : this()
    {
      if(xdoc != null)
      {
        GeneratedOn = ParseXmlAttrDateTime(xdoc.Root, "dateGenerated");

        IndividualsList = xdoc.Root.Element(S("INDIVIDUALS")).Elements(S("INDIVIDUAL")).Select(s => new IndividualItem(this, s)).Where(w => w != null).ToList();
        EntitiesList = xdoc.Root.Element(S("ENTITIES")).Elements(S("ENTITY")).Select(s => new EntityItem(this, s)).Where(w => w != null).ToList();
      };
    }











    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Дата генерации (состояние) перечня.
    /// Значение атрибута dateGenerated <see cref="XElement"/> CONSOLIDATED_LIST.
    /// </summary>
    public DateTime? GeneratedOn { get; set; }











    /// <summary>
    /// Список личностей.
    /// Коллекция <see cref="XElement"/> INDIVIDUALS/INDIVIDUAL.
    /// </summary>
    public virtual List<IndividualItem> IndividualsList { get; set; }

    /// <summary>
    /// Список организаций.
    /// Коллекция <see cref="XElement"/> ENTITIES/ENTITY.
    /// </summary>
    public virtual List<EntityItem> EntitiesList { get; set; }

  }
}
