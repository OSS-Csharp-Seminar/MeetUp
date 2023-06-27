using System.Net.Sockets;
using MeetUp.Models;
using MeetUp.Utils;

namespace MeetUp.ViewModels;

public class MeetActivityCreateModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Time { get; set; }
    public int Capacity { get; set; }
    public IFormFile? Picture { get; set; }
    public string Address { get; set; }
    public int CityId { get; set; }
    public int CategoryId { get; set; }
    public string AppUserId { get; set; }

    public static MeetActivity To(MeetActivityCreateModel model, int locationId)
    {
        byte[] picture = null;
        if (model.Picture != null)
        {
            picture = FileUploadUtils.FileToBytes(model.Picture);
        }
        
        return new MeetActivity(
            model.Name,
            model.Description,
            model.Time,
            model.Capacity,
            picture,
            locationId,
            model.CategoryId,
            model.AppUserId);
    }
}