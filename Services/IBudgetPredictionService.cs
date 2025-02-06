// Services/IBudgetPredictionService.cs
using System.Threading.Tasks;

namespace GADApplication.Services
{
    public interface IBudgetPredictionService
    {
        Task<double?> PredictBudgetAsync(string objectiveText, double budgetLimit);
        Task<string> SuggestMandateAsync(string text); // Updated to return a single string
    }
}
