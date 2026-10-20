using HospitalAppointmentManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentManagement.Controllers;

public class StateManagementController : Controller
{
    private const string PatientCookie = "medicare-demo-patient";
    private const string SpecializationSession = "medicare-demo-specialization";

    public IActionResult Index()
    {
        return View(BuildModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Save(AppointmentViewModel model, string? specialization)
    {
        if (!string.IsNullOrWhiteSpace(model.PatientName))
        {
            Response.Cookies.Append(PatientCookie, model.PatientName.Trim(), new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddHours(2),
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.Lax
            });
        }

        if (!string.IsNullOrWhiteSpace(specialization))
        {
            HttpContext.Session.SetString(SpecializationSession, specialization);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ClearCookie()
    {
        Response.Cookies.Delete(PatientCookie);
        return RedirectToAction(nameof(Index));
    }

    private StateManagementViewModel BuildModel()
    {
        return new StateManagementViewModel
        {
            SavedPatientName = Request.Cookies[PatientCookie],
            SavedSpecialization = HttpContext.Session.GetString(SpecializationSession)
        };
    }
}