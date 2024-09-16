using TermianlApi.Models;

namespace TruckingCompanyApi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ContainerNumber { get; set; } 
       // public Terminal Terminal { get; set; }   // Assuming Terminal is a class from TermianlApi.Models
        public int TerminalId { get; set; } // Foreign key for Terminal
        //public TruckingCompany TruckingCompany { get; set; }
        public int TruckingCompanyId { get; set; }  // Foreign key for TruckingCompany
        //public Truck Truck { get; set; }
        public int TruckId { get; set; }  // Foreign key for Truck
        public string TicketNumber { get; set; }    
        public WorkType WorkType { get; set; }  // Enum for WorkType
    }

    public enum WorkType
    {
        PickFull,
        PickEmpty,
        DropFull,
        DropEmpty
    }
}
