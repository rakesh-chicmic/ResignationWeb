using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using ResignationWeb.Models.DTOs;
using ResignationWeb.Models;
using ResignationWeb.Services.Contracts;
using System.Text.Json;

namespace ResignationWeb.Pages.ResignationApplications
{
    public class ResignationApplicationsBase : ComponentBase
    {
        public APIResponse response = new APIResponse();
        public List<ResignationDTO> resignation = new List<ResignationDTO>();
        [Inject]
        public IResignationService? resignationService { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        public string selectedStatus = "Pending";
        public Dictionary<string, string> selectedStatuses = new Dictionary<string, string>();
        public ResignationStatusDTO ResignStatusDTO = new ResignationStatusDTO();
        protected async override Task OnInitializedAsync()
        {
            response = await resignationService!.GetAsync();
            if (response == null)
            {
                Toast!.ShowInfo("No Resignation Found");
            }
            string responseData = JsonSerializer.Serialize(response!.Data);
            resignation = JsonSerializer.Deserialize<List<ResignationDTO>>(responseData)!;
            Console.WriteLine(resignation);
        }
        protected async void UpdateStatus_Click(ChangeEventArgs e,ResignationDTO resign)
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


