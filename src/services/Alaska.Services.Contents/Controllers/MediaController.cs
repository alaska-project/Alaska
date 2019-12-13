using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Services.Contents.Domain.Models.Requests;
using Alaska.Services.Contents.Infrastructure.Abstractions;
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
        private readonly IMediaLibraryService _mediaLibraryService;

        public MediaController(IMediaLibraryService mediaLibraryService)
        {
            _mediaLibraryService = mediaLibraryService ?? throw new ArgumentNullException(nameof(mediaLibraryService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaFolder>>> GetRootFolders()
        {
            return Ok(await _mediaLibraryService.GetRootFolders());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaFolder>>> GetChildrenFolders([FromQuery]string folderId)
        {
            return Ok(await _mediaLibraryService.GetChildrenFolders(folderId));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaContent>>> GetFolderContents([FromQuery]string folderId)
        {
            return Ok(await _mediaLibraryService.GetFolderContents(folderId));
        }

        [HttpPost]
        public async Task<ActionResult<MediaFolder>> CreateFolder([FromQuery]string folderName, [FromQuery]string parentFolderId)
        {
            return Ok(await _mediaLibraryService.CreateFolder(folderName, parentFolderId));
        }

        [HttpPost]
        public async Task<ActionResult<MediaFolder>> CreateRootFolder([FromQuery]string folderName)
        {
            return Ok(await _mediaLibraryService.CreateRootFolder(folderName));
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFolder([FromQuery]string folderId)
        {
            await _mediaLibraryService.DeleteFolder(folderId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<MediaContent>> GetMedia([FromQuery]string mediaId)
        {
            return Ok(await _mediaLibraryService.GetMedia(mediaId));
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 4294967295)]
        [RequestSizeLimit(4294967295)]
        public async Task<ActionResult<MediaContent>> AddMedia([FromBody]MediaCreationRequest mediaContent)
        {
            return Ok(await _mediaLibraryService.AddMedia(mediaContent));
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMedia([FromQuery]string mediaId)
        {
            await _mediaLibraryService.DeleteMedia(mediaId);
            return Ok();
        }
    }
}
