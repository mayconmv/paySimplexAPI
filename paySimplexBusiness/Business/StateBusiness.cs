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
    public class StateBusiness : IStateContract
    {
        private readonly IStateRepository _stateRepository;
        public StateBusiness(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public object Create(StateModel obj, long userId)
        {
            try
            {
                var castedObject = obj.ToStateEntity();

                _stateRepository.Create(castedObject, userId);

                return castedObject.ToStateResultModel(StateResources.CreatedSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(StateResources.ErrorOnCreate);
            }
        }

        public object Delete(long id)
        {
            try
            {
                var castedObject = _stateRepository.Get(x => x.Id == id);

                _stateRepository.Delete(castedObject);

                return castedObject.ToStateResultModel(StateResources.DeleteSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(StateResources.ErrorOnDelete);
            }
        }

        public object Get()
        {
            try
            {
                return _stateRepository.Get(x => x.Id > 0)
                                       .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(StateResources.ErrorOnGet);
            }
        }

        public object GetById(long id)
        {
            try
            {
                return _stateRepository.Get(x => x.Id == id)
                                       .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(StateResources.ErrorOnGet);
            }
        }

        public object GetByName(string name)
        {
            try
            {
                return _stateRepository.GetMany(x => x.Name == name)?[0]
                                       .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(StateResources.ErrorOnGet);
            }
        }

        public object GetMany(string arguments)
        {
            try
            {
                return _stateRepository.GetMany(x => x.Name.Contains(arguments))
                                       .ToBaseSuccessModel(CommonResources.GetSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(StateResources.ErrorOnGet);
            }
        }

        public object Update(StateModel obj, long userId)
        {
            try
            {
                var castedObject = obj.ToStateEntity();
                _stateRepository.Update(castedObject, userId);

                return castedObject.ToStateResultModel(StateResources.UpdatedSuccessfull);
            }
            catch (Exception ex)
            {
                return ex.ToBaseErrorModel(StateResources.ErrorOnUpdate);
            }
        }
    }
}

public static class StateExtensions
{
    public static State ToStateEntity(this StateModel state)
    {
        return new State
        {
            Id = state?.Id ?? 0,
            Name = state?.Name,
            CreateDate = state?.CreateDate,
            CreatedBy = state?.CreatedBy,
            UpdateDate = state?.UpdateDate,
            UpdatedBy = state?.UpdatedBy
        };
    }

    public static StateModel ToStateModel(this State state)
    {
        return new StateModel
        {
            Id = state.Id,
            Name = state.Name,
            CreateDate = state.CreateDate,
            CreatedBy = state.CreatedBy,
            UpdateDate = state.UpdateDate,
            UpdatedBy = state.UpdatedBy
        };
    }

    public static List<StateModel> ToStateCollectionModel(this List<State> states)
    {
        return states.Select(x => new StateModel
        {
            Id = x.Id,
            Name = x.Name,
            CreateDate = x.CreateDate,
            CreatedBy = x.CreatedBy,
            UpdateDate = x.UpdateDate,
            UpdatedBy = x.UpdatedBy
        }).ToList();
    }

    public static BaseResultModel ToStateResultModel(this State obj, string message)
    {
        return new BaseResultModel
        {
            Result = obj,
            Message = message
        };
    }
}
