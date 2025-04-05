using Domain.Entities.Admin;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IAdminService
{
    Task<Result<UserActivityData>> GetWeeklyActivity(int passCode, int iterationsBackwards);

    Task<Result<UserActivityData>> GetMonthlyActivity(int passCode, int iterationsBackwards);

    Task<Result> DeactivateNotification(int id, int passCode);

    Task<Result> ActivateNotification(int id, int passCode);

    Task<Result> CreateNotification(int passCode, string heading, string message, string color);

    Task<Result<ModalNotification>> GetAll(int passCode);
}