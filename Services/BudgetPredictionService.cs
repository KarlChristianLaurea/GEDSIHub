// Services/BudgetPredictionService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GADApplication.Services
{
    public class BudgetPredictionService : IBudgetPredictionService
    {
        private readonly HttpClient _httpClient;

        public BudgetPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method for Budget Prediction
        public async Task<double?> PredictBudgetAsync(string objectiveText, double budgetLimit)
        {
            var requestData = new
            {
                objective = objectiveText,
                budget_limit = budgetLimit
            };

            string flaskApiUrl = "https://mandatebudget-app.azurewebsites.net/predict_budget";

            var response = await _httpClient.PostAsJsonAsync(flaskApiUrl, requestData);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                if (result != null && result.averaged_budget != null)
                {
                    double averagedBudget = result.averaged_budget;
                    return Math.Round(averagedBudget, 2); // Format to two decimal places
                }
            }
            return null;
        }

        // Method for Mandate Suggestion
        public async Task<string> SuggestMandateAsync(string text)
        {
            var requestData = new { text = text };

            string flaskApiUrl = "https://mandatebudget-app.azurewebsites.net/predict";

            var response = await _httpClient.PostAsJsonAsync(flaskApiUrl, requestData);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                string predictedRas = result?.prediction; // Assuming the Flask API returns a single string for prediction
                return predictedRas;
            }
            return null;
        }
    }
}
