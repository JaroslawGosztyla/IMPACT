using Newtonsoft.Json;
using RestSharp;
using Tenders.Domain.Models;

namespace Tenders.Services;

public class TenderService
{
    private const string BaseUrl = "https://tenders.guru/api/pl/tenders";
    private const int MaxPage = 100;

    private readonly RestClient _client;

    public TenderService()
    {
        _client = new RestClient(BaseUrl);
    }

    private async Task<List<Tender>> FetchTendersAsync(int page)
    {
        var request = new RestRequest($"?page={page}", Method.Get);
        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
            throw new Exception("Error fetching tenders from API");

        var tenders = JsonConvert.DeserializeObject<TenderApiResponse>(response!.Content!);
        return tenders!.Data!;
    }

    private async Task<IEnumerable<TenderDto>> GetAllTendersAsync()
    {
        return (await Task.WhenAll(Enumerable.Range(1, MaxPage).Select(FetchTendersAsync)))
            .SelectMany(t => t).Select(x => new TenderDto(x)).ToList();
    }

    public async Task<List<TenderDto>> GetTenders(int page)
    {
        var tenders = await FetchTendersAsync(page);
        return tenders.Select(x => new TenderDto(x)).ToList();
    }

    public async Task<List<TenderDto>> GetTendersByPrice(decimal price, int page, int pageSize)
    {
        var tenders = await GetAllTendersAsync();
        return tenders.Where(t => t.AmountInEur == price).ApplyPagination(page, pageSize)!;
    }

    public async Task<List<TenderDto>> GetOrderedTendersByPrice(bool ascending, int page, int pageSize)
    {
        var tenders = await GetAllTendersAsync();
        return ascending 
            ? tenders.OrderBy(t => t.AmountInEur).ApplyPagination(page, pageSize)!
            : tenders.OrderByDescending(t => t.AmountInEur).ApplyPagination(page, pageSize)!;
    }

    public async Task<List<TenderDto>> GetTendersByDate(DateTime date, int page, int pageSize)
    {
        var tenders = await GetAllTendersAsync();
        return tenders.Where(t => t.Date == date).ApplyPagination(page, pageSize)!;
    }

    public async Task<List<TenderDto>> GetOrderedTendersByDate(bool ascending, int page, int pageSize)
    {
        var tenders = await GetAllTendersAsync();
        return ascending 
            ? tenders.OrderBy(t => t.Date).ApplyPagination(page, pageSize)!
            : tenders.OrderByDescending(t => t.Date).ApplyPagination(page, pageSize)!;
    }

    public async Task<List<TenderDto>> GetTendersBySupplier(int supplierId, int page, int pageSize)
    {
        var tenders = await GetAllTendersAsync();
        return tenders.Where(t => t.Suppliers.Any(s => s.Id == supplierId)).ApplyPagination(page, pageSize)!;
    }

    public async Task<TenderDto> GetTenderById(int id)
    {
        var request = new RestRequest($"/{id}", Method.Get);
        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
            throw new Exception($"Error fetching tender Id = {id} from API");

        var tender = JsonConvert.DeserializeObject<Tender>(response!.Content!);
        return new TenderDto(tender!);
    }
}
