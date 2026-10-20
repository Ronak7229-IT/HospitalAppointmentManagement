using HospitalAppointmentManagement.Extensions;
using HospitalAppointmentManagement.Models;
using HospitalAppointmentManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalAppointmentManagement.Controllers;

public class AppointmentsController : Controller
{
    private readonly DoctorService doctorService;

    public AppointmentsController(DoctorService doctorService)
    {
        this.doctorService = doctorService;
    }

    [HttpGet]
    public IActionResult Create(int? doctorId)
    {
        var model = new AppointmentViewModel();
        if (doctorId.HasValue && doctorService.GetDoctor(doctorId.Value) is not null)
        {
            model.DoctorId = doctorId.Value;
        }

        PopulateDoctors(model.DoctorId);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AppointmentViewModel model)
    {
        var doctor = doctorService.GetDoctor(model.DoctorId);
        if (doctor is null)
        {
            ModelState.AddModelError(nameof(model.DoctorId), "Please choose an available doctor.");
        }

        if (model.AppointmentDate.Date < DateTime.Today)
        {
            ModelState.AddModelError(nameof(model.AppointmentDate), "Appointment date cannot be in the past.");
        }

        if (!ModelState.IsValid || doctor is null)
        {
            PopulateDoctors(model.DoctorId);
            return View(model);
        }

        var confirmation = new AppointmentConfirmationViewModel
        {
            PatientName = model.PatientName,
            PatientEmail = model.PatientEmail,
            DoctorName = doctor.DoctorName,
            AppointmentDate = model.AppointmentDate,
            ConsultationFee = doctor.ConsultationFee,
            ServiceFee = DoctorExtensions.ServiceFee,
            TotalCharge = doctor.CalculateTotalCharge()
        };

        return View("Success", confirmation);
    }

    private void PopulateDoctors(int selectedDoctorId)
    {
        ViewBag.DoctorOptions = doctorService.GetDoctors();
        ViewBag.SelectedDoctorId = selectedDoctorId;
    }
}