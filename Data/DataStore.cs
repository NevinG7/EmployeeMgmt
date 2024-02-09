using EmployeeMgmt.Models;

namespace EmployeeMgmt.Data
{
    public class DataStore
    {
        public List<Employee> Employees { get; set; }

        public List<LeaveRequest> LeaveRequests { get; set; }

        public DataStore() {

            Employees = new List<Employee>();
            LeaveRequests = new List<LeaveRequest>();

            Employee employee1 = new Employee("a", "Manager", 0); //1
            Employee employee2 = new Employee("Jane Smith", "Manager", 1); //2
            Employee employee3 = new Employee("Alice Johnson", "Employee", 1);//3
            Employee employee4 = new Employee("Bob Brown", "Employee", 2);//4
            Employee employee5 = new Employee("Emma Davis", "Employee", 2);//5
            Employee employee6 = new Employee("Michael Wilson", "Employee", 1);//6
            Employees.Add(employee1);
            Employees.Add(employee2);
            Employees.Add(employee3);
            Employees.Add(employee4);
            Employees.Add(employee5);
            Employees.Add(employee6);    
        }


        public Employee GetByLoginDetails(LoginRequest loginRequest)
        {
          var employee =  Employees.Find(emp=> emp.UserName==loginRequest.UserName && emp.Password==loginRequest.Password);
          return employee;
        }
        public List<Employee> GetReportee(int ManagerID) {
           var Found= Employees.FindAll(emp => emp.ManagerID == ManagerID);

            return Found;

        }

        public List<Employee> GetAllEmployees()
        {
            return Employees;
        }

        public Employee GetByEmployeeId(int Id)
        {
            var Emp=Employees.Find(emp=>emp.ID == Id);
            return Emp;
        }

        public void AddEmployee(Employee emp)
        {
            Employees.Add(emp);
        }

        public List<LeaveRequest> GetAllLeaves()
        {
            return LeaveRequests;
        }

        public void AddLeave(LeaveRequest leave)
        {
            LeaveRequests.Add(leave);
        }

        public LeaveRequest GetLeaveById(int Id)
        {
            return LeaveRequests.Find(leave => leave.Id==Id);
        }

        public void ApproveLeave(int id)
        {
            var leave =GetLeaveById(id);

            leave.Status = "Approved";
        }

        public void RejectLeave(int id)
        {
            var leave = GetLeaveById(id);

            leave.Status = "Rejected";
        }
    }
}
