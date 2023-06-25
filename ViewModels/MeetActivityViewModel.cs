using MeetUp.Models;
using MeetUp.Utils;

namespace MeetUp.ViewModels;

public class MeetActivityViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Time { get; set; }
    public int Capacity { get; set; }
    public IFormFile Picture { get; set; }
    public int LocationId { get; set; }
    public int CategoryId { get; set; }

    public static MeetActivity to(MeetActivityViewModel model)
    {
        return new MeetActivity(model.Name,
            model.Description,
            model.Time,
            model.Capacity,
            FileUploadUtils.FileToBytes(model.Picture),
            model.LocationId,
            model.CategoryId);
    }
}