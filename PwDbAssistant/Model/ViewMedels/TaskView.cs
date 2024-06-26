using System.Threading.Tasks;

namespace GfsBalloting.Model.ViewMedels
{
    public class TaskView
    {
        public string     Duration          {get;set;}
        public string     TaskPriority      {get;set;}
        public string     TotalDays         {get;set;}
        public string     SubTaskId         {get;set;}
        public string     TaskId            {get;set;}
        public string     TaskDays          {get;set;}
        public string     TotalHours        {get;set;}
        public string     HoursPerDay       {get;set;}
        public string UserId { get; set; }
    }
}
