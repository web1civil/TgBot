﻿using System.ComponentModel.DataAnnotations;

public class Note
{
    public int Id { get; set; }
    public long UserId { get; set; }
    public string? Header { get; set; }
    public string? Text { get; set; }
    public int StageCreate { get; set; }
}