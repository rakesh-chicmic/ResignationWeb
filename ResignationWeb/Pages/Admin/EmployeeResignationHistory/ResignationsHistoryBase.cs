using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using ResignationWeb.Models;
using ResignationWeb.Models.DTOs;
using ResignationWeb.Services.Contracts;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ResignationWeb.Pages.Admin.EmployeeResignationHistory
{
    public class ResignationsHistoryBase : ComponentBase
    {
        public APIResponse response = new APIResponse();
        public List<ResignationWithUser> resignation = new List<ResignationWithUser>();
        [Inject]
        public IResignationService? resignationService { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        public int index = 1;
        public int limit = 1;
        protected async override Task OnInitializedAsync()
        {
             await LoadData();
        }

        protected async Task LoadData()
        {
            response = await resignationService!.GetAsync(index, limit, "");
            if (response == null)
            {
                Toast!.ShowInfo("No Resignation Found");
            }
            string responseData = JsonSerializer.Serialize(response!.Data);
            resignation = JsonSerializer.Deserialize<List<ResignationWithUser>>(responseData)!;
            StateHasChanged();
            Console.WriteLine(resignation.Count);
        }
    }
}
