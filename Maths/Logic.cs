using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathsExercise.Maths
{
    public class Logic
    {
        public static string GetRandomFormula(){
            int ParameterCount = 4;
            string Formula="";
            Random rand = new Random();
            string[] Oper= new string[] {"+","-","*","/"};
            for(int i=1 ;i<= ParameterCount ;i++)
            {
                int a = rand.Next(1,100) ;
                if(i==1){
                    Formula = a.ToString() ;
                }
                else{
                    int b = rand.Next(1,Oper.Length);
                    Formula = Formula + Oper[b] + a;

                }
            }
            return Formula;
        } 
        public static string GetRandomFormula(List<string> operations,int quantityarithmetic,string maxValueType,int maxValue){
            string Formula="";
            Random rand = new Random();
            List<int> paramters= new List<int>();   //paramters.length = operations.length + 1;
            List<string> _operations =new List<string>();
            List<string> tempOperations =new List<string>();
            int item=rand.Next(1 , maxValue);
            paramters.Add(item);
            Console.Write("operations=");
            foreach (string s in operations)
            {tempOperations.Add(s);}

            Console.WriteLine("");
            Console.WriteLine("operations.count=" + operations.Count.ToString());
            
            Console.WriteLine("quantityarithmetic=" + quantityarithmetic.ToString());
            Console.WriteLine("maxValueType=" + maxValueType.ToString());
            Console.WriteLine("maxValue=" + maxValue.ToString());
            
            for(int index =1 ; index <=quantityarithmetic ; index ++ ){
                string oper;
                if(tempOperations.Count==0){
                    foreach (string s in operations)
                    {tempOperations.Add(s);}
                }
                // OBS: rand.Next[1,100] means Fetch a number in 1..99 . It is not possible to fetch 100.
                // OBS: If you want to get 100, you must use rand.Next(1,101);
                oper = tempOperations[rand.Next(0,tempOperations.Count)];
                tempOperations.Remove(oper);
                switch(oper){
                    case "+":
                        int RandomMax = maxValue;
                        item=rand.Next(1,RandomMax);
                        paramters.Add(item);
                        _operations.Add(oper);
                        Formula = GenerateFormula(paramters,_operations);
                        break;
                    case "-":
                        int value = 0;
                        RandomMax = maxValue;
                        if(Formula.Length>0){ 
                            //calculate value
                            value = System.Convert.ToInt32(CalcParenthesesExpression.CalculateParenthesesExpression(Formula));
                            Console.WriteLine("subtraction : value :" + value.ToString());
                            while( value <= 1 )
                            {
                                item=rand.Next(1,maxValue);
                                RandomMax = item;
                                // add one time of addtion
                                paramters.Add(item);
                                _operations.Add("+");
                                Formula = GenerateFormula(paramters,_operations);
                                value = System.Convert.ToInt32(CalcParenthesesExpression.CalculateParenthesesExpression(Formula));
                                Console.WriteLine("subtraction : value :" + value.ToString());
                            }
                            
                        }else{
                            // fetch first param as result.
                            //first value must bigger than 0;
                            value = paramters[paramters.Count-1];
                        }
                        //ensure that the answer > 0;
                        if(value > maxValue )
                        {
                            RandomMax = maxValue;
                        }
                        else {
                            RandomMax = value;
                        }
                        item=rand.Next(1,RandomMax);
                        paramters.Add(item);
                        _operations.Add(oper);
                
                        Formula = GenerateFormula(paramters,_operations);
                        break;
                    case "*":
                        RandomMax = maxValue;
                        item=rand.Next(1,RandomMax);
                        paramters.Add(item);
                        _operations.Add(oper);
                        Formula = GenerateFormula(paramters,_operations);
                        break;
                    case "/":
                        value = maxValue;
                        RandomMax = maxValue;
                        if(Formula.Length>0){ 
                            //calculate value
                            value = System.Convert.ToInt32(CalcParenthesesExpression.CalculateParenthesesExpression(Formula));
                            Console.WriteLine("division : value :" + value.ToString());
                            while( value <= 1 )
                            {
                                item=rand.Next(1,maxValue);
                                RandomMax = item;
                                // add one time of addtion
                                paramters.Add(item);
                                _operations.Add("+");
                                Formula = GenerateFormula(paramters,_operations);
                                value = System.Convert.ToInt32(CalcParenthesesExpression.CalculateParenthesesExpression(Formula));
                                Console.WriteLine("division : value :" + value.ToString());
                            }
                            
                        }else{
                            // fetch first param as result.
                            value = paramters[paramters.Count-1];
                        }
                        if(value > maxValue )
                        {
                            RandomMax = maxValue;
                        }
                        else {
                            RandomMax = value;
                        }
                        item=rand.Next(1,RandomMax);
                        int m = value % item;
                        if(m!=0){
                            // add one time of subtraction
                            paramters.Add(m);
                            _operations.Add("-");   
                            Formula = GenerateFormula(paramters,_operations);
                        }
                        paramters.Add(item);
                        _operations.Add(oper);
                        Formula = GenerateFormula(paramters,_operations) ;
                        break;
                    default:
                        Formula = "0+0";
                        break;   
                }     
            }
            return Formula;
        }
        private static string GenerateFormula(List<int> paramters, List<string> operations){
            string Formula ="";
            int index = 0 ;
            int operLevel = 1;
            bool IsSameLevel = true;
            string oper;
            foreach(int param in paramters)
            {
                if (Formula==""){
                    Formula = param.ToString() ;
                }else{
                    
                    oper = operations[index-1];
                    if( index==1 ) {
                        operLevel = GetOperLevel(oper);
                    }else{
                        if(operLevel==GetOperLevel(oper))
                        {
                            IsSameLevel = true;
                        }
                        else{
                            IsSameLevel = false;
                            operLevel=GetOperLevel(oper);
                        }
                    }
                    switch(oper){
                        case "*":
                            if(IsSameLevel){
                                Formula += " " + oper + " " + param.ToString();
                                
                            }else{
                                Formula = "(" + Formula + ")" + " " + oper + " " + param.ToString();
                            }
                            break;
                        case "/":
                            if(IsSameLevel){
                                Formula += " " + oper + " " + param.ToString();
                                
                            }else{
                                Formula = "(" + Formula + " )" + " " + oper + " " + param.ToString();
                            }
                            break;
                        default:
                            //addtion and substraction
                            Formula= Formula + " " + operations[index-1] + " " + param.ToString();
                            break;

                    }
                    
                }
                index++;
            }
            return Formula;
        }
        private static int GetOperLevel(string oper){
            int level;
            if(oper=="+" || oper=="-")
            {
                level = 1;
            } else if(oper=="*" || oper=="/") {
                level = 2;
            } else {
                level = 0 ;
            }
            return level;
        }
    }
}
