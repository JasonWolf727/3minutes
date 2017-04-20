using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Urho;
using Urho.Shapes;
using Urho.Gui;
using Urho.Resources;
using CC_X.Model;
using Urho.Actions;
using System.Net.Sockets;
using System.Timers;

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
        Window selectDiff;
        Window giveCharName;
        Window locWindow;
        Window gameOverWind;
        Window loadGameWind;

        Button mainMenu;
        Button helpBtn;
        Button aboutBtn;
        Button hallOfFameBtn;
        Button backBtn;
        Button exitBtn;
        Button continueBtn;
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
        Button level1Btn;
        Button level2Btn;
        Button level3Btn;
        Button SaveBtn;
        Button LoadBtn;

        Text menuBtnText;
        Text helpBtnText;
        Text aboutBtnText;
        Text hallOfFameBtnText;
        Text hallOfFameMsg;
        Text backBtnText;
        Text exitBtnText;
        Text continueBtnText;
        Text welcomeMsg;
        Text aboutMsg;
        Text helpMsg;
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
        Text coordinates;
        Text time;
        Text health;
        Text messageHelper;
        Text gameOverText;
        Text level1Txt;
        Text level2Txt;
        Text level3Txt;
        Text saveText;
        Text loadBtnText;


        LineEdit xSet;
        LineEdit ySet;
        LineEdit zSet;
        LineEdit enterCharName;
        LineEdit enterFileName;

        Timer timer;
        Dispatcher Dispatcher = Dispatcher.CurrentDispatcher;

        Node currentNode;
        Node swat;
        Node ninja;
        Node mutant;
        int numNodes;
        int nodeSelect = 1;
        public bool DeveloperMode { get; set; }
        public bool GameStart { get; set; }

        public Node lightNode { get; private set; }

        public Light light { get; private set; }
        public string ForwardAniFile { get; set; }
        public string BackwardAniFile { get; set; }
        public string LeftAniFile { get; set; }
        public string RightAniFile { get; set; }
        public string IdleAniFile { get; set; }
        public string DeathAniFile { get; set; }
        public int timeTotal { get; set; }
        public int seconds { get; set; }
        public int secondsTen { get; set; }
        public int minutes { get; set; }
        public string TimeDisplay { get; set; }
        public int CurrentTime { get; set; }
        public int LastLevelTime { get; set; }
        public uint LightID { get; set; }
        public int SelectedChar { get; set; }

        //Create an instance of GameController
        GameController game = new GameController(Difficulty.Easy); //Temp difficulty
        HighScore HS = new HighScore();

        //Store scene nodes, but keep main character separate
        public Dictionary<int, Node> NodesInScene;
        Node MainChar;
        int Car2ID;

        public Program(ApplicationOptions options) : base(options) { }

        protected override void Start()
        {
            base.Start();

            //Set up scene and add light to the scene
            SetupScene();

            //Setup Root UI and chache
            uiRoot = UI.Root;
            cache = ResourceCache;

            //Setup font and style
            style = cache.GetXmlFile("UI/DefaultStyle.xml");
            font = cache.GetFont("Fonts/Anonymous Pro.ttf");

            //Setup UI default style
            uiRoot.SetStyleAuto(style);

            //Setup the menu
            SetupWindows();

            //Add buttons to main menu
            SetupButtons();

            //Set up Developer tools
            SetupDeveloperMode();

            //Establish a Location Setter Window
            SetupLSW();

            //Turn off game controls
            GameStart = false;
        }

        protected void SetupScene()
        {
            //Generate scene light and shadow effects
            lightNode = Scene.CreateChild("DirectionalLight");
            lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
            LightID = lightNode.ID;
            var light = lightNode.CreateComponent<Light>();
            light.LightType = LightType.Directional;
            light.CastShadows = true;
            light.ShadowBias = new BiasParameters(0.00025f, 0.5f);
            light.ShadowCascade = new CascadeParameters(10.0f, 50.0f, 200.0f, 0.0f, 0.8f);
            light.SpecularIntensity = 0.05f;
            light.Color = new Color(1.2f, 1.2f, 1.2f);
            light.Brightness = 0.75f;
           
            //Set up camera pos
            CameraNode.Position = new Vector3(75, 0, 0);

            //Set up timer for game
            TimeDisplay = "0:00";
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Elapsed_Interval;            
        }

        private void Elapsed_Interval(object sender, ElapsedEventArgs e)
        {
            ++timeTotal;
            CurrentTime = timeTotal;
            if(timeTotal%60 == 0 && timeTotal != 0)
            {
                secondsTen = 0;
                seconds = 0;
                ++minutes;
            }
            else if (timeTotal % 10 == 0 && timeTotal != 0)
            {
                seconds = 0;
                ++secondsTen;
            }
            else
            {
                ++seconds;
            }
            TimeDisplay = minutes + ":" + secondsTen + seconds;
        }
        public void ResetTime()
        {
            timeTotal = 0;
            seconds = 0;
            secondsTen = 0;
            minutes = 0;
            CurrentTime = 0;
            timer.Stop();
        }

        public void UpdateLastLevelTime()
        {
            LastLevelTime = timeTotal;
            game.LastLevelTime = LastLevelTime;
        }
        public void UpdateCurrentTime()
        {
            CurrentTime = timeTotal;
            game.CurrentTime = timeTotal;
        }
        protected void SetupWindows()
        {
            //Setup main menu/title screen
            menu = uiRoot.CreateWindow();
            menu.SetStyleAuto(null);
            menu.SetMinSize(300, 600);
            menu.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            menu.Opacity = 0.85f;

            //Add welcome message
            welcomeMsg = menu.CreateText("Welcome", 0);
            welcomeMsg.SetFont(font, 18);
            welcomeMsg.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            welcomeMsg.Value = "Welcome to\n\r\r\rCC-X!";

            //Setup High Score screen
            hallOfFame = uiRoot.CreateWindow();
            hallOfFame.SetStyleAuto(null);
            hallOfFame.SetMinSize(300, 600);
            hallOfFame.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            hallOfFame.Opacity = 0.85f;
            hallOfFame.Visible = false;

            //Add High Score message
            hallOfFameMsg = hallOfFame.CreateText();
            hallOfFameMsg.SetFont(font, 18);
            hallOfFameMsg.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            hallOfFameMsg.Value = "Hall of Fame\n";

            //Setup About screen
            about = uiRoot.CreateWindow();
            about.SetStyleAuto(null);
            about.SetMinSize(300, 600);
            about.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            about.Opacity = 0.85f;
            about.Visible = false;

            //Add About message
            aboutMsg = about.CreateText();
            aboutMsg.SetFont(font, 18);
            aboutMsg.SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Top);
            aboutMsg.Value = "         About\n\n\n\n   This program was\n       created by\n\n     Joshua Case\n     Michael Johannes\n     Carlos Santana\n\n     March 14, 2017\n           to\n     April 24, 2017\n\n     Please enjoy\n     responsibly!\n \n\n    Models used by\n      permission\n";

            //Setup Help screen
            help = uiRoot.CreateWindow();
            help.SetStyleAuto(null);
            help.SetMinSize(300, 600);
            help.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            help.Opacity = 0.85f;
            help.Visible = false;

            //Setup Name Char Window
            giveCharName = uiRoot.CreateWindow("LocationWindow", 8);
            giveCharName.SetStyleAuto(null);
            giveCharName.SetMinSize(300, 165);
            giveCharName.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            giveCharName.Opacity = 0.85f;
            giveCharName.Visible = false;

            submitCharNameText = giveCharName.CreateText();
            submitCharNameText.SetFont(font, 18);
            submitCharNameText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            submitCharNameText.Value = "\r\r\r\rEnter a name: ";            

            //Add text input to Name Main Char window
            enterCharName = giveCharName.CreateLineEdit("EnterCharName", 2);

            enterCharName.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            enterCharName.SetMinSize(280, 30);
            enterCharName.SetMaxSize(280, 30);
            enterCharName.SetStyleAuto(null);            

            //Add Help message
            helpMsg = help.CreateText();
            helpMsg.SetFont(font, 18);
            helpMsg.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            helpMsg.Value = "      Help\n\n\n  Use the arrow\n  keys to move\n\n       /\\ \n       || \n   <=      =>\n       ||\n       \\/ \n\n  Click and move\n  the mouse to\n  pan the screen\n  left and right\n\n   ----------\n   |        |\n   | <    > |\n   |        |\n   ----------\n ";

            //Set up timer for game
            time = uiRoot.CreateText("Time", 13);
            time.SetFont(font, 15);
            time.SetAlignment(HorizontalAlignment.Right, VerticalAlignment.Top);
            time.Value = "";
            time.SetColor(Color.Red);
            time.Visible = false;

            //Set up health for game
            health = uiRoot.CreateText("Time", 13);
            health.SetFont(font, 15);
            health.SetPosition(900,20);
            health.Value = "";
            health.SetColor(Color.Red);
            health.Visible = false;

            //Setup End of level screen
            gameOverWind = uiRoot.CreateWindow();
            gameOverWind.SetStyleAuto(null);
            gameOverWind.SetMinSize(300, 600);
            gameOverWind.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            gameOverWind.Opacity = 0.85f;
            gameOverWind.Visible = false;

            //Add end of level message
            gameOverText = gameOverWind.CreateText();
            gameOverText.SetFont(font, 18);
            gameOverText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            gameOverText.Value = "Your time: 0 seconds";

            mainMenu = gameOverWind.CreateButton();
            mainMenu.SetMinSize(100, 30);
            mainMenu.SetStyleAuto(null);
            mainMenu.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
            mainMenu.SubscribeToReleased(BackClick);

            menuBtnText = mainMenu.CreateText("backBtnText", 1);
            menuBtnText.SetFont(font, 12);
            menuBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            menuBtnText.Value = "Menu";
            mainMenu.SubscribeToReleased(EndLevelClick);

            //Load game window
            loadGameWind = uiRoot.CreateWindow();
            loadGameWind.SetStyleAuto(null);
            loadGameWind.SetMinSize(300, 600);
            loadGameWind.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            loadGameWind.Opacity = 0.85f;
            loadGameWind.Visible = false;
        }

        protected void SetupButtons()
        {
            //Load each button with their respective attributes
            newGameBtn = menu.CreateButton("NewGameBtn", 1);
            newGameBtn.SetMinSize(230, 40);
            newGameBtn.SetStyleAuto(null);
            newGameBtn.Position = new IntVector2(35, 235);

            loadGameBtn = menu.CreateButton("LoadGameBtn", 2);
            loadGameBtn.SetMinSize(230, 40);
            loadGameBtn.SetStyleAuto(null);
            loadGameBtn.Position = new IntVector2(35, 280);

            hallOfFameBtn = menu.CreateButton("HallOfFameBtn", 6);
            hallOfFameBtn.SetMinSize(230, 40);
            hallOfFameBtn.SetStyleAuto(null);
            hallOfFameBtn.Position = new IntVector2(35, 325);            

            continueBtn = menu.CreateButton("ExitBtn", 3);
            continueBtn.SetMinSize(230, 40);
            continueBtn.SetStyleAuto(null);
            continueBtn.Position = new IntVector2(35, 370);

            exitBtn = menu.CreateButton("ExitBtn", 3);
            exitBtn.SetMinSize(230, 40);
            exitBtn.SetStyleAuto(null);
            exitBtn.Position = new IntVector2(35, 415);

            helpBtn = menu.CreateButton("HelpBtn", 4);
            helpBtn.SetMinSize(100, 30);
            helpBtn.SetStyleAuto(null);
            helpBtn.SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Bottom);

            aboutBtn = menu.CreateButton("AboutBtn", 5);
            aboutBtn.SetMinSize(100, 30);
            aboutBtn.SetStyleAuto(null);
            aboutBtn.SetAlignment(HorizontalAlignment.Right, VerticalAlignment.Bottom);

            //Add set button to Name Main Char window
            submitCharName = giveCharName.CreateButton("SubmitCharNameBtn", 1);
            submitCharName.SetStyleAuto(null);
            submitCharName.SetMinSize(100, 30);
            submitCharName.SetMaxSize(100, 30);
            submitCharName.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
            submitCharName.SubscribeToReleased(SubmitCharNameClick);            

            level1Btn = gameOverWind.CreateButton("Level1Btn", 1);
            level1Btn.SetMinSize(230, 40);
            level1Btn.SetStyleAuto(null);
            level1Btn.Position = new IntVector2(35, 250);
            level1Btn.Visible = false;

            level2Btn = gameOverWind.CreateButton("Level2Btn", 2);
            level2Btn.SetMinSize(230, 40);
            level2Btn.SetStyleAuto(null);
            level2Btn.Position = new IntVector2(35, 295);
            level2Btn.Visible = false;

            level3Btn = gameOverWind.CreateButton("Level3Btn", 3);
            level3Btn.SetMinSize(230, 40);
            level3Btn.SetStyleAuto(null);
            level3Btn.Position = new IntVector2(35, 340);
            level3Btn.Visible = false;

            easy = menu.CreateButton("EasyBtn", 12);
            easy.SetMinSize(230, 40);
            easy.SetStyleAuto(null);
            easy.Position = new IntVector2(35, 235);
            easy.Visible = false;

            medium = menu.CreateButton("EasyBtn", 12);
            medium.SetMinSize(230, 40);
            medium.SetStyleAuto(null);
            medium.Position = new IntVector2(35, 280);
            medium.Visible = false;

            hard = menu.CreateButton("EasyBtn", 12);
            hard.SetMinSize(230, 40);
            hard.SetStyleAuto(null);
            hard.Position = new IntVector2(35, 325);
            hard.Visible = false;

            LoadBtn = loadGameWind.CreateButton("EasyBtn", 12);
            LoadBtn.SetMinSize(100,30);
            LoadBtn.SetStyleAuto(null);
            LoadBtn.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);

            enterFileName = loadGameWind.CreateLineEdit("XSet", 2);

            enterFileName.Position = new IntVector2(10, 535);
            enterFileName.SetMinSize(280, 30);
            enterFileName.SetMaxSize(280, 30);
            enterFileName.SetStyleAuto(null);

            //Add text to the buttons
            newGameText = newGameBtn.CreateText("newGameText", 1);
            newGameText.SetFont(font, 16);
            newGameText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            newGameText.Value = "New Game";

            loadGameText = loadGameBtn.CreateText("loadGameText", 1);
            loadGameText.SetFont(font, 16);
            loadGameText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            loadGameText.Value = "Load Game";

            hallOfFameBtnText = hallOfFameBtn.CreateText("aboutBtnText", 1);
            hallOfFameBtnText.SetFont(font, 16);
            hallOfFameBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            hallOfFameBtnText.Value = "High Scores";

            continueBtnText = continueBtn.CreateText("ContinueBtnText", 1);
            continueBtnText.SetFont(font, 16);
            continueBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            continueBtnText.Value = "Cont. Progress";

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

            Text nameText = submitCharName.CreateText("NameText", 1);
            nameText.SetFont(font, 12);
            nameText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            nameText.Value = "Submit";

            level1Txt = level1Btn.CreateText("aboutBtnText", 1);
            level1Txt.SetFont(font, 16);
            level1Txt.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            level1Txt.Value = "Level 1";

            level2Txt = level2Btn.CreateText("aboutBtnText", 1);
            level2Txt.SetFont(font, 16);
            level2Txt.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            level2Txt.Value = "Level 2";

            level3Txt = level3Btn.CreateText("aboutBtnText", 1);
            level3Txt.SetFont(font, 16);
            level3Txt.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            level3Txt.Value = "Level 3";

            easyText = easy.CreateText("EasyText", 1);
            easyText.SetFont(font, 16);
            easyText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            easyText.Value = "Easy";

            mediumText = medium.CreateText("MediumText", 1);
            mediumText.SetFont(font, 16);
            mediumText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            mediumText.Value = "Medium";

            hardText = hard.CreateText("HardText", 1);
            hardText.SetFont(font, 16);
            hardText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            hardText.Value = "Hard";

            loadBtnText = LoadBtn.CreateText("LoadGAmeText", 1);
            loadBtnText.SetFont(font, 12);
            loadBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            loadBtnText.Value = "Load";

            //Subscribe buttons to event handlers
            newGameBtn.SubscribeToReleased(NewGameClick);
            loadGameBtn.SubscribeToReleased(LoadGameClick);
            LoadBtn.SubscribeToReleased(SubmitFileClick);
            continueBtn.SubscribeToReleased(ContinueClick);
            exitBtn.SubscribeToReleased(_ => Exit());
            helpBtn.SubscribeToReleased(HelpClick);
            aboutBtn.SubscribeToReleased(AboutClick);
            submitCharName.SubscribeToReleased(SubmitCharNameClick);
            hallOfFameBtn.SubscribeToReleased(HallOfFameClick);
            level1Btn.SubscribeToReleased(LoadLevel1);
            level2Btn.SubscribeToReleased(LoadLevel2);
            level3Btn.SubscribeToReleased(LoadLevel3);
            easy.SubscribeToReleased(EasyClick);
            medium.SubscribeToReleased(MediumClick);
            hard.SubscribeToReleased(HardClick);
        }

        protected void SetupDeveloperMode()
        {
            coordinates = uiRoot.CreateText("Coordinates", 6);
            coordinates.SetFont(font, 12);
            coordinates.SetAlignment(HorizontalAlignment.Right, VerticalAlignment.Top);
            coordinates.Value = "";
            coordinates.SetStyle("Text", null);
            coordinates.SetColor(Color.Black);
            coordinates.Visible = false;
            DeveloperMode = false;

            messageHelper = uiRoot.CreateText("MessageHelper", 3);
            messageHelper.SetFont(font, 14);
            messageHelper.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            messageHelper.SetColor(Color.Red);
            messageHelper.Value = "";
            messageHelper.Visible = false;
        }

        protected void SetupLSW()
        {
            //Set up location setter window
            locWindow = uiRoot.CreateWindow("LocationWindow", 8);
            locWindow.SetStyleAuto(null);
            locWindow.SetMinSize(300, 165);
            locWindow.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            locWindow.Opacity = 0.85f;
            locWindow.Visible = false;

            //Add set button to loc setter window
            Button set = locWindow.CreateButton("SetBtn", 1);
            set.SetStyleAuto(null);
            set.SetMinSize(100, 30);
            set.SetMaxSize(100, 30);
            set.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
            set.SubscribeToPressed(setLocClick);

            Text setText = set.CreateText("SetText", 1);
            setText.SetFont(font, 12);
            setText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            setText.Value = "Set";

            //Add text input to loc setter window
            xSet = locWindow.CreateLineEdit("XSet", 2);
            ySet = locWindow.CreateLineEdit("YSet", 3);
            zSet = locWindow.CreateLineEdit("ZSet", 4);

            xSet.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            xSet.SetMinSize(280, 30);
            xSet.SetMaxSize(280, 30);
            xSet.SetStyleAuto(null);
            xSet.Text = "X: ";

            ySet.Position = new IntVector2(10, 45);
            ySet.SetMinSize(280, 30);
            ySet.SetMaxSize(280, 30);
            ySet.SetStyleAuto(null);
            ySet.Text = "Y: ";

            zSet.Position = new IntVector2(10, 90);
            zSet.SetMinSize(280, 30);
            zSet.SetMaxSize(280, 30);
            zSet.SetStyleAuto(null);
            zSet.Text = "Z: ";
        }

        protected override void OnUpdate(float timeStep)
        {
            base.OnUpdate(timeStep);
            if (Input.GetKeyPress(Key.M) && (menu.Visible == true | timeTotal > 2))
            {
                DeveloperMode = !DeveloperMode;
                coordinates.Visible = !coordinates.Visible;
            } 
            if(DeveloperMode)
            {
                DeveloperCommands(timeStep);
            }        
            if(GameStart)
            {
                GameCommands(timeStep);
                game.MainChar.Position = MainChar.Position;
            }            
        }

        //Assigns keyboard input to corresponding developer commands. Developer commands: used for building game only.
        private void DeveloperCommands(float timeStep)
        {
            time.Visible = false;

            if(menu.Visible == true)
            {
                menu.Visible = false;
            }
            if (Input.GetKeyPress(Key.N1)) nodeSelect = 1;
            if (Input.GetKeyPress(Key.N2)) nodeSelect = 2;
            if (Input.GetKeyPress(Key.N3)) nodeSelect = 3;
            if (Input.GetKeyPress(Key.N4)) nodeSelect = 4;
            if (Input.GetKeyPress(Key.N5)) nodeSelect = 5;

            if (currentNode != null)
            {
                coordinates.Value = "Current node type:" + GetSelectedNodeType() + "\n\n" + currentNode.Name + ": (" + currentNode.Position.X.ToString() + ", " + currentNode.Position.Y.ToString() + ", " + currentNode.Position.Z.ToString() + ")\n\nBack To Menu: M key\nSelect: G key\nSet Loc: L key\nInsert Node: INS Key\nMove Node: Keypad\nRotate: T Key" + "\nCamera Rotation:\n(" + CameraNode.Rotation.X.ToString() + ", " + CameraNode.Rotation.Y.ToString() + ", " + CameraNode.Rotation.Z.ToString() + "\nNode Rotation:\n(" + currentNode.Rotation.X.ToString() + ", " + currentNode.Rotation.Y.ToString() + ", " + currentNode.Rotation.Z.ToString() /*+ "\n" + CameraNode.Position.X + "," + CameraNode.Position.Y + "," + CameraNode.Position.Z*/;
            }
            else
            {
                coordinates.Value = "Current node type:" + GetSelectedNodeType() + "\n\n--: (--, --, --)\n\nBack To Menu: M key\nSelect: G key\nSet Loc: L key\nInsert Node: INS Key\nMove Node: Keypad\nRotate: T Key\nCamera Rotation:\n(" + CameraNode.Rotation.X.ToString() + ", " + CameraNode.Rotation.Y.ToString() + ", " + CameraNode.Rotation.Z.ToString();
            }

            MoveCamera = true;
            MoveSpeed = 10f;
            float speed = MoveSpeed;

            if (Input.GetKeyDown(Key.Shift))
                speed *= 0.2f;

            if (Input.GetKeyDown(Key.W)) CameraNode.Translate(Vector3.UnitZ * speed * timeStep);
            if (Input.GetKeyDown(Key.S)) CameraNode.Translate(-Vector3.UnitZ * speed * timeStep);
            if (Input.GetKeyDown(Key.A)) CameraNode.Translate(-Vector3.UnitX * speed * timeStep);
            if (Input.GetKeyDown(Key.D)) CameraNode.Translate(Vector3.UnitX * speed * timeStep);
            if (Input.GetKeyDown(Key.PageUp)) CameraNode.Translate(Vector3.UnitY * speed * timeStep);
            if (Input.GetKeyDown(Key.PageDown)) CameraNode.Translate(-Vector3.UnitY * speed * timeStep);
            if (Input.GetKeyDown(Key.R))
            {
                // Reset camera
                CameraNode.Position = new Vector3(75,0,0);
                CameraNode.Rotation = new Quaternion(-0.2f,0,0); //Quaternion.Identity
            }            
            //Create a node
            if (Input.GetKeyPress(Key.Insert)) CreateNode();

            //Select a node
            if (Input.GetKeyPress(Key.G)) currentNode = GetNodeUserIsLookingAt();

            //Bring up location setter
            if (Input.GetKeyPress(Key.L) && currentNode != null && locWindow.Visible == false)
            {
                BringUpLocSetter();
            }

            if (currentNode != null)
            {
                if (Input.GetKeyDown(Key.KP_9)) currentNode.Translate(Vector3.UnitZ * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.KP_5)) currentNode.Translate(-Vector3.UnitZ * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.KP_8)) currentNode.Translate(Vector3.UnitY * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.KP_2)) currentNode.Translate(-Vector3.UnitY * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.KP_4)) currentNode.Translate(-Vector3.UnitX * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.KP_6)) currentNode.Translate(Vector3.UnitX * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.T)) currentNode.Rotate(new Quaternion(0.5f, 0, 0), TransformSpace.World);
            }
            if (currentNode != null)
            {
                if (Input.GetKeyDown(Key.I)) currentNode.Translate(Vector3.UnitZ * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.J)) currentNode.Translate(-Vector3.UnitZ * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.U)) currentNode.Translate(Vector3.UnitY * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.N)) currentNode.Translate(-Vector3.UnitY * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.H)) currentNode.Translate(-Vector3.UnitX * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.K)) currentNode.Translate(Vector3.UnitX * speed * timeStep * 0.2f, TransformSpace.World);
                if (Input.GetKeyDown(Key.T)) currentNode.Rotate(new Quaternion(0.5f, 0, 0), TransformSpace.World);
            }
        }

        //Assigns keyboard input to corresponding main character logic.
        private void GameCommands(float timeStep)
        {
            MoveCamera = true;
            time.Value = TimeDisplay;
            time.Visible = true;
            health.Visible = true;
            UpdateHealth();
            game.EndLevel();
            UpdateGameObjPos();
            MoveCars(timeStep);
            level1Btn.Visible = true;

            if(Input.GetKeyPress(Key.C)) { game.EnableCheat(); }

            float speed;
            if (game.CheatModeEn) { speed = 10; }
            else { speed = 1; }

            if (game.MainChar.Health <= 0 |(game.GameOver && timeTotal > 10))
            {
                if (game.MainChar.Health <= 0) PlayAnimation(MainChar, DeathAniFile);
                else PlayAnimation(MainChar, IdleAniFile);

                game.GameOver = false;
                GameStart = false;
                UpdateLastLevelTime();
                ResetTime();
                gameOverText.Value = "Level " + game.GetLevelStatus() + "!\nUser Name: " + game.MainCharName + "\nYour time: " + LastLevelTime + " seconds\nQualifying time: " + game.GetQualTime() + "\nStatus: " + game.MainChar.GetStatus() + "\nTotal Experience: " + game.MainChar.Experience;
                LevelAdvanceAssess();
                gameOverWind.Visible = true;
                HS.AddHighScore(game.MainCharName, LastLevelTime);              

            }
            else
            {
                List<object> collisionData = game.DetectCollision();
                if (Input.GetKeyDown(Key.Up) && MainChar.Position.Z <= 144 /*&& (((bool)(collisionData[0])) != true | ((Vector3)(collisionData[1])).Z < MainChar.Position.Z)*/) { CameraNode.Translate(Vector3.UnitZ * timeStep * speed * 2, TransformSpace.World); MainChar.Translate(Vector3.UnitZ * timeStep * speed * 2, TransformSpace.World); PlayAnimation(MainChar, ForwardAniFile); }
                else if (Input.GetKeyDown(Key.Down) && MainChar.Position.Z >= 1.4 /*&& (((bool)(collisionData[0])) != true | ((Vector3)(collisionData[1])).Z > MainChar.Position.Z)*/) { CameraNode.Translate(-Vector3.UnitZ * timeStep * speed * 2, TransformSpace.World); MainChar.Translate(-Vector3.UnitZ * timeStep * speed * 2, TransformSpace.World); PlayAnimation(MainChar, BackwardAniFile); }
                else if (Input.GetKeyDown(Key.Left) && MainChar.Position.X >= 1.5f /*&& (((bool)(collisionData[0])) != true | ((Vector3)(collisionData[1])).X > MainChar.Position.X)*/) { CameraNode.Translate(-Vector3.UnitX * timeStep * speed * 2, TransformSpace.World); MainChar.Translate(-Vector3.UnitX * timeStep * speed * 2, TransformSpace.World); PlayAnimation(MainChar, LeftAniFile); }
                else if (Input.GetKeyDown(Key.Right) && MainChar.Position.X <= 148 /*&& (((bool)(collisionData[0])) != true | ((Vector3)(collisionData[1])).X < MainChar.Position.X)*/) { CameraNode.Translate(Vector3.UnitX * timeStep * speed * 2, TransformSpace.World); MainChar.Translate(Vector3.UnitX * timeStep * speed * 2, TransformSpace.World); PlayAnimation(MainChar, RightAniFile); }
                else
                {
                    PlayAnimation(MainChar, IdleAniFile);                    
                }
            }            
        }

        private void UpdateGameObjPos()
        {
            foreach(GameObj obj in game.GameObjCollection.Values)
            {
                if(obj is Enemy)
                {
                    Node node = Scene.GetNode(obj.ID);
                    obj.Position = node.Position;
                    ((Enemy)(obj)).persnlBubble.X = Convert.ToInt32(node.Position.X);
                    ((Enemy)(obj)).persnlBubble.Y = Convert.ToInt32(node.Position.Z);
                }
            }
            game.MainChar.Position = MainChar.Position;
            game.MainChar.persnlBubble.X = Convert.ToInt32(MainChar.Position.X);
            game.MainChar.persnlBubble.Y = Convert.ToInt32(MainChar.Position.Z);
        }

        //Move the car to the right 1 timestep. 
        //If X > 80 resets the cars position.
        private void MoveCar(float timeStep)
        {
            //FarLaneCar
            if (game.GameObjCollection[Convert.ToUInt32((Car2ID - 1))].Position.X > 80f)
            {
                Vector3 resetPosition = new Vector3(65f, -.5f, 104.3f);
                game.GameObjCollection[Convert.ToUInt32((Car2ID - 1))].Position = resetPosition;
            }
            else
            {
                Vector3 movePosition = new Vector3(game.GameObjCollection[Convert.ToUInt32((Car2ID - 1))].Position.X + timeStep, -.5f, 104.3f);
                game.GameObjCollection[Convert.ToUInt32((Car2ID - 1))].Position = movePosition;
            }

            //CloseLaneCar
            if (game.GameObjCollection[Convert.ToUInt32((Car2ID))].Position.X > 80f)
            {
                Vector3 resetPosition = new Vector3(65f, -.5f, 103.5f);
                game.GameObjCollection[Convert.ToUInt32((Car2ID))].Position = resetPosition;
            }
            else
            {
                Vector3 movePosition = new Vector3(game.GameObjCollection[Convert.ToUInt32((Car2ID))].Position.X + timeStep, -.5f, 103.5f);
                game.GameObjCollection[Convert.ToUInt32((Car2ID))].Position = movePosition;
            }
        }

        //Adjusts on-screen health notification to match game.MainChar health
        private void UpdateHealth()
        {
            game.DetectCollision();
            health.Value = "Health: " + game.MainChar.Health.ToString();                        
        }
                
        //Event handler for new game button
        void NewGameClick(ReleasedEventArgs args)
        {
            newGameBtn.Visible = false;
            loadGameBtn.Visible = false;
            hallOfFameBtn.Visible = false;
            aboutBtn.Visible = false;
            helpBtn.Visible = false;
            exitBtn.Visible = false;
            continueBtn.Visible = false;

            easy.Visible = true;
            medium.Visible = true;
            hard.Visible = true;
        }
        void LoadLevel1(ReleasedEventArgs args)
        {
            gameOverWind.Visible = false;
            game.SetUpLevel(Level.One);
            GameStart = true;
        }

        void LoadLevel2(ReleasedEventArgs args)
        {
            gameOverWind.Visible = false;
            game.SetUpLevel(Level.Two);
            GameStart = true;
        }

        void LoadLevel3(ReleasedEventArgs args)
        {
            gameOverWind.Visible = false;
            game.SetUpLevel(Level.Three);
            GameStart = true;
        }
        //Event handler for load game button
        void LoadGameClick(ReleasedEventArgs args)
        {
            menu.Visible = false;
            loadGameWind.Visible = true;
            string filepath = "C:\\Users\\csant714\\Desktop\\3minutes\\CC-X\\CC-X\\Model\\LoadSaveTest.csv";
            //Console.WriteLine(filepath);

        }
        void SubmitFileClick(ReleasedEventArgs args)
        {
            loadGameWind.Visible = false;
            menu.Visible = true;
        }
        void ContinueClick(ReleasedEventArgs args)
        {
            menu.Visible = false;
            if(game.MainCharName != "")
            {
                string diff = "";
                if(game.DifficutlySelected == Difficulty.Easy) { diff = "Easy"; }
                else if (game.DifficutlySelected == Difficulty.Medium) { diff = "Medium"; }
                else if (game.DifficutlySelected == Difficulty.Hard) { diff = "Hard"; }
                gameOverText.Value = "\nUser Name: " + game.MainCharName + "\nTotal Experience: 0" + game.MainChar.Experience + "\nDifficulty: " + diff;                
            }
            else
            {
                gameOverText.Value = "\nUser Name: " + game.MainCharName + "\nTotal Experience: 0\nDifficulty: ";

            }
            gameOverWind.Visible = true;
        }
        //Event handler for main menu button
        void MenuClick(ReleasedEventArgs args)
        {
            
        }
        //Event handler for help button
        void HelpClick(ReleasedEventArgs args)
        {
            menu.Visible = false;
            help.Visible = true;

            backBtn = help.CreateButton();
            backBtn.SetMinSize(100, 30);
            backBtn.SetStyleAuto(null);
            backBtn.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
            backBtn.SubscribeToReleased(BackClick);

            backBtnText = backBtn.CreateText("backBtnText", 1);
            backBtnText.SetFont(font, 12);
            backBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            backBtnText.Value = "Back";
        }
        void EndLevelClick(ReleasedEventArgs args)
        {
            gameOverWind.Visible = false;
            menu.Visible = true;            
        }

        //Event handler for about button
        void AboutClick(ReleasedEventArgs args)
        {
            menu.Visible = false;
            about.Visible = true;

            backBtn = about.CreateButton();
            backBtn.SetMinSize(100, 30);
            backBtn.SetStyleAuto(null);
            backBtn.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
            backBtn.SubscribeToReleased(BackClick);

            backBtnText = backBtn.CreateText("backBtnText", 1);
            backBtnText.SetFont(font, 12);
            backBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            backBtnText.Value = "Back";
        }
        //Event handler for high score/hall of fame button
        void HallOfFameClick(ReleasedEventArgs args)
        {
            menu.Visible = false;
            hallOfFame.Visible = true;

            backBtn = hallOfFame.CreateButton();
            backBtn.SetMinSize(100, 30);
            backBtn.SetStyleAuto(null);
            backBtn.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
            backBtn.SubscribeToReleased(BackClick);

            backBtnText = backBtn.CreateText("backBtnText", 1);
            backBtnText.SetFont(font, 12);
            backBtnText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            backBtnText.Value = "Back";

            string finalScoreMsg = "\n  High Scores\n Fastest Times\n" + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            List<string> scoresToDisplay = HS.GetHighScores();
            for (int pos = 0; pos < scoresToDisplay.Count(); pos++)
            {
                finalScoreMsg += "   " + scoresToDisplay[pos].ToString() + Environment.NewLine;
            }
            hallOfFameMsg.Value = finalScoreMsg;
        }
        //Event handler for for back button
        void BackClick(ReleasedEventArgs args)
        {
            about.Visible = false;
            hallOfFame.Visible = false;
            help.Visible = false;
            menu.Visible = true;
        }
        //Event handler for character selection option 1
        void CharOptn1Click(ReleasedEventArgs args)
        {
            swat.Remove();
            ninja.Remove();
            mutant.Remove();
            

            charOptn1.Visible = false;
            charOptn2.Visible = false;
            charOptn3.Visible = false;

            CreateMainChar(1);

            GameStart = true;
            DeveloperMode = false;

            //Create level 1
            game.SetUpLevel(Level.One);
        }        
        //Event handler for character selection option 2
        void CharOptn2Click(ReleasedEventArgs args)
        {
            swat.Remove();
            ninja.Remove();
            mutant.Remove();            

            charOptn1.Visible = false;
            charOptn2.Visible = false;
            charOptn3.Visible = false;

            CreateMainChar(2);

            GameStart = true;
            DeveloperMode = false;

            //Create level 1
            game.SetUpLevel(Level.One);
        }
        //Event handler for character selection option 3
        void CharOptn3Click(ReleasedEventArgs args)
        {
            swat.Remove();
            ninja.Remove();
            mutant.Remove();            

            charOptn1.Visible = false;
            charOptn2.Visible = false;
            charOptn3.Visible = false;

            CreateMainChar(3);

            GameStart = true;
            DeveloperMode = false;

            //Create level 1
            game.SetUpLevel(Level.One);
        }
        //Event handler for developer button
        void DeveloperClick(ReleasedEventArgs args)
        {
           
        }
        //Event handler for location setter
        void setLocClick(PressedEventArgs args)
        {
            string xStr = xSet.Text;
            string yStr = ySet.Text;
            string zStr = zSet.Text;
            try
            {
                float xCoor = (float)(Convert.ToDouble(xStr.Replace("X: ", "").Trim()));
                float yCoor = (float)(Convert.ToDouble(yStr.Replace("Y: ", "").Trim()));
                float zCoor = (float)(Convert.ToDouble(zStr.Replace("Z: ", "").Trim()));

                //currentNode.Position = new Vector3(xCoor, yCoor, zCoor);
                currentNode.Position = new Vector3(xCoor, yCoor, zCoor);

            }
            catch (Exception e)
            {
                messageHelper.Value = "Do not delete 'X: ','Y: ', or 'Z: '. Enter only numbers.";
                messageHelper.Visible = true;
            }

            locWindow.Visible = false;
            MoveCamera = true;
        }
        public void SetUpCharSelection()
        {
            menu.Visible = false;

            //Set up Character selection
            charOptn1 = uiRoot.CreateButton("Select1", 10);
            charOptn1.SetMinSize(100, 30);
            charOptn1.SetStyleAuto(null);
            charOptn1.SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Center);
            charOptn1.SubscribeToReleased(CharOptn1Click);

            charOptn1Text = charOptn1.CreateText();
            charOptn1Text.SetFont(font, 16);
            charOptn1Text.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            charOptn1Text.Value = "Select";

            charOptn2 = uiRoot.CreateButton("Select2", 11);
            charOptn2.SetMinSize(100, 30);
            charOptn2.SetStyleAuto(null);
            charOptn2.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            charOptn2.SubscribeToReleased(CharOptn2Click);

            charOptn2Text = charOptn2.CreateText();
            charOptn2Text.SetFont(font, 16);
            charOptn2Text.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            charOptn2Text.Value = "Select";

            charOptn3 = uiRoot.CreateButton("Select3", 12);
            charOptn3.SetMinSize(100, 30);
            charOptn3.SetStyleAuto(null);
            charOptn3.SetAlignment(HorizontalAlignment.Right, VerticalAlignment.Center);
            charOptn3.SubscribeToReleased(CharOptn3Click);

            charOptn3Text = charOptn3.CreateText();
            charOptn3Text.SetFont(font, 16);
            charOptn3Text.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            charOptn3Text.Value = "Select";

            //Display characters to choose from
            //Swat
            swat = Scene.CreateChild("Swat" + numNodes);
            swat.Position = new Vector3(74.20757f, -0.50523f, 1.62f);

            var component2 = swat.CreateComponent<AnimatedModel>();
            component2.Model = ResourceCache.GetModel("Models/Swat/Swat.mdl");
            component2.SetMaterial(ResourceCache.GetMaterial("Materials/Soldier_body1.xml"));
            component2.SetMaterial(ResourceCache.GetMaterial("Materials/Soldier_head6.xml"));
            swat.CreateComponent<AnimationController>();
            swat.SetScale(0.2f);
            PlayAnimation(swat, "Swat/Swat_Idle.ani");

            //Ninja
            ninja = Scene.CreateChild("Ninja" + numNodes);
            ninja.Position = new Vector3(75, -0.50523f, 1.62f);
            ninja.Yaw(180, TransformSpace.Local);

            var component3 = ninja.CreateComponent<AnimatedModel>();
            component3.Model = ResourceCache.GetModel("Models/NinjaSnowWar/Ninja.mdl");
            component3.SetMaterial(ResourceCache.GetMaterial("Materials/Ninja.xml"));
            ninja.CreateComponent<AnimationController>();
            ninja.SetScale(0.2f);
            PlayAnimation(ninja, "NinjaSnowWar/Ninja_Idle1.ani");

            //Mutant
            mutant = Scene.CreateChild("Mutant" + numNodes);
            mutant.Position = new Vector3(75.79312f, -0.50523f, 1.62f);

            var component4 = mutant.CreateComponent<AnimatedModel>();
            component4.Model = ResourceCache.GetModel("Models/Mutant/Mutant.mdl");
            component4.SetMaterial(ResourceCache.GetMaterial("Materials/mutant_M.xml"));
            mutant.CreateComponent<AnimationController>();
            mutant.SetScale(0.2f);
            PlayAnimation(mutant, "Mutant/Mutant_Idle1.ani");
        }
        //Event handler for easy difficulty button
        void EasyClick(ReleasedEventArgs args)
        {
            game.DifficutlySelected = Difficulty.Easy;
            menu.Visible = false;

            newGameBtn.Visible = true;
            loadGameBtn.Visible = true;
            hallOfFameBtn.Visible = true;
            aboutBtn.Visible = true;
            helpBtn.Visible = true;
            exitBtn.Visible = true;
            continueBtn.Visible = true;

            easy.Visible = false;
            medium.Visible = false;
            hard.Visible = false;


            giveCharName.Visible = true;
        }
        //Event handler for medium difficulty button
        void MediumClick(ReleasedEventArgs args)
        {
            game.DifficutlySelected = Difficulty.Medium;
            menu.Visible = false;

            newGameBtn.Visible = true;
            loadGameBtn.Visible = true;
            hallOfFameBtn.Visible = true;
            aboutBtn.Visible = true;
            helpBtn.Visible = true;
            exitBtn.Visible = true;
            continueBtn.Visible = true;

            easy.Visible = false;
            medium.Visible = false;
            hard.Visible = false;

            giveCharName.Visible = true;
        }
        //Event handler for hard difficulty button
        void HardClick(ReleasedEventArgs args)
        {
            game.DifficutlySelected = Difficulty.Hard;
            menu.Visible = false;

            newGameBtn.Visible = true;
            loadGameBtn.Visible = true;
            hallOfFameBtn.Visible = true;
            aboutBtn.Visible = true;
            helpBtn.Visible = true;
            exitBtn.Visible = true;
            continueBtn.Visible = true;

            easy.Visible = false;
            medium.Visible = false;
            hard.Visible = false;

            giveCharName.Visible = true;
        }
        //Event handler for submitCharName button
        void SubmitCharNameClick(ReleasedEventArgs args)
        {
            giveCharName.Visible = false;
            string name = enterCharName.Text;
            game.MainCharName = name;

            SetUpCharSelection();
        }
        //Event handler for cheat mode button
        void CheatModeClick(ReleasedEventArgs args)
        {

        }        
        public void CreateNode()
        {
            ++numNodes;
            string name = GetSelectedNodeType();
            //Names of node types           

            var node = Scene.CreateChild(name + numNodes);
            node.Position = Camera.Node.Position;            
            node.Translate(new Vector3((float)Input.MousePosition.X / Graphics.Width, (float)Input.MousePosition.Y / Graphics.Height, 2));            

            if(nodeSelect == 1)
            {
                node.Rotation = Camera.Node.Rotation;
                var component2 = node.CreateComponent<Urho.Shapes.Plane>();
                component2.SetMaterial(Material.FromImage("Textures/grassPt1.jpg"));
                component2.SetMaterial(Material.FromImage("Textures/grassPt2.jpg"));                   
                node.Rotate(new Quaternion(-90, 0, 0), TransformSpace.Local);
                node.SetScale(3f);                
            }
            if (nodeSelect == 2)
            {
                node.Rotation = Camera.Node.Rotation;
                var component2 = node.CreateComponent<AnimatedModel>();
                component2.Model = ResourceCache.GetModel("Models/Mutant/Mutant.mdl");
                component2.SetMaterial(ResourceCache.GetMaterial("Materials/mutant_M.xml"));
                node.CreateComponent<AnimationController>();
                node.SetScale(0.2f);
            }
            if (nodeSelect == 3)
            {
                var component2 = node.CreateComponent<AnimatedModel>();
                component2.Model = ResourceCache.GetModel("Models/Rock3.mdl");
                component2.SetMaterial(Material.FromImage("Textures/Rock-Texture-Surface.jpg"));
                node.Roll(-90, TransformSpace.Local);
                node.SetScale(0.02f);
            }
            if (nodeSelect == 4)
            {
                node.Rotation = new Quaternion(90, 0, 0);
                var component = node.CreateComponent<AnimatedModel>();
                component.Model = ResourceCache.GetModel("Models/tree2.mdl");
                component.SetMaterial(Material.FromImage("Textures/branch.png"));               


                Node Leaves = node.CreateChild("TreeType1Leaves", 1);
                var component2 = Leaves.CreateComponent<AnimatedModel>();
                component2.Model = ResourceCache.GetModel("Models/tree2trunk.mdl");
                component2.SetMaterial(Material.FromImage("Textures/bark.jpg"));
                node.SetScale(0.2f);
            }
            if (nodeSelect == 5)
            {
                CreateVolks(node.Position);
            }
        }

        //Create tree of style 1
        public void CreateTree1(Vector3 position)
        {
            Node Trunk = Scene.CreateChild("TreeType1",34);


            Trunk.Rotation = new Quaternion(90, 0, 0);
            var component = Trunk.CreateComponent<AnimatedModel>();
            component.Model = ResourceCache.GetModel("Models/tree2.mdl");
            component.SetMaterial(Material.FromImage("Textures/branch.png"));
            Trunk.CreateComponent<AnimationController>();


            Node Leaves = Trunk.CreateChild("TreeType1Leaves", 1);
            var component2 = Leaves.CreateComponent<AnimatedModel>();
            component2.Model = ResourceCache.GetModel("Models/tree2trunk.mdl");
            component2.SetMaterial(Material.FromImage("Textures/bark.jpg"));

            Trunk.Position = position;
            Trunk.SetScale(0.2f);

            //Add to GameController Dictionary
            Nature Tree = new Nature(Nature.NatureType.Tree, Trunk.Position);
            Tree.ID = Trunk.ID;
            game.GameObjCollection[Tree.ID] = Tree;

            Nature TreePt2 = new Nature(Nature.NatureType.Tree, Leaves.Position);
            TreePt2.ID = Leaves.ID;
            game.GameObjCollection[TreePt2.ID] = TreePt2;
        }
        public void CreateRock(Vector3 position)
        {            
            Node RockNode = Scene.CreateChild("Rock");


            RockNode.Rotation = new Quaternion(90, 0, 0);
            RockNode.Roll(90, TransformSpace.World);
            var component = RockNode.CreateComponent<AnimatedModel>();
            component.Model = ResourceCache.GetModel(("Models/Rock3.mdl"));
            component.SetMaterial(Material.FromImage("Textures/Rock-Texture-Surface.jpg"));
            RockNode.CreateComponent<AnimationController>();            

            RockNode.Position = position;
            RockNode.SetScale(0.2f);

            //Add to GameController Dictionary
            Nature gameRock = new Nature(Nature.NatureType.Rock, RockNode.Position);
            gameRock.ID = RockNode.ID;
            game.GameObjCollection[gameRock.ID] = gameRock;
        }
        //Creates car, with body as parent node
        public void CreateAudi(Vector3 position)
        {
            Node body = Scene.CreateChild();
            body.Rotation = new Quaternion(90, 0, 0);
            var component = body.CreateComponent<AnimatedModel>();
            component.Model = ResourceCache.GetModel("Models/Audi/AudiBody.mdl");
            component.SetMaterial(Material.FromImage("Textures/Audi R8-white.jpg"));
            body.CreateComponent<AnimationController>();

            Node brakeLights = body.CreateChild();
            var brakeLightsComponent = brakeLights.CreateComponent<AnimatedModel>();
            brakeLightsComponent.Model = ResourceCache.GetModel("Models/Audi/AudiBrakeLights.mdl");
            brakeLightsComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Led_Light.001.xml"));
            brakeLights.CreateComponent<AnimationController>();

            Node doorHandles = body.CreateChild();
            doorHandles.CreateComponent<AnimationController>();
            var doorHandlesComponent = doorHandles.CreateComponent<AnimatedModel>();
            doorHandlesComponent.Model = ResourceCache.GetModel("Models/Audi/AudiDoorHandles.mdl");
            doorHandlesComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Aluminum2.xml"));

            Node emblem = body.CreateChild();
            emblem.CreateComponent<AnimationController>();
            var emblemComponent = emblem.CreateComponent<AnimatedModel>();
            emblemComponent.Model = ResourceCache.GetModel("Models/Audi/AudiEmblem.mdl");
            emblemComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Aluminum2.xml"));

            Node emblemBack = body.CreateChild();
            emblemBack.CreateComponent<AnimationController>();
            var emblemBackComponent = emblemBack.CreateComponent<AnimatedModel>();
            emblemBackComponent.Model = ResourceCache.GetModel("Models/Audi/AudiEmblemBack.mdl");
            emblemBackComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Aluminum2.xml"));

            Node exhaust = body.CreateChild();
            exhaust.CreateComponent<AnimationController>();
            var exhaustComponent = exhaust.CreateComponent<AnimatedModel>();
            exhaustComponent.Model = ResourceCache.GetModel("Models/Audi/AudiExhaust.mdl");
            exhaustComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Aluminum2.xml"));

            Node frontGrill = body.CreateChild();
            frontGrill.CreateComponent<AnimationController>();
            var frontGrillComponent = frontGrill.CreateComponent<AnimatedModel>();
            frontGrillComponent.Model = ResourceCache.GetModel("Models/Audi/AudiFrontGrill.mdl");
            frontGrillComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Aluminum2.xml"));

            Node frontHeadL = body.CreateChild();
            frontHeadL.CreateComponent<AnimationController>();
            var frontHeadLComponent = frontHeadL.CreateComponent<AnimatedModel>();
            frontHeadLComponent.Model = ResourceCache.GetModel("Models/Audi/AudiFrontHeadL.mdl");
            frontHeadLComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/tyyre.xml"));

            Node frontMudFlap = body.CreateChild();
            frontMudFlap.CreateComponent<AnimationController>();
            var frontMudFlapComponent = frontMudFlap.CreateComponent<AnimatedModel>();
            frontMudFlapComponent.Model = ResourceCache.GetModel("Models/Audi/AudiFrontMudFlap.mdl");
            frontMudFlapComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Body_Material0.xml"));

            Node tipGrill = body.CreateChild();
            tipGrill.CreateComponent<AnimationController>();
            var tipGrillComponent = tipGrill.CreateComponent<AnimatedModel>();
            tipGrillComponent.Model = ResourceCache.GetModel("Models/Audi/AudiFrontTipGrill.mdl");
            tipGrillComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Karbon.001.xml"));

            Node frontWinsh = body.CreateChild();
            frontWinsh.CreateComponent<AnimationController>();
            var frontWinshComponent = frontWinsh.CreateComponent<AnimatedModel>();
            frontWinshComponent.Model = ResourceCache.GetModel("Models/Audi/AudiFrontWinsh.mdl");
            frontWinshComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/tyyre.xml"));

            Node insideBackGrill = body.CreateChild();
            insideBackGrill.CreateComponent<AnimationController>();
            var insideBackGrillComponent = insideBackGrill.CreateComponent<AnimatedModel>();
            insideBackGrillComponent.Model = ResourceCache.GetModel("Models/Audi/AudiInsideBackGrill.mdl");
            insideBackGrillComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/tyyre.xml"));

            Node insideFrontGrill = body.CreateChild();
            insideFrontGrill.CreateComponent<AnimationController>();
            var insideFrontGrillComponent = insideFrontGrill.CreateComponent<AnimatedModel>();
            insideFrontGrillComponent.Model = ResourceCache.GetModel("Models/Audi/AudiInsideFrontGill.mdl");
            insideFrontGrillComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/tyyre.xml"));

            Node insideGrill = body.CreateChild();
            insideGrill.CreateComponent<AnimationController>();
            var insideGrillComponent = insideGrill.CreateComponent<AnimatedModel>();
            insideGrillComponent.Model = ResourceCache.GetModel("Models/Audi/AudiInsideGrillply.mdl");
            insideGrillComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/tyyre.xml"));

            Node licensePlate = body.CreateChild();
            licensePlate.CreateComponent<AnimationController>();
            var licensePlateComponent = licensePlate.CreateComponent<AnimatedModel>();
            licensePlateComponent.Model = ResourceCache.GetModel("Models/Audi/AudiLicensePlate.mdl");
            licensePlateComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Karbon.001.xml"));

            Node rearMudFlap = body.CreateChild();
            rearMudFlap.CreateComponent<AnimationController>();
            var rearMudFlapComponent = rearMudFlap.CreateComponent<AnimatedModel>();
            rearMudFlapComponent.Model = ResourceCache.GetModel("Models/Audi/AudiRearMudFlap.mdl");
            rearMudFlapComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Body_Material0.xml"));

            Node rearViews = body.CreateChild();
            rearViews.CreateComponent<AnimationController>();
            var rearViewsComponent = rearViews.CreateComponent<AnimatedModel>();
            rearViewsComponent.Model = ResourceCache.GetModel("Models/Audi/AudiRearViews.mdl");
            rearViewsComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Body_Material0.xml"));

            Node rearWinshield = body.CreateChild();
            rearWinshield.CreateComponent<AnimationController>();
            var rearWinshieldComponent = rearWinshield.CreateComponent<AnimatedModel>();
            rearWinshieldComponent.Model = ResourceCache.GetModel("Models/Audi/AudiRearWinshield.mdl");
            rearWinshieldComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/tyyre.xml"));

            Node sideBackWinsh = body.CreateChild();
            sideBackWinsh.CreateComponent<AnimationController>();
            var sideBackWinshComponent = sideBackWinsh.CreateComponent<AnimatedModel>();
            sideBackWinshComponent.Model = ResourceCache.GetModel("Models/Audi/AudiSideBackWinsh.mdl");
            sideBackWinshComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Body_Material0.xml"));

            Node sideWind = body.CreateChild();
            sideWind.CreateComponent<AnimationController>();
            var sideWindComponent = sideWind.CreateComponent<AnimatedModel>();
            sideWindComponent.Model = ResourceCache.GetModel("Models/Audi/AudiSideWind.mdl");
            sideWindComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/tyyre.xml"));

            Node tailFin = body.CreateChild();
            tailFin.CreateComponent<AnimationController>();
            var tailFinComponent = tailFin.CreateComponent<AnimatedModel>();
            tailFinComponent.Model = ResourceCache.GetModel("Models/Audi/AudiTailFin.mdl");
            tailFinComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Karbon.001.xml"));

            Node wheels = body.CreateChild();
            wheels.CreateComponent<AnimationController>();
            var wheelsComponent = wheels.CreateComponent<AnimatedModel>();
            wheelsComponent.Model = ResourceCache.GetModel("Models/Audi/AudiWheels.mdl");
            wheelsComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/Aluminum2.xml"));

            Node tires = body.CreateChild();
            tires.CreateComponent<AnimationController>();
            var tiresComponent = tires.CreateComponent<AnimatedModel>();
            tiresComponent.Model = ResourceCache.GetModel("Models/Audi/AudiTires.mdl");
            tiresComponent.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/tyyre.xml"));

            body.Yaw(-90, TransformSpace.World);
            body.SetScale(0.135f);
            body.Position = position;

            //Add to GameController Dictionary
            Enemy Audi = new Enemy(body.Position);
            Audi.ObjType = Enemy.EnemyType.Car;
            Audi.ID = body.ID;
            Audi.Strength = 100;
            game.GameObjCollection[Audi.ID] = Audi;

            //Stores the node ID of the Audi body
            Car2ID = Convert.ToInt32(Audi.ID);
        }
        public void CreateVolks(Vector3 position, float yaw = -90, Enemy.CarDir direction = Enemy.CarDir.Right, float speed = 20, int strength = 100)
        {
            Node volks = Scene.CreateChild();
            volks.Rotation = new Quaternion(90, 0, 0);
            var component = volks.CreateComponent<AnimatedModel>();
            component.Model = ResourceCache.GetModel("Models/volks2.mdl");
            component.SetMaterial(ResourceCache.GetMaterial("Materials/Audi/tyyre.xml"));            
            volks.CreateComponent<AnimationController>();

            volks.Yaw(yaw, TransformSpace.World);
            volks.SetScale(0.165f);
            volks.Position = position;

            //Add to GameController Dictionary
            Enemy Volks = new Enemy(volks.Position);
            Volks.ObjType = Enemy.EnemyType.Car;
            Volks.ID = volks.ID;
            Volks.Strength = strength;
            Volks.CarMovingDirection = direction;
            Volks.CarSpeed = speed;
            game.GameObjCollection[Volks.ID] = Volks;

            //Stores the node ID of the volks car
            Car2ID = Convert.ToInt32(Volks.ID);
        }

        //Create Ground for first level
        public void CreateGroundLevel1()
        {
            ++numNodes;
            Node node = Scene.CreateChild("Plane" + numNodes);
            
            var component2 = node.CreateComponent<Urho.Shapes.Plane>();
            component2.SetMaterial(Material.FromImage("Textures/grassPt1.jpg"));
            component2.SetMaterial(Material.FromImage("Textures/grassPt2.jpg"));

            //node.Pitch(-20,TransformSpace.Local);

            for (int row = 0; row < 50; ++row)
            {
                var node2 = node.CreateChild("Plane" + numNodes);
                var component3 = node2.CreateComponent<Urho.Shapes.Plane>();
                if (row != 34)
                {                    
                    component3.SetMaterial(Material.FromImage("Textures/grassPt1.jpg"));
                    component3.SetMaterial(Material.FromImage("Textures/grassPt2.jpg"));
                    node2.Position = new Vector3(node.Position.X, node.Position.Y, 1f * row);
                }
                //Road
                else
                {
                    component3.SetMaterial(Material.FromImage("Textures/Road1.jpg"));
                    node2.Position = new Vector3(node.Position.X, node.Position.Y, 1f * row);
                    node2.Yaw(90, TransformSpace.Local);
                }
                Nature plane2 = new Nature(Nature.NatureType.Plane, node2.Position);
                plane2.ID = node2.ID;
                game.GameObjCollection[plane2.ID] = plane2;

                for (int col = 0; col < 50; ++col)
                {
                    var node3 = node.CreateChild("Plane" + numNodes);
                    //node2.Pitch(-20, TransformSpace.Local);
                    var component4 = node3.CreateComponent<Urho.Shapes.Plane>();
                    if (row != 34 && (row != 41 | (col < 23 | col > 26)))
                    {                        
                        component4.SetMaterial(Material.FromImage("Textures/grassPt1.jpg"));
                        component4.SetMaterial(Material.FromImage("Textures/grassPt2.jpg"));
                        node3.Position = new Vector3(1f + col, node.Position.Y, 1f * row);
                    }

                    else if (row == 41 && col >=23 && col <= 26)
                    {
                        component4.Color = Color.White;
                        node3.Position = new Vector3(1f + col, node.Position.Y, 1f * row);                        
                    }

                    //Road
                    else if (row == 34)
                    {
                        component4.SetMaterial(Material.FromImage("Textures/Road1.jpg"));
                        node3.Position = new Vector3(1f + col, node.Position.Y, 1f * row);
                        node3.Yaw(90, TransformSpace.Local);
                    }
                    Nature plane3 = new Nature(Nature.NatureType.Plane, node3.Position);
                    plane3.ID = node3.ID;
                    game.GameObjCollection[plane3.ID] = plane3;
                }
            }

            node.Position = new Vector3(-0.7f, -0.5f, 2);
            node.SetScale(3f);

            Nature plane = new Nature(Nature.NatureType.Plane,node.Position);
            plane.ID = node.ID;
            game.GameObjCollection[plane.ID] = plane;
        }

        //Create forest for level 1
        public void CreateForestLevel1()
        {
            for(float xpos = 46; xpos < 103; xpos += 5)
            {
                for (float zpos = 0; zpos < 135; zpos += 5)  //Syntax for counting by 2 from http://stackoverflow.com/questions/14413404/c-sharp-for-loop-increment-by-2-trouble
                {
                    Vector3 LeftLeftTreePos = new Vector3(xpos, -.5f, 2.5f + zpos);
                    Vector3 LeftMiddleTreePos = new Vector3(xpos +1, -.5f, zpos);
                    Vector3 RightMiddleTreePos = new Vector3(xpos +2, -.5f, zpos);
                    Vector3 RightRightTreePos = new Vector3(xpos +3, -.5f, 2.5f + zpos);

                    if ((zpos <= 101 | zpos >= 106) && xpos == 75)
                    {
                        LeftLeftTreePos = new Vector3(73f, -.5f, 2.5f + zpos);
                        LeftMiddleTreePos = new Vector3(74f, -.5f, zpos);
                        RightMiddleTreePos = new Vector3(76f, -.5f, zpos);
                        RightRightTreePos = new Vector3(77f, -.5f, 2.5f + zpos);
                        CreateTree1(LeftLeftTreePos);
                        CreateTree1(LeftMiddleTreePos);
                        CreateTree1(RightMiddleTreePos);
                        CreateTree1(RightRightTreePos);
                    }
                    else
                    {
                        //LeftLeftTreePos = new Vector3(73f, -.5f, 2.5f + zpos);
                        //LeftMiddleTreePos = new Vector3(74f, -.5f, zpos);
                        //RightMiddleTreePos = new Vector3(76f, -.5f, zpos);
                        //RightRightTreePos = new Vector3(77f, -.5f, 2.5f + zpos);

                        //Vector3 LeftMiddleMiddleTreePos = new Vector3(74.4f, -2f, zpos);
                        //Vector3 RightMiddleMiddleTreePos = new Vector3(75.6f, -2f, zpos);
                        CreateTree1(LeftLeftTreePos);
                        CreateTree1(LeftMiddleTreePos);
                        CreateTree1(RightMiddleTreePos);
                        CreateTree1(RightRightTreePos);
                        //CreateTree1(RightMiddleMiddleTreePos);
                        //CreateTree1(LeftMiddleMiddleTreePos);
                    }
                }

            }
        }
        //Moves the cars
        public void MoveCars(float timeStep)
        {
            foreach(GameObj obj in game.GameObjCollection.Values)
            {
                if(obj is Enemy)
                {
                    if(((Enemy)(obj)).ObjType == Enemy.EnemyType.Car)
                    {
                        Node node = Scene.GetNode((uint)(obj.ID));
                        if(((Enemy)(obj)).CarMovingDirection == Enemy.CarDir.Right)
                        {
                            if (node.Position.X >= 148)
                            {
                                obj.Position = new Vector3(0, -0.4327534f, 103.4266f);
                                node.Position = new Vector3(0, -0.4327534f, 103.4266f);
                            }
                            else
                            {
                                node.Translate(Vector3.UnitX * timeStep * ((Enemy)(obj)).CarSpeed, TransformSpace.World);
                            }
                        }
                        else
                        {
                            if (node.Position.X <= 0)
                            {
                                obj.Position = new Vector3(148, -0.4327534f, 104.3f);
                                node.Position = new Vector3(148, -0.4327534f, 104.3f);
                            }
                            else
                            {
                                node.Translate(-Vector3.UnitX * timeStep * ((Enemy)(obj)).CarSpeed, TransformSpace.World);
                            }
                        }
                        
                    }
                }
            }
        }

        //Create cars for level 1
        public void CreateCarsLevel1()
        {
            Vector3 FarLaneAudiInitialPlacement = new Vector3(140, -0.4327534f, 104.3f);            
            Vector3 CloseLaneAudiInitialPlacement = new Vector3(74f, -0.4327534f, 103.4266f); 
            CreateVolks(FarLaneAudiInitialPlacement,90,Enemy.CarDir.Left);
            CreateVolks(CloseLaneAudiInitialPlacement);
            CreateVolks(new Vector3(148, -0.4327534f, 104.3f), 90, Enemy.CarDir.Left,18);
            CreateVolks(new Vector3(70, -0.4327534f, 103.4266f), speed: 18);
            CreateVolks(new Vector3(28, -0.4327534f, 104.3f), 90, Enemy.CarDir.Left, 5);
            CreateVolks(new Vector3(129, -0.4327534f, 103.4266f), speed: 5);
            CreateVolks(new Vector3(120, -0.4327534f, 104.3f), 90, Enemy.CarDir.Left, 10);
            CreateVolks(new Vector3(55, -0.4327534f, 103.4266f), speed: 10);
        }
        public void CreateCarsLevel2()
        {
            Vector3 FarLaneAudiInitialPlacement = new Vector3(140, -0.4327534f, 104.3f);
            Vector3 CloseLaneAudiInitialPlacement = new Vector3(74f, -0.4327534f, 103.4266f);
            CreateVolks(FarLaneAudiInitialPlacement, 90, Enemy.CarDir.Left, strength: 200);
            CreateVolks(CloseLaneAudiInitialPlacement, strength: 200);
            CreateVolks(new Vector3(148, -0.4327534f, 104.3f), 90, Enemy.CarDir.Left, 18, strength: 200);
            CreateVolks(new Vector3(70, -0.4327534f, 103.4266f), speed: 18, strength: 200);
            CreateVolks(new Vector3(28, -0.4327534f, 104.3f), 90, Enemy.CarDir.Left, 5, strength: 200);
            CreateVolks(new Vector3(129, -0.4327534f, 103.4266f), speed: 5, strength: 200);
            CreateVolks(new Vector3(120, -0.4327534f, 104.3f), 90, Enemy.CarDir.Left, 10, strength: 200);
            CreateVolks(new Vector3(55, -0.4327534f, 103.4266f), speed: 10, strength: 200);
            //for (int i = 0; i < 10; ++i)
            //{
            //    CreateVolks(new Vector3(148, -0.4327534f, 104.3f - 6 * i), 90, Enemy.CarDir.Left, 18);
            //    CreateVolks(new Vector3(70, -0.4327534f, 103.4266f - 6 * i), speed: 18);
            //    CreateVolks(new Vector3(28, -0.4327534f, 104.3f - 6 * i), 90, Enemy.CarDir.Left, 5);
            //    CreateVolks(new Vector3(129, -0.4327534f, 103.4266f - 6 * i), speed: 5);
            //    CreateVolks(new Vector3(120, -0.4327534f, 104.3f - 6 * i), 90, Enemy.CarDir.Left, 10);
            //    CreateVolks(new Vector3(55, -0.4327534f, 103.4266f - 6 * i), speed: 10);
            //}

        }
        //Create Ground for first level
        public void CreateGroundLevel2()
        {
            ++numNodes;
            Node node = Scene.CreateChild("Plane" + numNodes);

            var component2 = node.CreateComponent<Urho.Shapes.Plane>();
            component2.SetMaterial(Material.FromImage("Textures/Soil_Cracked.jpg"));

            //node.Pitch(-20,TransformSpace.Local);

            for (int row = 0; row < 50; ++row)
            {
                var node2 = node.CreateChild("Plane" + numNodes);
                var component3 = node2.CreateComponent<Urho.Shapes.Plane>();
                if (row != 34)
                {
                    component3.SetMaterial(Material.FromImage("Textures/Soil_Cracked.jpg"));
                    node2.Position = new Vector3(node.Position.X, node.Position.Y, 1f * row);
                }
                //Road
                else
                {
                    component3.SetMaterial(Material.FromImage("Textures/RoadDry.jpg"));
                    node2.Position = new Vector3(node.Position.X, node.Position.Y, 1f * row);
                    node2.Yaw(90, TransformSpace.Local);
                }
                Nature plane2 = new Nature(Nature.NatureType.Plane, node2.Position);
                plane2.ID = node2.ID;
                game.GameObjCollection[plane2.ID] = plane2;

                for (int col = 0; col < 50; ++col)
                {
                    var node3 = node.CreateChild("Plane" + numNodes);
                    //node2.Pitch(-20, TransformSpace.Local);
                    var component4 = node3.CreateComponent<Urho.Shapes.Plane>();
                    if (row != 34 && (row != 41 | (col < 23 | col > 26)))
                    {
                        component4.SetMaterial(Material.FromImage("Textures/Soil_Cracked.jpg"));
                        node3.Position = new Vector3(1f + col, node.Position.Y, 1f * row);
                    }

                    else if (row == 41 && col >= 23 && col <= 26)
                    {
                        component4.Color = Color.White;
                        node3.Position = new Vector3(1f + col, node.Position.Y, 1f * row);
                    }

                    //Road
                    else if (row == 34)
                    {
                        component4.SetMaterial(Material.FromImage("Textures/RoadDry.jpg"));
                        node3.Position = new Vector3(1f + col, node.Position.Y, 1f * row);
                        node3.Yaw(90, TransformSpace.Local);
                    }
                    Nature plane3 = new Nature(Nature.NatureType.Plane, node3.Position);
                    plane3.ID = node3.ID;
                    game.GameObjCollection[plane3.ID] = plane3;
                }
            }

            node.Position = new Vector3(-0.7f, -0.5f, 2);
            node.SetScale(3f);

            Nature plane = new Nature(Nature.NatureType.Plane, node.Position);
            plane.ID = node.ID;
            game.GameObjCollection[plane.ID] = plane;
        }
        public void CreateRocksLevel2()
        {
            for (float xpos = 46; xpos < 103; xpos += 5)
            {
                for (float zpos = 0; zpos < 135; zpos += 5)  //Syntax for counting by 2 from http://stackoverflow.com/questions/14413404/c-sharp-for-loop-increment-by-2-trouble
                {
                    Vector3 LeftLeftRockPos = new Vector3(xpos, -.40f, 2.5f + zpos);
                    Vector3 LeftMiddleRockPos = new Vector3(xpos + 1, -.40f, zpos);
                    Vector3 RightMiddleRockPos = new Vector3(xpos + 2, -.40f, zpos);
                    Vector3 RightRightRockPos = new Vector3(xpos + 3, -.40f, 2.5f + zpos);

                    if ((zpos <= 101 | zpos >= 106) && xpos == 75)
                    {
                        LeftLeftRockPos = new Vector3(73f, -.40f, 2.5f + zpos);
                        LeftMiddleRockPos = new Vector3(74f, -.40f, zpos);
                        RightMiddleRockPos = new Vector3(76f, -.40f, zpos);
                        RightRightRockPos = new Vector3(77f, -.40f, 2.5f + zpos);
                        CreateRock(LeftLeftRockPos);
                        CreateRock(LeftMiddleRockPos);
                        CreateRock(RightMiddleRockPos);
                        CreateRock(RightRightRockPos);
                    }
                    else
                    {
                        CreateRock(LeftLeftRockPos);
                        CreateRock(LeftMiddleRockPos);
                        CreateRock(RightMiddleRockPos);
                        CreateRock(RightRightRockPos);                        
                    }
                }

            }
        }

        //Create Level 3 
        //Create ground level 3
        public void CreateGroundLevel3()
        {
            ++numNodes;
            Node node = Scene.CreateChild("Plane" + numNodes);

            var component2 = node.CreateComponent<Urho.Shapes.Plane>();
            component2.SetMaterial(Material.FromImage("Textures/Soil_Cracked.jpg"));

            //node.Pitch(-20,TransformSpace.Local);

            for (int row = 0; row < 50; ++row)
            {
                var node2 = node.CreateChild("Plane" + numNodes);
                var component3 = node2.CreateComponent<Urho.Shapes.Plane>();
                if (row != 34)
                {
                    component3.SetMaterial(Material.FromImage("Textures/Soil_Cracked.jpg"));
                    node2.Position = new Vector3(node.Position.X, node.Position.Y, 1f * row);
                }
                //Road
                else
                {
                    component3.SetMaterial(Material.FromImage("Textures/RoadDry.jpg"));
                    node2.Position = new Vector3(node.Position.X, node.Position.Y, 1f * row);
                    node2.Yaw(90, TransformSpace.Local);
                }
                Nature plane2 = new Nature(Nature.NatureType.Plane, node2.Position);
                plane2.ID = node2.ID;
                game.GameObjCollection[plane2.ID] = plane2;

                for (int col = 0; col < 50; ++col)
                {
                    var node3 = node.CreateChild("Plane" + numNodes);
                    //node2.Pitch(-20, TransformSpace.Local);
                    var component4 = node3.CreateComponent<Urho.Shapes.Plane>();
                    if (row != 18 && row != 34 && (row != 41 | (col < 23 | col > 26)))
                    {
                        component4.SetMaterial(Material.FromImage("Textures/Soil_Cracked.jpg"));
                        node3.Position = new Vector3(1f + col, node.Position.Y, 1f * row);
                    }

                    else if (row == 41 && col >= 23 && col <= 26)
                    {
                        component4.Color = Color.White;
                        node3.Position = new Vector3(1f + col, node.Position.Y, 1f * row);
                    }

                    //Road
                    else if (row == 34 | row == 18)
                    {
                        component4.SetMaterial(Material.FromImage("Textures/RoadDry.jpg"));
                        node3.Position = new Vector3(1f + col, node.Position.Y, 1f * row);
                        node3.Yaw(90, TransformSpace.Local);
                    }
                    Nature plane3 = new Nature(Nature.NatureType.Plane, node3.Position);
                    plane3.ID = node3.ID;
                    game.GameObjCollection[plane3.ID] = plane3;
                }
            }

            node.Position = new Vector3(-0.7f, -0.5f, 2);
            node.SetScale(3f);

            Nature plane = new Nature(Nature.NatureType.Plane, node.Position);
            plane.ID = node.ID;
            game.GameObjCollection[plane.ID] = plane;

        }

        public void SetUpLevel1(Difficulty difficulty)
        {
            gameOverWind.Visible = false;
            for (int i = 0; i < 6; ++i) { game.ResetLevel(); }
            game.GameOver = false;
            CreateGroundLevel1();
            CreateForestLevel1();
            CreateCarsLevel1();  
                      
            //Start timer
            timer.Start();
        }

        public void SetUpLevel2(Difficulty difficulty)
        {
            gameOverWind.Visible = false;
            for (int i = 0; i < 6; ++i) { game.ResetLevel(); }

            CreateGroundLevel2();
            CreateRocksLevel2();
            CreateCarsLevel2();

            //Start timer
            timer.Start();
        }

        public void SetUpLevel3(Difficulty difficulty)
        {
            gameOverWind.Visible = false;
            for (int i = 0; i < 6; ++i) { game.ResetLevel(); }

            CreateGroundLevel3();
            //Start timer
            timer.Start();
        }

        public void SetUpLevel(Level level, Difficulty difficulty)
        {
            switch (level)
            {
                case Level.One:
                    {
                        SetUpLevel1(difficulty);
                        break;
                    }
                case Level.Two:
                    {
                        SetUpLevel2(difficulty);
                        break;
                    }
                case Level.Three:
                    {
                        SetUpLevel3(difficulty);
                        break;
                    }
            }
        }
        public void CreateMainChar1()
        {
            SelectedChar = 1;

            game.MainChar.SelectedCharType = MainCharacter.MainCharOptn.Swat;

            MainChar = Scene.CreateChild("Swat" + numNodes);
            MainChar.Position = new Vector3(75, -0.50523f, 1.62f);
            MainChar.Yaw(180, TransformSpace.Local);

            var component2 = MainChar.CreateComponent<AnimatedModel>();
            component2.Model = ResourceCache.GetModel("Models/Swat/Swat.mdl");
            component2.SetMaterial(ResourceCache.GetMaterial("Materials/Soldier_body1.xml"));
            component2.SetMaterial(ResourceCache.GetMaterial("Materials/Soldier_head6.xml"));
            MainChar.CreateComponent<AnimationController>();
            MainChar.SetScale(0.2f);

            ForwardAniFile = "Swat/Swat_SprintFwd.ani";
            BackwardAniFile = "Swat/Swat_SprintBwd.ani";
            LeftAniFile = "Swat/Swat_SprintLeft.ani";
            RightAniFile = "Swat/Swat_SprintRight.ani";
            IdleAniFile = "Swat/Swat_Idle.ani";
            DeathAniFile = "Swat/Swat_DeathFromBack.ani";
        }
        public void CreateMainChar2()
        {
            SelectedChar = 2;

            game.MainChar.SelectedCharType = MainCharacter.MainCharOptn.Ninja;

            MainChar = Scene.CreateChild("Ninja" + numNodes);
            MainChar.Position = new Vector3(75, -0.50523f, 1.62f);

            var component2 = MainChar.CreateComponent<AnimatedModel>();
            component2.Model = ResourceCache.GetModel("Models/NinjaSnowWar/Ninja.mdl");
            component2.SetMaterial(ResourceCache.GetMaterial("Materials/Ninja.xml"));
            MainChar.CreateComponent<AnimationController>();
            MainChar.SetScale(0.2f);

            ForwardAniFile = "NinjaSnowWar/Ninja_Walk.ani";
            BackwardAniFile = "NinjaSnowWar/Ninja_Walk.ani";
            LeftAniFile = "NinjaSnowWar/Ninja_Walk.ani";
            RightAniFile = "NinjaSnowWar/Ninja_Walk.ani";
            IdleAniFile = "NinjaSnowWar/Ninja_Idle1.ani";
            DeathAniFile = "NinjaSnowWar/Ninja_Death1.ani";
        }
        public void CreateMainChar3()
        {
            SelectedChar = 3;

            game.MainChar.SelectedCharType = MainCharacter.MainCharOptn.Mutant;

            MainChar = Scene.CreateChild("Mutant" + numNodes);
            MainChar.Position = new Vector3(75, -0.50523f, 1.62f);
            MainChar.Yaw(180, TransformSpace.Local);

            var component2 = MainChar.CreateComponent<AnimatedModel>();
            component2.Model = ResourceCache.GetModel("Models/Mutant/Mutant.mdl");
            component2.SetMaterial(ResourceCache.GetMaterial("Materials/mutant_M.xml"));
            MainChar.CreateComponent<AnimationController>();
            MainChar.SetScale(0.2f);

            ForwardAniFile = "Mutant/Mutant_Run.ani";
            BackwardAniFile = "Mutant/Mutant_Run.ani";
            LeftAniFile = "Mutant/Mutant_Run.ani";
            RightAniFile = "Mutant/Mutant_Run.ani";
            IdleAniFile = "Mutant/Mutant_Idle0.ani";
            DeathAniFile = "Mutant/Mutant_Death.ani";

        }

        public void CreateMainChar(int num)
        {
            switch (num)
            {
                case 1:
                    {
                        CreateMainChar1();
                        break;
                    }
                case 2:
                    {
                        CreateMainChar2();
                        break;
                    }
                case 3:
                    {
                        CreateMainChar3();
                        break;
                    }
            }
        }
        public void ResetLevel()
        {
            foreach (uint id in game.GameObjCollection.Keys)
            {
                if (id != null)
                {
                    Node node = Scene.GetNode(id);
                    node.RemoveAllChildren();
                    Scene.RemoveChild(node);
                }
            }
            // Reset camera
            CameraNode.Position = new Vector3(75, 0, 0);
            CameraNode.Rotation = new Quaternion(-0.2f, 0, 0);
            uint mainCharID;

            ////Reset timer and scene
            //Node node2 = Scene.GetNode(LightID);
            //node2.RemoveAllChildren();
            //Scene.RemoveChild(node2);
            //SetupScene();            
            //timeTotal = 0;
            //timer.Start();            
            uint camID = CameraNode.ID;
            uint lightID = LightNode.ID;
            uint lightID2 = lightNode.ID;
            //lightNode.RemoveAllChildren();
            //Scene.RemoveChild(lightNode);        
            foreach (Node node in Scene.Children)
            {
                if (node.ID != camID && node.ID != lightID && node.ID != lightID2)
                {                 
                    if(MainChar != null)
                    {
                        mainCharID = MainChar.ID;
                        if(node.ID != mainCharID)
                        {                            
                            node.RemoveAllChildren();
                            Scene.RemoveChild(node);
                        }                        
                    }                   
                }
            }
            //CreateMainChar(SelectedChar);
            
            

            //Reset Main Character and scene
            if (MainChar != null)
            {
                //Reset Main Character
                MainChar.Position = new Vector3(75, -0.50523f, 1.62f);
                PlayAnimation(MainChar, IdleAniFile);

                //Reset light
                //SetupScene();
            }
        }

        public void LevelAdvanceAssess()
        {
            if (game.PassLevel())
            {
                if(game.CurrentLevel == Level.One)
                {
                    level2Btn.Visible = true;
                }
                else if (game.CurrentLevel == Level.Two)
                {
                    level3Btn.Visible = true;
                }
            }
        }

        //Brings up location setter window for developer mode
        public void BringUpLocSetter()
        {
            locWindow.Visible = true;
            messageHelper.Visible = false;
            MoveCamera = false;
            xSet.Text = "X: ";
            ySet.Text = "Y: ";
            zSet.Text = "Z: ";
        }
        //Returns string name of current node type select (for developer mode)
        public string GetSelectedNodeType()
        {
            string s = "";
            if (nodeSelect == 1) s = "Plane";
            if (nodeSelect == 2) s = "Mutant";
            if (nodeSelect == 3) s = "Rock";
            if (nodeSelect == 4) s = "Tree1";
            if (nodeSelect == 5) s = "Audi";
            return s;
        }
        //Return node that cursor is pointing at
        protected Node GetNodeUserIsLookingAt()
        {
            Ray cameraRay = GetMouseRay();

            var result = Scene.GetComponent<Octree>().RaycastSingle(cameraRay, RayQueryLevel.Triangle, 100, DrawableFlags.Geometry, 0x70000000);
            if (result != null)
            {
                return result.Value.Node;
            }
            return null;
        }
        
        //Return the ray that passes through the cursor's location
        public Ray GetMouseRay()
        {
            return Camera.GetScreenRay(
                (float)Input.MousePosition.X / Graphics.Width,
                (float)Input.MousePosition.Y / Graphics.Height);
        }

        //Load and play animation from file
        void PlayAnimation(Node node, string file)
        {
            node.RemoveAllActions();

            bool looped = false;
            if (!file.ToLower().Contains("death") )
            {
                looped = true;
            }

            if (!file.ToLower().Contains("death"))
            {
                node.RunActions(new RepeatForever(new MoveBy(1f, node.Rotation * new Vector3(0, 0, 0))));
            }

            AnimationController animation = node.GetComponent<AnimationController>();
            animation.StopAll(0.2f);           

            animation.Play("Models/" + file, 0, looped, 0.2f);            
        }

        static void Main(string[] args)
        {
            var app = new Program(new ApplicationOptions("Data")
            {
                TouchEmulation = true,
                WindowedMode = true
            });
            app.game.gui = app;
            app.Run();
        }        
    }
}
