using MediatR;
using Microsoft.AspNetCore.Http;
using TeamManagementSystem.Application.Common.CurrentUser;

namespace TeamManagementSystem.Application.Common.Behaviours;

/// <summary>
/// Token validation behaviour class to check users tokens in claims before making any request
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class TokenValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{

    private readonly ICurrentUser _currentUser;

    public TokenValidationBehaviour (ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_currentUser.Id))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        // Proceed to the next behavior or handler
        return await next();
    }
}
