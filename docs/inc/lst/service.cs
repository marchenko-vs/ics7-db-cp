public class Service
{
    private IServiceRepository<Service> _db;

    [Key]
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool EconomyClass { get; set; }
    public bool FirstClass { get; set; }
    public bool BusinessClass { get; set; }
}
