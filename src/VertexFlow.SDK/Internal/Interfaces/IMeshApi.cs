﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VertexFlow.SDK.Internal.Interfaces
{
    internal interface IMeshApi
    {
        string BaseAddress { get; }
        
        Task Create<TRequest>(TRequest meshRequest, CancellationToken cancellationToken);
        Task<TResponse> GetAsync<TResponse>(string meshId, CancellationToken cancellationToken);
        Task<IEnumerable<TResponse>> GetAllAsync<TResponse>(CancellationToken cancellationToken);
        Task UpdateAsync<TRequest>(string meshId, TRequest meshRequest, CancellationToken cancellationToken);
        Task DeleteAsync(string meshId, CancellationToken cancellationToken);
    }
}