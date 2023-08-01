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
        public List<ResignationDTO> resignation = new List<ResignationDTO>();
        [Inject]
        public IResignationService? resignationService { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        protected async override Task OnInitializedAsync()
        {
            response = await resignationService!.GetAsync();
            if (response == null)
            {
                Toast!.ShowInfo("No Resignation Found");
            }
            string responseData = JsonSerializer.Serialize(response!.Data);
            resignation = JsonSerializer.Deserialize<List<ResignationDTO>>(responseData)!;
        }
    }
}
