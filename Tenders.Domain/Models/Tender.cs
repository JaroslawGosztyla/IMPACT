namespace Tenders.Domain.Models;
public class Tender
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Awarded_Value_Eur { get; set; }
    public List<TenderAward> Awarded { get; set; }
}
