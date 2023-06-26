using MeetUp.Models;

namespace MeetUp.ViewModels;

public class MeetActivityViewModel
{
    public MeetActivity meetActivity { get; set; }
    public ICollection<AppUser> members { get; set; }
    public bool canJoin { get; set; }

}