using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using EmployeesCRUD.Helpers;
using EmployeesCRUD.Models;

namespace EmployeesCRUD.Repository
{
    public interface IEmployeeRepository
    {
        Task<PagedList<EmployeeModel>> GetAllEmployees(UserParams userParams);
        Task<EmployeeModel> GetEmployee(int id);
        Task<bool> AddEmployee(EmployeeModel employeeModel);
        Task<bool> UpdateEmployee(int id, [FromBody] EmployeeModel employeeModel);
        Task<bool> DeleteEmployeeById(int id);
        Task<bool> DeleteMultipleEmployee(int[] ids);
    }
}