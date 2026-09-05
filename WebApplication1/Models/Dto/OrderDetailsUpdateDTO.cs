using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Dto;

public class OrderDetailsUpdateDTO
{
    [Required]
    public int OrderDetailId { get; set; }
    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int? Rating { get; set; }
}