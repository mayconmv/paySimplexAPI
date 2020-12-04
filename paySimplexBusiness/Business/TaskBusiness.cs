using paySimplexBusiness.Contracts;
using paySimplexBusiness.Models;
using paySimplexBusiness.Util;
using paySimplexData.Contracts;
using paySimplexData.Entities;
using paySimplexResources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace paySimplexBusiness.Business
{
    public class TaskBusiness : ITaskContract
    {
        private readonly ITaskRepository _taskRepository;
        public TaskBusiness(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public object Create(TaskModel obj, long userId)
        {
            try
            {
                var castedObject = obj.ToTaskEntity();

                _taskRepository.Create(castedObject, userId);

                return castedObject.ToTaskResultModel(TaskResources.CreatedSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(TaskResources.ErrorOnCreate);
            }
        }

        public object Delete(long id)
        {
            try
            {
                var castedObject = _taskRepository.Get(x => x.Id == id);

                _taskRepository.Delete(castedObject);

                return castedObject.ToTaskResultModel(TaskResources.DeleteSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(TaskResources.ErrorOnDelete);
            }
        }

        public object Get()
        {
            try
            {
                return _taskRepository.Get(x => x.Id > 0)
                                      .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(TaskResources.ErrorOnGet);
            }
        }

        public object GetById(long id)
        {
            try
            {
                return _taskRepository.Get(x => x.Id == id, new List<string> { "State", "User" })
                                      .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(TaskResources.ErrorOnGet);
            }
        }

        public object GetByName(string title)
        {
            try
            {
                return _taskRepository.GetMany(x => x.Title == title)?.FirstOrDefault()
                                      .ToBaseSuccessModel(CommonResources.GetSuccessfull); ;
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(TaskResources.ErrorOnGet);
            }
        }

        public object GetMany(string arguments)
        {
            try
            {
                return _taskRepository.GetMany(x => x.Title.Contains(arguments) ||
                                                    x.Description.Contains(arguments))
                                      .ToBaseSuccessModel(CommonResources.GetSuccessfull); ;
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(TaskResources.ErrorOnGet);
            }
        }

        public object GetTimeInProgress(long id)
        {
            try
            {
                var result = _taskRepository.Get(x => x.Id == id, new List<string> { "State", "User" });
                if (result.EndDate != null)
                    return new TaskEstimatedTimeModel()
                    {
                        Days = result.EndDate.Value.Subtract(result.StartDate.Value).Days,
                        Hours = result.EndDate.Value.Subtract(result.StartDate.Value).Hours,
                        Minutes = result.EndDate.Value.Subtract(result.StartDate.Value).Minutes
                    }.ToBaseSuccessModel(CommonResources.GetSuccessfull);
                else
                    return result.ToTaskResultModel(TaskResources.ErrorOnGetEndDate, true);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(TaskResources.ErrorOnGet);
            }
        }

        public object Update(TaskModel obj, long userId)
        {
            try
            {
                var castedObject = obj.ToTaskEntity();

                _taskRepository.Update(castedObject, userId);

                return castedObject.ToTaskResultModel(TaskResources.UpdatedSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(TaskResources.ErrorOnUpdate);
            }
        }

        public object UploadFile(long taskId, string fileName, byte[] fileBytes, long userId)
        {
            try
            {
                var task = _taskRepository.Get(x => x.Id == taskId, new List<string> { "State", "User" })
                                          .ToTaskModel();
                task.FileName = fileName;
                task.FileContent = fileBytes;

                var castedObject = task.ToTaskEntity();

                _taskRepository.Update(castedObject, userId);

                return castedObject.ToTaskResultModel(TaskResources.UpdatedSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(TaskResources.ErrorOnUpdate);
            }
        }
    }
}

public static class TaskExtensions
{
    public static Task ToTaskEntity(this TaskModel task)
    {
        return new Task
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            StartDate = task.StartDate,
            EstimatedTime = task.EstimatedTime,
            EndDate = task.EndDate,
            FileName = task.FileName,
            FileContent = task.FileContent,
            UserId = task.UserId,
            StateId = task.StateId,
            CreateDate = task.CreateDate,
            CreatedBy = task.CreatedBy,
            UpdateDate = task.UpdateDate,
            UpdatedBy = task.UpdatedBy
        };
    }

    public static TaskModel ToTaskModel(this Task task)
    {
        return new TaskModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            StartDate = task.StartDate,
            EstimatedTime = task.EstimatedTime,
            EndDate = task.EndDate,
            FileName = task.FileName,
            FileContent = task.FileContent,
            UserId = task.UserId,
            User = task.User.ToUserModel(),
            StateId = task.StateId,
            State = task.State.ToStateModel(),
            CreateDate = task.CreateDate,
            CreatedBy = task.CreatedBy,
            UpdateDate = task.UpdateDate,
            UpdatedBy = task.UpdatedBy
        };
    }

    public static   List<TaskModel> ToTaskCollectionModel(this List<Task> tasks)
    {
        return tasks.Select(x => new TaskModel
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            StartDate = x.StartDate,
            EstimatedTime = x.EstimatedTime,
            EndDate = x.EndDate,
            FileName = x.FileName,
            FileContent = x.FileContent,
            UserId = x.UserId,
            User = x.User.ToUserModel(),
            StateId = x.StateId,
            State = x.State.ToStateModel(),
            CreateDate = x.CreateDate,
            CreatedBy = x.CreatedBy,
            UpdateDate = x.UpdateDate,
            UpdatedBy = x.UpdatedBy
        }).ToList();
    }

    public static BaseSuccessModel ToTaskResultModel(this Task obj, string message, bool error = false)
    {
        return new BaseSuccessModel
        {
            Error = error,
            Result = obj,
            Message = message
        };
    }
}
