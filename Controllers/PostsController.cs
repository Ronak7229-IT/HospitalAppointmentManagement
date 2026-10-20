using System.Net;
using System.Text.Json;
using HospitalAppointmentManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentManagement.Controllers;

public class PostsController : Controller
{
    private readonly IHttpClientFactory httpClientFactory;

    public PostsController(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var client = httpClientFactory.CreateClient("PostsApi");
            using var response = await client.GetAsync("posts", cancellationToken);
            if (response.StatusCode is not HttpStatusCode.OK)
            {
                ViewBag.Error = "The posts service is temporarily unavailable. Please try again shortly.";
                return View(Array.Empty<Post>());
            }

            var posts = await response.Content.ReadFromJsonAsync<List<Post>>(cancellationToken: cancellationToken);
            return View(posts ?? []);
        }
        catch (OperationCanceledException)
        {
            ViewBag.Error = cancellationToken.IsCancellationRequested
                ? "The posts request was cancelled."
                : "The posts service took too long to respond. Please try again shortly.";
            return View(Array.Empty<Post>());
        }
        catch (HttpRequestException)
        {
            ViewBag.Error = "We could not connect to the external posts service.";
            return View(Array.Empty<Post>());
        }
        catch (JsonException)
        {
            ViewBag.Error = "The posts service returned an unexpected response.";
            return View(Array.Empty<Post>());
        }
    }
}