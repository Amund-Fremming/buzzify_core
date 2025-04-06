using Domain.Entities.Admin;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IAdminService
{
    Task<Result<UserActivityData>> GetRecentUserActivity(int passCode);

    Task<Result<UserActivityData>> GetHistoricUserActivity(int passCode);

    Task<Result> SetNotification(int id, int passCode, bool displayNotification);

    Task<Result> CreateNotification(int passCode, string heading, string message, string color);

    Task<Result<List<ModalNotification>>> GetAll(int passCode);
}