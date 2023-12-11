using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsApiController : ControllerBase
    {
        [HttpGet]
        public string GetAgents()
        {
            return "List Of Agents";
        }

        [HttpGet("{id}")]
        public string GetAgent(int id)
        {
            return "Single Agent";
        }
    }
}