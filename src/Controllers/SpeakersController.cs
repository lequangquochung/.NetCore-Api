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
    public class SpeakersController : ControllerBase
    {
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;
        public SpeakersController(ICampRepository campRepository, IMapper mapper)
        {
            _campRepository = campRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpeakers()
        {
            try
            {
                var SpeakersList = await _campRepository.GetAllSpeakersAsync();

                if (!SpeakersList.Any())
                {
                    return NotFound();
                }

                return Ok(SpeakersList);
            }
            catch
            {
                return this.StatusCode(500, "Database Shutdown");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<SpeakersModel>> GetSpeaker(int speakerId)
        {
            try
            {
                var Speakers = await _campRepository.GetSpeakerAsync(speakerId);

                return _mapper.Map<SpeakersModel>(Speakers);
            }
            catch
            {
                return this.StatusCode(500, "Database Failure");
            }
        }
        [HttpGet("moniker")]
        public async Task<ActionResult<SpeakersModel[]>> GetSpeakersByMoniker(string moniker)
        {
            try
            {
                var Speakers = await _campRepository.GetSpeakersByMonikerAsync(moniker);

                return _mapper.Map<SpeakersModel[]>(Speakers);
            }
            catch
            {
                return this.StatusCode(500, "Database Failure");
            }
        }
    }
}