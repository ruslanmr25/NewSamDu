using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewSamDU.Application.DTOs.ProjectsDTO;
using NewSamDU.Application.Responses;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;
using NewSamDU.Infrastructure.Repositories;

namespace NewSamDU.Api.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ExternalProjectController : ControllerBase
    {
        protected readonly ExternalProjectRepository projectRepository;

        public ExternalProjectController(ExternalProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjectAsync()
        {
            PaginatedResult<ExternalProject> projects = await projectRepository.GetAllAsync();
            return Ok(new Response<PaginatedResult<ExternalProject>>(projects));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProjectDTO dto)
        {
            return Ok();
        }
    }
}
