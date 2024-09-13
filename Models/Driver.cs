public class Driver
{
    public int Id { get; set; }
    public string DriverName { get; set; }
    public int TruckingCompanyId { get; set; }

    // Navigation property (if needed)
    public TruckingCompany TruckingCompany { get; set; }
}
