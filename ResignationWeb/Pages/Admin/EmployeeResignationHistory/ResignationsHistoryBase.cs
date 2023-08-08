using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        public DataList dataList = new DataList();
        public PaginationModel pagination = new PaginationModel();
        public List<ResignationWithUser> resignation = new List<ResignationWithUser>();
        [Inject]
        public IResignationService? resignationService { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        [Inject]
        public IJSRuntime jSRuntime { get; set; }
        protected async override Task OnInitializedAsync()
        {
             await LoadData();
        }

        protected async Task LoadData()
        {
            response = await resignationService!.GetAsync(pagination.ItemsPerPage,pagination.CurrentPage, null, 1, null, null, null);
            if (response == null)
            {
                Toast!.ShowInfo("No Resignation Found");
            }
            string responseData = JsonSerializer.Serialize(response!.Data);
            dataList = JsonSerializer.Deserialize<DataList>(responseData)!;
            string data = JsonSerializer.Serialize(dataList.data);
            resignation = JsonSerializer.Deserialize<List<ResignationWithUser>>(data)!;
            pagination.TotalItems = dataList.totalCount;
            StateHasChanged();
        }

        public void ShowDetails()
        {
            jSRuntime.InvokeVoidAsync("ShowDetailsModal");
        }
    }
}
