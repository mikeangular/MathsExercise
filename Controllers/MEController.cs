using MathsExercise.Models;
using MathsExercise.ViewModels;
using MathsExercise.DAL;
using MathsExercise.BLL;
using MathsExercise.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;



namespace MathsExercise.Controllers
{
    [Route("zh-hans/api/[controller]")]
    [Route("en/api/[controller]")]
    [Route("sv/api/[controller]")]
    [Route("api/[controller]")]
    public class MEController
    {
        private readonly MEDBContext _context;

        public MEController(MEDBContext context)
        {
            _context = context;
        }
        [HttpGet("[action]/{id?}")]
        public ReturnClass TestGet(string Id)
        {
            ReturnClass rtn= new ReturnClass();
            rtn.Message = "This is create by server and This[" + Id + " ] is received from paramter ";
            SystemLogBLL bll= new SystemLogBLL(this._context);
            SystemLog data = new SystemLog();
            data.CreateTime = DateTime.Now;
            data.Action = "Get";
            data.Message = "TestGet was executed one time , paramater Id = [" + Id.ToString() + "]";
            bll.Insert(data); 
            return rtn;
        }

        [HttpPut("[action]")]
        public ReturnClass TestPut([FromBody]TestJsonClass jsondata)
        {
            ReturnClass rtn= new ReturnClass();
            rtn.Message = "jsondata's id = " + jsondata.id.ToString() + ";  jsondata's message = " + jsondata.message + ";";
            SystemLogBLL bll= new SystemLogBLL(this._context);
            SystemLog data = new SystemLog();
            data.CreateTime = DateTime.Now;
            data.Action = "Put";
            data.Message = "TestPut was executed one time , jsondata 's Id = [" + jsondata.id.ToString() + "]jsondata's message = [" + jsondata.message + "]";
            bll.Insert(data); 
            return rtn;
        }
        
        [HttpGet("[action]/{formula?}")]
        public string Test(string Formula = "((8 + 14 - 14) * 2 + 1 ) / 3"){
            //((8 + 14 - 14) * 2 + 1 ) / 3
            Random rand = new Random();
            string s="Random.Next(1,10):";
            for(int i=0; i < 5; i++)
            {
                int x= rand.Next(1,10);
                s += x.ToString() + " ";
            }
            return s; //CalcParenthesesExpression.CalculateParenthesesExpression(Formula);
        }
        [HttpGet("[action]/{hashvalue}/{amount?}/{operation?}/{quantityarithmetic?}/{MaxValueType?}/{MaxValue?}")]
        public IEnumerable<VMathsExercise> GetQuestion(string hashvalue,int amount, string operation, int quantityarithmetic,string maxValueType, int maxValue)
        {
            #region Initial paramter
            if(hashvalue.Length!=36)
            {
                // wrong value for hashvalue;
                return null;
            }
            int _amount = 20;
            List<string> operations = new List<string>();
            int _quantityarithmetic=2;
            // ensure quantity is valid data.
            switch(amount)
            {
                case 5:
                    _amount = amount;
                    break;
                case 10:
                    _amount = amount;
                    break;
                case 30:
                    _amount = amount;
                    break;
                case 40:
                    _amount = amount;
                    break;
                case 50:
                    _amount = amount;
                    break;
                default:
                    _amount = 20;
                    break;
            }
            Char[] items = operation.ToLower().ToCharArray();
            foreach(char item in items)
            {
                if(item=='a')
                {operations.Add("+");continue;}
                if(item=='b')
                {operations.Add("-");continue;}
                if(item=='c')
                {operations.Add("*");continue;}
                if(item=='d')
                {operations.Add("/");continue;}
                
            }
            //ensure operations has one action at least.
            if(operations.Count<=0){
                operations.Add("+");
            }
            //ensure quantityarithmetic is between 1 and 5;
            switch(quantityarithmetic)
            {
                case 1:
                    _quantityarithmetic = quantityarithmetic;
                    break;
                case 3:
                    _quantityarithmetic = quantityarithmetic;
                    break;
                case 4:
                    _quantityarithmetic = quantityarithmetic;
                    break;
                case 5:
                    _quantityarithmetic = quantityarithmetic;
                    break;
                default:
                    _quantityarithmetic = 2;
                    break;
            }
            // 
            string _maxValueType = "2";
            int _maxValue = maxValue<1?20:maxValue;

            Setting setting = new Setting();
            setting.CreateTime = DateTime.Now;
            setting.Amount = _amount;
            setting.QuantityOfOperation  = _quantityarithmetic;
            setting.MaxValue = _maxValue;
            setting.GUIDValue = hashvalue;
            setting.Operations = ""; 
            foreach(string item in operations){
                setting.Operations += item;
            }
            // _context.Setting.Add(setting);
            // _context.SaveChanges();
            SettingBll settingBll = new SettingBll(this._context);
            if (settingBll.Insert(setting))
            {
                setting = settingBll.GetOne(hashvalue);
            }else{
                return null;
            }

            #endregion 
            MathsExercisesBll exerBll = new MathsExercisesBll(this._context);
            List<MathsExercises> Exercises = new List<MathsExercises>();
            for ( int i=0 ; i <_amount ;i++)
            {
                // Console.WriteLine("I=" + i.ToString());
                MathsExercises item = new MathsExercises();
                item.SettingId = setting.ID;
                item.Formula = Logic.GetRandomFormula(operations,_quantityarithmetic,_maxValueType,_maxValue);
                item.UserAnswer = "";
                item.RightAnswer = CalcParenthesesExpression.CalculateParenthesesExpression(item.Formula);
                item.CreateTime = DateTime.Now;
                item.SaveTime = null;

                // {  HashValue = guid.ToString(),Formula = "1+2", Anwser = "", CreateTime = DateTime.Now, SaveTime = null  
                // _context.Exercises.Add(item);
                Exercises.Add(item);

            }
            exerBll.Create(Exercises);

            
            List<MathsExercises>  result = exerBll.GetData(setting.ID);
            List<VMathsExercise> rtn = new List<VMathsExercise>();
            foreach(MathsExercises question in result )
            {
                VMathsExercise item = new VMathsExercise();
                item.ID = question.ID ;
                item.Formula = question.Formula;
                item.SettingId = question.SettingId;
                item.CreateTime = question.CreateTime;
                item.RightAnswer = question.RightAnswer;
                rtn.Add(item);
                
            }
            return rtn;
        }

