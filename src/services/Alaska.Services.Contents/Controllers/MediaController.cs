using Alaska.Services.Contents.Application.Commands.Media;
using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Services.Contents.Domain.Models.Requests;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Controllers
{
    [Route("alaska/api/[controller]/[action]")]
    public class MediaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMediaQueries _mediaQueries;

        public MediaController(
            IMediator mediator,
            IMediaQueries mediaQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mediaQueries = mediaQueries ?? throw new ArgumentNullException(nameof(mediaQueries));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaFolder>>> GetRootFolders()
        {
            var result = await _mediaQueries.GetRootFolders();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaFolder>>> GetChildrenFolders([FromQuery]string folderId)
        {
            var result = await _mediaQueries.GetChildrenFolders(folderId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaContent>>> GetFolderContents([FromQuery]string folderId)
        {
            var result = await _mediaQueries.GetFolderContents(folderId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MediaFolder>> CreateFolder([FromQuery]string folderName, [FromQuery]string parentFolderId)
        {
            var result = await _mediator.Send(new CreateFolderCommand(folderName, parentFolderId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MediaFolder>> CreateRootFolder([FromQuery]string folderName)
        {
            var result = await _mediator.Send(new CreateRootFolderCommand(folderName));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFolder([FromQuery]string folderId)
        {
            await _mediator.Send(new DeleteFolderCommand(folderId));
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<MediaContent>> GetMedia([FromQuery]string mediaId)
        {
            var media = await _mediaQueries.GetMedia(mediaId);
            return Ok(media);
        }

        [HttpPost]
        public async Task<ActionResult<MediaContent>> AddMedia([FromBody]MediaCreationRequest mediaContent)
        {
            var media = await _mediator.Send(new AddMediaCommand(mediaContent));
            return Ok(media);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMedia([FromQuery]string mediaId)
        {
            await _mediator.Send(new DeleteMediaCommand(mediaId));
            return Ok();
        }
    }
}
