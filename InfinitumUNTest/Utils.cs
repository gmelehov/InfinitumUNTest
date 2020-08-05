using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;




namespace InfinitumUNTest
{
  /// <summary>
  /// Вспомогательные методы.
  /// </summary>
  public static class Utils
  {


    /// <summary>
    /// Возвращает объект <see cref="XName"/> с локальным именем, равным указанному.
    /// </summary>
    /// <param name="name">Локальное имя XML-элемента.</param>
    /// <returns></returns>
    public static XName S(string name) => XName.Get(name, "");




    /// <summary>
    /// Возвращает содержимое указанного XML-элемента,
    /// сконвертированное в <see cref="DateTime"/>.
    /// Возвращает <see langword="null"/> в случае неудачной конвертации.
    /// </summary>
    /// <param name="xelem">XML-элемент.</param>
    /// <returns></returns>
    public static DateTime? ParseXmlDateTime(XElement xelem)
    {
      bool dateResult = DateTime.TryParse(xelem?.Value, out DateTime dateParsed);
      return dateResult ? dateParsed as DateTime? : null;
    }

    /// <summary>
    /// Возвращает значение указанного атрибута в указанном XML-элементе,
    /// сконвертированное в <see cref="DateTime"/>.
    /// Возвращает <see langword="null"/> в случае неудачной конвертации.
    /// </summary>
    /// <param name="xelem">XML-элемент.</param>
    /// <param name="attrName">Атрибут XML-элемента.</param>
    /// <returns></returns>
    public static DateTime? ParseXmlAttrDateTime(XElement xelem, string attrName)
    {
      bool dateResult = DateTime.TryParse(xelem?.Attribute(attrName)?.Value, out DateTime dateParsed);
      return dateResult ? dateParsed as DateTime? : null;
    }

    /// <summary>
    /// Возвращает содержимое указанного XML-элемента,
    /// сконвертированное в <see cref="short"/>.
    /// Возвращает <see langword="null"/> в случае неудачной конвертации.
    /// </summary>
    /// <param name="xelem">XML-элемент.</param>
    /// <returns></returns>
    public static short? ParseXmlShort(XElement xelem)
    {
      bool shortResult = short.TryParse(xelem?.Value, out short shortParsed);
      return shortResult ? shortParsed as short? : null;
    }

    /// <summary>
    /// Возвращает содержимое указанного XML-элемента,
    /// сконвертированное в <see cref="int"/>.
    /// Возвращает <see langword="null"/> в случае неудачной конвертации.
    /// </summary>
    /// <param name="xelem">XML-элемент.</param>
    /// <returns></returns>
    public static int? ParseXmlInt(XElement xelem)
    {
      bool intResult = int.TryParse(xelem?.Value, out int intParsed);
      return intResult ? intParsed as int? : null;
    }

  }
}
