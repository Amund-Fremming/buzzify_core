using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class Round : ITypeScriptModel
{
    [Key]
    public int Id { get; set; }

    public int SpinGameId { get; set; }

    public bool Completed { get; set; }

    public void FinishRound() => Completed = true;

    public static Round Create(int spinGameId)
        => new()
        {
            SpinGameId = spinGameId,
            Completed = false,
        };
}