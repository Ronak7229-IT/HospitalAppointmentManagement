using HospitalAppointmentManagement.Models;

namespace HospitalAppointmentManagement.Extensions;

public static class DoctorExtensions
{
    public const decimal ServiceFee = 100m;

    public static decimal CalculateTotalCharge(this Doctor doctor)
    {
        return doctor.ConsultationFee + ServiceFee;
    }
}