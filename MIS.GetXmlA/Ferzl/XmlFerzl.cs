
public class XmlFerzl
{
    static string FerzlVersion = RepositorySettings.GetSection("FerzlVersion");

    // Пространства имён
    static XNamespace SOAP_ENV = "http://schemas.xmlsoap.org/soap/envelope/";
    static XNamespace ns3 = $"http://ffoms.ru/types/{FerzlVersion}/mpiPersonInfoSchema";
    static XNamespace ns6 = $"http://ffoms.ru/types/{FerzlVersion}/mpiNilSchema";
    static XNamespace ns5 = $"http://ffoms.ru/types/{FerzlVersion}/mpiPersonObjectsControlSchema";
    static XNamespace ns8 = $"http://ffoms.ru/types/{FerzlVersion}/mpiLegalRepresentationSchema";
    static XNamespace ns7 = $"http://ffoms.ru/types/{FerzlVersion}/mpiNRSchema";
    static XNamespace ns9 = $"http://ffoms.ru/types/{FerzlVersion}/mpiAttachControlSchema";
    static XNamespace ns11 = $"http://ffoms.ru/types/{FerzlVersion}/insuranceSuspendSchema";
    static XNamespace ns10 = $"http://ffoms.ru/types/{FerzlVersion}/mpiAsyncOperationsSchema";
    static XNamespace ns2 = $"http://ffoms.ru/types/{FerzlVersion}/commonTypes";
    static XNamespace ns4 = $"http://ffoms.ru/types/{FerzlVersion}/mpiPolicyApplicationSchema";

    public static PersonData ParseSoapResponse(string xml)
    {
        var doc = XDocument.Parse(xml);

        var body = doc.Root?.Element(SOAP_ENV + "Body");
        if (body == null)
        {
            //throw new InvalidOperationException("Нет SOAP-Body");
            //Console.WriteLine("Нет SOAP-Body");
        }
            

        var response = body.Element(ns3 + "getPersonDataResponse");
        if (response == null)
        {
            //throw new InvalidOperationException("Нет getPersonDataResponse");
            //Console.WriteLine("Нет getPersonDataResponse");
        }
            

        var pd = response.Element(ns3 + "pd");
        if (pd == null)
        {
            //throw new InvalidOperationException("Нет pd");
            //Console.WriteLine("Нет pd");

            return null;
        }
            

        var result = new PersonData
        {
            ExternalRequestId = response.Element(ns2 + "externalRequestId")?.Value,
            Oip = pd.Element(ns3 + "oip")?.Value
        };

        // === Полисы ===
        result.Policies = pd
            .Element(ns3 + "policy")
            ?.Elements(ns3 + "policyItems")
            .Select(pi => new Policy
            {
                Enp = pi.Element(ns2 + "enp")?.Value,
                pcyDateB = (DateOnly)ParseDate(pi.Element(ns2 + "pcyDateB")?.Value),
                pcyDateT = ParseDate(pi.Element(ns2 + "pcyDateT")?.Value),
                Type = pi.Element(ns2 + "pcyType")?.Value,
                Status = pi.Element(ns2 + "pcyStatus")?.Value,
                InsurerName = pi.Element(ns2 + "insurName")?.Value,
                InsurerOgrn = pi.Element(ns2 + "insurOgrn")?.Value,
                Category = pi.Element(ns2 + "pcyCategory")?.Value,
                Surname = pi.Element(ns2 + "surname")?.Value,
                FirstName = pi.Element(ns2 + "firstName")?.Value,
                Patronymic = pi.Element(ns2 + "patronymic")?.Value,
                BirthDay = ParseDate(pi.Element(ns2 + "birthDay")?.Value),
                Gender = pi.Element(ns2 + "gender")?.Value
            })
            .ToList() ?? new List<Policy>();

        // === Документы ===
        result.Documents = pd
            .Element(ns3 + "dudl")
            ?.Elements(ns3 + "dudlItems")
            .Select(di => new Document
            {
                Series = di.Element(ns2 + "dudlSer")?.Value,
                Number = di.Element(ns2 + "dudlNum")?.Value,
                IssueDate = ParseDate(di.Element(ns2 + "dudlDateB")?.Value),
                Type = di.Element(ns2 + "dudlType")?.Value,
                Issuer = di.Element(ns2 + "issuer")?.Value,
                BirthPlace = di.Element(ns2 + "birthplace")?.Value,
                BirthDay = ParseDate(di.Element(ns2 + "birthDay")?.Value),
                Citizenship = di.Element(ns2 + "ctznOksm")?.Value,
                Status = di.Element(ns3 + "dudlStatus")?.Value,
                Gender = di.Element(ns3 + "gender")?.Value
            })
            .ToList() ?? new List<Document>();

        // === Прикрепления ===
        result.Attachments = pd
            .Element(ns3 + "attach")
            ?.Elements(ns3 + "attachItems")
            .Select(ai => new Attachment
            {
                AreaType = ai.Element(ns2 + "areaType")?.Value,
                Method = ai.Element(ns2 + "attachMethod")?.Value,
                DateFrom = ParseDate(ai.Element(ns2 + "dateAttachB")?.Value),
                MoId = ai.Element(ns2 + "moId")?.Value,
                MoCode = ai.Element(ns2 + "moCode")?.Value,
                DepId = ai.Element(ns2 + "depId")?.Value,
                Status = ai.Element(ns2 + "attachStatus")?.Value
            })
            .ToList() ?? new List<Attachment>();

        // === СНИЛС ===
        result.SnilsList = pd
            .Element(ns3 + "snils")
            ?.Elements(ns3 + "snilsItems")
            .Select(si => new SnilsInfo
            {
                Number = si.Element(ns3 + "snils")?.Value,
                Status = si.Element(ns3 + "statusSnils")?.Value
            })
            .ToList() ?? new List<SnilsInfo>();

        return result;
    }

