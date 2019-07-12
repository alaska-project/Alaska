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

                var newFolderName = Guid.NewGuid().ToString();
                var rootFolder = await client.PostJsonAsync<MediaFolder>($"{MediaLibraryApi}/CreateRootFolder?folderName={newFolderName}");


            }
        }
    }
}
