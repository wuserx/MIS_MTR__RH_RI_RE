    //Таблица А.35 F014 Классификатор причин отказа в оплате медицинской помощи
    [Table("OplOtk", Schema = "Spravoch")]
    public class OplOtk
    {
        public int Id { get; set; }

        public string? HASH { get; set; }
        public string? version { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? publishDate { get; set; }
        public bool? IsUpdating { get; set; }
        public string? HttpResponse { get; set; }

        public int? Kod { get; set; }

        public int? IDVID { get; set; }

        public string? Naim { get; set; }

        [StringLength(20)]
        public string? Osn { get; set; }

        public string? Komment { get; set; }

        [StringLength(20)]
        public string? KodPG { get; set; }
        public decimal? KoefNO { get; set; }

        public decimal? KoefSS { get; set; }

        public DateTime? DATEBEG { get; set; }

        public DateTime? DATEEND { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }