using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TESTING_API.Models;

namespace TESTING_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly string connectionString;

        public RegisterController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:SqlServerDb"] ?? "";
        }
        [HttpPost]
        public IActionResult CreateUser(loginModel model)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                        connection.Open();
                    string sql = "INSERT INTO USERS (name, password,emailAdress,lastName,phone_number,identifier) VALUES (@name, @password,@emailAdress,@lastName,@phone_number,@identifier)";
                    using (var command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@name", model.name);
                      command.Parameters.AddWithValue("@password", model.password);
                        command.Parameters.AddWithValue("@emailAdress",model.emailAdress);
                        command.Parameters.AddWithValue("lastName",model.lastName);
                        command.Parameters.AddWithValue("phone_number",model.phone_number);
                        command.Parameters.AddWithValue("identifier",model.identifier);

                        command.ExecuteNonQuery();
                    }
                   return Ok();
                }
            }catch (Exception ex) { 
            Console.WriteLine(ex);
            
            ModelState.AddModelError("loginModel","Error alert");
            return BadRequest(ModelState);

            }

           
        }
       

    }
}
