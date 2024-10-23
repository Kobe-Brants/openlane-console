namespace BikeConsole.Domain;

public class BikeContainer
{
    public required string ContainerId { get; set; }
    public required List<Bike> Bikes { get; set; }
}