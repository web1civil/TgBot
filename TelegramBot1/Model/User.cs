using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    public long ChatId { get; set; }
    public string? Name { get; set; }
}