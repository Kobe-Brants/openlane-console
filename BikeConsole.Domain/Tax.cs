namespace BikeConsole.Domain;

public class Tax
{
    public required string TaxId { get; set; }
    public required string Type { get; set; }
    public double Percentage { get; set; }
    
    public required Bike Bike { get; set; }
}