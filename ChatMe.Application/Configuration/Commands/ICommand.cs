﻿namespace ChatMe.Application.Configuration.Commands
{
    using MediatR;
    using System;

    public interface ICommand : IRequest
    {
        Guid Id { get; }
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }
}
