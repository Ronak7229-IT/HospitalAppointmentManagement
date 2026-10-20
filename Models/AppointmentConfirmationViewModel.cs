namespace HospitalAppointmentManagement.Models;

public class AppointmentConfirmationViewModel
{
    public string PatientName { get; set; } = string.Empty;
    public string? PatientEmail { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public decimal ConsultationFee { get; set; }
    public decimal ServiceFee { get; set; }
    public decimal TotalCharge { get; set; }
}