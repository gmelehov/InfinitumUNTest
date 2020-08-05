using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;




namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Псевдоним сущности (личности/организации).
  /// </summary>
  public abstract class Alias
  {







    /// <summary>
    /// Извлекает значения из дочерних элементов, 
    /// общих для <see cref="XElement"/> INDIVIDUAL_ALIAS и <see cref="XElement"/> ENTITY_ALIAS.
    /// </summary>
    /// <param name="xelem"><see cref="XElement"/> INDIVIDUAL_ALIAS (ENTITY_ALIAS).</param>
    /// <returns></returns>
    public virtual Alias FillCommonInfo(XElement xelem)
    {
      if (xelem != null && (xelem.Name.LocalName == "ENTITY_ALIAS" || xelem.Name.LocalName == "INDIVIDUAL_ALIAS"))
      {
        Quality = xelem.Element(S("QUALITY"))?.Value;
        Name = xelem.Element(S("ALIAS_NAME"))?.Value;
      };
      return this;
    }


    /// <summary>
    /// Проверяет корректность созданного при парсинге XML-данных экземпляра.
    /// Возвращает <see langword="false"/>, если дочерние элементы 
    /// <see cref="XElement"/> INDIVIDUAL_ALIAS (ENTITY_ALIAS) содержали пустые значения.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsCorrect() => !string.IsNullOrWhiteSpace(Quality) && !string.IsNullOrWhiteSpace(Name);







    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> QUALITY.
    /// </summary>
    public string Quality { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> ALIAS_NAME.
    /// </summary>
    public string Name { get; set; }

  }
}
