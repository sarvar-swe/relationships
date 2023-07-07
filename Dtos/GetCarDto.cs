public class GetCarDto
{
    public GetCarDto(Car entity)
    {
        Id = entity.Id;
        Brand = entity.Brand;
        Model = entity.Model;
        ManufacturedAt = entity.ManufacturedAt;
        OwnerId = entity.OwnerId;
        Owner = entity.Owner is null ? null : new GetUserDto(entity.Owner);
    }
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    
    public DateTime ManufacturedAt { get; set; }
    public Guid OwnerId { get; set; }
    public GetUserDto Owner { get; set; }
}