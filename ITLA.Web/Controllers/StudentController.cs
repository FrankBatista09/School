using ITLA.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ITLA.BLL.Models;
using ITLA.BLL.Dtos;
using ITLA.Web.Models;

namespace ITLA.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // GET: StudentController
        public ActionResult Index()
        {
            var students = (List<StudentModel>)_studentService.GetAll().Data;

            var studentModel = students.Select(cd => new Models.Student()
            {
                LastName = cd.LastName,
                Name = cd.FirstName,
                Id = cd.Id,
                EnrollmentDate = cd.EnrollmentDate,
                Deleted = cd.Deleted

            });

            return View(studentModel);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var student = (StudentModel)_studentService.GetById(id).Data;

            Student Modelstudent = new Student()
            {
                Id = student.Id,
                EnrollmentDate = student.EnrollmentDate,
                Name = student.FirstName,
                LastName = student.LastName
            };
            return View(Modelstudent);

        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student studentModel)
        {
            try
            {
                StudentSaveDto saveStudentDto = new StudentSaveDto()
                {
                    UserId = 1,
                    AuditDate = System.DateTime.Now,
                    EnrollmentDate = studentModel.EnrollmentDate.Value,
                    FirstName = studentModel.Name,
                    LastName = studentModel.LastName
                };

                _studentService.SaveStudent(saveStudentDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = (StudentModel)_studentService.GetById(id).Data;

            Student Modelstudent = new Student()
            {
                Id = student.Id,
                EnrollmentDate = student.EnrollmentDate,
                Name = student.FirstName,
                LastName = student.LastName
            };

            return View(Modelstudent);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student studentModel)
        {
            try
            {
                var myModel = studentModel;

                StudentUpdateDto studentUpdate = new StudentUpdateDto()
                {
                    Id = studentModel.Id,
                    AuditDate = System.DateTime.Now,
                    EnrollmentDate = studentModel.EnrollmentDate.Value,
                    FirstName = studentModel.Name,
                    LastName = studentModel.LastName,
                    UserId = 1
                };

                _studentService.UpdateStudent(studentUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var student = (StudentModel)_studentService.GetById(id).Data;

            Student Modelstudent = new Student()
            {
                Id = student.Id,
                EnrollmentDate = student.EnrollmentDate,
                Name = student.FirstName,
                LastName = student.LastName
            };
            return View(Modelstudent);

        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Student studentModel)
        {
            try
            {
                var myModel = studentModel;

                BLL.Dtos.StudentRemoveDto studentRemove = new BLL.Dtos.StudentRemoveDto()
                {
                    Id = studentModel.Id
                };

                _studentService.RemoveStudent(studentRemove);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
