using MeetUp.Interfaces;
using MeetUp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MeetUp.Controllers;

public class ImageController : Controller
{
    private readonly IMeetActivityService meetActivityService;

    public ImageController(IMeetActivityService _service)
    {
        meetActivityService = _service;
    }

    public ActionResult Show(int activityId)
    {
        var activity = meetActivityService.GetById(activityId).Result;
        var imageData = activity.Picture;

        return File(imageData, "image/jpg");

    }
}