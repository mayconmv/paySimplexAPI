using Microsoft.AspNetCore.Mvc;
using paySimplexBusiness.Contracts;
using paySimplexBusiness.Models;

namespace paySimplexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserContract _userContract;
        private readonly ApplicationUserModel _applicationUserModel;

        public UserController(IUserContract userContract)
        {
            _userContract = userContract;
            _applicationUserModel = new ApplicationUserModel();
        }

        #region Get
        [HttpGet("GetById")]
        public IActionResult GetById(long id)
        {
            var result = _userContract.GetById(id);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetMany")]
        public IActionResult GetMany(string arguments)
        {
            var result = _userContract.GetMany(arguments);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var result = _userContract.GetByName(name);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }
        #endregion

        #region Post
        [HttpPost("Create")]
        public IActionResult Create(UserModel userModel)
        {
            var result = _userContract.Create(userModel, _applicationUserModel.Id);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(UserModel userModel)
        {
            var result = _userContract.Update(userModel, _applicationUserModel.Id);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(long id)
        {
            var result = _userContract.Delete(id);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }
        #endregion
    }
}
