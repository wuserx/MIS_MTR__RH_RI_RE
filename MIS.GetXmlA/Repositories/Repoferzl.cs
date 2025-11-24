using Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;

public class Repoferzl
{
    private static readonly ConcurrentDictionary<string, (string Token, DateTimeOffset ExpiresAt)> _tokenCache = new();

    public string Surname { get; }
    public string FirstName { get; }
    public string? Patronymic { get; }
    public DateTime? BirthDay { get; }
    public DateTime? DtFrom { get; }
    public DateTime? DtTo { get; }
    public string? snils { get; }
    public string? dudlNum { get; }
    public string? dudlSer { get; }    
    public string? TerrOkato { get; }
    public string? dudlType { get;  }

    public Repoferzl(ZakSluch zakSluch)
    {
        Surname = zakSluch.FAM;
        FirstName = zakSluch.IM;
        Patronymic = zakSluch.OT;
        BirthDay = zakSluch.DR;
        DtFrom = zakSluch.DATE_Z_1;
        DtTo = zakSluch.DATE_Z_2;
        snils = zakSluch.SNILS;
        dudlNum = zakSluch.DOCNUM;
        dudlSer = zakSluch.DOCSER;
        TerrOkato = "83000";
        dudlType = zakSluch.DOCTYPE;
    }
    
    public Repoferzl(ZakSluch_mtr zakSluch)
    {
        Surname = zakSluch.FAM;
        FirstName = zakSluch.IM;
        Patronymic = zakSluch.OT;
        BirthDay = zakSluch.DR;
        DtFrom = zakSluch.DATE_Z_1;
        DtTo = zakSluch.DATE_Z_2;
        snils = zakSluch.SNILS;
        dudlNum = zakSluch.DOCNUM;
        dudlSer = zakSluch.DOCSER;
        TerrOkato = zakSluch.ST_OKATO;
        dudlType = zakSluch.DOCTYPE;
    }
    

    public async Task<List<Policy>> GetPolicy()
    {
        //var result = await GetDataFerzlByFIODR();

        var result = await FindPersonsByPersCriteriaFIODR();

        if(result == null)
            return new List<Policy>();


        return GetXmlByDataFerzl(result);
    }

    private async Task<string> FindPersonsByPersCriteriaFIODR()
    {
        // 1. Валидация входных данных (опционально, но рекомендуется)
        if (string.IsNullOrWhiteSpace(Surname))
            throw new ArgumentException("Фамилия обязательна.", nameof(Surname));
        if (string.IsNullOrWhiteSpace(FirstName))
            throw new ArgumentException("Имя обязательно.", nameof(FirstName));
        //if (string.IsNullOrWhiteSpace(BirthDay))
        //    throw new ArgumentException("Дата рождения c обязательна. (не более года)", nameof(DtFrom));
        //if (string.IsNullOrWhiteSpace(BirthDay))
        //    throw new ArgumentException("Дата рождения по обязательна. (не более года)", nameof(DtTo));

        // 2. Создаём JSON-тело без лишних полей (если patronymic null → не включаем)
        var requestPayload = new
        {
            Surname,
            FirstName,
            Patronymic,
            TerrOkato
        };

        //var requestPayload = new
        //{
        //    Surname,
        //    FirstName,
        //    Patronymic, // может быть null — JsonConvert удалит из JSON, если установлено IgnoreNullValues
        //    birthDayFrom = new DateOnly(DtFrom.Value.Year, 1, 1).ToString("yyyy-MM-dd"),
        //    birthDayTo = new DateOnly(DtTo.Value.Year, 12, 31).ToString("yyyy-MM-dd"),
        //    TerrOkato
        //};

        var jsonBody = JsonConvert.SerializeObject(requestPayload, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore // Исключаем null-поля из JSON
        });

        var response = await PostAsync(jsonBody, "UrlfindPersonsByPersCriteriaFiodr");

        // 4. Проверка статуса
        if (!response.IsSuccessStatusCode)
        {
            var message = $"HTTP ошибка: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync().ConfigureAwait(false)}";
            throw new HttpRequestException(message);
        }

