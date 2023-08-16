using System;
using System.Threading.Tasks;

namespace SingletonDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke(
                () => PrintEmployeeDetails(), 
                () => PrintStudentDetails()
                );

            Console.ReadLine();
        }

        private static void PrintStudentDetails()
        {
            Singleton FromStudent = Singleton.GetInstance;
            FromStudent.PrintDetails("From Student: This is a message");
        }

        private static void PrintEmployeeDetails()
        {
            Singleton FromEmployee = Singleton.GetInstance;
            FromEmployee.PrintDetails("From Employee: This is a message");
        }
    }
}
