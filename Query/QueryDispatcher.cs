using Microsoft.Extensions.DependencyInjection;
using T1.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1.Query;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
    {
        var queryHandler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();
        Debug.Assert(null != queryHandler);

        TResult res = await queryHandler.HandleAsync(query);
        return res;
    }
}
