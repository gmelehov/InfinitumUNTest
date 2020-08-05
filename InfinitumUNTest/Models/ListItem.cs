using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Сущность (личность/организация).
  /// </summary>
  public abstract class ListItem
  {
    public ListItem()
    {
      LastDayUpdates = new List<LastDayUpdate>();
      Addresses = new List<Address>();
    }











    /// <summary>
    /// Парсинг дочерних элементов, общих для
    /// <see cref="XElement"/> ENTITY и <see cref="XElement"/> INDIVIDUAL.
    /// </summary>
    /// <param name="xelem"><see cref="XElement"/> ENTITY (INDIVIDUAL).</param>
    /// <returns></returns>
    public virtual ListItem FillCommonInfo(XElement xelem)
    {
      if(xelem != null && (xelem.Name.LocalName == "ENTITY" || xelem.Name.LocalName == "INDIVIDUAL"))
      {
        DataId = ParseXmlInt(xelem.Element(S("DATAID"))) ?? 0;
        VersionNum = ParseXmlInt(xelem.Element(S("VERSIONNUM"))) ?? 1;
        FirstName = xelem.Element(S("FIRST_NAME"))?.Value;
        UNListType = xelem.Element(S("UN_LIST_TYPE"))?.Value;
        RefNumber = xelem.Element(S("REFERENCE_NUMBER"))?.Value;
        ListedOn = ParseXmlDateTime(xelem.Element(S("LISTED_ON")));
        DelistedOn = ParseXmlDateTime(xelem.Element(S("DELISTED_ON")));
        OriginalScript = xelem.Element(S("NAME_ORIGINAL_SCRIPT"))?.Value;
        Comments = xelem.Element(S("COMMENTS1"))?.Value;
        ListType = xelem.Element(S("LIST_TYPE"))?.Element(S("VALUE")).Value;
        SortKey = xelem.Element(S("SORT_KEY"))?.Value;
        SortKeyLastMod = xelem.Element(S("SORT_KEY_LAST_MOD"))?.Value;
      };
      return this;
    }

    /// <summary>
    /// Парсинг дочернего элемента LAST_DAY_UPDATED, 
    /// добавление сгенерированных непустых значений в соответствующий список.
    /// </summary>
    /// <param name="xelem">Дочерний <see cref="XElement"/> элемент LAST_DAY_UPDATED.</param>
    /// <returns></returns>
    public virtual ListItem FillLastDayUpdates(XElement xelem)
    {
      if(xelem != null && xelem.Name.LocalName == "LAST_DAY_UPDATED")
      {
        this.LastDayUpdates = xelem.Elements(S("VALUE")).Select(s => new LastDayUpdate(this, s)).Where(w => w != null && w.Value != null).ToList();
      };

      return this;
    }












    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> DATAID.
    /// </summary>
    public int DataId { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> VERSIONNUM.
    /// </summary>
    public int VersionNum { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> FIRST_NAME.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> UN_LIST_TYPE.
    /// </summary>
    public string UNListType { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> REFERENCE_NUMBER.
    /// </summary>
    public string RefNumber { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> LISTED_ON.
    /// </summary>
    public DateTime? ListedOn { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> DELISTED_ON.
    /// </summary>
    public DateTime? DelistedOn { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> NAME_ORIGINAL_SCRIPT.
    /// </summary>
    public string OriginalScript { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> LIST_TYPE/VALUE.
    /// </summary>
    public string ListType { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> COMMENTS1.
    /// </summary>
    public string Comments { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> SORT_KEY.
    /// </summary>
    public string SortKey { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> SORT_KEY_LAST_MOD.
    /// </summary>
    public string SortKeyLastMod { get; set; }













    /// <summary>
    /// Коллекция <see cref="XElement"/> LAST_DAY_UPDATED/VALUE.
    /// </summary>
    public virtual List<LastDayUpdate> LastDayUpdates { get; set; }

    /// <summary>
    /// Сведения об адресах личности/организации.
    /// Коллекция <see cref="XElement"/> INDIVIDUAL_ADDRESS (ENTITY_ADDRESS).
    /// </summary>
    public virtual List<Address> Addresses { get; set; }

  }
}
