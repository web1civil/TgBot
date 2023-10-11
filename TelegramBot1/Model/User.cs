using System.ComponentModel.DataAnnotations;

public class User
{
    
    public int Id { get; set; }
    public int TgChatId { get; set; }
    public string? Name { get; set; }
}