using Microsoft.AspNetCore.Mvc;
using System;

namespace SeleniumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly Browser browser;
        private readonly LoginAutomation loginAutomation;
        public TestController(Browser browser,LoginAutomation loginAutomation)
        {
            this.browser = browser;
            this.loginAutomation = loginAutomation;
        }

        [HttpPost]
        public CarInfo Post([FromBody] SiteInfo site)
        {
            var webDriver = browser.LaunchBrowser();
            CarInfo car = new();
            try
            {
                loginAutomation.Login(
                    site.Username,
                    site.Password,
                    site.Url,
                    webDriver
                    );
            }
            catch (Exception ex)
            {
                car.Msg = ex.Message;
                return car;
            }
            finally
            {
                car = browser.getData(webDriver);
                webDriver.Quit();
                webDriver.Close();
            }
            return car;
        }
    }
}
