using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TESTING_API.Models;

namespace TESTING_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly string connectionString;
        private int userType = 0 ;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IConfiguration configuration)
        {
            _logger = logger;
            connectionString = configuration["ConnectionStrings:SqlServerDb"] ?? "";
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            // Check if the username and password match the dummy user
            if (IsValidUser(model.Username, model.Password))
            {
                // Generate and return a token (dummy token)
                var token = GenerateToken(model.Username);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }
        }

        private bool IsValidUser(string username, string password)
        {
          
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM USERS";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                User user = new User();
                                user.ID = reader.GetInt32(0);
                                user.name = reader.GetString(1);
                                user.password=reader.GetString(2);
                                user.lastName = reader.GetString(3);
                                user.emailAdress = reader.GetString(4);
                                user.phone_number = reader.GetString(5);
                                user.identifier = reader.GetInt32(6);

                                if (user.name.Equals(username) && user.password.Equals(password))
                                { 
                                    userType = user.identifier;
                                    return true;
                                }
                                
                            }
                        }


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
               
            }

            return false;
        }

        private string GenerateToken(string username)
        {
            // Dummy token (for demonstration purposes)
            return userType.ToString();
        }
    }

    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

   
    



}


