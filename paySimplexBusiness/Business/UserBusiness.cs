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
    public class UserBusiness : IUserContract
    {
        private readonly IUserRepository _userRepository;
        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public object Create(UserModel obj, long userId)
        {
            try
            {
                var castedObject = obj.ToUserEntity();

                _userRepository.Create(castedObject, userId);

                return castedObject.ToUserResultModel(UserResources.CreatedSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(UserResources.ErrorOnCreate);
            }
        }

        public object Delete(long id)
        {
            try
            {
                var castedObject = _userRepository.Get(x => x.Id == id);
                
                _userRepository.Delete(castedObject);

                return castedObject.ToUserResultModel(UserResources.DeleteSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(UserResources.ErrorOnDelete);
            }
        }

        public object Get()
        {
            try
            {
                return _userRepository.Get(x => x.Id > 0)
                                      .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(UserResources.ErrorOnGet);
            }
        }

        public object GetById(long id)
        {
            try
            {
                return _userRepository.Get(x => x.Id == id)
                                      .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(UserResources.ErrorOnGet);
            }
        }

        public object GetByName(string name)
        {
            try
            {
                return _userRepository.GetMany(x => x.Name == name)?[0]
                                      .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(UserResources.ErrorOnGet);
            }
        }

        public object GetMany(string name)
        {
            try
            {
                return _userRepository.GetMany(x => x.Name == name )
                                      .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(UserResources.ErrorOnGet);
            }
        }

        public object Update(UserModel obj, long userId)
        {
            try
            {
                _userRepository.Update(obj.ToUserEntity(), userId);

                return obj.ToBaseSuccessModel(UserResources.UpdatedSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(UserResources.ErrorOnUpdate);
            }
        }
    }
}

public static class UserExtensions
{
    public static User ToUserEntity(this UserModel user)
    {
        return new User
        {
            Id = user?.Id ?? 0,
            Name = user?.Name,
            CreateDate = user?.CreateDate,
            CreatedBy = user?.CreatedBy,
            UpdateDate = user?.UpdateDate,
            UpdatedBy = user?.UpdatedBy
        };
    }

    public static UserModel ToUserModel(this User user)
    {
        return new UserModel
        {
            Id = user.Id,
            Name = user.Name,
            CreateDate = user.CreateDate,
            CreatedBy = user.CreatedBy,
            UpdateDate = user.UpdateDate,
            UpdatedBy = user.UpdatedBy
        };
    }

    public static List<UserModel> ToUserCollectionModel(this List<User> users)
    {
        return users.Select(x => new UserModel
        {
            Id = x.Id,
            Name = x.Name,
            CreateDate = x.CreateDate,
            CreatedBy = x.CreatedBy,
            UpdateDate = x.UpdateDate,
            UpdatedBy = x.UpdatedBy
        }).ToList();
    }

    public static BaseResultModel ToUserResultModel(this User obj, string message)
    {
        return new BaseResultModel
        {
            Result = obj,
            Message = message
        };
    }
}
