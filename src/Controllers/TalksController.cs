using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class TalksController : ControllerBase
    {
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;

        public TalksController(ICampRepository campRepository, IMapper mapper)
        {
            _campRepository = campRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TalksModel[]>> Get(string moniker, bool includeSpeakers = false)
        {
            try
            {
                var listTalks = await _campRepository.GetTalksByMonikerAsync(moniker, includeSpeakers);

                TalksModel[] talksModels = _mapper.Map<TalksModel[]>(listTalks);

                return talksModels;
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Shutdown");
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> Get(string moniker, int talkId, bool includeSpeakers = false)
        {
            try
            {
                var listTalks = await _campRepository.GetTalkByMonikerAsync(moniker, talkId, includeSpeakers);

                TalksModel talksModels = _mapper.Map<TalksModel>(listTalks);

                return Ok(talksModels);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Shutdown");
            }
        }

    }
}