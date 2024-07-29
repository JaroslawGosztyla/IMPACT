namespace Tenders.Domain.Models;
public class TenderDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal AmountInEur { get; set; }
    public List<SupplierDto> Suppliers { get; set; }

    public TenderDto(Tender tender)
    {
        Id = tender.Id;
        Date = tender.Date;
        Title = tender.Title;
        Description = tender.Description;
        AmountInEur = tender.Awarded_Value_Eur;
        Suppliers = tender.Awarded.SelectMany(x => x.Suppliers)
            .Select(x => new SupplierDto(x)).Distinct().ToList();
    }
}
