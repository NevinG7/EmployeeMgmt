using EmployeeMgmt.Data;
using EmployeeMgmt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeMgmt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private DataStore _DataStore;
        public EmployeeController(DataStore dataStore) {
            _DataStore = dataStore;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var LoginEmp = _DataStore.GetByLoginDetails(loginRequest);
            if (LoginEmp == null)
            {
                return Unauthorized("Invaid Username or Password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,LoginEmp.ID.ToString() ),
                new Claim(ClaimTypes.Name , LoginEmp.UserName.ToString()),
                new Claim(ClaimTypes.Role , LoginEmp.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
           
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok("Logged In Successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult getEmployees()
        {
            var Employees=_DataStore.GetAllEmployees();

            return Ok(Employees);


        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public IActionResult GetEmployeeById(int id) {
            var emp= _DataStore.GetByEmployeeId(id);
            return Ok(emp);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult AddEmployee([FromBody] AddEmployeeDTO data) {
            var NewEmp = new Employee(data.name, data.role, data.ManagerID);
            _DataStore.AddEmployee(NewEmp);
            return Ok(NewEmp);
        }

        [HttpGet]
        [Authorize(Roles ="Manager")]
        [Route("Reportee")]
        public IActionResult GetReportee()
        {
            var UserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            var emp = _DataStore.GetReportee(Convert.ToInt32(UserIdClaim.Value));

            return Ok(emp);

        }


        [HttpGet]
        [Route("leave")]
        [Authorize]
        public IActionResult showleaves()
        {
            var UserRoleClaim = User.FindFirst(ClaimTypes.Role);
            if (UserRoleClaim.Value == "Employee") {
                var UserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                

            }

            var leaves = _DataStore.GetAllLeaves();
            return Ok(leaves);
        }

        [HttpPost]
        [Route("leave")]
        [Authorize]
        public IActionResult AddLeave()
        {
            var UserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            var emp = _DataStore.GetByEmployeeId(Convert.ToInt32(UserIdClaim.Value));


            var leave = new LeaveRequest(emp.ID, emp.ManagerID);
            _DataStore.AddLeave(leave);
            return Ok(leave);
        }

        [HttpPost]
        [Route("leave/Approve/{id}")]
        [Authorize(Roles ="Manager")]
        public IActionResult ApproveLeave(int id)
        {
            _DataStore.ApproveLeave(id);
            return Ok(_DataStore.GetLeaveById(id));
            
        }

        [HttpPost]
        [Route("leave/Reject/{id}")]
        [Authorize(Roles = "Manager")]
        public IActionResult RejectLeave(int id)
        {
            _DataStore.ApproveLeave(id);
            return Ok(_DataStore.GetLeaveById(id));

        }

    }
}
