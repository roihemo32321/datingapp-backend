using dating_backend.Helpers;
using System.Text.Json;

namespace dating_backend.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(header, jsonOptions));
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination"); // Letting the client to get cors to read the pagination header.
        }
    }
}
