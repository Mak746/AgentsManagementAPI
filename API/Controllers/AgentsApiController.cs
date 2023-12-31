using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using API.Errors;
using Microsoft.AspNetCore.Authorization;
namespace API.Controllers
{

    public class AgentsApiController : BaseApiController
    {
        private readonly IAgentService _agentService;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;


        public AgentsApiController(IAgentService agentService, IMapper mapper, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _agentService = agentService;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize]
        [HttpGet("getAllAgents")]
        public async Task<IActionResult> GetAllAgentsAsync()
        {
            try
            {
                var agentsList = await _agentService.ListAllAgentsAsync();

                return Ok(agentsList);
            }
            catch (Exception ex)
            {


                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }


        }


        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSingleAgent(Guid id)
        {
            try
            {
                var singleAgent = await _agentService.GetAgentByIdAsync(id);


                if (singleAgent == null) return NotFound(new ApiResponse(404));

                return Ok(singleAgent);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }

        [Authorize]
        [Consumes("multipart/form-data")]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AgentsDto>> CreateAgentAsync([FromForm] AgentsDto agent)
        {
            try
            {
                if (agent.File != null && agent.FileImage != null)
                {

                    var fileName = Path.GetFileName(agent.File.FileName);
                    var fileNameIImage = Path.GetFileName(agent.FileImage.FileName);

                    string ext = Path.GetExtension(agent.File.FileName);
                    string extImage = Path.GetExtension(agent.FileImage.FileName);
                    if (ext.ToLower() != ".pdf" && (extImage.ToLower() != ".png" && extImage.ToLower() != ".jpg"))
                    {
                        return BadRequest(new ApiResponse(400, "The file is not of type pdf & the image is not type of PNG or JPG"));

                    }
                    if (ext.ToLower() != ".pdf")
                    {
                        //return BadRequest("The file is not of type pdf");
                        return BadRequest(new ApiResponse(400, "The file is not of type pdf."));

                    }
                    if (extImage.ToLower() != ".png" && extImage.ToLower() != ".jpg")
                    {

                        //return BadRequest("The image is not of type PNG or JPG");
                        return BadRequest(new ApiResponse(400, "The image is not type of PNG or JPG."));


                    }


                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "licenseFiles", fileName);
                    var filePathImage = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileNameIImage);


                    using (FileStream filestream = System.IO.File.Create(filePath))
                    {
                        agent.File.CopyTo(filestream);
                        filestream.Flush();

                    }
                    using (FileStream filestream = System.IO.File.Create(filePathImage))
                    {
                        agent.FileImage.CopyTo(filestream);
                        filestream.Flush();

                    }


                    var fromDto = _mapper.Map<Agent>(agent);
                    fromDto.LicenseAttachment = GetBinaryFile(filePath);
                    fromDto.AgentPhoto = GetBinaryFile(filePathImage);

                    fromDto.LicenseAttachmentPath = filePath;
                    fromDto.AgentPhotoPath = filePathImage;
                    //
                    agent.LicenseAttachment = GetBinaryFile(filePath);
                    agent.AgentPhoto = GetBinaryFile(filePathImage);

                    agent.LicenseAttachmentPath = filePath;
                    agent.AgentPhotoPath = filePathImage;
                    fromDto.Id = Guid.NewGuid();

                    agent.Id = fromDto.Id;

                    var result = await _agentService.AddAgent(fromDto);
                }
                else
                {
                    return BadRequest(new ApiResponse(400, "Either the file or the image is required!"));

                }


                return Ok(agent);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }
        private byte[] GetBinaryFile(string filename)
        {

            byte[] bytes;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
            }
            return bytes;



        }

        [Authorize]
        [Consumes("multipart/form-data")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AgentsDto>> UpdateAgentAsync([FromForm] AgentsDto agent)
        {
            try
            {
                var singleAgent = await _agentService.GetAgentByIdAsync(agent.Id);

                if (singleAgent == null) return NotFound(new ApiResponse(404));

                if (agent.File != null && agent.FileImage != null)
                {

                    var fileName = Path.GetFileName(agent.File.FileName);
                    var fileNameIImage = Path.GetFileName(agent.FileImage.FileName);

                    string ext = Path.GetExtension(agent.File.FileName);
                    string extImage = Path.GetExtension(agent.FileImage.FileName);
                    if (ext.ToLower() != ".pdf" && (extImage.ToLower() != ".png" && extImage.ToLower() != ".jpg"))
                    {
                        return BadRequest(new ApiResponse(400, "The file is not of type pdf & the image is not of type PNG or JPG"));

                    }
                    if (ext.ToLower() != ".pdf")
                    {
                        //return BadRequest("The file is not of type pdf");
                        return BadRequest(new ApiResponse(400, "The file is not of type pdf."));

                    }
                    if (extImage.ToLower() != ".png" && extImage.ToLower() != ".jpg")
                    {

                        //return BadRequest("The image is not of type PNG or JPG");
                        return BadRequest(new ApiResponse(400, "The image is not type of PNG or JPG."));


                    }
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "licenseFiles", fileName);
                    var filePathImage = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileNameIImage);
                    using (FileStream filestream = System.IO.File.Create(filePath))
                    {
                        agent.File.CopyTo(filestream);
                        filestream.Flush();

                    }
                    using (FileStream filestream = System.IO.File.Create(filePathImage))
                    {
                        agent.FileImage.CopyTo(filestream);
                        filestream.Flush();

                    }


                    var fromDto = _mapper.Map<Agent>(agent);
                    fromDto.LicenseAttachment = GetBinaryFile(filePath);
                    fromDto.AgentPhoto = GetBinaryFile(filePathImage);

                    fromDto.LicenseAttachmentPath = filePath;
                    fromDto.AgentPhotoPath = filePathImage;
                    //
                    agent.LicenseAttachment = GetBinaryFile(filePath);
                    agent.AgentPhoto = GetBinaryFile(filePathImage);

                    agent.LicenseAttachmentPath = filePath;
                    agent.AgentPhotoPath = filePathImage;

                    var result = await _agentService.UpdateAgent(fromDto);
                }
                else
                {
                    return BadRequest(new ApiResponse(400, "Either the file or the image is required!"));

                    // return BadRequest("Either the file or the image is required!");

                }

                return Ok(agent);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteAgent(Guid id)
        {
            try
            {
                var singleAgent = await _agentService.GetAgentByIdAsync(id);

                if (singleAgent == null) return NotFound(new ApiResponse(404));

                var deletedAgent = await _agentService.DeleteAgent(id);
                if (deletedAgent == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }


        }

    }

}