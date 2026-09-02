namespace Rekaz.Api.Application.Interfaces;

using Rekaz.Api.Core.DTOs;

public interface IHomeService
{
    Task<HomeDataDto> GetAggregatedHomeDataAsync(int? serviceId, string? date, CancellationToken cancellationToken = default);
}
