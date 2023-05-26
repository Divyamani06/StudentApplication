using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Model;
using StudentApplication.Service;

namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentSerivce _service;

        public StudentController(IStudentSerivce serivce)
        {
            _service= serivce;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentDetails(StudentModel student)
        {
            var result= await _service.AddStudentDetails(student);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentDetails()
        {
            var result=await _service.GetStudentDeatils();
            return Ok(result);
        }
        [HttpGet("Id")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result=await _service.GetStudentDetailesById(id);

            return Ok(result);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudentDetails(int id,StudentModel student)
        {
            var result = await _service.UpdateStudentDetails(id, student);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStudentDetails(int id)
        {
            var result= await _service.DeleteStudentDetails(id);
            return Ok(result);
        }
    }
}
