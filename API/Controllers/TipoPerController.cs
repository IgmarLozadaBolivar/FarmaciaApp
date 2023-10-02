using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class TipoPerController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public TipoPerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoPerDto>>> Get()
    {
        var tipoPersonas = await unitOfWork.TipoPersonas.GetAllAsync();
        return mapper.Map<List<TipoPerDto>>(tipoPersonas);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoPerxPerDto>>> Get11([FromQuery] Params tipoPerParams)
    {
        var tipoPersonas = await unitOfWork.TipoPersonas.GetAllAsync(tipoPerParams.PageIndex, tipoPerParams.PageSize, tipoPerParams.Search);
        var lstTipoPersonaDto = mapper.Map<List<TipoPerxPerDto>>(tipoPersonas.registros);
        return new Pager<TipoPerxPerDto>(lstTipoPersonaDto, tipoPersonas.totalRegistros, tipoPerParams.PageIndex, tipoPerParams.PageSize, tipoPerParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoPerDto>> Get(int id)
    {
        var tipoPersona = await unitOfWork.TipoPersonas.GetByIdAsync(id);
        if (tipoPersona == null)
        {
            return NotFound();
        }
        return this.mapper.Map<TipoPerDto>(tipoPersona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoPersona>> Post(TipoPerDto tipoPerDto)
    {
        var tipoPersonas = this.mapper.Map<TipoPersona>(tipoPerDto);
        this.unitOfWork.TipoPersonas.Add(tipoPersonas);
        await unitOfWork.SaveAsync();
        if (tipoPersonas == null)
        {
            return BadRequest();
        }
        tipoPerDto.Id = tipoPersonas.Id;
        return CreatedAtAction(nameof(Post), new { id = tipoPerDto.Id }, tipoPerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoPerDto>> Put(int id, [FromBody] TipoPerDto tipoPerDto)
    {
        if (tipoPerDto == null)
        {
            return NotFound();
        }
        var tipoPersona = this.mapper.Map<TipoPersona>(tipoPerDto);
        unitOfWork.TipoPersonas.Update(tipoPersona);
        await unitOfWork.SaveAsync();
        return tipoPerDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var tipoPersona = await unitOfWork.TipoPersonas.GetByIdAsync(id);
        if (tipoPersona == null)
        {
            return NotFound();
        }
        unitOfWork.TipoPersonas.Remove(tipoPersona);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}