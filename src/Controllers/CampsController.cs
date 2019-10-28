using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using AutoMapper;
using CoreCodeCamp.Model;

namespace CoreCodeCamp.Controller
{
    [Route("api/[controller]")]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository _Camprepository;
        private readonly IMapper _mapper;

        public CampsController(ICampRepository _camprepository, IMapper mapper)
        {
            _Camprepository = _camprepository;
            _mapper = mapper;
        }

        [HttpGet]           
        public async Task<ActionResult<CampModels[]>> GetCamps(bool includeTalk = false)
        {
            try
            {
                var listCamps = await _Camprepository.GetAllCampsAsync(includeTalk);

                CampModels[] campModels = _mapper.Map<CampModels[]>(listCamps);

                return campModels;
            }
            catch (Exception)
            {
                return this.StatusCode(500,"Database Shutdown");
            }
        }

        //[HttpGet("getspeaker")]
        //public async Task<ActionResult<TalkModel[]>> GetTalk(bool includeSpeak = false)
        //{
        //    try
        //    {
        //        var listTalks = await _Camprepository.GetAllCampsAsync(includeSpeak);

        //        TalkModel[] talkModels = _mapper.Map<TalkModel[]>(listTalks);

        //        return talkModels;
        //    }
        //    catch (Exception)
        //    {
        //        return this.StatusCode(500, "Database Shutdown");
        //    }
        //}

        //[HttpGet("{moniker}")]
        //public async Task<IActionResult> Get(string moniker)
        //{
        //    try
        //    {
        //        var result = await _Camprepository.GetAllCampsAsync(moniker);

        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        return this.StatusCode(500, "Database Shutdown");
        //    }
        //}
        [HttpGet("search")]
        public async Task<IActionResult> SearchByDate (DateTime theDate)
        {
            try
            {
                var result = await _Camprepository.GetAllCampsByEventDate(theDate);
                if (!result.Any())
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Shutdown");
            }
        }
    }
}
