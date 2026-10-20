namespace HospitalAppointmentManagement.Models;

public class HomeViewModel
{
    public IReadOnlyList<Doctor> Doctors { get; set; } = [];
    public int SpecializationCount => Doctors.Select(doctor => doctor.Specialization).Distinct().Count();
}