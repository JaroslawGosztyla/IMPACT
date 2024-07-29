namespace Tenders.Domain.Models;
public class TenderApiResponse
{
    public int Page_Count { get; set; }
    public int Page_Number { get; set; }
    public int Page_Size { get; set; }
    public int Total { get; set; }
    public List<Tender>? Data { get; set; }
}
