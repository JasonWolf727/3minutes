using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Resources;
using CC_X.Model;

namespace CC_X
{
    class Program : SimpleApplication, IObserver
    {
        UIElement uiRoot;
        ResourceCache cache;
        XmlFile style;
        Font font;

        Window menu;
        Window hallOfFame;
        Window help;
        Window about;
        Window setLoc;
        Window setName;
        Window chooseChar;
        Window selectDiff;
        Window giveCharName;

        Button mainMenu;
        Button helpBtn;
        Button aboutBtn;
        Button hallOfFameBtn;
        Button developerBtn;
        Button backBtn;
        Button exitBtn;
        Button newGameBtn;
        Button loadGameBtn;
        Button charOptn1;
        Button charOptn2;
        Button charOptn3;
        Button easy;
        Button medium;
        Button hard;
        Button submitCharName;
        Button cheatMode;

        Text menuBtnText;
        Text helpBtnText;
        Text aboutBtnText;
        Text hallOfFameBtnText;
        Text developerBtnText;
        Text backBtnText;
        Text exitBtnText;
        Text welcomeMsg;
        Text aboutMsg;
        Text loadGameText;
        Text newGameText;
        Text charOptn1Text;
        Text charOptn2Text;
        Text charOptn3Text;
        Text easyText;
        Text mediumText;
        Text hardText;
        Text submitCharNameText;
        Text cheatModeText;

        LineEdit xSet;
        LineEdit ySet;
        LineEdit zSet;
        LineEdit charName;

        //Create an instance of GameController
        GameController game;

        //Store scene nodes, but keep main character separate
        public Dictionary<int, Node> NodesInScene;
        Node MainChar;

        public Program(ApplicationOptions options) : base(options) { }

        protected override void Start()
        {
            base.Start();

            //Setup Root UI and chache
            uiRoot = UI.Root;            
            cache = ResourceCache;

            //Setup font and style
            style = cache.GetXmlFile("UI/DefaultStyle.xml");
            font = cache.GetFont("Fonts/Anonymous Pro.ttf");

            //Setup UI default style
            uiRoot.SetStyleAuto(style);

            //Setup main menu/title screen
            menu = uiRoot.CreateWindow("Menu", 1);
            menu.SetStyleAuto(null);
            menu.SetMinSize(300, 600);
            menu.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);            
            menu.Opacity = 0.85f;

            //Add welcome message
            welcomeMsg = menu.CreateText("Welcome", 0);
            welcomeMsg.SetFont(font, 18);
            welcomeMsg.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            welcomeMsg.Value = "Welcome to\n\r\r\rCC-X!";

            //Add buttons to main menu
            newGameBtn = menu.CreateButton("NewGameBtn", 1);
            newGameBtn.SetMinSize(230, 40);
            newGameBtn.SetStyleAuto(null);
            newGameBtn.Position = new IntVector2(35, 235);

            loadGameBtn = menu.CreateButton("LoadGameBtn", 2);
            loadGameBtn.SetMinSize(230, 40);
            loadGameBtn.SetStyleAuto(null);
            loadGameBtn.Position = new IntVector2(35, 280);

            exitBtn = menu.CreateButton("ExitBtn", 3);
            exitBtn.SetMinSize(230, 40);
            exitBtn.SetStyleAuto(null);
            exitBtn.Position = new IntVector2(35, 325); 

            helpBtn = menu.CreateButton("HelpBtn", 4);
            helpBtn.SetMinSize(100, 30);
            helpBtn.SetStyleAuto(null);
            helpBtn.SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Bottom);

            aboutBtn = menu.CreateButton("AboutBtn", 5);
            aboutBtn.SetMinSize(100, 30);
            aboutBtn.SetStyleAuto(null);
            aboutBtn.SetAlignment(HorizontalAlignment.Right, VerticalAlignment.Bottom);


            //Add text to the buttons
            newGameText = newGameBtn.CreateText("newGameText", 1);
            newGameText.SetFont(font, 16);
            newGameText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            newGameText.Value = "New Game";

            loadGameText = loadGameBtn.CreateText("loadGameText", 1);
            loadGameText.SetFont(font, 16);
            loadGameText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            loadGameText.Value = "Load Game";

            exitBtnText = exitBtn.CreateText("exitBtnText", 1);
            exitBtnText.SetFont(font, 16);
            exitBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            exitBtnText.Value = "Exit";

            helpBtnText = helpBtn.CreateText("helpBtnText", 1);
            helpBtnText.SetFont(font, 12);
            helpBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            helpBtnText.Value = "Help";

            aboutBtnText = aboutBtn.CreateText("aboutBtnText", 1);
            aboutBtnText.SetFont(font, 12);
            aboutBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            aboutBtnText.Value = "About";

            //Subscribe buttons to event handlers
            newGameBtn.SubscribeToReleased(NewGameClick);
            loadGameBtn.SubscribeToReleased(LoadGameClick);
            exitBtn.SubscribeToReleased(_ => Exit());
            helpBtn.SubscribeToReleased(HelpClick);
            aboutBtn.SubscribeToReleased(AboutClick);
        }

        protected override void OnUpdate(float timeStep)
        {
            base.OnUpdate(timeStep);
        }

        //Assigns keyboard input to corresponding developer commands. Developer commands: used for building game only.
        private void DeveloperCommands()
        {

        }

        //Assigns keyboard input to corresponding main character logic.
        private void GameCommands()
        {

        }

        //Adjusts on-screen health notification to match game.MainChar health
        private void UpdateHealth()
        {

        }
                
        //Event handler for new game button
        void NewGameClick(ReleasedEventArgs args)
        {

        }
        //Event handler for load game button
        void LoadGameClick(ReleasedEventArgs args)
        {

        }
        //Event handler for main menu button
        void MenuClick(ReleasedEventArgs args)
        {
            
        }
        //Event handler for help button
        void HelpClick(ReleasedEventArgs args)
        {

        }

        //Event handler for about button
        void AboutClick(ReleasedEventArgs args)
        {

        }
        //Event handler for high score/hall of fame button
        void HallOfFameClick(ReleasedEventArgs args)
        {

        }
        //Event handler for for back button
        void BackClick(ReleasedEventArgs args)
        {

        }
        //Event handler for character selection option 1
        void CharOptn1Click(ReleasedEventArgs args)
        {

        }
        //Event handler for character selection option 2
        void CharOptn2Click(ReleasedEventArgs args)
        {

        }
        //Event handler for character selection option 3
        void CharOptn3Click(ReleasedEventArgs args)
        {

        }
        //Event handler for developer button
        void DeveloperClick(ReleasedEventArgs args)
        {
           
        }
        //Event handler for location setter
        void setLocClick(PressedEventArgs args)
        {
                        
        }
        //Event handler for easy difficulty button
        void EasyClick(ReleasedEventArgs args)
        {

        }
        //Event handler for medium difficulty button
        void MediumClick(ReleasedEventArgs args)
        {

        }
        //Event handler for hard difficulty button
        void HardClick(ReleasedEventArgs args)
        {

        }
        //Event handler for submitCharName button
        void SubmitCharNameClick(ReleasedEventArgs args)
        {

        }
        //Event handler for cheat mode button
        void CheatModeClick(ReleasedEventArgs args)
        {

        }
        //Return node that cursor is pointing at
        protected Node GetNodeUserIsLookingAt()
        {
            throw new NotImplementedException();
        }
        
        //Return the ray that passes through the cursor's location
        public Ray GetMouseRay()
        {
            throw new NotImplementedException();
        }

        //Load and play animation from file
        void PlayAnimation(string file)
        {

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

        
    }
}
