using Application.Contracts;
using Domain.Abstractions;
using Domain.Contracts;
using Domain.Entities.Admin;
using Domain.Shared.ResultPattern;

namespace Application.Services;

public class AdminService(IGenericRepository genericRepository, IUserBaseRepository userRepository) : IAdminService
{
    private readonly int _validPassCode = 1234;

    public async Task<Result> SetNotification(int id, int passCode, bool displayNotification)
    {
        if (passCode != _validPassCode)
        {
            return new Error("Unauthorized.");
        }

        var result = await genericRepository.GetById<ModalNotification>(id);
        if (result.IsError || result.IsEmpty)
        {
            return result.Error;
        }

        result.Data.SetDisplayNotification(displayNotification);
        var saveResult = await genericRepository.Update(result.Data);
        return saveResult;
    }

    public async Task<Result> CreateNotification(int passCode, string heading, string message, string color)
    {
        if (passCode != _validPassCode)
        {
            return new Error("Unauthorized.");
        }

        var modal = ModalNotification.Create(heading, message, color);
        var result = await genericRepository.Create(modal);
        return result;
    }

    public async Task<Result<List<ModalNotification>>> GetAll(int passCode)
    {
        if (passCode != _validPassCode)
        {
            return new Error("Unauthorized.");
        }

        var result = await genericRepository.GetAll<ModalNotification>();
        return result;
    }

    public async Task<Result<UserActivityData>> GetRecentUserActivity(int passCode)
    {
        if (passCode != _validPassCode)
        {
            return new Error("Unauthorized.");
        }

        var result = await userRepository.GetAll();
        if (result.IsError)
        {
            return result.Error;
        }

        var users = result.Data;
        return new UserActivityData()
        {
            Today = users.Where(u => u.LastActive.DayOfYear == DateTime.Now.DayOfYear).Count(),
            Weekly = users.Where(u => u.LastActive > DateTime.Now.AddDays(-7)).Count(),
            Monthly = users.Where(u => u.LastActive > DateTime.Now.AddDays(-30)).Count(),
        };
    }

    public Task<Result<UserActivityData>> GetHistoricUserActivity(int passCode)
    {
        // TODO
        throw new NotImplementedException();
    }
}