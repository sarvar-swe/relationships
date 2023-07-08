using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public CarsController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCar([FromBody] CreateCarDto carDto)
    {
        var owner = await dbContext.Users
            .FirstOrDefaultAsync(x  => x.Id == carDto.OwnerId);
        if(owner is null)
            return BadRequest("Owner does not exist.");

        var savedCar = dbContext.Cars.Add(new Car
        {
            Id = Guid.NewGuid(),
            Brand = carDto.Brand,
            Model = carDto.Model,
            ManufacturedAt = carDto.ManufacturedAt,
            Color = carDto.Color,
            Owner = owner
        });

        await dbContext.SaveChangesAsync();

        return Ok(new GetCarDto(savedCar.Entity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCar([FromRoute] Guid id)
    {
        var car = await dbContext.Cars
            .FirstOrDefaultAsync(u => u.Id == id);
        
        if(car is null)
            return NotFound();
        
        return Ok(new GetCarDto(car));
    }

    [HttpGet]
    public async Task<IActionResult> GetCars([FromQuery] string search)
    {
        var carsQuery = dbContext.Cars.AsQueryable();
        
        if(false == string.IsNullOrWhiteSpace(search))
            carsQuery = carsQuery.Where(u => 
                u.Brand.ToLower().Contains(search.ToLower()) ||
                u.Brand.ToLower().Contains(search.ToLower()));

        var cars = await carsQuery
            .Select(u => new GetCarDto(u))
            .ToListAsync();

        return Ok(cars);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar([FromRoute] Guid Id, UpdateCarDto updateCar)
    {
        var car = await dbContext.Cars
            .FirstOrDefaultAsync(u => u.Id == Id);
        
        if(car is null)
            return NotFound();
        
        // if(await dbContext.Cars.AnyAsync(u => u.Model.ToLower() == updateCar.Model.ToLower()))
        //     return Conflict("User with this username exists");

        car.Brand = updateCar.Brand;
        car.Model = updateCar.Model;
        car.Color = updateCar.Color;
        car.ManufacturedAt = updateCar.ManufacturedAt;
        car.OwnerId = updateCar.OwnerId;

        await dbContext.SaveChangesAsync();
        return Ok(car.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar([FromRoute] Guid id)
    {
        var car = await dbContext.Cars.FirstOrDefaultAsync(u => u.Id == id);
        if(car is null)
            return NotFound();

        dbContext.Cars.Remove(car);
        await dbContext.SaveChangesAsync();

        return Ok();
    }
    
}