        [HttpPut("[action]")]
        public ReturnClass PutResult([FromBody]IEnumerable<VMathsExercise> listdata){
            ReturnClass rtn = new ReturnClass();
            rtn.Message ="Great!Great!";
            MathsExercisesBll exerBll = new MathsExercisesBll(this._context);
            if (exerBll.Update(listdata))
            {
                rtn.Message = "Great! You have submitted all of answers.";
            }
            else{
                rtn.Message = "Sorry! It happened error when the system saved the answers";
            }
            return rtn;
        }
        [HttpGet("[action]/{age?}")]
        public IEnumerable<MathsExercises> GetExample(int age)
        {
            //  return _context.Exercises.ToArray<MathsExercises>();
              var rng = new Random();
              int SettingId = rng.Next(1,101);

            Guid guid = Guid.NewGuid();
            for ( int i=0 ; i <5;i++)
            {
                MathsExercises item = new MathsExercises();
                item.SettingId = SettingId;// guid.ToString();
                item.Formula = Logic.GetRandomFormula();
                item.UserAnswer = "";
                item.RightAnswer = CalcParenthesesExpression.CalculateParenthesesExpression(item.Formula);
                item.CreateTime = DateTime.Now;
                item.SaveTime = null;

                // {  HashValue = guid.ToString(),Formula = "1+2", Anwser = "", CreateTime = DateTime.Now, SaveTime = null  
                _context.Exercises.Add(item);

            
            }
            _context.SaveChanges();

            return _context.Exercises.Where(item => item.SettingId == SettingId).ToList();
            
            // return Enumerable.Range(1, 5).Select(index => new MathsExercise.Models.MathsExercises
            // {
            //     // DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
            //     // TemperatureC = rng.Next(-20, 55),
            //     // Summary = Summaries[rng.Next(Summaries.Length)]
            //     ID = index,
            //     HashValue = index.ToString(), 
            //     Formula="1+2", 
            //     Anwser ="", 
            //     CreateTime = DateTime.Today ,
            //     SaveTime = DateTime.Now

            // });
        }

        
    }

    public class ReturnClass
    {
        public string Message { get; set; }
    }
    public class TestJsonClass
    {
        public string message;
        public int id;
        
    }
}