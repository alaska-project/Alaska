﻿using Alaska.Extensions.Contents.Contentful.Models;
using Alaska.Services.Contents.Domain.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Application.Commands
{
    internal class CreateContentCommand : IRequest<ContentItemReference>
    {
        public CreateContentCommand(ContentCreationRequest creationRequest)
        {
            CreationRequest = creationRequest ?? throw new ArgumentNullException(nameof(creationRequest));
        }

        public ContentCreationRequest CreationRequest { get; }
    }
}
