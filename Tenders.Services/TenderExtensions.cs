using Tenders.Domain.Models;

namespace Tenders.Services;
public static class TenderExtensions
{
    public static List<TenderDto>? ApplyPagination(this IEnumerable<TenderDto> tenders, int page, int pageSize)
    {
        return tenders.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }
}
