namespace Tenders.Domain.Models;
public class SupplierDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public SupplierDto(Supplier supplier)
    {
        Id = supplier.Id;
        Name = supplier.Name;
    }
}
