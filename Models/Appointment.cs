using TermianlApi.Models;

namespace TruckingCompanyApi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
       
        public string ContainerNumber { get; set; } 
        
        public Terminal Terminal { get; set; }       
        public TruckingCompany TruckingCompany { get; set; }  
        // public Truck Truck { get; set; }
        public string TicketNumber { get; set; }    
    }
}
