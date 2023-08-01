using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using ResignationWeb.Models;
using ResignationWeb.Models.DTOs;
using ResignationWeb.Services.Contracts;
using System.Text.Json;
using System.Threading.Tasks;

namespace ResignationWeb.Pages.ApplyResignation
{
    public class ApplyResignationBase : ComponentBase
    {
        public APIResponse response = new APIResponse();
        public List<ResignationDTO> resignation = new List<ResignationDTO>();
        public ResignationRequestDTO resignRequest = new ResignationRequestDTO();
        [Inject]
        public IResignationService? resignationService { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        protected async override Task OnInitializedAsync()
        {
            response = await resignationService!.GetAsync();
            string responseData = JsonSerializer.Serialize(response!.Data);
            resignation = JsonSerializer.Deserialize<List<ResignationDTO>>(responseData)!;
           
        }
        protected async Task CreateResignation_Click(ResignationRequestDTO resignRequest)
        {
            try
            {
                response = await resignationService!.CreateAsync(resignRequest);
                if (response.Status)
                {
                    Toast!.ShowSuccess("Resignation Applied Successfully");
                }
                await OnInitializedAsync();
                StateHasChanged();

            }
            catch (Exception)
            {
                throw;
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
