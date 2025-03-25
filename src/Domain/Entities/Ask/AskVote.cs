using Domain.Abstractions;
using Domain.Shared.TypeScript;

namespace Domain.Entities.Ask;

public class AskVote : VoteBase<AskVote>, ITypeScriptModel;