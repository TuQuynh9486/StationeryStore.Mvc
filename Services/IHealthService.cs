using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public interface IHealthService
{
    Task<List<HealthCheckItemViewModel>>
        GetHealthChecksAsync();
}