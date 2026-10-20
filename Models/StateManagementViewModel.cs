using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentManagement.Models;

public class StateManagementViewModel
{
    [Display(Name = "Patient name")]
    [StringLength(80)]
    public string? PatientName { get; set; }

    [Display(Name = "Specialization")]
    public string? Specialization { get; set; }

    public string? SavedPatientName { get; set; }
    public string? SavedSpecialization { get; set; }
}