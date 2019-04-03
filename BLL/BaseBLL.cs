using MathsExercise.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MathsExercise.BLL
{
    public abstract class BaseBLL
    {
        public MEDBContext _medbContext ;
        public BaseBLL(MEDBContext medbContext)
        {
            _medbContext = medbContext;
        }

        // public abstract List<T> GetData<T>(T t) where T:class;
        // public abstract T GetData<T>(int Id) where T:class;
        
    }
}