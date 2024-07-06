using FileDataApi.Models;
using FileDataApi.Requests;
using FileDataApi.Responses;
using FileDataApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesDataController : ControllerBase
    {
        private readonly ISortService sortService;
        private readonly IFileService fileService;

        public FilesDataController(ISortService sortService, IFileService fileService)
        {
            this.sortService = sortService;
            this.fileService = fileService;
        }

        [HttpPost("UploadFiles")]
        public IActionResult UploadFiles([FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            try
            {
                ProcessedFilesResponse response = fileService.ProcessFiles(files);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("SortData")]
        public IActionResult SortDataAsync([FromBody] SortCompaniesRequest request)
        {
            try
            {
                if (sortOptions == null || companies == null || companies.Count == 0)
                {
                    return BadRequest("Invalid request: Missing companies list or sort options.");
                }

                List<Company> sortedCompanies = sortService.SortCompanies(companies, sortOptions);

                return Ok(sortedCompanies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
