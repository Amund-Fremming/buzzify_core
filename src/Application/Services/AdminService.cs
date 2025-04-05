using Application.Contracts;
using Domain.Entities.Admin;
using Domain.Shared.ResultPattern;

namespace Application.Services;

public class AdminService : IAdminService
{
    public Task<Result> ActivateNotification(int id, int passCode) => throw new NotImplementedException();

    public Task<Result> CreateNotification(int passCode, string heading, string message, string color) => throw new NotImplementedException();

    public Task<Result> DeactivateNotification(int id, int passCode) => throw new NotImplementedException();

    public Task<Result<ModalNotification>> GetAll(int passCode) => throw new NotImplementedException();

    public Task<Result<UserActivityData>> GetMonthlyActivity(int passCode, int iterationsBackwards) => throw new NotImplementedException();

    public Task<Result<UserActivityData>> GetWeeklyActivity(int passCode, int iterationsBackwards) => throw new NotImplementedException();
}