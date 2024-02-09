namespace EmployeeMgmt.Models
{
    public class Employee
    {

        private static int _GlobalEmpID ;
        public string Name { get; set; }

        public string UserName { get; set; } 
        public string Password { get; set; } 
        public int ID { get; set; }
        public string Role { get; set; }
        public int ManagerID { get; set; }

        public Employee(string name, string role , int ManagerID)
        {
            this.Name = name;
            _GlobalEmpID++;
            this.ID = _GlobalEmpID;
            this.Role = role;
            this.ManagerID = ManagerID;
            this.Password = "pass";
            this.UserName = name;
        }
    }
}
