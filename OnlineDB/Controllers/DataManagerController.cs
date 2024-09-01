using Microsoft.AspNetCore.Mvc;
using OnlineDB.Model.Auth;
using OnlineDB.Model.DataManage;
using OnlineDB.Service.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace OnlineDB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DataManagerController : ControllerBase
    {
        readonly IDataManagerService _providerService;

        public DataManagerController(IDataManagerService providerService)
        {
            _providerService = providerService;
        }

        private string GetHash(string input)
        {
            using SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        [HttpPost]
        public async Task<IActionResult> WriteDataInColl([FromBody] WriteDataModel model)
        {
            UserNameModel userName = new UserNameModel();
            string? name = User.Identity?.Name;
            userName.Name = GetHash(name);

            var action = await _providerService.WriteData(model, userName.Name);
            return StatusCode(StatusCodes.Status200OK, new Response { Status = action.StatusCode.ToString(), Message = action.Description });
        }

        [HttpPost]
        public async Task<IActionResult> ReadDataFromColl([FromBody] ReadDataModel model)
        {
            UserNameModel userName = new UserNameModel();
            string? name = User.Identity?.Name;
            userName.Name = GetHash(name);

            var action = await _providerService.ReadData(model, userName.Name);
            return Ok(new { Status = action.status.StatusCode.ToString(), Message = action.status.Description, Data = action.data });
        }

    }
}
