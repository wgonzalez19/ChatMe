﻿namespace ChatMe.Application.Configuration.Queries
{
    using MediatR;

    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
