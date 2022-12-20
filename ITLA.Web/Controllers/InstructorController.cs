using ITLA.BLL.Contracts;
using ITLA.BLL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ITLA.Web.Models;
using ITLA.BLL.Models;

namespace ITLA.Web.Controllers
{
    public class InstructorController :Controller
    {
        private readonly IInstructorService _instructorService;
        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }
        // GET: StudentController
        public ActionResult Index()
        {
            var instructors = (List<InstructorModel>)_instructorService.GetAll().Data;

            var instructorModel = instructors.Select(cd => new Models.Instructor()
            {
                LastName = cd.LastName,
                Name = cd.FirstName,
                Id = cd.Id,
                HireDate = cd.HireDate,
                Deleted = cd.Deleted

            });

            return View(instructorModel);
        }

        // GET: InstructorController/Details/5
        public ActionResult Details(int id)
        {
            var instructor = (InstructorModel)_instructorService.GetById(id).Data;

            Instructor Modelinstructor = new Instructor()
            {
                Id = instructor.Id,
                HireDate = instructor.HireDate,
                Name = instructor.FirstName,
                LastName = instructor.LastName
            };

            return View(Modelinstructor);
        }

        // GET: InstructorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstructorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Instructor instructorModel)
        {
            try
            {
                InstructorSaveDto saveInstructorDto = new InstructorSaveDto()
                {
                    UserId = 1,
                    AuditDate = System.DateTime.Now,
                    HireDate = instructorModel.HireDate.Value,
                    FirstName = instructorModel.Name,
                    LastName = instructorModel.LastName
                };

                _instructorService.SaveInstructor(saveInstructorDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InstructorController/Edit/5
        public ActionResult Edit(int id)
        {
            var instructor = (InstructorModel)_instructorService.GetById(id).Data;

            Instructor Modelinstructor = new Instructor()
            {
                Id = instructor.Id,
                HireDate = instructor.HireDate,
                Name = instructor.FirstName,
                LastName = instructor.LastName
            };

            return View(Modelinstructor);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Instructor instructorModel)
        {
            try
            {
                var myModel = instructorModel;

                BLL.Dtos.InstructorUpdateDto instructorUpdate = new BLL.Dtos.InstructorUpdateDto()
                {
                    Id = instructorModel.Id,
                    AuditDate = System.DateTime.Now,
                    HireDate = instructorModel.HireDate.Value,
                    FirstName = instructorModel.Name,
                    LastName = instructorModel.LastName,
                    UserId = 1
                };

                _instructorService.UpdateInstructor(instructorUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InstructorController/Delete/5
        public ActionResult Delete(int id)
        {
            var instructor = (InstructorModel)_instructorService.GetById(id).Data;

            Instructor Modelinstructor = new Instructor()
            {
                Id = instructor.Id,
                HireDate = instructor.HireDate,
                Name = instructor.FirstName,
                LastName = instructor.LastName

            };
            return View(Modelinstructor);
        }

        // POST: InstructorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Instructor instructorModel)
        {
            try
            {
                var myModel = instructorModel;

                BLL.Dtos.InstructorRemoveDto instructorRemove = new BLL.Dtos.InstructorRemoveDto()
                {
                    Id = instructorModel.Id
                };

                _instructorService.DeleteInstructor(instructorRemove);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
