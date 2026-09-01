namespace Rekaz.Api.Core.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreateBookingDto
{
    [Required(ErrorMessage = "FullName is required")]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "BusinessType is required")]
    [StringLength(100)]
    public string BusinessType { get; set; } = string.Empty;

    [Required(ErrorMessage = "CountryCode is required")]
    [StringLength(10)]
    public string CountryCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "ServiceId must be greater than 0")]
    public int ServiceId { get; set; }

    [Required(ErrorMessage = "BookingDate is required")]
    public string BookingDate { get; set; } = string.Empty;

    [Required(ErrorMessage = "SelectedSlot is required")]
    public string SelectedSlot { get; set; } = string.Empty;
}
