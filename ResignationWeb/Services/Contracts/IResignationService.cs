using ResignationWeb.Models;
using ResignationWeb.Models.DTOs;

namespace ResignationWeb.Services.Contracts
{
    public interface IResignationService
    {
        Task<APIResponse> CreateAsync(ResignationRequestDTO resignRequest);
        Task<APIResponse> GetAsync(int? limit, int? index, string? sortKey, int? sortDirection, string? id, int? status, string? userId);
        Task<APIResponse> UpdateAsync(string resignId, ResignationRequestDTO resignUpdate);
        Task<APIResponse> UpdateStatusAsync(string resignId, ResignationStatusDTO resignStatus);
        Task<APIResponse> DeleteAsync(string resignId);
    }
}
