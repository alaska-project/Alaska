using Alaska.Extensions.Media.Azure.IntegrationTests.Infrastructure;
using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Web.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
                foreach (var folder in folders)
                    await client.PostJsonAsync($"{MediaLibraryApi}/DeleteFolder?folderId={folder.Id}");

                Thread.Sleep(1000);
                folders = await client.GetJsonAsync<List<MediaFolder>>($"{MediaLibraryApi}/GetRootFolders");
                Assert.Empty(folders);

                var rootFolderName = Guid.NewGuid().ToString();
                var rootFolder = await client.PostJsonAsync<MediaFolder>($"{MediaLibraryApi}/CreateRootFolder?folderName={rootFolderName}");

                var rootFolders = await client.GetJsonAsync<List<MediaFolder>>($"{MediaLibraryApi}/GetRootFolders");
                Assert.Contains(rootFolders, x => x.Id == rootFolder.Id);

                var childFolderName = Guid.NewGuid().ToString();
                var childFolder = await client.PostJsonAsync<MediaFolder>($"{MediaLibraryApi}/CreateFolder?folderName={childFolderName}&parentFolderId={rootFolder.Id}");

                var childrenFolders = await client.GetJsonAsync<List<MediaFolder>>($"{MediaLibraryApi}/GetChildrenFolders?folderId={rootFolder.Id}");
                Assert.Contains(childrenFolders, x => x.Id == childFolder.Id);

                var fileName = "alaska.jpg";
                var contentType = "image/jpeg";
                var testImageContent = File.ReadAllBytes($"TestContents\\{fileName}");
                var uploadedContent = await client.PostJsonAsync<MediaContent>($"{MediaLibraryApi}/AddMedia?name={fileName}&contentType={contentType}&folderId={childFolder.Id}", Convert.ToBase64String(testImageContent));

                Assert.Equal(fileName, uploadedContent.Name);
                Assert.False(string.IsNullOrEmpty(uploadedContent.ThumbnailUrl));

                var folderContents = await client.GetJsonAsync<List<MediaContent>>($"{MediaLibraryApi}/GetFolderContents?folderId={childFolder.Id}");
                Assert.Single(folderContents);
                Assert.Equal(fileName, folderContents.First().Name);
                Assert.Equal(uploadedContent.Id, folderContents.First().Id);
                Assert.Equal(uploadedContent.Url, folderContents.First().Url);
                Assert.Equal(uploadedContent.ThumbnailUrl, folderContents.First().ThumbnailUrl);

                await client.PostJsonAsync($"{MediaLibraryApi}/DeleteMedia?mediaId={uploadedContent.Id}");
                folderContents = await client.GetJsonAsync<List<MediaContent>>($"{MediaLibraryApi}/GetFolderContents?folderId={childFolder.Id}");

                Assert.Empty(folderContents);

                await client.PostJsonAsync($"{MediaLibraryApi}/DeleteFolder?folderId={rootFolder.Id}");

                rootFolders = await client.GetJsonAsync<List<MediaFolder>>($"{MediaLibraryApi}/GetRootFolders");
                Assert.Empty(rootFolders);
            }
        }
    }
}
