using System.Collections.Generic;
using SamplePrism.Domain.Models.Tickets;

namespace SamplePrism.Services
{
    public interface IDepartmentService 
    {
        Department GetDepartment(int id);
        IEnumerable<Department> GetDepartments();
        void UpdatePriceTag(string departmentName,string priceTag);
        void ResetCache();
    }
}
