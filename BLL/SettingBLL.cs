using MathsExercise.DAL;
using MathsExercise.Models;
using MathsExercise.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MathsExercise.BLL
{
    public class SettingBll : BaseBLL
    {
        public SettingBll(MEDBContext medbContext) : base(medbContext)
        {
        }

        public Setting GetOne(int Id) {
            List<Setting> result = this._medbContext.Setting.Where(item => item.ID  == Id).ToList();
            if(result.Count>0)
            {
                return result[0];
            } else {
                return null;
            }
            
        }
        public Setting GetOne(string guid) {
            List<Setting> result = this._medbContext.Setting.Where(item => item.GUIDValue == guid).ToList();
            if(result.Count>0)
            {
                return result[0];
            } else {
                return null;
            }
            
        }
        
        public bool Insert(Setting setting)
        {
            bool rtn = false;
            this._medbContext.Add(setting);
            if(this._medbContext.SaveChanges()==1)
                return true;
            else 
                return rtn;
        }
    }
}