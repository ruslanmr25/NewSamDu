using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewSamDU.Application.DTOs.MenuDTO;
using NewSamDU.Application.DTOs.Queries;
using NewSamDU.Application.Responses;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;
using NewSamDU.Infrastructure.Repositories;

namespace NewSamDU.Api.Controllers
{
    [Route("api/menus")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        protected readonly MenuRepository menuRepository;

        protected IMapper mapper;

        public MenuController(MenuRepository menuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] MenuQuery query)
        {
            List<Menu> menus = await menuRepository.GetAllAsync(query);
            return Ok(new Response<List<Menu>>(menus));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateMenuDTO dto)
        {
            var menu = mapper.Map<Menu>(dto);

            var entity = await menuRepository.CreateAsync(menu);
            return Ok(new Response<Menu>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateMenuDTO dto)
        {
            var menu = await menuRepository.GetAsync(id);

            if (menu is null)
            {
                return NotFound();
            }

            mapper.Map(dto, menu);

            var entity = await menuRepository.UpdateAsync(menu);
            return Ok(new Response<Menu>(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Menu? menu = await menuRepository.GetAsync(id);

            if (menu is null)
            {
                return NotFound();
            }

            await menuRepository.DeleteAsync(menu);

            return Ok(new Response());
        }
    }
}
