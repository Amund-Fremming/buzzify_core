using Domain.Contracts;
using Domain.Entities.Spin;
using Infrastructure.Abstractions;

namespace Infrastructure.Repositories;

public class SpinPlayerRepository(IAppDbContext context) : RepositoryBase<SpinPlayer>(context), ISpinPlayerRepository
{
}