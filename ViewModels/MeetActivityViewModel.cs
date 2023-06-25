using MeetUp.Models;
using MeetUp.Utils;

namespace MeetUp.ViewModels;

public class MeetActivityViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Time { get; set; }
    public int Capacity { get; set; }
    public IFormFile Picture { get; set; }
    public int LocationId { get; set; }
    public int CategoryId { get; set; }

    private MeetActivityViewModel(int id, string name, string description, DateTime time, int capacity, FormFile picture, int locationId, int categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        Time = time;
        Capacity = capacity;
        Picture = picture;
        LocationId = locationId;
        CategoryId = categoryId;
    }
    
    public static MeetActivity to(MeetActivityViewModel model)
    {
        return new MeetActivity(
            model.Name,
            model.Description,
            model.Time,
            model.Capacity,
            FileUploadUtils.FileToBytes(model.Picture),
            model.LocationId,
            model.CategoryId);
    }
    
    public static MeetActivityViewModel from(MeetActivity entity)
    {
        var memoryStream = new MemoryStream(entity.Picture);
        return new MeetActivityViewModel(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Time,
            entity.Capacity,
            new FormFile(memoryStream, 0, memoryStream.Length, "picture", "picture_"+entity.Id),
            entity.LocationId,
            entity.CategoryId);
    }
}