    public static (List<Policy> policy, List<PersonShort> persons, PaginationInfo pagination, string externalRequestId) ParseFindPersonsResponse(string xml)
    {
        try
        {
            var doc = XDocument.Parse(xml);

            var body = doc.Root?.Element(SOAP_ENV + "Body");
            if (body == null)
            {
                Console.WriteLine("Нет SOAP-Body");
                return (null, new List<PersonShort>(), null, null);
            }

            var response = body.Element(ns3 + "findPersonsByPersCriteriaResponse");
            if (response == null)
            {
                //Console.WriteLine("Нет findPersonsByPersCriteriaResponse");
                return (null, new List<PersonShort>(), null, null);
            }
            else
            {
                var count = response
                .Element(ns3 + "persons")
                ?.Elements(ns3 + "pagination")
                ?.Select((p => int.Parse(p.Element(ns2 + "count")?.Value?.Trim() ?? "0")))
                .Sum();

                if(count == 0)
                {
                    //Console.WriteLine($"Нaйдено {count}");
                    return (null, new List<PersonShort>(), null, null);
                }                
            }

                string externalRequestId = response.Element(ns2 + "externalRequestId")?.Value;

            var persons = response
                .Element(ns3 + "persons")
                ?.Elements(ns3 + "personDataShortItem")
                .Select(p => new PersonShort
                {
                    Fio = p.Element(ns3 + "fio")?.Value?.Trim(),
                    Enp = p.Element(ns3 + "enp")?.Value,
                    BirthDay = ParseDate(p.Element(ns3 + "birthDay")?.Value),
                    Gender = int.TryParse(p.Element(ns3 + "gender")?.Value, out int g) ? g : (int?)null,
                    Oip = p.Element(ns3 + "oip")?.Value
                })
                .ToList() ?? new List<PersonShort>();


            var policy = response
                .Element(ns3 + "persons")
                ?.Elements(ns3 + "personDataShortItem")
            .Select(pi => new Policy
             {
                 Enp = pi.Element(ns3 + "enp")?.Value,                 
                 Surname = pi.Element(ns3 + "fio")?.Value,                
                 BirthDay = ParseDate(pi.Element(ns3 + "birthDay")?.Value),
                 Gender = pi.Element(ns3 + "gender")?.Value
             })
            .ToList() ?? new List<Policy>();

            var paginationElem = response.Element(ns3 + "persons")?.Element(ns3 + "pagination");
            var pagination = paginationElem != null ? new PaginationInfo
            {
                PageNumber = int.TryParse(paginationElem.Element(ns2 + "pageNumber")?.Value, out int pn) ? pn : 1,
                ItemsPerPage = int.TryParse(paginationElem.Element(ns2 + "itemPerPage")?.Value, out int ipp) ? ipp : 25,
                TotalCount = int.TryParse(paginationElem.Element(ns2 + "count")?.Value, out int cnt) ? cnt : 0
            } : null;

            return (policy, persons, pagination, externalRequestId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка \"{ex.Message}\" при парсинге xml");
            return (null, new List<PersonShort>(), null, null);
        }
    }

    // Вспомогательный метод парсинга даты (с игнорированием некорректных)
    private static DateOnly? ParseDate(string? value)
    {
        if (string.IsNullOrEmpty(value) || !DateOnly.TryParse(value, out DateOnly date))
            return null;
        return date;
    }

}
