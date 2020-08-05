using System.Xml.Linq;



namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Титул личности.
  /// <see cref="XElement"/> TITLE/VALUE.
  /// </summary>
  public class Title : StringValue 
  {
    public Title() : base() { }
    public Title(IndividualItem item, XElement xelem) : base(item, xelem) { }
  }
}
