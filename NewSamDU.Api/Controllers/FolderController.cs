using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewSamDU.Api.Helpers;
using NewSamDU.Application.DTOs.FolderDTO;
using NewSamDU.Application.DTOs.QueryDto;
using NewSamDU.Application.Responses;

namespace NewSamDU.Api.Controllers;

[Route("api/folders")]
[ApiController]
[Authorize(Roles = "Admin,Manager")]
public class FolderController : ControllerBase
{
    protected readonly FolderHelper folderHelper;

    public FolderController(FolderHelper folderHelper)
    {
        this.folderHelper = folderHelper;
    }

    [HttpGet]
    public IActionResult GetFolders([FromQuery] FolderQuery query)
    {
        var folders = folderHelper.GetFolders(query.Path);
        return Ok(new Response<object>(new { Folders = folders }));
    }

    [HttpPost]
    public IActionResult CreateFolder(CreateFolderDTO dto)
    {
        folderHelper.CreateFolder(dto.Path, dto.FolderName);
        return Ok(new Response());
    }

    [HttpPut]
    public IActionResult RenameFolder(RenameFolderDTO dto)
    {
        folderHelper.RenameFolder(dto.Path, dto.OldName, dto.NewName);
        return Ok(new Response());
    }

    [HttpDelete]
    public IActionResult DeleteFolder(DeleteFolderDTO dto)
    {
        folderHelper.DeleteFolder(dto.Path);
        return Ok(new Response());
    }
}
