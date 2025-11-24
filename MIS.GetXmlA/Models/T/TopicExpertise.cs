[Table("Topic", Schema = "Reestr")]
public class TopicExpertise
{
    public int Id { get; set; }
    public int Num { get; set; } // Номер проверки

    public int Month { get; set; } //Основной период для целевой проверки
    public int Year { get; set; }

    public Percents Percents { get; set; }

    //МЭЭ
    public string Execution1 { get; set; } // 1.МКБ
    public string Execution2 { get; set; } // 2.Стоимость лечения c
    public string Execution2_2 { get; set; } // 2.Стоимость лечения по 
    public string Execution3 { get; set; } // 3. Длительность лечения с
    public string Execution3_2 { get; set; } // 3. Длительность лечения по
    public string Execution4 { get; set; } // 4.Возраст с
    public string Execution4_2 { get; set; } // 4.Возраст по 
    public string Execution5 { get; set; } // 5. Профиль
    public string Execution6 { get; set; } // 6.Sql

    //ЭКМП
    public string Execution21 { get; set; } // 1.МКБ
    public string Execution22 { get; set; } // 2.Стоимость лечения с
    public string Execution22_2 { get; set; } // 2.Стоимость лечения по
    public string Execution23 { get; set; } // 3. Длительность лечения с
    public string Execution23_2 { get; set; } // 3. Длительность лечения по
    public string Execution24 { get; set; } // 4.Возраст с
    public string Execution24_2 { get; set; } // 4.Возраст по
    public string Execution25 { get; set; } // 5. Профиль
    public string Execution26 { get; set; } // 6.Sql
}