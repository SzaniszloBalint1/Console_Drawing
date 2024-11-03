using System.ComponentModel.DataAnnotations;

public class Drawing
{
    [Key] public int Id { get; set; }

    [Required]
    [StringLength(500)]
    public string Data { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}
