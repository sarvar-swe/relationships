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

    
}