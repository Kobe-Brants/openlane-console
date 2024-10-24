namespace BikeConsole.Core.Mapper.DTO_s.ResourceData;

public class BikeAuctionCreatedData : ResourceDataBase
{
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string Partner { get; set; }
    public string Country { get; set; }
}