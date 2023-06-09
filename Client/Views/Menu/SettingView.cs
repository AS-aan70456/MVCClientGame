using Client.Controllers;
using Client.Interfeices;
using GraphicsEngine;
using GraphicsEngine.Bilder;
using GraphicsEngine.Shared;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Client.Views{
    class SettingView : IRender{
        public RenderWindow window { get; }

        // this is core node
        private UIObject Canvas; 

        // UI Element
        private UIObject Header;
        private UIObject Body;

        private UIObject GameForm;
        private UIObject AudioForm;
        private UIObject GraphicseForm;


        public SettingView(RenderWindow window){
            this.window = window;
            Router router = Router.Init();

            //init Form
            GameForm = InitFormSettingGame();
            GraphicseForm = InitFormSettingGraphics();
            AudioForm = InitFormSettingAudio();

            //init Background
            Canvas = new UIObject();
            Canvas.SetSize((Vector2f)window.Size);
            Canvas.AddDrawble(new RectBilder(@"Resurces\Img\UI\BackGroundSetting.png").Init());

            //init Header
            Header = new UIObject();

            UIObject graphicsButton = new UIObject();
            graphicsButton.MousePressed += (object sender, MouseButtonEventArgs @event) =>{
                int Code = Body.GetHashCode();
                    Body = GraphicseForm;
                    Canvas.SetNode(Code,Body);
            };
            graphicsButton.SetSize(new Vector2f(245, 90));
            graphicsButton.SetPosition(new Vector2f(20, 13));
            graphicsButton.AddDrawble(new RectBilder(@"Resurces\Img\UI\Button2.png").Init());
            graphicsButton.AddDrawble(new TextBilder("Graphics", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(48)
                .Init()
            );

            UIObject audioButton = new UIObject();
            audioButton.MousePressed += (object sender, MouseButtonEventArgs @event) => {
                int Code = Body.GetHashCode();
                Body = AudioForm;
                Canvas.SetNode(Code, Body);
            };
            audioButton.SetSize(new Vector2f(245, 90));
            audioButton.SetPosition(new Vector2f(285, 13));
            audioButton.AddDrawble(new RectBilder(@"Resurces\Img\UI\Button2.png").Init());
            audioButton.AddDrawble(new TextBilder("Audio", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(48)
                .Init()
            );

            UIObject gameButton = new UIObject();
            gameButton.MousePressed += (object sender, MouseButtonEventArgs @event) => {
                int Code = Body.GetHashCode();
                Body = GameForm;
                Canvas.SetNode(Code, Body);
            };
            gameButton.SetSize(new Vector2f(245, 90));
            gameButton.SetPosition(new Vector2f(550, 13));
            gameButton.AddDrawble(new RectBilder(@"Resurces\Img\UI\Button2.png").Init());
            gameButton.AddDrawble(new TextBilder("Game", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(48)
                .Init()
            );

            UIObject exitButton = new UIObject();
            exitButton.MousePressed += (object sender, MouseButtonEventArgs @event) => {
                Config.Save();
                router.graphicsControllers.SetController(new MenuController());
            };
            exitButton.SetSize(new Vector2f(130, 90));
            exitButton.SetPosition(new Vector2f((window.Size.X) - 150, 13));
            exitButton.AddDrawble(new RectBilder(@"Resurces\Img\UI\Exit.png").Init());

            Header.addNode(graphicsButton);
            Header.addNode(audioButton);
            Header.addNode(gameButton);
            Header.addNode(exitButton);

            //init Body
            Body = GraphicseForm;

            //Add nods. Header end Body
            Canvas.addNode(Header);
            Canvas.addNode(Body);

            window.MouseButtonPressed += MousePressed;
            window.MouseButtonReleased += MouseReleased;
            window.MouseMoved += MouseMove;
        }

        //init form Graphics
        public UIObject InitFormSettingGraphics() {
            UIObject FormSettingGraphics = new UIObject();
            FormSettingGraphics.SetSize(new Vector2f(930, 595));
            FormSettingGraphics.SetPosition(new Vector2f((window.Size.X / 2) - FormSettingGraphics.Size.X / 2, 100 + (window.Size.Y / 2) - FormSettingGraphics.Size.Y / 2));
            FormSettingGraphics.AddDrawble(new RectBilder(@"Resurces\Img\UI\1.png").Init());


            //CheckBox
            CheckBox checkBoxFPS = new CheckBox(
                () => { Config.config.isDisplayFPS = true;},
                () => { Config.config.isDisplayFPS = false;},
       
                @"Resurces\Img\UI\CheckF.png",
                @"Resurces\Img\UI\CheckT.png",
                Config.config.isDisplayFPS
            );
            checkBoxFPS.SetSize(new Vector2f(70, 70));
            checkBoxFPS.SetPosition(new Vector2f((FormSettingGraphics.Size.X - checkBoxFPS.Size.X) - 20, 20));
       
            CheckBox CheckBoxTransparantTextures = new CheckBox(
                () => { Config.config.isTransparantTextures = true; },
                () => { Config.config.isTransparantTextures = false; },

                @"Resurces\Img\UI\CheckF.png",
                @"Resurces\Img\UI\CheckT.png",
                Config.config.isTransparantTextures
            );
            CheckBoxTransparantTextures.SetSize(new Vector2f(70, 70));
            CheckBoxTransparantTextures.SetPosition(new Vector2f((FormSettingGraphics.Size.X - checkBoxFPS.Size.X) - 20, 40 + 70));

            CheckBox CheckBoxFullScrean = new CheckBox(
                () => { Config.config.isFullScrean = true;
                    Router.Init().graphicsControllers.InitFullScrean();
                    },
                () => { Config.config.isFullScrean = false;
                    Router.Init().graphicsControllers.InitScrean();
                },

                @"Resurces\Img\UI\CheckF.png",
                @"Resurces\Img\UI\CheckT.png",
                Config.config.isFullScrean
            );
            CheckBoxFullScrean.SetSize(new Vector2f(70, 70));
            CheckBoxFullScrean.SetPosition(new Vector2f((FormSettingGraphics.Size.X - checkBoxFPS.Size.X) - 20, 60 + 140));

            // Borders
            UIObject BorderFps = new UIObject(new Vector2f(20, 20), new Vector2f(245, 70));
            BorderFps.AddDrawble(new RectBilder(@"Resurces\Img\UI\Table.png").Init());
            BorderFps.AddDrawble(
                new TextBilder("FPS", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(32)
                .Init()
            );

            UIObject BorderTransparantTextures = new UIObject(new Vector2f(20, 40 + (70 * 1)), new Vector2f(245, 70));
            BorderTransparantTextures.AddDrawble(new RectBilder(@"Resurces\Img\UI\Table.png").Init());
            BorderTransparantTextures.AddDrawble(
                new TextBilder("Trans Textures", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(32)
                .Init()
            );

            UIObject FullScrean = new UIObject(new Vector2f(20, 60 + (70 * 2)), new Vector2f(245, 70));
            FullScrean.AddDrawble(new RectBilder(@"Resurces\Img\UI\Table.png").Init());
            FullScrean.AddDrawble(
                new TextBilder("Full Screan", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(32)
                .Init()
            );

            UIObject BorderFov = new UIObject(new Vector2f(20, 80 + (70 * 3)), new Vector2f(245, 70));
            BorderFov.AddDrawble(new RectBilder(@"Resurces\Img\UI\Table.png").Init());
            BorderFov.AddDrawble(
                new TextBilder("Fov", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(32)
                .Init()
            );

            UIObject BorderReycast = new UIObject(new Vector2f(20, 100 + (70 * 4)), new Vector2f(245, 70));
            BorderReycast.AddDrawble(new RectBilder(@"Resurces\Img\UI\Table.png").Init());
            BorderReycast.AddDrawble(
                new TextBilder("Count Rey", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(32)
                .Init()
            );

            Text BorderFovViewText = new TextBilder(Config.config.fov.ToString(), @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(32)
                .Init();
            UIObject BorderFovView = new UIObject(new Vector2f((FormSettingGraphics.Size.X - 150) - 20, 80 + (70 * 3)), new Vector2f(150, 70));
            BorderFovView.AddDrawble(new RectBilder(@"Resurces\Img\UI\TableMin.png").Init());
            BorderFovView.AddDrawble(BorderFovViewText);

            Text BorderReyViewText = new TextBilder(Config.config.numRey.ToString(), @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(32)
                .Init();
            UIObject BorderReyView = new UIObject(new Vector2f((FormSettingGraphics.Size.X - 150) - 20, 100 + (70 * 4)), new Vector2f(150, 70));
            BorderReyView.AddDrawble(new RectBilder(@"Resurces\Img\UI\TableMin.png").Init());
            BorderReyView.AddDrawble(BorderReyViewText);


            //Slidebar
            Slider sliderFov = new Slider(40, 80, Config.config.fov, @"Resurces\Img\UI\G.png",(int Vaule) => {
                BorderFovViewText.DisplayedString = Vaule.ToString();
                Config.config.fov = Vaule;
            });
            sliderFov.AddDrawble(new RectBilder(@"Resurces\Img\UI\LineP.png").Init());
            sliderFov.SetSize(new Vector2f(450, 36));
            sliderFov.SetPosition(new Vector2f(285, 310));
            

            Slider sliderRey = new Slider(60, 1000, Config.config.numRey, @"Resurces\Img\UI\G.png", (int Vaule) => {
                BorderReyViewText.DisplayedString = Vaule.ToString();
                Config.config.numRey = Vaule;
            });
            sliderRey.AddDrawble(new RectBilder(@"Resurces\Img\UI\LineP.png").Init());
            sliderRey.SetSize(new Vector2f(455, 35));
            sliderRey.SetPosition(new Vector2f(280, 410));

            //Append Node
            FormSettingGraphics.addNode(sliderFov);
            FormSettingGraphics.addNode(sliderRey);

            FormSettingGraphics.addNode(FullScrean);
            FormSettingGraphics.addNode(BorderFov);
            FormSettingGraphics.addNode(BorderFps);
            FormSettingGraphics.addNode(BorderReycast);
            FormSettingGraphics.addNode(BorderTransparantTextures);
       
            FormSettingGraphics.addNode(checkBoxFPS);
            FormSettingGraphics.addNode(CheckBoxFullScrean);
            FormSettingGraphics.addNode(CheckBoxTransparantTextures);

            FormSettingGraphics.addNode(BorderFovView);
            FormSettingGraphics.addNode(BorderReyView);

            return FormSettingGraphics;
        }
       
        // init form Game
        public UIObject InitFormSettingGame() {
            UIObject FormSettingGame = new UIObject();
            FormSettingGame.SetSize(new Vector2f(930, 595));
            FormSettingGame.AddDrawble(new RectBilder(@"Resurces\Img\UI\1.png").Init());

            // List Boxs
            Text ListText = new TextBilder("Easily", @"Resurces\Font\Samson.ttf").CharacterSize(32).FillColor(new Color(122, 68, 74)).Init();
            ListBox listBox = new ListBox(new string[] {"Easily", "Normal", "Hard" }, 0, (int countList) => {
                ListText.DisplayedString = new string[] { "Easily", "Normal", "Hard" }[countList];
            });
            listBox.SetPosition(new Vector2f(20, 20));
            listBox.SetSize(new Vector2f(245, 70));
            listBox.AddDrawble(new RectBilder(@"Resurces\Img\UI\Table.png").Init());
            listBox.AddDrawble(ListText);

            // Border

            // Add nodes
            FormSettingGame.addNode(listBox);
       
            FormSettingGame.SetPosition(new Vector2f((window.Size.X / 2) - FormSettingGame.Size.X / 2, 100 + (window.Size.Y / 2) - FormSettingGame.Size.Y / 2));
       
            return FormSettingGame;
        }

        public UIObject InitFormSettingAudio() {
            UIObject FormSettingGame = new UIObject();
            FormSettingGame.SetSize(new Vector2f(930, 595));
            FormSettingGame.AddDrawble(new RectBilder(@"Resurces\Img\UI\1.png").Init());


            // Add nodes
            

            FormSettingGame.SetPosition(new Vector2f((window.Size.X / 2) - FormSettingGame.Size.X / 2, 100 + (window.Size.Y / 2) - FormSettingGame.Size.Y / 2));

            return FormSettingGame;
        }

        public void Render(){
            foreach (var el in Canvas.GetGraphicsPackages())
                window.Draw((Drawable)el);
            Canvas.EventUpdata();
        }

        private void MousePressed(object sender, MouseButtonEventArgs @event){
            Canvas.EventMousePressed(sender,  @event);
        }
        private void MouseReleased(object sender, MouseButtonEventArgs @event){
            Canvas.EventMouseReleassed(sender, @event);
        }
        private void MouseMove(object sender, MouseMoveEventArgs @event){
            Canvas.EventMouseMoved(sender, @event);
        }

        //function diz connect event
        public void DizActivation(){
            window.MouseButtonPressed -= MousePressed; 
            window.MouseButtonReleased -= MouseReleased;
            window.MouseMoved -= MouseMove;
        }

    }
}
