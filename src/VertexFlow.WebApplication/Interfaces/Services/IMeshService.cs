﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VertexFlow.WebApplication.Models;

namespace VertexFlow.WebApplication.Interfaces.Services
{
    public interface IMeshService
    {
        Task AddAsync(Mesh mesh);
        Task<Mesh> GetAsync(int meshId);
        IAsyncEnumerable<Mesh> GetAllAsync();
        Task UpdateAsync(int meshId, Mesh newMesh);
        Task DeleteAsync(int meshId);
    }
}