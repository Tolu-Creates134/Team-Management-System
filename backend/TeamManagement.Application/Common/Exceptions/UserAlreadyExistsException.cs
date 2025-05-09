using System;

namespace TeamManagementSystem.Application.Common.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string email)
        : base($"A user with the email '{email}' already exists.") { }

}
