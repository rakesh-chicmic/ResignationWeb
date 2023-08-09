using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using ResignationWeb.Models;
using ResignationWeb.Models.DTOs;
using ResignationWeb.Services.Contracts;
using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using BlazorBootstrap;
using Microsoft.JSInterop;

namespace ResignationWeb.Pages.ApplyResignation
{
    public class ApplyResignationBase : ComponentBase
    {
        public APIResponse response = new APIResponse();
        public DataList dataList = new DataList();
        public List<ResignationWithUser> resignation = new List<ResignationWithUser>();
        public ResignationRequestDTO resignRequest = new ResignationRequestDTO() { ResignationDate= DateTime.Now };
        [Inject]
        public IResignationService? resignationService { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        [Inject]
        public IJSRuntime jSRuntime { get; set; }
        private string DeleteCategoryId { get; set; } = null!;
        protected async override Task OnInitializedAsync()
        {
            await LoadData();

        }

        protected async Task LoadData()
        {
            response = await resignationService!.GetAsync(null, null, null, 1, null, null, null);
            string responseData = JsonSerializer.Serialize(response!.Data);
            dataList = JsonSerializer.Deserialize<DataList>(responseData)!;
            string data = JsonSerializer.Serialize(dataList.data);
            resignation = JsonSerializer.Deserialize<List<ResignationWithUser>>(data)!;
            StateHasChanged();
        }
        protected async Task CreateResignation_Click(ResignationRequestDTO resignRequest)
        {
            try
            {
                response = await resignationService!.CreateAsync(resignRequest);
                if (response.Status)
                {
                    Toast!.ShowSuccess("Resignation Applied Successfully");
                    //await SweetAlert();
                }
                await OnInitializedAsync();
                StateHasChanged();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void HandleDelete(string id)
        {
            DeleteCategoryId = id;
            jSRuntime.InvokeVoidAsync("ShowDeleteConfirmationModal");
        }

        private async Task SweetAlert()
        {
            await jSRuntime.InvokeVoidAsync("Toaster", "success", "Resignation Applied successfully!");
        }

        protected async Task ConfirmDelete_Click(bool isConfirmed)
        {

            if (isConfirmed && DeleteCategoryId != null)
            {
                //delete
                try
                {
                    response = await resignationService!.DeleteAsync(DeleteCategoryId);
                    if (response.Status)
                    {
                        Toast!.ShowSuccess("Resignation Withdrawn Successfully");
                    }
                    await jSRuntime.InvokeVoidAsync("HideDeleteConfirmationModal");
                    await OnInitializedAsync();
                    StateHasChanged();

                }
                catch (Exception)
                {
                    throw;
                }                
                
            }

        }
        protected async Task WithdrawResignation_Click(string id)
        {
            try
            {
                response = await resignationService!.DeleteAsync(id);
                if (response.Status)
                {
                    Toast!.ShowSuccess("Resignation Withdrawn Successfully");
                }
                await OnInitializedAsync();
                StateHasChanged();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
