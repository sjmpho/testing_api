using System.ComponentModel.DataAnnotations;

namespace TESTING_API.Models
{
    public class Bookings_Database_model
    {
        public int Id { get; set; }
        public String UserID {get; set;}
        public String bookingDetails { get; set;}
        public string Job_ID { get; set;}
        public String Reference_number { get; set;}
        public String Vehicle_make {  get; set;}
        public String Vehicle_Model { get; set;}
        public String Production_year { get; set;}
        public String booking_type { get; set;}
    }
}
