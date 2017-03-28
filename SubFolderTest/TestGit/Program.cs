using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Resources;
using Urho.Shapes;

namespace TestGit 
{
    class Program : SimpleApplication
    {
        Program(ApplicationOptions options = null) :base(options) { }
        
        protected override void Start()
        {
            //Insert Garbage here
        }
        
        
        static void Main(string[] args)
        {
            var app = new Program(new ApplicationOptions("Data")
            {
                TouchEmulation = true,
                WindowedMode = true
            });
            app.Run();
        }


        public void MyNoReturnMethod()
        {

        }

        static void CaseHarmfull()
        {
            //Harmfull
            //Harmfull2You
            System.Console.WriteLine("Hello");
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
                System.Console.WriteLine(i);
                num += i;
            }
            int newbie = ThisShouldBeHarmlessSantana(num, 7);
            System.Console.WriteLine(newbie);
        }

        static int ThisShouldBeHarmlessSantana(int a, int b)
        {
            return a + b;
        }
    }
}
