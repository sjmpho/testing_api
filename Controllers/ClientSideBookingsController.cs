using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TESTING_API.Models;

namespace TESTING_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientSideBookingsController : ControllerBase
    {
        private readonly string connectionString;
        public ClientSideBookingsController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:SqlServerDb"] ?? "";
        }
        [HttpPost(Name = "PostBooking")]
        public IActionResult postABooking(booking model)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO BOOKINGS (UserID,bookingDetails,Reference_number,Vehicle_make,Vehicle_Model,Production_year,booking_type ) VALUES (@UserID, @bookingDetails,@Reference_number,@Vehicle_make,@Vehicle_Model,@Production_year,@booking_type)";
                    using (var command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@bookingDetails", model.bookingDetails);
                        command.Parameters.AddWithValue("@Reference_number", model.Reference_number);
                        command.Parameters.AddWithValue("@Vehicle_make", model.Vehicle_make);
                        command.Parameters.AddWithValue("@Vehicle_Model", model.Vehicle_Model);
                        command.Parameters.AddWithValue("@Production_year",model.Production_year);
                        command.Parameters.AddWithValue("@booking_type", model.booking_type);


                        command.ExecuteNonQuery();
                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                ModelState.AddModelError("loginModel", "Error alert");
                return BadRequest(ModelState);

            }

        }




        [HttpGet]

        public IActionResult GetBookings() {

            List<Bookings_Database_model> bookings = new List<Bookings_Database_model>();

            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM BOOKINGS";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Bookings_Database_model booking = new Bookings_Database_model();
                               
                                booking.Id = reader.GetInt32(0);
                                booking.UserID = reader.GetString(1);
                                booking.bookingDetails = reader.GetString(2);

                                bookings.Add(booking);
                            }
                        }


                        command.ExecuteNonQuery();
                    }
                 }



                }catch (Exception ex)
            {
                ModelState.AddModelError("booking", "Error alert");
                return BadRequest(ModelState);
            }



            return Ok(bookings);
        }
}
}
