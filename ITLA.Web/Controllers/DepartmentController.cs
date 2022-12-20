using ITLA.BLL.Contracts;
using ITLA.BLL.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ITLA.BLL.Models;
using Department = ITLA.Web.Models.Department;

namespace ITLA.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        // GET: StudentController
        public ActionResult Index()
        {
            var departments = (List<BLL.Models.DepartmentModel>)_departmentService.GetAll().Data;

            var departmentModel = departments.Select(cd => new Models.Department()
            {

                DepartmentID = cd.DepartmentID,
                Name = cd.Name,
                Budget = cd.Budget,
                StartDate = cd.StartDate,
                Administrator = cd.Administrator,
                Deleted = cd.Deleted
            });

            return View(departmentModel);
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            var department = (DepartmentModel)_departmentService.GetById(id).Data;

            Department Modeldepartment = new Department()
            {
                DepartmentID = department.DepartmentID,
                StartDate = department.StartDate,
                Name = department.Name,
                Budget = department.Budget,
                Administrator = department.Administrator
            };
            return View(Modeldepartment);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department departmentModel)
        {
            try
            {
                DepartmentSaveDto savedepartmentDto = new DepartmentSaveDto()
                {
                    UserId = 1,
                    AuditDate = System.DateTime.Now,
                    Name = departmentModel.Name,
                    Budget = departmentModel.Budget,
                    StartDate = departmentModel.StartDate,
                    Administrator = departmentModel.Administrator
                
                };

                _departmentService.SaveDepartment(savedepartmentDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            var department = (DepartmentModel)_departmentService.GetById(id).Data;

            Department Modeldepartment= new Department()
            {
                DepartmentID = department.DepartmentID,
                Name = department.Name,
                Budget = department.Budget,
                StartDate = department.StartDate,
                Administrator = department.Administrator
            };
            return View(Modeldepartment);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Department departmentModel)
        {
            try
            {
                var myModel = departmentModel;

                DepartmentUpdateDto departmentUpdate = new DepartmentUpdateDto()
                {
                    Id = id,
                    AuditDate = System.DateTime.Now,
                    Name = departmentModel.Name,
                    Budget = departmentModel.Budget,
                    Administrator = departmentModel.Administrator,
                    StartDate = departmentModel.StartDate.Value,
                    UserId = 1
                };

                _departmentService.UpdateDepartment(departmentUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            var department = (DepartmentModel)_departmentService.GetById(id).Data;

            Department Modeldepartment = new Department()
            {
                DepartmentID = department.DepartmentID,
                StartDate = department.StartDate,
                Name = department.Name,
                Administrator = department.Administrator,
                Budget = department.Budget
            };
            return View(Modeldepartment);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Department departmentModel)
        {
            try
            {
                var myModel = departmentModel;

                BLL.Dtos.DepartmentRemoveDto departmentRemove = new BLL.Dtos.DepartmentRemoveDto()
                {
                    Id = id
                };

                _departmentService.RemoveDepartment(departmentRemove);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
