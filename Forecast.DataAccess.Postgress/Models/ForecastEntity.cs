using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Forecast.DataAccess.Postgress.Models;

public class ForecastEntity
{
    [Key]
    public int Id { get; set; }

    [Column(name: "Date")]
    public string Date { get; set; } = string.Empty;

    [Column(name: "Temperature")]
    public double Temperature { get; set; } = 0;

    [Column(name: "About")]
    public string About { get; set; } = string.Empty;

    [Column(name: "Region")]
    public string Region { get; set; } = string.Empty;

    [Column(name: "Response")]
    public string Response { get; set; } = string.Empty;
    public ForecastEntity() { }
    public ForecastEntity(string Date, double Temperature, string About, string Region, string Response)
    {
        this.Date = Date;
        this.Temperature = Temperature;
        this.About = About;
        this.Region = Region;
        this.Response = Response;
    }
}
