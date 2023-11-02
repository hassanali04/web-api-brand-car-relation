using System.ComponentModel.DataAnnotations.Schema;

namespace assignment.Responses
{
    public class BrandResponse
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string name { get; set; }
        public List<CarResponse> Cars { get; set; }
    }
    public class CarResponse
    {
     //   public string name { get; set; }
        public int Year { get; set; }
        public string model { get; set; }
    }
}
