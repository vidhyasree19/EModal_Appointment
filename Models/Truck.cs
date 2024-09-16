using TruckingCompanyApi.Models;

public class Truck
{
    public int Id { get; set; }
    public string TruckNo { get; set; }
    public bool IsChasis { get; set; }
    public int TruckingCompanyId { get; set; }
    public string DriverName { get; set; }

    // Navigation property (if needed)
   public TruckingCompany TruckingCompany { get; set; }
   // public ICollection<Appointment> Appointments{get;set;}=new List<Appointment>();

}
