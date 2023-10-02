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

public class GenController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GenController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GenDto>>> Get()
    {
        var generos = await unitOfWork.Generos.GetAllAsync();
        return mapper.Map<List<GenDto>>(generos);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<GenxPerDto>>> Get11([FromQuery] Params genParams)
    {
        var generos = await unitOfWork.Generos.GetAllAsync(genParams.PageIndex, genParams.PageSize, genParams.Search);
        var lstGenerosDto = mapper.Map<List<GenxPerDto>>(generos.registros);
        return new Pager<GenxPerDto>(lstGenerosDto, generos.totalRegistros, genParams.PageIndex, genParams.PageSize, genParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenDto>> Get(int id)
    {
        var genero = await unitOfWork.Generos.GetByIdAsync(id);
        if (genero == null)
        {
            return NotFound();
        }
        return this.mapper.Map<GenDto>(genero);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Genero>> Post(GenDto genDto)
    {
        var generos = this.mapper.Map<Genero>(genDto);
        this.unitOfWork.Generos.Add(generos);
        await unitOfWork.SaveAsync();
        if (generos == null)
        {
            return BadRequest();
        }
        genDto.Id = generos.Id;
        return CreatedAtAction(nameof(Post), new { id = genDto.Id }, genDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenDto>> Put(int id, [FromBody] GenDto genDto)
    {
        if (genDto == null)
        {
            return NotFound();
        }
        var genero = this.mapper.Map<Genero>(genDto);
        unitOfWork.Generos.Update(genero);
        await unitOfWork.SaveAsync();
        return genDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var genero = await unitOfWork.Generos.GetByIdAsync(id);
        if (genero == null)
        {
            return NotFound();
        }
        unitOfWork.Generos.Remove(genero);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}