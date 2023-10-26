using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Reflection;
 
namespace CS
{
    using static System.Console;
 
    class Program
    {
        static void Main(string[] args)
        {
            Test testInstance = new Test();
            Type t = typeof(Test);
            MemberInfo[] members = t.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
 
            WriteLine($"Class Full Name: {t.FullName}\n");  //CS.Test
            foreach (MemberInfo member in members)
            {
                WriteLine($"Member: {member.ToString()}"); // == WriteLine($"({member})");
                WriteLine($"Name: {member.Name},\tType: {member.MemberType},\tClass: {member.DeclaringType}\n");
            }
 
            WriteLine($"testInstance.GetType(): {testInstance.GetType()}"); //CS.Test
            WriteLine($"typeof(Test): {typeof(Test)}"); //CS.Test
            WriteLine($"testInstance.GetType() == typeof(Test): {testInstance.GetType() == typeof(Test)}"); //True
        }
    }
 
    class Test
    {
        int _a;
        int _b;
 
        public Test()
        {
            _a = 1;
            _b = 2;
        }
 
        public Test(int a, int b)
        {
            _a = a;
            _b = b;
        }
 
        private void Info()
        {
            WriteLine("Test Info.");
        }
    }
}