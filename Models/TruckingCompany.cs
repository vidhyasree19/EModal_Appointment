// namespace TruckingCompanyApi.Models
// {
//     public class TruckingCompany
//     {
//         public int Id { get; set; } 
//         public string TruckingCompany_Name { get; set; }
//         public string Truck_No { get; set; }
//         public string Driver_Name { get; set; }
//         public bool Chassis { get; set; }
//         public int Container_Size { get; set; }
//     }
// }
using TruckingCompanyApi.Models;

public class TruckingCompany
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public WorkType WorkType { get; set; }

    public ICollection<Truck> Trucks { get; set; } = new List<Truck>();
   public ICollection<Appointment> Appointments{get;set;}=new List<Appointment>();

}

public enum WorkType
{
    PickFull,
    PickEmpty,
    DropFull,
    DropEmpty
}
