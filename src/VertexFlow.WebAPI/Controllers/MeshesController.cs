﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VertexFlow.WebAPI.Interfaces;
using VertexFlow.WebAPI.Models;
using VertexFlow.WebApplication.Interfaces.Services;

namespace VertexFlow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeshesController : ControllerBase
    {
        private readonly IMeshDataMapper _mapper;
        private readonly IMeshService _meshService;

        public MeshesController(IMeshService meshService, IMeshDataMapper mapper)
        {
            _mapper = mapper;
            _meshService = meshService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MeshRequest meshRequest)
        {
            await _meshService.AddAsync(_mapper.FromRequest(meshRequest));
            return CreatedAtAction(nameof(Get), new {meshId = meshRequest.Id}, meshRequest);
        }

        [HttpGet("{meshId}")]
        public async Task<IActionResult> Get(string meshId)
        {
            var mesh = await _meshService.GetAsync(meshId); 
            return Ok(_mapper.ToResponse(mesh));
        }
        
        [HttpGet]
        public async IAsyncEnumerable<MeshResponse> GetAll()
        {
            await foreach (var mesh in _meshService.GetAllAsync())
            {
                yield return _mapper.ToResponse(mesh);
            }

            // return _meshService.GetAllAsync();
        }

        [HttpPut("{meshId}")]
        public async Task<IActionResult> Update([FromBody] MeshRequest meshRequest)
        {
            await _meshService.UpdateAsync(meshRequest.Id, _mapper.FromRequest(meshRequest));
            return NoContent();
        }

        [HttpDelete("{meshId}")]
        public async Task<IActionResult> Delete(string meshId)
        {
            await _meshService.DeleteAsync(meshId);
            return NoContent();
        }
    }
}