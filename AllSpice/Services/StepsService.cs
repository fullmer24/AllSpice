using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class StepsService
    {
        private readonly StepsRepository _stepsRepo;

        public StepsService(StepsRepository stepsRepo)
        {
            _stepsRepo = stepsRepo;
        }
        internal List<Steps> GetStepsByRecipeId(int id)
        {
            List<Steps> steps = _stepsRepo.GetStepsByRecipeId(id);
            if (steps == null)
            {
                throw new Exception("no steps by that Id");
            }
            return steps;
        }

        internal List<Steps> GetAll()
        {
            return _stepsRepo.GetAll();
        }

        internal Steps Create(Steps newStep)
        {
            return _stepsRepo.Create(newStep);
        }
    }
}