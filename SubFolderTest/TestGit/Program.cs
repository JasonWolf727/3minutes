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
        Window menu;
        Button close;
        UIElement uiRoot;
        Font font;

        Program(ApplicationOptions options = null) :base(options) { }

        protected override void Start()
        {
            base.Start();

            var cache = ResourceCache;

            XmlFile style = cache.GetXmlFile("UI/DefaultStyle.xml");
            font = cache.GetFont("Fonts/Anonymous Pro.ttf");
            
            uiRoot = UI.Root;
            uiRoot.SetDefaultStyle(style);

            menu = uiRoot.CreateWindow("MyWindow", 1);
            menu.SetStyleAuto(null);
            menu.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            menu.SetMinSize(300, 600);
            menu.Visible = true;

            close = menu.CreateButton("Close", 1);
            close.SetStyleAuto(null);
            close.SetMinSize(100, 30);
            close.SetMaxSize(100, 30);
            close.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);

            close.SubscribeToReleased(_=>Exit());

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
