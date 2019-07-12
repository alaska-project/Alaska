using Alaska.Extensions.Media.Azure.IntegrationTests.Infrastructure;
using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alaska.Extensions.Media.Azure.IntegrationTests.Scenarios
{
    public class ManageFoldersScenario : AzureMediaScenarioBase
    {
        [Fact]
        public async Task ManageFolders()
        {
            using (var server = CreateServer())
            using (var client = server.CreateClient())
            {
                var folders = await client.GetJsonAsync<List<MediaFolder>>($"{MediaLibraryApi}/GetRootFolders");

                if (folders.Any())
                    folders.ToList().ForEach(x => 
                    {
                        
                    });

                var rootFolderName = Guid.NewGuid().ToString();
                var rootFolder = await client.PostJsonAsync<MediaFolder>($"{MediaLibraryApi}/CreateRootFolder?folderName={rootFolderName}");

                var rootFolders = await client.GetJsonAsync<List<MediaFolder>>($"{MediaLibraryApi}/GetRootFolders");
                Assert.Contains(rootFolders, x => x.Id == rootFolder.Id);

                var childFolderName = Guid.NewGuid().ToString();
                var childFolder = await client.PostJsonAsync<MediaFolder>($"{MediaLibraryApi}/CreateFolder?folderName={childFolderName}&parentFolderId={rootFolder.Id}");

                var childrenFolders = await client.GetJsonAsync<List<MediaFolder>>($"{MediaLibraryApi}/GetChildrenFolders?folderId={rootFolder.Id}");
                Assert.Contains(childrenFolders, x => x.Id == childFolder.Id);
            }
        }
    }
}
