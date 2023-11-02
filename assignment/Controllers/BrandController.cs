using assignment.Context;
using assignment.Models;
using assignment.Requests;
using assignment.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{


    public CarDBContext _dbContext;

    public BrandController(CarDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    

    //public List<Brand> GetBrands()
    //{
    //    return _dbContext.Brands
    //        .Select(brand => new Brand
    //        {
    //            Id = brand.Id,
    //            name = brand.name,
    //        })
    //        .ToList();
    //}


    //[HttpGet("{id}")]
    //public ActionResult<Brand> GetBrand(int id)
    //{
    //    var brand = _dbContext.Brands
    //        .FirstOrDefault(b => b.Id == id);

    //    if (brand == null)
    //        return NotFound("Brand Not Found.");

    //    return brand;
    //}
    //[HttpGet]
    //public async Task<ActionResult<Car>> Getcars()
    //{
    //    var data = await _dbContext.Cars.ToListAsync();
    //    return Ok(data);
    //}
    //[HttpPost]
    //public async Task<ActionResult<Car>> newcarentity(Car car)
    //{
    //    await _dbContext.Cars.AddAsync(car);
    //    // await _dbContext.Cars.SaveChangesAsync();
    //    return Ok(car);
    //}
    //1
    [HttpGet]
    public List<BrandResponse> GetBrands()
    {
        return _dbContext.Brands.Include(b=> b.Cars)
            .Select(brand => new BrandResponse
            {                                               
                 Id = brand.Id,
                name = brand.name,
                Cars = brand.Cars.Select(c => new CarResponse { model = c.model, Year = c.Year }).ToList(),
            })
            .ToList();
    }
    //2
    [HttpGet("{id}")]
    public ActionResult<BrandResponse> GetBrand(int id)
    {
        var Brand = _dbContext.Brands//.Include(b => b.Cars)
            .Select(b => new BrandResponse
            {
                Id = b.Id,
                name = b.name,
              //  Cars= b.Cars.Select(c => new CarResponse { model = c.model, Year = c.Year}).ToList(),
            })
            .FirstOrDefault(m => m.Id == id);
        if (Brand == null)
            return NotFound("Brand Not Found.");
        return Brand;

    }
    //3
    [HttpPost]
    public ActionResult<BrandResponse> CreateDetail(BrandRequest brandRequest)
    {
        var Brand = new Brand
        {
            name = brandRequest.name,
        };
        _dbContext.Brands.Add(Brand);
        _dbContext.SaveChanges();

        var result = new BrandResponse
        {

            name = brandRequest.name,
        };
        return Created(result.Id.ToString(), result);
    }
    //4
    [HttpPut("{id}")]
    public ActionResult<bool> UpdateBrand(int id, BrandRequest request)
    {
        var Brand = _dbContext.Brands
            .FirstOrDefault(m => m.Id == id);
        if (Brand == null)
            return NotFound("Brand Not Found.");

        Brand.name = request.name;

        _dbContext.Update(Brand);
        _dbContext.SaveChanges();
        return true;

    }
    //5
    [HttpDelete("{id}")]
    public ActionResult<bool> DeleteBrand(int id)
    {
        var Brand = _dbContext.Brands
            .FirstOrDefault(m => m.Id == id);
        if (Brand == null)
            return NotFound("Brand Not Found.");
        _dbContext.Remove(Brand);
        _dbContext.SaveChanges();
        return true;

    }
    [HttpPost("SaveUserData/{brandId}")]
    public ActionResult AddCarToBrand(int brandId,CarRequest request)
    {
        var brand = _dbContext.Brands.Where(b => b.Id == brandId).Include(b => b.Cars).First();

        if (brand == null)
        {
            return NotFound("Car brand not found.");
        }

        brand.Cars.Add(new Car
        {
            model = request.model,
            Year = request.Year,
            //name = request.Name
        });

        var result = new CarResponse
        {
           
            Year = request.Year,
            model = request.model,
        };
        _dbContext.Update(brand);
        _dbContext.SaveChanges();
        return Created(result.model.ToString(), result);
    }
}



    //private readonly CarDBContext _dbContext;

    //public BrandController(CarDBContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    //[HttpGet]
    //public List<Brand> GetBrands()
    //{
    //    return _dbContext.Brands
    //        .Select(brand => new Brand
    //        {
    //            Id = brand.Id,
    //            name = brand.name,
    //        })
    //        .ToList();
    //}

    //[HttpGet("{id}")]
    //public ActionResult<Brand> GetBrand(int id)
    //{
    //    var brand = _dbContext.Brands
    //        .FirstOrDefault(b => b.Id == id);

    //    if (brand == null)
    //        return NotFound("Brand Not Found.");

    //    return brand;

    //public class BrandController : ControllerBase
    //{

    //    private readonly CarDBContext _context;

    //    public BrandController(CarDBContext context)
    //    {
    //        _context = context;
    //    }

    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
    //    {
    //        return await _context.Brands.ToListAsync();
    //    }

    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Brand>> GetBrand(int id)
    //    {
    //        var brand = await _context.Brands.FindAsync(id);

    //        if (brand == null)
    //        {
    //            return NotFound();
    //        }

    //        return brand;
    //    }

    //    [HttpPost]
    //    public async Task<ActionResult<Brand>> PostBrand(Brand brand)
    //    {
    //        _context.Brands.Add(brand);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction(nameof(GetBrand), new { id = brand.BrandId }, brand);
    //    }

    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutBrand(int id, Brand brand)
    //    {
    //        if (id != Brand.brandId)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(brand).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!BrandExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteBrand(int id)
    //    {
    //        var brand = await _context.Brands.FindAsync(id);
    //        if (brand == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Brands.Remove(brand);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool BrandExists(int id)
    //    {
    //        return _context.Brands.Any(e => e.BrandId == id);
    //    }
    //}


