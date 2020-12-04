using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using paySimplexBusiness.Contracts;
using paySimplexBusiness.Models;
using System.IO;

namespace paySimplexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskContract _taskContract;
        private readonly ApplicationUserModel _applicationUserModel;

        public TaskController(ITaskContract taskContract)
        {
            _taskContract = taskContract;
            _applicationUserModel = new ApplicationUserModel();
        }

        #region Get
        [HttpGet("GetById")]
        public IActionResult GetById(long id)
        {
            var result = _taskContract.GetById(id);
            if (result is BaseResultModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetMany")]
        public IActionResult GetMany(string arguments)
        {
            var result = _taskContract.GetMany(arguments);
            if (result is BaseResultModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var result = _taskContract.GetByName(name);
            if (result is BaseResultModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetTimeInProgress")]
        public IActionResult GetTimeInProgress(long id)
        {
            var result = _taskContract.GetTimeInProgress(id);
            if (result is BaseResultModel)
                return Ok(result);
            else
                return BadRequest(result);
        }
        #endregion

        #region Post
        [HttpPost("Create")]
        public IActionResult Create(TaskModel taskModel)
        {
            var result = _taskContract.Create(taskModel, _applicationUserModel.Id);
            if (result is BaseResultModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut("Update")]
        public IActionResult Update(TaskModel taskModel)
        {
            var result = _taskContract.Update(taskModel, _applicationUserModel.Id);
            if (result is BaseResultModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            var result = _taskContract.Delete(id);
            if (result is BaseResultModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPatch("UploadFile")]
        public IActionResult UploadFile(IFormFile file, long taskId)
        {
            var result = _taskContract.UploadFile(taskId, file.FileName, ConvertToBytes(file), _applicationUserModel.Id);
            if (result is BaseResultModel)
                return Ok(result);
            else
                return BadRequest(result);
        }

        private byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }
        #endregion
    }
}
