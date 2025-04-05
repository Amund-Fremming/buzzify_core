using Domain.Entities.Admin;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IAdminService
{
    Task<Result<UserActivityData>> GetUserActivityData(int passCode);

    Task<Result> DeactivateNotification(int passCode);

    Task<Result> ActivateNotification(int passCode, string heading, string message, string color);
}