using Domain.Abstractions;
using Domain.Shared.TypeScript;

namespace Domain.Entities.Ask;

public sealed class AskVote : VoteBase<AskVote>, ITypeScriptModel;