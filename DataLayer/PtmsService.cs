using PwDbAssistant.DbConnect;

using PwDbAssistant.Common;
using ExcelDataReader;
using System.Data;
using GfsBalloting.Model;
using GfsBalloting.Model.ViewMedels;

namespace PwDbAssistant.DataLayer
{
    public class PtmsService
    {
        private IGenericRepository<Users> _IgenericRepository;

        APIResponse apiresponse = new APIResponse();


        public PtmsService(IGenericRepository<Users> igenericRepository)
        {
            _IgenericRepository = igenericRepository;
        }

        public APIResponse CreateUsers(Users users)
        {
            var res = _IgenericRepository.ExecuteQuery<Users>(users, "usp_Create_Update_Users").ToList();
            apiresponse.Response = res;
            return apiresponse;
        }
       
        public APIResponse CreateTask(Tasks task)
        {


           Guid TaskId = Guid.NewGuid();    
           Guid SubTaskId = Guid.NewGuid();

            task.Duration.ToString();
            task.TaskId = TaskId;


            int TotalDays =Convert.ToInt32( (Convert.ToDateTime(task.Duration) - (DateTime.Now)).TotalDays);
            float TotalHours = Convert.ToInt32( (Convert.ToDateTime(task.Duration) - (DateTime.Now)).TotalHours);

            task.TotalDays = TotalDays;
            task.TotalHours = TotalHours;





            var res = _IgenericRepository.ExecuteQuery<Tasks>(task, "usp_Create_Update_Task").ToList();


            for (int i = 1; i <= TotalDays; i++)
            {
                SubTask subTask = new SubTask
                {
                    SubTaskId = Guid.NewGuid(),
                    TaskId = TaskId,
                    TaskDays = i,
                   // TotalHours = TotalHours.ToString(),
                    HoursPerDay = (TotalHours / 24),
                    UserId = task.UserId
                };

                _IgenericRepository.ExecuteQuery<SubTask>(subTask, "usp_Create_Update_SubTask").ToList();

            }
            apiresponse.Response = res;
            return apiresponse;
        }


        public APIResponse CreateUserOffHours(UsersOffHours users)
        {
            var res = _IgenericRepository.ExecuteQuery<UsersOffHours>(users, "usp_Create_Update_UserOffHours").ToList();
            apiresponse.Response = res;
            return apiresponse;
        }
     
        public APIResponse GetUserOffHours(UsersOffHours users)
        {


            object obj = new 
            {

                UserId = users.UserId

            };

            var res = _IgenericRepository.ExecuteQuery<UsersOffHours>(obj, "usp_GetUserOffHours").ToList();
            apiresponse.Response = res;
            return apiresponse;
        }
        
        public APIResponse GetTakes(TaskFilter taskFilter)
        {


            var res = _IgenericRepository.ExecuteQuery<TaskView>(taskFilter, "usp_GetTakes").ToList();
            apiresponse.Response = res;
            return apiresponse;
        }

         public APIResponse GetTakesReminder(TaskFilter taskFilter)
        {


            taskFilter.FromDate = DateTime.Now.ToShortTimeString();
            taskFilter.Todate = DateTime.Now.AddHours(3).ToShortTimeString();


            var res = _IgenericRepository.ExecuteQuery<TaskView>(taskFilter, "usp_GetTakes").ToList();
            apiresponse.Response = res;
            return apiresponse;
        }


    }

}
