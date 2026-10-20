using HospitalAppointmentManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentManagement.Controllers;

public class DoctorsController : Controller
{
    private readonly DoctorService doctorService;

    public DoctorsController(DoctorService doctorService)
    {
        this.doctorService = doctorService;
    }

    public IActionResult Index()
    {
        return View(doctorService.GetDoctors());
    }

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
    public IActionResult Details(int id)
    {
        var doctor = doctorService.GetDoctor(id);
        return doctor is null ? NotFound() : View(doctor);
    }
}