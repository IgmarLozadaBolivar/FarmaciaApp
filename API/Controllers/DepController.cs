using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class DepController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        //private readonly  IMapper mapper;
        //, IMapper mapper
        public DepController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            //this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Departamento>>> Get()
        {
            var departamentos = await unitOfWork.Departamentos.GetAllAsync();
            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var departamento = await unitOfWork.Departamentos.GetByIdAsync(id);
            return Ok(departamento);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Departamento>> Post(Departamento departamentos)
        {
            this.unitOfWork.Departamentos.Add(departamentos);
            await unitOfWork.SaveAsync();
            if (departamentos == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = departamentos.Id }, departamentos);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Departamento>> Put(int id, [FromBody] Departamento departamento)
        {
            if (departamento == null)
            {
                return NotFound();
            }
            unitOfWork.Departamentos.Update(departamento);
            await unitOfWork.SaveAsync();
            return departamento;
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
}