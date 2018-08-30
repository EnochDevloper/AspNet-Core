using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly MySchoolContext _context;

        public StudentController(MySchoolContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["name"] = "中国，世界";
            return View();
        }

        /// <summary>
        /// list列表
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            List<Student> list = _context.Student.ToList();
            return View(list);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="sid">学生编号</param>
        /// <returns></returns>
        public IActionResult Details(Guid id)
        {
            var m_student = _context.Student.FirstOrDefault(c => c.s_id == id);
            return View(m_student);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="name">账号</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public JsonResult Register(string name, string passWord)
        {
            int result = 0;
            _context.Student.Add(new Student()
            {
                s_id = Guid.NewGuid(),
                s_name = name,
                s_address = "重庆",
                s_age = 26,
                s_Grade_ID = new Guid("8977C3AF-1D71-4553-B431-5DA584FDBDC1"),
                s_loginName = name,
                s_passWord = passWord,
                s_phone = "13637748963",
                s_remark = "简简单单就好",
                s_sex = 0,
                s_status = 1,
                s_createDate = DateTime.Now
            });
            result = _context.SaveChanges();
            return Json(new { count = result });
        }


    }
}