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

public class DepController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public DepController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DepDto>>> Get()
    {
        var departamentos = await unitOfWork.Departamentos.GetAllAsync();
        return mapper.Map<List<DepDto>>(departamentos);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<DepxCiuDto>>> Get11([FromQuery] Params depParams)
    {
        var departamentos = await unitOfWork.Departamentos.GetAllAsync(depParams.PageIndex, depParams.PageSize, depParams.Search);
        var lstDepartamentosDto = mapper.Map<List<DepxCiuDto>>(departamentos.registros);
        return new Pager<DepxCiuDto>(lstDepartamentosDto, departamentos.totalRegistros, depParams.PageIndex, depParams.PageSize, depParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DepDto>> Get(int id)
    {
        var departamento = await unitOfWork.Departamentos.GetByIdAsync(id);
        if (departamento == null)
        {
            return NotFound();
        }
        return this.mapper.Map<DepDto>(departamento);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Departamento>> Post(DepDto depDto)
    {
        var departamentos = this.mapper.Map<Departamento>(depDto);
        this.unitOfWork.Departamentos.Add(departamentos);
        await unitOfWork.SaveAsync();
        if (departamentos == null)
        {
            return BadRequest();
        }
        depDto.Id = departamentos.Id;
        return CreatedAtAction(nameof(Post), new { id = depDto.Id }, depDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepDto>> Put(int id, [FromBody] DepDto depDto)
    {
        if (depDto == null)
        {
            return NotFound();
        }
        var departamento = this.mapper.Map<Departamento>(depDto);
        unitOfWork.Departamentos.Update(departamento);
        await unitOfWork.SaveAsync();
        return depDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var departamento = await unitOfWork.Departamentos.GetByIdAsync(id);
        if (departamento == null)
        {
            return NotFound();
        }
        unitOfWork.Departamentos.Remove(departamento);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}