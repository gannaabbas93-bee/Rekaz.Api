namespace Rekaz.Api.Core.Interfaces;

using Rekaz.Api.Core.DTOs;

public interface IHomeService
{
    Task<HomeDataDto> GetAggregatedHomeDataAsync(int? serviceId, string? date);
}
