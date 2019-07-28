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
        private readonly IMediaLibraryService _mediaLibrary;

        public MediaController(IMediaLibraryService mediaLibrary)
        {
            _mediaLibrary = mediaLibrary ?? throw new ArgumentNullException(nameof(mediaLibrary));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaFolder>>> GetRootFolders()
        {
            var result = await _mediaLibrary.GetRootFolders();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaFolder>>> GetChildrenFolders([FromQuery]string folderId)
        {
            var result = await _mediaLibrary.GetChildrenFolders(folderId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaContent>>> GetFolderContents([FromQuery]string folderId)
        {
            var result = await _mediaLibrary.GetFolderContents(folderId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MediaFolder>> CreateFolder([FromQuery]string folderName, [FromQuery]string parentFolderId)
        {
            var result = await _mediaLibrary.CreateFolder(folderName, parentFolderId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MediaFolder>> CreateRootFolder([FromQuery]string folderName)
        {
            var result = await _mediaLibrary.CreateRootFolder(folderName);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFolder([FromQuery]string folderId)
        {
            await _mediaLibrary.DeleteFolder(folderId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<MediaContent>> GetMedia([FromQuery]string mediaId)
        {
            var media = await _mediaLibrary.GetMedia(mediaId);
            return Ok(media);
        }

        [HttpPost]
        public async Task<ActionResult<MediaContent>> AddMedia([FromBody]MediaCreationRequest mediaContent)
        {
            var media = await _mediaLibrary.AddMedia(mediaContent.Name, mediaContent.ContentType, Convert.FromBase64String(mediaContent.MediaContent), mediaContent.FolderId);
            return Ok(media);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMedia([FromQuery]string mediaId)
        {
            await _mediaLibrary.DeleteMedia(mediaId);
            return Ok();
        }
    }
}
