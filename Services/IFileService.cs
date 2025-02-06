using GADApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace GADApplication.Services
{
    public interface IFileService
    {
        Task<byte[]> GetFileDataAsync(int activityId, string fileType);
    }

    public class FileService : IFileService
    {
        private readonly ApplicationDbContext _context;

        public FileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GetFileDataAsync(int activityId, string fileType)
        {
            var activity = await _context.GARActivities.FirstOrDefaultAsync(a => a.Id == activityId);

            if (activity == null)
                return null;

            return fileType switch
            {
                "SpecialOrder" => activity.FileDataSpecialOrder,
                "Pictures" => activity.FileDataPictures,
                "BudgetReport" => activity.FileDataBudgetReport,
                "EvaluationAttendance" => activity.FileDataEvaluationAttendance,
                _ => null
            };
        }
    }
}
