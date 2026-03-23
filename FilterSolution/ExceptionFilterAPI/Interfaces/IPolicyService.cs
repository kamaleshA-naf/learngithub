using ExceptionFilterAPI.Models.DTOs;

namespace ExceptionFilterAPI.Interfaces
{
    public interface IPolicyService
    {
        public Task<AddPolicyResponseDto> AddPolicyAsync(AddPolicyRequestDto policy);
    }
}
