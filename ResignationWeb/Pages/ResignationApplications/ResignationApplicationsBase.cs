using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using ResignationWeb.Models.DTOs;
using ResignationWeb.Models;
using ResignationWeb.Services.Contracts;
using System.Text.Json;
using System.Collections.Generic;
using System;
using Microsoft.JSInterop;

namespace ResignationWeb.Pages.ResignationApplications
{
    public class ResignationApplicationsBase : ComponentBase
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
        public string selectedStatus = "Pending";
        public Dictionary<string, string> selectedStatuses = new Dictionary<string, string>();
        public ResignationStatusDTO ResignStatusDTO = new ResignationStatusDTO();
        protected async override Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected async Task LoadData()
        {
           
            response = await resignationService!.GetAsync(pagination.ItemsPerPage, pagination.CurrentPage, null , 1, null , 1 , null);
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
        protected async void UpdateStatus_Click(ChangeEventArgs e, ResignationWithUser resign)
        {
            try
             {
                var selectedStatusValue = e.Value.ToString();
                switch (selectedStatusValue)
                {
                    case "1":
                        ResignStatusDTO.Status = 1;
                        break;
                    case "2":
                        ResignStatusDTO.Status = 2;
                        break;
                    case "3":
                        ResignStatusDTO.Status = 3;
                        break;
                    case "4":
                        ResignStatusDTO.Status = 4;
                        break;
                    default:
                        ResignStatusDTO.Status = 1;
                        break;
                }
                if(ResignStatusDTO.Status == 1)
                {
                    return;
                }
                ResignStatusDTO.RelievingDate = resign.relievingDate;
                response = await resignationService!.UpdateStatusAsync(resign.id, ResignStatusDTO);
                if (response.Status)
                {
                    Toast!.ShowSuccess("Resignation status Updated");
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