        // 5. Чтение и возврат содержимого
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    private async Task<string> GetDataFerzlByFIODR()
    {
        // 1. Валидация входных данных (опционально, но рекомендуется)
        if (string.IsNullOrWhiteSpace(Surname))
            throw new ArgumentException("Фамилия обязательна.", nameof(Surname));
        if (string.IsNullOrWhiteSpace(FirstName))
            throw new ArgumentException("Имя обязательно.", nameof(FirstName));
        //if (string.IsNullOrWhiteSpace(BirthDay))
        //    throw new ArgumentException("Дата рождения обязательна.", nameof(BirthDay));

        // 2. Создаём JSON-тело без лишних полей (если patronymic null → не включаем)
        var requestPayload = new
        {
            Surname,
            FirstName,
            Patronymic, // может быть null — JsonConvert удалит из JSON, если установлено IgnoreNullValues
            BirthDay = BirthDay.Value.ToString("yyyy-MM-dd"),
            DtFrom = new DateOnly(DtFrom.Value.Year, 1, 1).ToString("yyyy-MM-dd"),//начало периода, за который выдаются изменения
            DtTo = new DateOnly(DtTo.Value.Year, 12, 31).ToString("yyyy-MM-dd"),//конец периода, за который выдаются изменения
             
        };

        var jsonBody = JsonConvert.SerializeObject(requestPayload, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore // Исключаем null-поля из JSON
        });

        var response = await PostAsync(jsonBody, "UrlFIODR");

        // 4. Проверка статуса
        if (!response.IsSuccessStatusCode)
        {
            var message = $"HTTP ошибка: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync().ConfigureAwait(false)}";
            throw new HttpRequestException(message);
        }

        // 5. Чтение и возврат содержимого
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    private List<Policy> GetXmlByDataFerzl(string responseXml)
    {
        try
        {
            PersonData person = XmlFerzl.ParseSoapResponse(responseXml);  

            if(person == null)
                return new List<Policy>();

            //// Примеры фильтрации:

            // Актуальные полисы (до сегодня)
            //var activePolicies = person.Policies
            //    .Where(p => p.pcyDateB >= DateOnly.FromDateTime(DateTime.Today))
            //    .ToList();

            // Полисы с ENP
            var enpPolicies = person.Policies
                .Where(p => !string.IsNullOrEmpty(p.Enp))
                //.Select(p => (ENP: p.Enp, PcyDateB: p.pcyDateB) )
                .ToList();

            //// Прикрепления к конкретной МО
            //var moAttachments = person.Attachments
            //    .Where(a => a.MoCode == "4534534")
            //    .ToList();

            //// Вывести ФИО
            //var fio = person.Policies
            //    .Select(p => $"{p.Surname} {p.FirstName} {p.Patronymic}")
            //    .FirstOrDefault();       

            return enpPolicies;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return new List<Policy>();
        }
    }

      

    private async Task<HttpResponseMessage> PostAsync(string body, string Url)
    {
        try
        {
            // определяем коллекцию сервисов
            var services = new ServiceCollection();
            // добавляем сервисы, связанные с HttpClient, в том числе IHttpClientFactory
            services.AddHttpClient();
            // создаем провайдер сервисов
            var serviceProvider = services.BuildServiceProvider();
            // получаем сервис IHttpClientFactory
            IHttpClientFactory httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            var buffer = System.Text.Encoding.UTF8.GetBytes(body);

            var Content = new ByteArrayContent(buffer);

            Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClient httpClient = httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetAccessToken(httpClient));

       
            return await httpClient.PostAsync(RepositorySettings.GetSection(Url), Content);
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent($"{ex.Message}")
            });
        }
    }

    private async Task<string?> GetAccessToken(HttpClient httpClient)
    {
        const string cacheKey = "FERZL_TOKEN";

        var cached = _tokenCache.GetOrAdd(cacheKey, _ => (Token: string.Empty, ExpiresAt: DateTimeOffset.MinValue));

        if (cached.ExpiresAt > DateTimeOffset.Now)
            return cached.Token;

        string url = RepositorySettings.GetSection("UrlHash");

        if (url.IsNullOrEmpty())
            throw new Exception("Url для запроса hash ferzl пустой см. settings");

        var httpAccessToken = await httpClient.GetAsync(url);

        _tokenCache[cacheKey] = (await httpAccessToken.Content.ReadAsStringAsync().ConfigureAwait(false), DateTimeOffset.Now.AddMinutes(55)); // обновлять за 5 мин до истечения

        return await httpAccessToken.Content.ReadAsStringAsync();
    }

}

