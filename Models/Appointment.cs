namespace TruckingCompanyApi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public string TruckingCompanyName { get; set; } // Assuming it references TruckingCompany
    }
}
