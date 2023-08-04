using ResignationWeb.Models;
using ResignationWeb.Models.DTOs;

namespace ResignationWeb.Services.Contracts
{
    public interface IResignationService
    {
        Task<APIResponse> CreateAsync(ResignationRequestDTO resignRequest);
        Task<APIResponse> GetAsync(int? index, int? limit, string? resignId);
        Task<APIResponse> UpdateAsync(string resignId, ResignationRequestDTO resignUpdate);
        Task<APIResponse> UpdateStatusAsync(string resignId, ResignationStatusDTO resignStatus);
        Task<APIResponse> DeleteAsync(string resignId);
    }
}
