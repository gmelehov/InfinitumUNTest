using System.Xml.Linq;




namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Должность личности.
  /// <see cref="XElement"/> DESIGNATION/VALUE.
  /// </summary>
  public class Designation : StringValue 
  {
    public Designation() : base() { }
    public Designation(IndividualItem item, XElement xelem) : base(item, xelem) { }
  }
}
