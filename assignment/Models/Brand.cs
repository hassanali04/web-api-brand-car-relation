using System.ComponentModel.DataAnnotations;

namespace assignment.Models
{
    public class Brand
    {
        public int Id { get; set; }
      
        public string  name { get; set; }
       // public ICollection<Car> Cars { get; set; } = new List<Car>();
        public List<Car> Cars { get; set; }
    }
    public class Car
    {
        //public string name { get; set; } 
      //  public int Id { get; set; }      
        public string model { get; set; }      
        public int Year { get; set; }
        public int BrandId { get; set; }
        public LoginRequest loginRequest { get; set; }
      //  public Brand brand { get; set; }
    }
}
