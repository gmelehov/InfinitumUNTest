using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

using InfinitumUNTest.Models;
using InfinitumUNTest.WebApi.Data;
using InfinitumUNTest.WebApi.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;





namespace InfinitumUNTest.WebApi.Controllers
{
  /// <summary>
  /// Контроллер.
  /// </summary>
  [Route("api")]
  public class InfController : Controller
  {
    private readonly InfTestContext ctx;
    private readonly IInfLoggerService logger;




    public InfController(InfTestContext context, IInfLoggerService loggerService)
    {
      ctx = context;
      logger = loggerService;
    }







    /// <summary>
    /// POST-запрос.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("collectData")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult OnPostData() => GetDataResult();



    /// <summary>
    /// GET-запрос.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("collectData")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult OnGetData() => GetDataResult();









    /// <summary>
    /// Реализует необходимую логику обработки полученных XML-данных с перечнем.
    /// </summary>
    /// <returns></returns>
    private IActionResult GetDataResult()
    {
      // Получаем исходный XML-документ
      XDocument xdoc = GetXmlData(@"https://scsanctions.un.org/resources/xml/ru/consolidated.xml");

      // Если XML-документ содержит корректные данные (правильно распарсен)...
      if (xdoc != null)
      {
        // Сохраняем результат проверки состояния перечня
        bool? check = ctx.HasThisGenerationDate(xdoc);

        // Если проверка вернула null
        if (!check.HasValue)
        {
          // Записываем в лог сообщение об ошибке
          var entry = logger.LogError("В полученном перечне отсутствуют сведения о его состоянии. Данные не сохранены.");
          logger.Log(ctx, entry);

          // Возвращаем ответ с кодом 400 и записью об ошибке
          // НОВЫЕ ДАННЫЕ В БД НЕ СОХРАНЯЮТСЯ.
          return BadRequest(entry);
        }
        // ... иначе, если в XML-документе присутствует необходимый атрибут dateGenerated...
        else
        {
          // Если в БД отсутствует перечень с полученным состоянием...
          if (check.Value == false)
          {
            // Пытаемся создать новый экземпляр перечня и сохранить его в БД
            try
            {
              // Генерируем инфо-сообщение о получении нового состояния
              logger.Log(ctx, logger.LogInfo("Получено новое состояние перечня."));

              // Создаем экземпляр перечня и сохраняем его в БД
              var clist = new ConsolidatedList(xdoc);
              ctx.ConsolidatedLists.Add(clist);
              ctx.SaveChanges();

              // Генерируем еще одно инфо-сообщение об удачном сохранении нового перечня
              var entry = logger.LogInfo("Новое состояние перечня сохранено.");
              logger.Log(ctx, entry);

              // Возвращаем ответ с кодом 200 и сообщением об удачном сохранении нового перечня
              // НОВЫЕ ДАННЫЕ СОХРАНЕНЫ В БД.
              return Ok(entry);
            }
            // Обрабатываем возможные ошибки (исключения)
            catch (Exception ex)
            {
              // Записываем в лог сообщение об ошибке
              var entry = logger.LogError(ex.Message);
              logger.Log(ctx, entry);

              // Возвращаем ответ с кодом 500 и записью об ошибке
              // НОВЫЕ ДАННЫЕ В БД СОХРАНИТЬ НЕ УДАЛОСЬ.
              return new StatusCodeResult(500);
            };
          }
          // ... иначе, если в БД уже присутствует перечень с полученным состоянием...
          else
          {
            // Записываем в лог сообщение с предупреждением
            var entry = logger.LogWarning("Полученное состояние перечня уже имеется в базе данных.");
            logger.Log(ctx, entry);

            // Возвращаем ответ с кодом 400 и записью-предупреждением
            // ПОЛУЧЕННЫЕ ДАННЫЕ НЕ СОХРАНЯЮТСЯ В БД.
            return BadRequest(entry);
          };
        };
      }
      // ... иначе, если GET-запрос к источнику XML-данных вернул некорректные XML-данные,
      // которые не могут быть представлены в виде корректного XML-документа...
      else
      {
        // Записываем в лог сообщение об ошибке
        var entry = logger.LogError("Запрос вернул некорректный XML-документ. Данные не сохранены.");
        logger.Log(ctx, entry);

        // Возвращаем ответ с кодом 400 и записью об ошибке
        // ПОЛУЧЕННЫЕ ДАННЫЕ НЕ СОХРАНЯЮТСЯ В БД.
        return BadRequest(entry);
      };
    }




    /// <summary>
    /// Выполняет GET-запрос к источнику XML-данных, читает и парсит ответ.
    /// Возвращает результат парсинга в виде объекта <see cref="XDocument"/>.
    /// </summary>
    /// <param name="endpoint">Ссылка на получение XML-документа с актуальным перечнем.</param>
    /// <returns></returns>
    private XDocument GetXmlData(string endpoint)
    {
      using (var cli = new HttpClient())
      {
        var resp = cli.GetAsync($"{endpoint}");
        var res = resp.GetAwaiter().GetResult();
        bool isGZip = res.Content.Headers.ContentEncoding.Select(s => s.ToLower()).Contains("gzip");
        Task<string> result;
        if (isGZip)
        {
          using (var ressgzip = new GZipStream(res.Content.ReadAsStreamAsync().Result, CompressionMode.Decompress))
          {
            using (var sr = new StreamReader(ressgzip.BaseStream))
            {
              result = sr.ReadToEndAsync();
            };
          };
        }
        else
        {
          result = res.Content.ReadAsStringAsync();
        };

        XDocument xdocresult = XDocument.Parse(result.Result);

        return xdocresult;
      };
    }
    
  }
}
