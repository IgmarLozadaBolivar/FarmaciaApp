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
    [Route("[controller]")]
    public class PaisController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        //private readonly  IMapper mapper;
        //, IMapper mapper
        public PaisController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            //this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Pais>>> Get()
        {
            var paises = await unitOfWork.Paises.GetAllAsync();
            return Ok(paises);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var pais = await unitOfWork.Paises.GetByIdAsync(id);
            return Ok(pais);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pais>> Post(Pais pais)
        {
            this.unitOfWork.Paises.Add(pais);
            await unitOfWork.SaveAsync();
            if (pais == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = pais.Id }, pais);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pais>> Put(int id, [FromBody] Pais pais)
        {
            if (pais == null)
            {
                return NotFound();
            }
            unitOfWork.Paises.Update(pais);
            await unitOfWork.SaveAsync();
            return pais;
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
}