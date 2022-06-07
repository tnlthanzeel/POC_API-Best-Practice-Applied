﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC.Application.Features.Garment.GetGarmentTypeList;
using POC.Application.Features.SMV.Command.GetSMVBreakDownVersion;
using POC.Application.Features.Styles.GetStyleList;
using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Api.Controllers
{
    [Route("api/ie")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IEController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IEController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("garment/types", Name = "GetAllIEGarmentTypes")]
        [ProducesResponseType(typeof(ResponseResult<IEnumerable<GarmentTypeListViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseResult<IEnumerable<GarmentTypeListViewModel>>>> GetAllUsers()
        {
            try
            {

                var viewModel = await _mediator.Send(new GetGarmentTypeListQuery());
                return Ok(viewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("garment/style", Name = "GetAllIEStyle")]
        [ProducesResponseType(typeof(ResponseResult<IEnumerable<StyleListViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseResult<IEnumerable<StyleListViewModel>>>> GetAllStyles()
        {
            var viewModel = await _mediator.Send(new GetStyleListQuery());
            return Ok(viewModel);
        }


        [HttpGet("garment/smvbreakdown/{versionHDID}", Name = "GetBreakDownData")]
        [ProducesResponseType(typeof(ResponseResult<SMVBreakDownVersionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseResult<SMVBreakDownVersionViewModel>>> GetBreakDownData(string versionHDID)
        {
            try
            {
                var viewModel = await _mediator.Send(new GetSMVBreakDownVersionQuery() { VersionHDID = versionHDID });
                return Ok(viewModel);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
