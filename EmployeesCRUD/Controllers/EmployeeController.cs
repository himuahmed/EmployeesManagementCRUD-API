using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using EmployeesCRUD.Helpers;
using EmployeesCRUD.Models;
using EmployeesCRUD.Repository;

namespace EmployeesCRUD.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [Route("")]
        public async Task<IHttpActionResult> GetAllEmployees([FromUri]UserParams userParams)
        {
            if (userParams == null)
            {
                var userParams1 = new UserParams()
                {
                    PageSize = 5,
                    PageNumber = 1
                };
                userParams = userParams1;
            }

            var employees = await _employeeRepository.GetAllEmployees(userParams);
            
            if (employees != null)
            {
                return Ok(new { employees.TotalPage, employees.TotalCount, employees.CurrentPage, employees.PageSize, employees });
            }
            throw new Exception("Couldn't fetch employees.");
        }



        [Route("getemployee/{id:int}")]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            var empployee = await _employeeRepository.GetEmployee(id);

            if (empployee != null)
            {
                return Ok(empployee);
            }
            throw new Exception("Couldn't get the employee details.");
        }


        [HttpPost]
        [Route("addemployee")]
        public async Task<IHttpActionResult> AddEmployee(EmployeeModel employeeModel)
        {
            if(ModelState.IsValid)
            {
                var addedEmployee = await _employeeRepository.AddEmployee(employeeModel);
                if (addedEmployee)
                {
                    return Ok();
                }
                throw new Exception("Couldn't add user.");
            }
            throw new Exception("Invalid information.");

        }


        [HttpPut]
        [Route("updateemployee/{id:int}")]
        public async Task<IHttpActionResult> UpdateEmployee(int id, [FromBody] EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                var updateEmployee = await _employeeRepository.UpdateEmployee(id, employeeModel);
                if (updateEmployee)
                {
                    return Ok();
                }
                throw new Exception("Couldn't update employee details.");
            }
            throw new Exception("Couldn't update employee details.");
        }


        [HttpDelete]
        [Route("deleteemployee/{id:int}")]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            var deleteEmployee = await _employeeRepository.DeleteEmployeeById(id);
            if (deleteEmployee)
            {
                return Ok();
            }
            throw new Exception("Couldn't delete employee.");
        }


        [HttpDelete]
        [Route("deleteemployees")]
        public async Task<IHttpActionResult> DeleteMultipleEmployees([FromUri] int[] ids)
        {
            var deleteEmployees = await _employeeRepository.DeleteMultipleEmployee(ids);
            if (deleteEmployees)
            {
                return Ok();
            }
            throw new Exception("Couldn't delete records.");
        }


    }
}
