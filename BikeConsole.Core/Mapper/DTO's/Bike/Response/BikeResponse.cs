namespace BikeConsole.Core.Mapper.DTO_s.Bike.Response;

public class BikeResponse
{
    public List<Domain.Bike> Values { get; set; }
    public required string ContinuationToken { get; set; }
}