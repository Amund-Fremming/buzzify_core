﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Ask;

public class Question
{
    [Key]
    public int Id { get; }

    public int AskGameId { get; init; }
    
    [MaxLength(100)]
    public string Text { get; init; } = default!;

    private Question()
    { }

    public static Question Create(int gameId, string text)
        => new()
        {
            Text = text,
            AskGameId = gameId,
        };
}