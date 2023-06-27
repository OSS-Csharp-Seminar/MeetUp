using MeetUp.Models;
using MeetUp.Utils;

namespace MeetUp.ViewModels;

public class MeetActivityEditModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Time { get; set; }
    public int Capacity { get; set; }
    public IFormFile? Picture { get; set; }
    public byte[] PictureBytes { get; set; }
    public string Address { get; set; }
    public int CityId { get; set; }
    public int CategoryId { get; set; }
    public string AppUserId { get; set; }

    public MeetActivityEditModel()
    {
        
    }

    public MeetActivityEditModel(MeetActivity model)
    {
        Name = model.Name;
        Description = model.Description;
        Time = model.Time;
        Capacity = model.Capacity;
        PictureBytes = model.Picture;
        Address = model.Location.Address;
        CityId = model.Location.CityId;
        CategoryId = model.CategoryId;
        AppUserId = model.AppUserId;
    }
    
    public static MeetActivity To(MeetActivityEditModel model, int locationId)
    {
        byte[] picture;
        if (model.Picture != null)
        {
            picture = FileUploadUtils.FileToBytes(model.Picture);
        }
        else
        {
            picture = model.PictureBytes;
        }
        
        return new MeetActivity(
            model.Id,
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