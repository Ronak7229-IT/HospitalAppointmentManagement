using HospitalAppointmentManagement.Models;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalAppointmentManagement.Services;

public class DoctorService
{
    private const string DoctorsCacheKey = "medicare-doctors";
    private readonly IMemoryCache memoryCache;

    public DoctorService(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }

    public IReadOnlyList<Doctor> GetDoctors()
    {
        return memoryCache.GetOrCreate(DoctorsCacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3);
            return (IReadOnlyList<Doctor>)[
                new Doctor { DoctorId = 1, DoctorName = "Dr. Ananya Sharma", Specialization = "Cardiology", Experience = 14, ConsultationFee = 1800m },
                new Doctor { DoctorId = 2, DoctorName = "Dr. Rohan Mehta", Specialization = "Dermatology", Experience = 9, ConsultationFee = 1200m },
                new Doctor { DoctorId = 3, DoctorName = "Dr. Kavya Iyer", Specialization = "Pediatrics", Experience = 11, ConsultationFee = 1400m },
                new Doctor { DoctorId = 4, DoctorName = "Dr. Arjun Nair", Specialization = "Orthopedics", Experience = 16, ConsultationFee = 2000m },
                new Doctor { DoctorId = 5, DoctorName = "Dr. Meera Joshi", Specialization = "Neurology", Experience = 18, ConsultationFee = 2200m },
                new Doctor { DoctorId = 6, DoctorName = "Dr. Vikram Singh", Specialization = "General Medicine", Experience = 7, ConsultationFee = 900m }
            ];
        }) ?? [];
    }

    public Doctor? GetDoctor(int id)
    {
        return GetDoctors().FirstOrDefault(doctor => doctor.DoctorId == id);
    }
}