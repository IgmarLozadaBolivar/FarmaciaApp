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

public class PerController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly  IMapper mapper;
    
    public PerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PerDto>>> Get()
    {
        var personas = await unitOfWork.Personas.GetAllAsync();
        return mapper.Map<List<PerDto>>(personas);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PerxManyDto>>> Get11([FromQuery] Params personaParams)
    {
        var personas = await unitOfWork.Personas
            .GetAllAsync(personaParams.PageIndex, personaParams.PageSize, personaParams.Search);

        // Cargar las propiedades de navegación para cada persona
        foreach (var persona in personas.registros)
        {
            await unitOfWork.Personas.LoadCiudadesAsync(persona.Id);
            await unitOfWork.Personas.LoadGenerosAsync(persona);
            await unitOfWork.Personas.LoadTipoPersonasAsync(persona);
        }

        var lstPersonasDto = mapper.Map<List<PerxManyDto>>(personas.registros);

        var pager = new Pager<PerxManyDto>(lstPersonasDto, personas.totalRegistros, personaParams.PageIndex, personaParams.PageSize, personaParams.Search);

        return pager;
    }

    /* [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PerxManyDto>>> Get11([FromQuery] Params perParams)
    {
        var personas = await unitOfWork.Personas.GetAllAsync(perParams.PageIndex, perParams.PageSize, perParams.Search);
        var lstPersonasDto = mapper.Map<List<PerxManyDto>>(personas.registros);
        return new Pager<PerxManyDto>(lstPersonasDto, personas.totalRegistros, perParams.PageIndex, perParams.PageSize, perParams.Search);
    } */

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PerDto>> Get(int id)
    {
        var persona = await unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PerDto>(persona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Persona>> Post(PerDto perDto)
    {
        var personas = this.mapper.Map<Persona>(perDto);
        this.unitOfWork.Personas.Add(personas);
        await unitOfWork.SaveAsync();
        if (personas == null)
        {
            return BadRequest();
        }
        perDto.Id = personas.Id;
        return CreatedAtAction(nameof(Post), new { id = perDto.Id }, perDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PerDto>> Put(int id, [FromBody] PerDto perDto)
    {
        if (perDto == null)
        {
            return NotFound();
        }
        var persona = this.mapper.Map<Persona>(perDto);
        unitOfWork.Personas.Update(persona);
        await unitOfWork.SaveAsync();
        return perDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var persona = await unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null)
        {
            return NotFound();
        }
        unitOfWork.Personas.Remove(persona);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}