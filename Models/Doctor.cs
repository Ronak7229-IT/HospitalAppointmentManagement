using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentManagement.Models;

public class Doctor
{
    public int DoctorId { get; set; }

    [Required, StringLength(80)]
    public string DoctorName { get; set; } = string.Empty;

    [Required, StringLength(60)]
    public string Specialization { get; set; } = string.Empty;

    [Range(0, 60)]
    public int Experience { get; set; }

    [Range(typeof(decimal), "0.01", "100000")]
    public decimal ConsultationFee { get; set; }
}