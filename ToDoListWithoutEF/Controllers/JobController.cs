using Microsoft.AspNetCore.Mvc;
using ToDoListWithoutEF.Models;
using ToDoListWithoutEF.DataAccessLayer;
using NuGet.Protocol;

namespace ToDoListWithoutEF.Controllers
{
    public class JobController : Controller
    {
        JobDAL dal;
        private IConfiguration configuration;
        public JobController(IConfiguration _configuration) {
            this.configuration = _configuration;
            dal = new JobDAL(this.configuration);
        }  
        public IActionResult Index()
        {
            return View();
        }

        public bool Create(string Title, string Description, string Target, int Level, int AuthorId) {
            bool i = dal.CreateJob(Title, Description, Target, Level, AuthorId);
            return i;
        }

        public JsonResult GetDetail(int id)
        {
            Job res = dal.GetJobById(id);
            return Json(res);
        }

        [HttpPost]
        public int Edit(Job job)
        {
            if (job.Description == null)
            {
                job.Description = "";
            }
            if (job.Target == null)
            {
                job.Target = "";
            }

            if (ModelState.IsValid)
            {
                Job res = new Job { Id = job.Id, Title = job.Title, Description = job.Description, IsCheck = job.IsCheck, Level = job.Level, Target = job.Target, AuthorId = job.AuthorId, AuthorName = job.AuthorName };
                int i = dal.EditJob(res);
                return i;
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            return 0;
        }


        public bool Remove(int id)
        {
            return (dal.RemoveJob(id));
        }

        public JsonResult Search(int Type ,string SearchKeyword)
        {
            List<Job> res;
            if(SearchKeyword == null)
            {
                SearchKeyword = "";
            }
            res = dal.SearchJob(Type, SearchKeyword);
            
            return Json(res);
        }

    }
}
