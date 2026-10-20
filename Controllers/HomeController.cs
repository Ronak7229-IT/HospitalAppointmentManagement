using System.Diagnostics;
using HospitalAppointmentManagement.Services;
using Microsoft.AspNetCore.Mvc;
using HospitalAppointmentManagement.Models;

namespace HospitalAppointmentManagement.Controllers;

public class HomeController : Controller
{
    private readonly DoctorService doctorService;

    public HomeController(DoctorService doctorService)
    {
        this.doctorService = doctorService;
    }

    public IActionResult Index()
    {
        return View(new HomeViewModel { Doctors = doctorService.GetDoctors() });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
