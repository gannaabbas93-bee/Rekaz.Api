namespace Rekaz.Api.Core.Interfaces;

using Rekaz.Api.Core.Entities;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetActiveServicesAsync(CancellationToken cancellationToken = default);
    Task<Service?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
