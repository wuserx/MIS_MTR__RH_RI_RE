using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Repoferzl3
{
    private static readonly ConcurrentDictionary<string, (string Token, DateTimeOffset ExpiresAt)> _tokenCache = new();
        
    public string Surname { get; }
    public string FirstName { get; }
    public string? Patronymic { get; }
    public DateTime? BirthDay { get; }
    public DateTime? DtFrom { get; }
    public DateTime? DtTo { get; }
    public string? Snils { get; }
    public string? DudlNum { get; }
    public string? DudlSer { get; }
    public string? TerrOkato { get; }
    public string? DudlType { get; }
        
    

    // Конструкторы для инициализации данных (через ZakSluch)
    public Repoferzl3(ZakSluch zakSluch)
    {
        Surname = zakSluch.FAM;
        FirstName = zakSluch.IM;
        Patronymic = zakSluch.OT;
        BirthDay = zakSluch.DR;
        DtFrom = zakSluch.DATE_Z_1;
        DtTo = zakSluch.DATE_Z_2;
        Snils = zakSluch.SNILS;
        DudlNum = zakSluch.DOCNUM;
        DudlSer = zakSluch.DOCSER;
        TerrOkato = "83000";
        DudlType = zakSluch.DOCTYPE;
    }

    public Repoferzl3(ZakSluch_mtr zakSluch)
    {
        Surname = zakSluch.FAM;
        FirstName = zakSluch.IM;
        Patronymic = zakSluch.OT;
        BirthDay = zakSluch.DR;
        DtFrom = zakSluch.DATE_Z_1;
        DtTo = zakSluch.DATE_Z_2;
        Snils = zakSluch.SNILS;
        DudlNum = zakSluch.DOCNUM;
        DudlSer = zakSluch.DOCSER;
        TerrOkato = zakSluch.ST_OKATO;
        DudlType = zakSluch.DOCTYPE;
    }

    // Основной метод: найти лица → получить полный профиль → вернуть полисы с ENP
    public async Task<List<Policy>> GetPolicy()
    {
        try
        {
            // Шаг 0: Поиск лиц по ФИО+ДР+ПЕРИОД
            var result = await GetDataFerzlByFioDr();

            var profiles = GetPoliciesFromFullResponse(result);

            Console.Write(profiles.Any() ? "+" : "x");

            if (profiles.Count() > 0)
                return profiles;


            var searchScopes = new[]
            {
                PayloadWithFIOOkato(),      // Полные данные
                PayloadWithFIOkatoDost(), // Без отчества
                PayloadWithDudulDost(),   // Без региона
                PayloadWithFIOkatoDost13()
            };

            var result_find = default((List<Policy> policies, List<PersonShort> persons, PaginationInfo pagination, string requestId));

            foreach (var searchFunc in searchScopes)
            {
                result_find = await FindPersonsByPersCriteriaFIODR(searchFunc);
                if (result_find.policies?.Count > 0)
                    break; // Успешно нашли — выходим
            }


            var policies = result_find.policies ?? new List<Policy>();

            Console.Write(policies.Count > 0
                ? "*"
                : "x");

            if (policies.Count == 0)
            {
                Console.WriteLine("0");
                return new List<Policy>();
            }

            return policies;


            ////// далее надо продумать, нужен ли нам полный профиль
            ////// в общем енп достаточно, поэтому код далее
            ////// на консервацию

            ////// Берём первое лицо (обычно достаточно)
            ////// если будет массив надо пробежаться
            ////// и определить данные искомого пациента
            ////// пока так
            ////var person = persons[0];

            ////// Шаг 2: Получить полный профиль по OIP
            ////if (string.IsNullOrEmpty(person.Oip))
            ////{
            ////    Console.WriteLine("OIP отсутствует.");
            ////    return new List<Policy>();
            ////}

            ////var fullXml = await GetDataFerzlByOip(person.Oip);

            ////return XmlFerzl.ParseFindPersonsResponse(fullXml).policy;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в GetPolicy: {ex.Message}");
            return new List<Policy>();
        }
    }


    // === Метод 0: Получение полного профиля
    private async Task<string> GetDataFerzlByFioDr()
    {
        var payload = new
        {
            Surname,
            FirstName,
            Patronymic, // может быть null — JsonConvert удалит из JSON, если установлено IgnoreNullValues
            BirthDay = BirthDay.Value.ToString("yyyy-MM-dd"),
            DtFrom = new DateOnly(DtFrom.Value.Year, 1, 1).ToString("yyyy-MM-dd"),//начало периода, за который выдаются изменения
            DtTo = new DateOnly(DtTo.Value.Year, 12, 31).ToString("yyyy-MM-dd"),//конец периода, за который выдаются изменения

        };

        var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        var response = await PostAsync(json, "UrlFIODR");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Ошибка загрузки GetDataFerzlByFioDr: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");

        return await response.Content.ReadAsStringAsync();
    }

    private string PayloadWithFIOOkato()
    {
        var payload = new
        {
            Surname,
            FirstName,
            Patronymic,
            TerrOkato
        };

        var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        return json;
    }

    private string PayloadWithFIOkatoDost()
    {
        var payload = new
        {
            Surname = Surname[3],
            FirstName = FirstName[3],
            TerrOkato,
            dost = 1
        };

        var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        return json;
    }

    private string PayloadWithFIOkatoDost13()
    {
        var payload = new
        {
            Surname = Surname,
            TerrOkato,
            dost = 13
        };

        var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        return json;
    }

    private string PayloadWithDudulDost()
    {
        var payload = new
        {
            DudlType,
            DudlNum,
            dost = 123
        };

        var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        return json;
    }

    // === Метод 1: Поиск лиц по ФИО и региону ===
    private async Task<(List<Policy> policy, List<PersonShort> persons, PaginationInfo pagination, string requestId)> FindPersonsByPersCriteriaFIODR(string pay_load_json)
    {
        ValidateRequiredFields();                

        var response = await PostAsync(pay_load_json, "UrlFindPersonsByPersCriteriaFiodr");

        if (response.StatusCode != HttpStatusCode.OK)
        {
            Console.WriteLine($"Ошибка поиска: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
            return (null, new List<PersonShort>(), null, null);
        }

        var xml = await response.Content.ReadAsStringAsync();

        return XmlFerzl.ParseFindPersonsResponse(xml);
    }

    // === Метод 2: Получение списка коротких данных по OIP ===
    private async Task<string> GetDataFerzlByOip(string oip)
    {
        var payload = new 
            {
                Surname,
                FirstName,
                Patronymic,
                Oip = oip,
                TerrOkato
            };

        var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        var response = await PostAsync(json, "UrlGetDataFerzlByOip");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Ошибка загрузки профиля: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");

        return await response.Content.ReadAsStringAsync();
    }

    // === Обработка полного XML-ответа → извлечение полисов с ENP ===
    private List<Policy> GetPoliciesFromFullResponse(string xml)
    {
        try
        {
            var personData = XmlFerzl.ParseSoapResponse(xml);
            return personData?.Policies?
                .Where(p => !string.IsNullOrEmpty(p.Enp))
                .ToList() ?? new List<Policy>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка парсинга полного профиля: {ex.Message}");
            return new List<Policy>();
        }
    }

    // === Валидация обязательных полей ===
    private void ValidateRequiredFields()
    {
        if (string.IsNullOrWhiteSpace(Surname)) throw new ArgumentException("Фамилия обязательна.");
        if (string.IsNullOrWhiteSpace(FirstName)) throw new ArgumentException("Имя обязательно.");
        // При необходимости: проверить диапазон дат
    }

    // === Универсальный POST-запрос ===
    private async Task<HttpResponseMessage> PostAsync(string jsonBody, string Url)
    {
        // определяем коллекцию сервисов
        var services = new ServiceCollection();
        // добавляем сервисы, связанные с HttpClient, в том числе IHttpClientFactory
        services.AddHttpClient();
        // создаем провайдер сервисов
        var serviceProvider = services.BuildServiceProvider();
        // получаем сервис IHttpClientFactory
        IHttpClientFactory httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

        var client = httpClientFactory.CreateClient();
        var url = RepositorySettings.GetSection(Url);

        if (string.IsNullOrEmpty(url))
            throw new InvalidOperationException($"URL '{Url}' не найден в конфигурации.");

        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAccessToken(client).GetAwaiter().GetResult());

        return await client.PostAsync(url, content);
    }

    // === Получение токена (с кэшированием) ===
    private async Task<string> GetAccessToken(HttpClient client)
    {
        const string CacheKey = "FERZL_TOKEN";
        if (_tokenCache.TryGetValue(CacheKey, out var entry) && entry.ExpiresAt > DateTimeOffset.Now)
            return entry.Token;

        var tokenUrl = RepositorySettings.GetSection("UrlHash");
        if (string.IsNullOrEmpty(tokenUrl))
            throw new InvalidOperationException("UrlHash не задан в конфигурации.");

        var response = await client.GetAsync(tokenUrl);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Не удалось получить токен: {response.StatusCode}");

        var token = await response.Content.ReadAsStringAsync();
        var expiresAt = DateTimeOffset.Now.AddMinutes(55);

        _tokenCache.AddOrUpdate(CacheKey, (token, expiresAt), (_, _) => (token, expiresAt));

        return token;
    }
}
