using System.Xml.Linq;



namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Национальная принадлежность личности.
  /// <see cref="XElement"/> NATIONALITY/VALUE.
  /// </summary>
  public class Nationality : StringValue 
  {
    public Nationality() : base() { }
    public Nationality(IndividualItem item, XElement xelem) : base(item, xelem) { }
  }
}
