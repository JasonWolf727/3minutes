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
        Button startBtn;
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
        Text startBtnMsg;
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

        public Program(ApplicationOptions options) : base(options) { }

        protected override void Start()
        {
            base.Start();
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
                
        //Event handler for start button
        void StartClick(ReleasedEventArgs args)
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
