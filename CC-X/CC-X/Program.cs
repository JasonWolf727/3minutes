using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Shapes;
using Urho.Gui;
using Urho.Resources;
using CC_X.Model;
using Urho.Actions;

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
        Window locWindow;

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
        Text coordinates;
        Text messageHelper;

        LineEdit xSet;
        LineEdit ySet;
        LineEdit zSet;
        LineEdit charName;

        Node currentNode;
        int numNodes;
        int nodeSelect = 1;
        public bool DeveloperMode { get; set; }   
        public bool GameStart { get; set; }     

        public Node lightNode { get; private set; }

        public Light light { get; private set; }

        //Create an instance of GameController
        GameController game = new GameController(Difficulty.Easy); //Temp difficulty

        //Store scene nodes, but keep main character separate
        public Dictionary<int, Node> NodesInScene;
        Node MainChar;

        public Program(ApplicationOptions options) : base(options) { }

        protected override void Start()
        {
            base.Start();

            //Set up scene and add light to the scene           
            lightNode = Scene.CreateChild("DirectionalLight");
            lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
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

            //Setup Root UI and chache
            uiRoot = UI.Root;
            cache = ResourceCache;            

            //Setup font and style
            style = cache.GetXmlFile("UI/DefaultStyle.xml");
            font = cache.GetFont("Fonts/Anonymous Pro.ttf");

            //Setup UI default style
            uiRoot.SetStyleAuto(style);

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

            //Set up Developer tools
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

            //Set up game
            GameStart = false;

        }

        protected override void OnUpdate(float timeStep)
        {
            base.OnUpdate(timeStep);            
            if (game.EndLevel())
            {
                menu.Visible = true; //Temporary indicator
            }            
            if (Input.GetKeyPress(Key.M))
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
            if(menu.Visible == true)
            {
                menu.Visible = false;
            }
            if (Input.GetKeyPress(Key.N1)) nodeSelect = 1;
            if (Input.GetKeyPress(Key.N2)) nodeSelect = 2;
            if (Input.GetKeyPress(Key.N3)) nodeSelect = 3;
            if (Input.GetKeyPress(Key.N4)) nodeSelect = 4;

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
                if (Input.GetKeyDown(Key.T)) currentNode.Rotate(new Quaternion(0.5f, 0, 0), TransformSpace.Local);
            }
        }

        //Assigns keyboard input to corresponding main character logic.
        private void GameCommands(float timeStep)
        {
            MoveCamera = true;
            if (Input.GetKeyDown(Key.Up)) { CameraNode.Translate(Vector3.UnitZ * timeStep * 2); MainChar.Translate(-Vector3.UnitZ * timeStep * 2); PlayAnimation(MainChar, "Swat/Swat_SprintFwd.ani"); }
            else if (Input.GetKeyDown(Key.Down)) { CameraNode.Translate(-Vector3.UnitZ * timeStep * 2); MainChar.Translate(Vector3.UnitZ * timeStep * 2); PlayAnimation(MainChar, "Swat/Swat_SprintBwd.ani"); }
            else if (Input.GetKeyDown(Key.Left)) { CameraNode.Translate(-Vector3.UnitX * timeStep * 2); MainChar.Translate(Vector3.UnitX * timeStep * 2); PlayAnimation(MainChar, "Swat/Swat_SprintLeft.ani"); }
            else if (Input.GetKeyDown(Key.Right)) { CameraNode.Translate(Vector3.UnitX * timeStep * 2); MainChar.Translate(-Vector3.UnitX * timeStep * 2); PlayAnimation(MainChar, "Swat/Swat_SprintRight.ani"); }
            else
            {
                PlayAnimation(MainChar, "Swat/Swat_Idle.ani");
            }
        }

        //Adjusts on-screen health notification to match game.MainChar health
        private void UpdateHealth()
        {

        }
                
        //Event handler for new game button
        void NewGameClick(ReleasedEventArgs args)
        {
            menu.Visible = false;

            //Create level 1
            game.SetUpLevel(Level.One, Difficulty.Easy);
            //CreateGround();

            chooseChar = uiRoot.CreateWindow("ChooseChar", 3);
            chooseChar.SetStyleAuto(null);
            chooseChar.SetMinSize(300, 600);
            chooseChar.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            chooseChar.Opacity = 0.85f;

            charOptn1 = chooseChar.CreateButton();
            charOptn1.SetMinSize(100, 30);
            charOptn1.SetStyleAuto(null);
            charOptn1.SetAlignment(HorizontalAlignment.Center,VerticalAlignment.Bottom);
            charOptn1.SubscribeToReleased(CharOptn1Click);

            charOptn1Text = charOptn1.CreateText();
            charOptn1Text.SetFont(font, 16);
            charOptn1Text.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            charOptn1Text.Value = "Select";
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
            game.MainChar.SelectedCharType = MainCharacter.MainCharOptn.Swat;

            MainChar = Scene.CreateChild("Swat" + numNodes);
            MainChar.Position = new Vector3(75,-0.50523f,1.62f);
            MainChar.Yaw(180,TransformSpace.Local);
            
            var component2 = MainChar.CreateComponent<AnimatedModel>();
            component2.Model = ResourceCache.GetModel("Models/Swat/Swat.mdl");
            component2.SetMaterial(ResourceCache.GetMaterial("Materials/Soldier_body1.xml"));
            component2.SetMaterial(ResourceCache.GetMaterial("Materials/Soldier_head6.xml"));
            MainChar.CreateComponent<AnimationController>();
            MainChar.SetScale(0.2f);

            chooseChar.Visible = false;
            GameStart = true;
            DeveloperMode = false;
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
            string xStr = xSet.Text;
            string yStr = ySet.Text;
            string zStr = zSet.Text;
            try
            {
                float xCoor = (float)(Convert.ToDouble(xStr.Replace("X: ", "").Trim()));
                float yCoor = (float)(Convert.ToDouble(yStr.Replace("Y: ", "").Trim()));
                float zCoor = (float)(Convert.ToDouble(zStr.Replace("Z: ", "").Trim()));

                //currentNode.Position = new Vector3(xCoor, yCoor, zCoor);
                lightNode.Position = new Vector3(xCoor, yCoor, zCoor);

            }
            catch (Exception e)
            {
                messageHelper.Value = "Do not delete 'X: ','Y: ', or 'Z: '. Enter only numbers.";
                messageHelper.Visible = true;
            }

            locWindow.Visible = false;
            MoveCamera = true;
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
        public void CreateNode()
        {
            ++numNodes;
            string name = GetSelectedNodeType();
            //Names of node types           

            var node = Scene.CreateChild(name + numNodes);
            node.Position = Camera.Node.Position;
            node.Rotation = Camera.Node.Rotation;
            node.Translate(new Vector3((float)Input.MousePosition.X / Graphics.Width, (float)Input.MousePosition.Y / Graphics.Height, 2));            

            if(nodeSelect == 1)
            {
                var component2 = node.CreateComponent<Urho.Shapes.Plane>();
                component2.SetMaterial(Material.FromImage("Textures/grassPt1.jpg"));
                component2.SetMaterial(Material.FromImage("Textures/grassPt2.jpg"));                   
                node.Rotate(new Quaternion(-90, 0, 0), TransformSpace.Local);
                node.SetScale(3f);                
            }
            if (nodeSelect == 2)
            {
                var component2 = node.CreateComponent<AnimatedModel>();
                component2.Model = ResourceCache.GetModel("Models/Mutant/Mutant.mdl");
                component2.SetMaterial(ResourceCache.GetMaterial("Materials/mutant_M.xml"));
                node.CreateComponent<AnimationController>();
                node.SetScale(0.2f);
            }
            if (nodeSelect == 3)
            {
                var component2 = node.CreateComponent<AnimatedModel>();
                component2.Model = ResourceCache.GetModel("Models/Mushroom.mdl");
                component2.SetMaterial(ResourceCache.GetMaterial("Materials/Mushroom/Mushroom.xml"));                                
                node.SetScale(0.02f);
            }
            if (nodeSelect == 4)
            {
                var component2 = node.CreateComponent<AnimatedModel>();
                component2.Model = ResourceCache.GetModel("Models/ThreeTreesWithBackground.mdl");
                component2.SetMaterial(ResourceCache.GetMaterial("Materials/ThreeTreesWithBackground.xml"));
                node.SetScale(0.02f);
            }
        }

        //Create Ground
        public void CreateGround()
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

        public void SetUpLevel1(Difficulty difficulty)
        {
            CreateGround();
        }

        public void SetUpLevel2(Difficulty difficulty)
        {
            throw new NotImplementedException();
        }

        public void SetUpLevel3(Difficulty difficulty)
        {
            throw new NotImplementedException();
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
            if (nodeSelect == 3) s = "Mushroom";
            if (nodeSelect == 4) s = "Tree";
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
            if (file == "Swat/Swat_Idle.ani" | file == "Swat/Swat_RunBwd.ani" | file == "Swat/Swat_RunFwd.ani" | file == "Swat/Swat_RunLeft.ani" | file == "Swat/Swat_RunRight.ani" | file == "Swat/Swat_RunFwd.ani" | file == "Swat/Swat_SprintFwd.ani" | file == "Swat/Swat_SprintBwd.ani" | file == "Swat/Swat_SprintLeft.ani" | file == "Swat/Swat_SprintRight.ani")
            {
                looped = true;
            }

            if (file == "Swat/Swat_Idle.ani" | file == "Swat/Swat_RunBwd.ani" | file == "Swat/Swat_RunFwd.ani" | file == "Swat/Swat_RunLeft.ani" | file == "Swat/Swat_RunRight.ani" | file == "Swat/Swat_RunFwd.ani" | file == "Swat/Swat_SprintFwd.ani" | file == "Swat/Swat_SprintBwd.ani" | file == "Swat/Swat_SprintLeft.ani" | file == "Swat/Swat_SprintRight.ani")
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
