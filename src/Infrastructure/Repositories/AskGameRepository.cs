using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Abstractions;

namespace Infrastructure.Repositories;

public class AskGameRepository(IAppDbContext context) : RepositoryBase<AskGame>(context), IAskGameRepository
{
}