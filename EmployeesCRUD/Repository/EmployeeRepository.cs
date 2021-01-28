using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EmployeesCRUD.Helpers;
using EmployeesCRUD.Models;

namespace EmployeesCRUD.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        readonly employeesDBEntities  emp = new employeesDBEntities();
        public async Task<PagedList<EmployeeModel>> GetAllEmployees(UserParams userParams)
        {
            var tbl_employees =  emp.tbl_employee.Select(e=>new EmployeeModel()
            {
                Id = e.Id,
                Name = e.employee_name,
                Email = e.employee_email,
                Address = e.employee_address,
                Phone = e.employee_phoneNo
            });

            return await PagedList<EmployeeModel>.CreateAsync(tbl_employees, userParams.PageNumber, userParams.PageSize);

        }


        public async Task<EmployeeModel> GetEmployee(int id)
        {
            var employee = await emp.tbl_employee.Where(e => e.Id == id).Select(emp => new EmployeeModel()
            {
                Id = emp.Id,
                Name = emp.employee_name,
                Email = emp.employee_email,
                Address = emp.employee_address,
                Phone = emp.employee_phoneNo
            }).FirstOrDefaultAsync();

            return employee;
        }


        public async Task<bool> AddEmployee(EmployeeModel employeeModel)
        {
            var employee = new tbl_employee()
            {
                employee_name = employeeModel.Name,
                employee_email = employeeModel.Email,
                employee_address = employeeModel.Address,
                employee_phoneNo = employeeModel.Phone
            };

            emp.tbl_employee.Add(employee);
            return await emp.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteEmployeeById(int id)
        {
            var employee = await emp.tbl_employee.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
            {
                emp.tbl_employee.Remove(employee);
                return await emp.SaveChangesAsync() > 0;
            }

            return false;
        }


        public async Task<bool> UpdateEmployee(int id, EmployeeModel employeeModel)
        {
            var employee = await emp.tbl_employee.FirstOrDefaultAsync(e => e.Id == id);

            if (employee != null)
            {
                employee.employee_name = employeeModel.Name;
                employee.employee_email = employeeModel.Email;
                employee.employee_address = employeeModel.Address;
                employee.employee_phoneNo = employeeModel.Phone;

                return await emp.SaveChangesAsync()>0;
            }

            return false;

        }

        public async Task<bool> DeleteMultipleEmployee(int[] ids)
        {
            var batchDelete = await emp.tbl_employee.Where(e => ids.Contains(e.Id)).ToListAsync();
            foreach (var entity in batchDelete)
            {
                foreach (var id in ids)
                {
                     if (entity.Id == id)
                     {
                        emp.tbl_employee.Remove(entity);
                     }
                }
            }

            return await emp.SaveChangesAsync() > 0;
        }
        
    }
}