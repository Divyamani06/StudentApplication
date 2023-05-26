using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Model;

namespace StudentApplication.Service
{
    public class StudentSerivce : IStudentSerivce
    {
        private readonly ModelContext _context;

        public StudentSerivce(ModelContext context)
        {
            _context = context;

        }

        public async Task<StudentModel> AddStudentDetails(StudentModel student)
        {
            var details = new StudentModel()
            {
                Id = student.Id,
                Name = student.Name,
                DOB = student.DOB,
                Mark = student.Mark,
                Std = student.Std,
            };
            _context.Students.Add(details);
            await _context.SaveChangesAsync();
            return details;
        }
        public async Task<List<StudentModel>> GetStudentDeatils()
        {
            var details = await _context.Students.Select(x => new StudentModel()
            {
                Id = x.Id,
                Name = x.Name,
                DOB = x.DOB,
                Mark = x.Mark,
                Std = x.Std,
            }).ToListAsync();
            return details;
        }
        public async Task<StudentModel> GetStudentDetailesById(int Id)
        {
            var details = await _context.Students.Where(x => x.Id == Id).Select(x => new StudentModel()
            { 
                Id=x.Id,
                Name = x.Name,
                DOB = x.DOB,
                Mark = x.Mark,
                Std = x.Std,

            }).FirstOrDefaultAsync();
            return details;

        }
        public async Task<StudentModel> UpdateStudentDetails(int id,StudentModel student)
        {
            var details = await _context.Students.FindAsync(id);
            if (details != null)
            {
                details.Name = student.Name;
                details.DOB = student.DOB;
                details.Mark = student.Mark;
                details.Std = student.Std;
            }
            _context.Students.Update(details);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<StudentModel> DeleteStudentDetails(int id)
        {

            var details = await _context.Students.FindAsync(id);
            _context.Students.Remove(details);
            await _context.SaveChangesAsync();
            return details;
        }
    }
}
