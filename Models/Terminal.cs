using TruckingCompanyApi.Models;

namespace TermianlApi.Models
{
    public class Terminal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GateNo { get; set; }
        public int Slots { get; set; }
        public int Amount { get; set; }
        public ICollection<Appointment> Appointments{get;set;}=new List<Appointment>();

    }
}
