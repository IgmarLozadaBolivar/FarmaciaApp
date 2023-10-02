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

public class CiuController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CiuController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CiuDto>>> Get()
    {
        var ciudades = await unitOfWork.Ciudades.GetAllAsync();
        return mapper.Map<List<CiuDto>>(ciudades);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CiuxPerDto>>> Get11([FromQuery] Params ciudadParams)
    {
        var ciudades = await unitOfWork.Ciudades.GetAllAsync(ciudadParams.PageIndex, ciudadParams.PageSize, ciudadParams.Search);

        foreach (var ciudad in ciudades.registros)
        {
            await unitOfWork.Ciudades.LoadPersonasAsync(ciudad.Id);
        }

        var lstCiudadesDto = mapper.Map<List<CiuxPerDto>>(ciudades.registros);

        return new Pager<CiuxPerDto>(lstCiudadesDto, ciudades.totalRegistros, ciudadParams.PageIndex, ciudadParams.PageSize, ciudadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CiuDto>> Get(int id)
    {
        var ciudad = await unitOfWork.Ciudades.GetByIdAsync(id);
        if (ciudad == null)
        {
            return NotFound();
        }
        return this.mapper.Map<CiuDto>(ciudad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ciudad>> Post(CiuDto ciuDto)
    {
        var ciudades = this.mapper.Map<Ciudad>(ciuDto);
        this.unitOfWork.Ciudades.Add(ciudades);
        await unitOfWork.SaveAsync();
        if (ciudades == null)
        {
            return BadRequest();
        }
        ciuDto.Id = ciudades.Id;
        return CreatedAtAction(nameof(Post), new { id = ciuDto.Id }, ciuDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CiuDto>> Put(int id, [FromBody] CiuDto ciuDto)
    {
        if (ciuDto == null)
        {
            return NotFound();
        }
        var ciudad = this.mapper.Map<Ciudad>(ciuDto);
        unitOfWork.Ciudades.Update(ciudad);
        await unitOfWork.SaveAsync();
        return ciuDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var ciudad = await unitOfWork.Ciudades.GetByIdAsync(id);
        if (ciudad == null)
        {
            return NotFound();
        }
        unitOfWork.Ciudades.Remove(ciudad);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}