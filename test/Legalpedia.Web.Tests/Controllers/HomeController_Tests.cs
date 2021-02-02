using System.Threading.Tasks;
using Legalpedia.Models.TokenAuth;
using Legalpedia.Web.Controllers;
using Shouldly;
using Xunit;

namespace Legalpedia.Web.Tests.Controllers
{
    public class HomeController_Tests: LegalpediaWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}