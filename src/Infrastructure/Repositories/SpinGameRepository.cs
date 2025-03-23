using Domain.Contracts;
using Domain.Entities.Spin;
using Infrastructure.Abstractions;

namespace Infrastructure.Repositories;

public class SpinGameRepository(IAppDbContext context) : RepositoryBase<SpinGame>(context), ISpinGameRepository
{
}