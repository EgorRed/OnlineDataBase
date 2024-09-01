using Microsoft.AspNetCore.Mvc;
using OnlineDB.Model.Auth;
using OnlineDB.Model.StructureManager;
using OnlineDB.Service.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace OnlineDB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StructureManageController : ControllerBase
    {
        readonly IStructureManagerService _providerService;

        public StructureManageController(IStructureManagerService providerService)
        {
            _providerService = providerService;
        }

        private string GetHash(string input)
        {
            using SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }


        [HttpPost]
        public async Task<IActionResult> CreateStructureDbForUser([FromBody] CreateStructureForUserModel model)
        {
            UserNameModel userName = new UserNameModel();
            string? name = User.Identity?.Name;
            userName.Name = GetHash(name);

            var action = await _providerService.CreateStructureForUser(model, userName.Name);
            return StatusCode(StatusCodes.Status200OK, new Response { Status = action.StatusCode.ToString(), Message = action.Description });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDataFileForUser([FromBody] DeleteDataFileForUserModel model)
        {
            UserNameModel userName = new UserNameModel();
            string? name = User.Identity?.Name;
            userName.Name = GetHash(name);

            var action = await _providerService.DeleteDataFileForUser(model, userName.Name);
            return StatusCode(StatusCodes.Status200OK, new Response { Status = action.StatusCode.ToString(), Message = action.Description });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteStructureForUser([FromBody] DeleteStructureForUserModel model)
        {
            UserNameModel userName = new UserNameModel();
            string? name = User.Identity?.Name;
            userName.Name = GetHash(name);

            var action = await _providerService.DeleteStructureForUser(model, userName.Name);
            return StatusCode(StatusCodes.Status200OK, new Response { Status = action.StatusCode.ToString(), Message = action.Description });
        }


        [HttpPost]
        public async Task<IActionResult> GetTableStructure([FromBody] GetTableStructureModel model)
        {
            UserNameModel userName = new UserNameModel();
            string? name = User.Identity?.Name;
            userName.Name = GetHash(name);

            var action = await _providerService.GetTableStructure(model, userName.Name);
            return Ok(new { Status = action.status.StatusCode.ToString(), Message = action.status.Description, Data = action.data });
        }


        [HttpPost]
        public async Task<IActionResult> CreateNewColumnsForUser([FromBody] CreateNewColumnsForUserModel model)
        {
            UserNameModel userName = new UserNameModel();
            string? name = User.Identity?.Name;
            userName.Name = GetHash(name);

            var action = await _providerService.CreateNewColumnsForUser(model, userName.Name);
            return StatusCode(StatusCodes.Status200OK, new Response { Status = action.StatusCode.ToString(), Message = action.Description });
        }

    }
}
