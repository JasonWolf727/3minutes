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
    }
}
