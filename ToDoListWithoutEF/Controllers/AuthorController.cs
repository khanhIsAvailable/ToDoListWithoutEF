using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListWithoutEF.DataAccessLayer;
using ToDoListWithoutEF.Models;

namespace ToDoListWithoutEF.Controllers
{
    public class AuthorController : Controller
    {
        private IConfiguration configuration;
        private AuthorDAL authorDAL;
        public AuthorController(IConfiguration configuration) {
            this.configuration= configuration;
            authorDAL = new AuthorDAL(configuration);
        }
        public JsonResult GetAllAuthors()
        {
            List<Author> res = authorDAL.GetAllAuthors();

            return Json(res);
        }

        public bool Create(string Name, int Age, string? Note)
        {
            bool i = authorDAL.CreateAuthor(Name, Age, Note);
            return i;
        }
    }
}
