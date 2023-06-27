using MeetUp.Models;

namespace MeetUp.Data
{
    public class EmailMessage
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public void GenerateNotifySubscribedPlayer(string recepient, MeetActivity activity)
        {

            Email = recepient;
            Subject = "MeetUp - Activity approved!";
            Message = "Your participation has been approved for activity \"" + activity.Name + "\" at " + activity.Time.ToLocalTime().ToString();
            
        }

        public void GenerateNotifyAcitivtyOwner(string recepient, MeetActivity activity)
        {

            Email = recepient;
            Subject = "MeetUp - New participant!";
            Message = "There is a new participant for your activity \"" + activity.Name + "\" at " + activity.Time.ToLocalTime();
            
        }
    }
}
    