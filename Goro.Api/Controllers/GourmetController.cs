using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Goro.Api.Infrastructure;
using Goro.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Goro.Api.Controllers
{
    /// <summary>
    /// Gourmet API Controller
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class GourmetController : ControllerBase
    {
        private readonly GourmetClient _client;
        private readonly GourmetRepository _repository;
        private readonly IMapper _mapper;

        public GourmetController(GourmetClient client, GourmetRepository repository, IMapper mapper)
        {
            _client = client;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Gourmet>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var entity = await _client.SearchAsync();
            var result = _mapper.Map<List<Gourmet>>(entity);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [HttpGet("{lat:float}/{lng:float}")]
        [ProducesResponseType(typeof(IEnumerable<Gourmet>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(float lat, float lng)
        {
            var entity = await _client.SearchAsync(lng, lat);
            var result = _mapper.Map<List<Gourmet>>(entity);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }


        //     /// <summary>
        //     /// 
        //     /// </summary>
        //     /// <param name="keyword"></param>
        //     /// <returns></returns>
        //     /// <response code="200"></response>
        //     /// <response code="404"></response>
        //     [HttpGet("{keyword}")]
        //     [ProducesResponseType(typeof(IEnumerable<Gourmet>), 200)]
        //     [ProducesResponseType(404)]
        //     public async Task<IActionResult> Get(string keyword)
        //     {
        //var geocode = await GeocodeClient.GetGeocodeAsync(HttpUtility.UrlDecode(keyword));
        //var entity = await GourmetClient.SearchAsync(geocode.results[0].geometry.location.lng, geocode.results[0].geometry.location.lat);
        //         var result = _mapper.Map<List<Gourmet>>(entity);
        //         if (result == null || result.Count == 0)
        //         {
        //             return NotFound();
        //         }
        //         return Ok(result);
        //     }
    }
}