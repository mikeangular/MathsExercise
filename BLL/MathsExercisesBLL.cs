using MathsExercise.DAL;
using MathsExercise.Models;
using MathsExercise.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MathsExercise.BLL
{
    public class MathsExercisesBll : BaseBLL
    {
        public MathsExercisesBll(MEDBContext medbContext) : base(medbContext)
        {
        }

        public List<MathsExercises> GetData(int SettingId) {
            List<MathsExercises> result = this._medbContext.Exercises.Where(item => item.SettingId == SettingId).ToList();
            return result;
            
        }
        public MathsExercises GetOne(int Id) {
            List<MathsExercises> result = this._medbContext.Exercises.Where(item => item.ID  == Id).ToList();
            if(result.Count>0)
            {
                return result[0];
            } else {
                return null;
            }
        }

        public bool Insert(MathsExercises mathsExercise)
        {
            bool rtn = false;
            this._medbContext.Add(mathsExercise);
            if(this._medbContext.SaveChanges()==1)
                return true;
            else 
                return rtn;
        }

        public bool Create(List<MathsExercises> mathsExercises)
        {
            bool rtn = false;
            foreach(MathsExercises item in mathsExercises)
            {
                this._medbContext.Add(item);
            }
            if(this._medbContext.SaveChanges()==mathsExercises.Count)
                return true;
            else 
                return rtn;
        }
        public bool Update(IEnumerable<VMathsExercise> mathsExercises)
        {
            bool rtn = false;
            foreach(VMathsExercise item in mathsExercises)
            {
                MathsExercises maths=new MathsExercises();
                maths.ID = item.ID;
                maths.UserAnswer = item.UserAnswer;
                maths.SaveTime = item.SaveTime;
                this._medbContext.Attach(maths);
                this._medbContext.Entry(maths).Property(p=>p.UserAnswer).IsModified = true;
                this._medbContext.Entry(maths).Property(p=>p.SaveTime).IsModified = true;
            }
            if (this._medbContext.SaveChanges() != mathsExercises.Count())
            {
                return rtn;
            }
            else
            {
                return true;
            }
        }
    
    }
}