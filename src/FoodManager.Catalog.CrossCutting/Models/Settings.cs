namespace FoodManager.Catalog.CrossCutting.Models
{
    public interface ISettings
    {
        public MongoSettings MongoSettings { get; }
        public KeycloakSettings KeycloakSettings { get; }
        public MltSettings MltSettings { get; }
    }

    public class Settings : ISettings
    {
        public required MongoSettings MongoSettings { get; set; }
        public required KeycloakSettings KeycloakSettings { get; set; }

        public required MltSettings MltSettings { get; set; }
    }
}