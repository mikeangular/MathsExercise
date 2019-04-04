using MathsExercise.DAL;
using MathsExercise.Models;
using MathsExercise.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MathsExercise.BLL
{
    public class SystemLogBLL : BaseBLL
    {
        public SystemLogBLL(MEDBContext medbContext) : base(medbContext)
        {
        }

        public SystemLog GetOne(int Id) {
            List<SystemLog> result = this._medbContext.systemLog.Where(item => item.ID  == Id).ToList();
            if(result.Count>0)
            {
                return result[0];
            } else {
                return null;
            }
        }

        public bool Insert(SystemLog log)
        {
            bool rtn = false;
            this._medbContext.Add(log);
            if(this._medbContext.SaveChanges()==1)
                return true;
            else 
                return rtn;
        }

    }
}