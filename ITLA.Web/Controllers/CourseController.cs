using ITLA.BLL.Contracts;
using ITLA.BLL.Dtos;
using ITLA.BLL.Models;
using ITLA.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ITLA.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: StudentController
        public ActionResult Index()
        {
            var courses = (List<BLL.Models.CourseModel>)_courseService.GetAll().Data;

            var courseModel = courses.Select(cd => new Models.Course()
            {

                Id = cd.Id,
                Title = cd.Title,
                Credits = cd.Credits,
                DepartmentId = cd.DepartmentId,
                Deleted = cd.Deleted

            });

            return View(courseModel);
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            var course = (CourseModel)_courseService.GetById(id).Data;

            Course Modelcourse = new Course()
            {
                Id = course.Id,
                Credits = course.Credits,
                Title = course.Title,
                DepartmentId = course.DepartmentId
            };
            return View(Modelcourse);
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course courseModel)
        {
            try
            {
                CourseSaveDto savecourseDto = new CourseSaveDto()
                {
                    UserId = 1,
                    AuditDate = System.DateTime.Now,
                    Title = courseModel.Title,
                    Credits = courseModel.Credits,
                    DepartmentId = courseModel.DepartmentId


                };

                _courseService.SaveCourse(savecourseDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            var course = (CourseModel)_courseService.GetById(id).Data;

            Course Modelcourse = new Course()
            {
                Id = course.Id,
                DepartmentId = course.DepartmentId,
                Credits = course.Credits,
                Title = course.Title
            };

            return View(Modelcourse);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course courseModel)
        {
            try
            {
                var myModel = courseModel;

                BLL.Dtos.CourseUpdateDto courseUpdate = new BLL.Dtos.CourseUpdateDto()
                {
                    Id = courseModel.Id,
                    AuditDate = System.DateTime.Now,
                    Title = courseModel.Title,
                    Credits = courseModel.Credits,
                    DepartmentId = courseModel.DepartmentId,
                    UserId = 1
                };

                _courseService.UpdateCourse(courseUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            var course = (CourseModel)_courseService.GetById(id).Data;

            Course Modelcourse = new Course()
            {
                Id = course.Id,
                Title = course.Title,
                Credits = course.Credits,
                DepartmentId = course.DepartmentId
            };
            return View(Modelcourse);
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Course courseModel)
        {
            try
            {
                var myModel = courseModel;

                BLL.Dtos.CourseRemoveDto courseRemove = new BLL.Dtos.CourseRemoveDto()
                {
                    Id = courseModel.Id
                };

                _courseService.RemoveCourse(courseRemove);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }
    }
}
