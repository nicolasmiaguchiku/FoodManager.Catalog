using System.Text.Json.Serialization;

namespace FoodManager.Catalog.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Category
    {
        None,            //nenhum
        Promotion,       //promoção
        BestSellers,     //mais vendidos
        WellRated,       //bem avaliados   
        SideDishes,      //acompanhamentos
        Combos,          //combos
        Classics,        //clássicos
        Recommended,     //recomendados
        News,            //novidades
        GlutenFree       //sem glúten
    }
}