using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentManagement.Models;

public class AppointmentViewModel
{
    [Required, StringLength(80, MinimumLength = 2)]
    [Display(Name = "Patient name")]
    public string PatientName { get; set; } = string.Empty;

    [EmailAddress, StringLength(120)]
    public string? PatientEmail { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please choose a doctor.")]
    public int DoctorId { get; set; }

    [Required, DataType(DataType.Date)]
    [Display(Name = "Appointment date")]
    public DateTime AppointmentDate { get; set; } = DateTime.Today.AddDays(1);

    [StringLength(500)]
    public string? Reason { get; set; }
}