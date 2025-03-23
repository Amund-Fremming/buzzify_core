using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Abstractions;

namespace Infrastructure.Repositories;

public class SpinGameRepository(IAppDbContext context) : RepositoryBase<SpinGame>(context), ISpinGameRepository
{
}