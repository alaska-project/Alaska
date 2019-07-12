using Alaska.Services.Contents.Domain.Models.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Application.Query
{
    internal class AzureMediaLibraryQuery
    {
        public Task<IEnumerable<MediaFolder>> GetChildrenFolders(MediaFolder folder)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MediaContent>> GetFolderContents(MediaFolder folder)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MediaFolder>> GetRootFolders()
        {
            throw new NotImplementedException();
        }
    }
}
