using StudentApplication.Model;

namespace StudentApplication.Service
{
    public interface IStudentSerivce
    {
        Task<StudentModel> AddStudentDetails(StudentModel student);
        Task<StudentModel> DeleteStudentDetails(int id);
        Task<List<StudentModel>> GetStudentDeatils();
        Task<StudentModel> GetStudentDetailesById(int Id);
        Task<StudentModel> UpdateStudentDetails(int id, StudentModel student);
    }
}