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

public class PaisController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public PaisController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PaisDto>>> Get()
    {
        var paises = await unitOfWork.Paises.GetAllAsync();
        return mapper.Map<List<PaisDto>>(paises);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PaisxDepDto>>> Get11([FromQuery] Params paisParams)
    {
        var paises = await unitOfWork.Paises.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var lstPaisesDto = mapper.Map<List<PaisxDepDto>>(paises.registros);
        return new Pager<PaisxDepDto>(lstPaisesDto, paises.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaisDto>> Get(int id)
    {
        var pais = await unitOfWork.Paises.GetByIdAsync(id);
        if (pais == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PaisDto>(pais);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pais>> Post(PaisDto paisDto)
    {
        var paises = this.mapper.Map<Pais>(paisDto);
        this.unitOfWork.Paises.Add(paises);
        await unitOfWork.SaveAsync();
        if (paises == null)
        {
            return BadRequest();
        }
        paisDto.Id = paises.Id;
        return CreatedAtAction(nameof(Post), new { id = paisDto.Id }, paisDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PaisDto>> Put(int id, [FromBody] PaisDto paisDto)
    {
        if (paisDto == null)
        {
            return NotFound();
        }
        var pais = this.mapper.Map<Pais>(paisDto);
        unitOfWork.Paises.Update(pais);
        await unitOfWork.SaveAsync();
        return paisDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var pais = await unitOfWork.Paises.GetByIdAsync(id);
        if (pais == null)
        {
            return NotFound();
        }
        unitOfWork.Paises.Remove(pais);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}