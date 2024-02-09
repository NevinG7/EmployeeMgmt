namespace EmployeeMgmt.Models
{
    public class LeaveRequest
    {

        private static int _GlobalLeaveID;

        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public int EmployeeManagerID { get; set; }
        public string Status { get; set; }

        public LeaveRequest(int EmployeeID, int EmployeeManagerID) {

            _GlobalLeaveID++;
            this.Id = _GlobalLeaveID;
            this.EmployeeID = EmployeeID;
            this.EmployeeManagerID = EmployeeManagerID;
            this.Status = "Pending";

        }



    }
}
