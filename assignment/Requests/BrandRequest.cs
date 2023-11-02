using System.ComponentModel.DataAnnotations;

namespace assignment.Requests
{
    public class BrandRequest
    {
        [Required] public string name { get; set; }

    }
    public class CarRequest
    {
        [Required] public string model { get; set; }
        [Required] public int Year { get; set; }
        //[Required] public string Name { get; set; }
    }

    /*public class RegisterRequest
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Username { get; set; }
    }*/
}
