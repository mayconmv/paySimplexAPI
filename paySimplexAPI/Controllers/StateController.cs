using Microsoft.AspNetCore.Mvc;
using paySimplexBusiness.Contracts;
using paySimplexBusiness.Models;

namespace paySimplexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateContract _stateContract;
        private readonly ApplicationUserModel _applicationUserModel;

        public StateController(IStateContract stateContract)
        {
            _stateContract = stateContract;
            _applicationUserModel = new ApplicationUserModel();
        }

        #region Get
        [HttpGet]
        public IActionResult GetById(long id)
        {
            var result = _stateContract.GetById(id);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetMany(string arguments)
        {
            var result = _stateContract.GetMany(arguments);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetByName(string name)
        {
            var result = _stateContract.GetByName(name);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }
        #endregion

        #region Post
        [HttpPost("Create")]
        public IActionResult Create(StateModel stateModel)
        {
            var result = _stateContract.Create(stateModel, _applicationUserModel.Id);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(StateModel stateModel)
        {
            var result = _stateContract.Update(stateModel, _applicationUserModel.Id);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(long id)
        {
            var result = _stateContract.Delete(id);
            if (result is BaseSuccessModel)
                return Ok(result);
            else
                return BadRequest(result);
        }
        #endregion
    }
}
