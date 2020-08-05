using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;
using static InfinitumUNTest.Utils;





namespace InfinitumUNTest.Models
{
  /// <summary>
  /// Личность.
  /// <see cref="XElement"/> INDIVIDUAL.
  /// </summary>
  public class IndividualItem : ListItem
  {
    public IndividualItem() : base()
    {
      Aliases = new List<IndividualAlias>();
      DOBs = new List<IndividualDOB>();
      POBs = new List<IndividualPOB>();
      Docs = new List<IndividualDoc>();
      Titles = new List<Title>();
      Designations = new List<Designation>();
      Nationalities = new List<Nationality>();
    }
    public IndividualItem(ConsolidatedList consolidated, XElement xelem) : this()
    {
      if (consolidated != null)
      {
        ConsolidatedList = consolidated;
        ConsListId = consolidated.Id;
      };
      if (xelem != null && xelem.Name.LocalName == "INDIVIDUAL")
      {
        FillCommonInfo(xelem);

        SecondName = xelem.Element(S("SECOND_NAME"))?.Value;
        ThirdName = xelem.Element(S("THIRD_NAME"))?.Value;
        FourthName = xelem.Element(S("FOURTH_NAME"))?.Value;
        Gender = xelem.Element(S("GENDER"))?.Value;
        Nationality = xelem.Element(S("NATIONALITY2"))?.Value;
        SubmittedBy = xelem.Element(S("SUBMITTED_BY"))?.Value;


        Aliases = xelem.Elements(S("INDIVIDUAL_ALIAS")).Select(s => new IndividualAlias(this, s)).Where(w => w.IsCorrect()).ToList();
        DOBs = xelem.Elements(S("INDIVIDUAL_DATE_OF_BIRTH")).Select(s => new IndividualDOB(this, s)).Where(w => w != null && w.IsCorrect()).ToList();
        POBs = xelem.Elements(S("INDIVIDUAL_PLACE_OF_BIRTH")).Select(s => new IndividualPOB(this, s)).Where(w => w != null && w.IsCorrect()).ToList();
        Docs = xelem.Elements(S("INDIVIDUAL_DOCUMENT")).Select(s => new IndividualDoc(this, s)).Where(w => w != null && w.IsCorrect()).ToList();
        Addresses = xelem.Elements(S("INDIVIDUAL_ADDRESS")).Select(s => new Address(this, s)).Where(w => w != null && w.IsCorrect()).ToList();


        var titles = xelem.Element(S("TITLE"));
        var designations = xelem.Element(S("DESIGNATION"));
        var nationalities = xelem.Element(S("NATIONALITY"));

        if(titles != null) Titles.AddRange(titles.Elements().Select(s => new Title(this, s)).Where(w => w != null).ToList());
        if(designations != null) Designations.AddRange(designations.Elements().Select(s => new Designation(this, s)).Where(w => w != null).ToList());
        if(nationalities != null) Nationalities.AddRange(nationalities.Elements().Select(s => new Nationality(this, s)).Where(w => w != null).ToList());


        FillLastDayUpdates(xelem.Element(S("LAST_DAY_UPDATED")));
      };
    }















    /// <summary>
    /// Значение <see cref="XElement"/> SECOND_NAME.
    /// </summary>
    public string SecondName { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> THIRD_NAME.
    /// </summary>
    public string ThirdName { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> FOURTH_NAME.
    /// </summary>
    public string FourthName { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> GENDER.
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> NATIONALITY2.
    /// </summary>
    public string Nationality { get; set; }

    /// <summary>
    /// Значение <see cref="XElement"/> SUBMITTED_BY.
    /// </summary>
    public string SubmittedBy { get; set; }















    /// <summary>
    /// Сведения об известных псевдонимах личности.
    /// Коллекция <see cref="XElement"/> INDIVIDUAL_ALIAS.
    /// </summary>
    public virtual List<IndividualAlias> Aliases { get; set; }

    /// <summary>
    /// Сведения об известных/предполагаемых датах рождения личности.
    /// Коллекция <see cref="XElement"/> INDIVIDUAL_DATE_OF_BIRTH.
    /// </summary>
    public virtual List<IndividualDOB> DOBs { get; set; }

    /// <summary>
    /// Сведения об известных/предполагаемых местах рождения личности.
    /// Коллекция <see cref="XElement"/> INDIVIDUAL_PLACE_OF_BIRTH.
    /// </summary>
    public virtual List<IndividualPOB> POBs { get; set; }

    /// <summary>
    /// Сведения об известных документах личности.
    /// Коллекция <see cref="XElement"/> INDIVIDUAL_DOCUMENT.
    /// </summary>
    public virtual List<IndividualDoc> Docs { get; set; }

    /// <summary>
    /// Сведения о титулах личности.
    /// Коллекция дочерних элементов VALUE в <see cref="XElement"/> TITLE.
    /// </summary>
    public virtual List<Title> Titles { get; set; }

    /// <summary>
    /// Сведения о должностях личности.
    /// Коллекция дочерних элементов VALUE в <see cref="XElement"/> DESIGNATION.
    /// </summary>
    public virtual List<Designation> Designations { get; set; }

    /// <summary>
    /// Сведения о национальной принадлежности личности.
    /// Коллекция дочерних элементов VALUE в <see cref="XElement"/> NATIONALITY.
    /// </summary>
    public virtual List<Nationality> Nationalities { get; set; }














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
