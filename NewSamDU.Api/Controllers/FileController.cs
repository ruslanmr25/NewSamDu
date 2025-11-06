using Microsoft.AspNetCore.Mvc;
using NewSamDU.Api.Helpers;
using NewSamDU.Application.DTOs.FileDTO;
using NewSamDU.Application.DTOs.QueryDto;
using NewSamDU.Application.Responses;

namespace NewSamDU.Api.Controllers;

[Route("api/files")]
[ApiController]
public class FileController : ControllerBase
{
    protected readonly FileHelper fileHelper;

    public FileController(FileHelper fileHelper)
    {
        this.fileHelper = fileHelper;
    }

    [HttpGet]
    public IActionResult GetFiles([FromQuery] FolderQuery query)
    {
        var files = fileHelper.GetFiles(query.Path);
        return Ok(new Response<List<GetFileDTO>>(files));
    }

    [HttpPost]
    public async Task<IActionResult> CreateFile([FromForm] CreateFileDTO dto)
    {
        await fileHelper.UploadFile(dto.File, dto.Path);
        return Ok(new Response());
    }

    [HttpDelete]
    public IActionResult DeleteFile(DeleteFileDTO dto)
    {
        fileHelper.DeleteFile(dto.Path);
        return Ok(new Response());
    }
}
