using Client.Controllers;
using Client.Interfeices;
using Client.Services;
using Client.Views.Shared;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views{
    class SettingView : IRender{
        public RenderWindow window { get; }

        // this is core node
        private Rectangle Background; 

        // UI Element
        private GuiBase Header;
        private GuiBase Body;

        private GuiBase GameForm;
        private GuiBase GraphicseForm;

        public SettingView(RenderWindow window){
            this.window = window;
            Router router = Router.Init();

            //init Form
            GameForm = InitFormSettingGame();
            GraphicseForm = InitFormSettingGraphics();

            //init Background
            Background = new Rectangle();
            Background.SetSize((Vector2f)window.Size);
            Background.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\BackGroundSetting.png"));
            Background.SetPosition(new Vector2f());

            //init Header
            Header = new Rectangle();

            Button graphicsButton = new Button((Vector2i mousePos) => {
                int Code = Body.GetHashCode();
                Body = GraphicseForm;
                Background.SetNode(
                    Code,
                    Body
                );
            });
            graphicsButton.text = new Text("Graphics", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            graphicsButton.text.FillColor = new Color(122, 68, 74);
            graphicsButton.text.CharacterSize = 48;

            graphicsButton.SetSize(new Vector2f(245, 90));
            graphicsButton.SetPosition(new Vector2f(20, 13));
            graphicsButton.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Button2.png"));

            Button GameButton = new Button((Vector2i mousePos) => {
                int Code = Body.GetHashCode();
                Body = GameForm;
                Background.SetNode(
                    Code,
                    Body
                );
            });
            GameButton.text = new Text("Game", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            GameButton.text.FillColor = new Color(122, 68, 74);
            GameButton.text.CharacterSize = 48;

            GameButton.SetSize(new Vector2f(245, 90));
            GameButton.SetPosition(new Vector2f(285, 13));
            GameButton.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Button2.png"));

            Button exitButton = new Button((Vector2i mousePos) => {
                router.graphicsControllers.SetController(new MenuController());
                Config.Save();
            });
            exitButton.SetSize(new Vector2f(130, 90));
            exitButton.SetPosition(new Vector2f((window.Size.X) - 150, 13));
            exitButton.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Exit.png"));

            Header.addNode(graphicsButton);
            Header.addNode(GameButton);
            Header.addNode(exitButton);


            //init Body
            Body = GraphicseForm;

            //Add nods. Header end Body
            Background.addNode(Header);
            Background.addNode(Body);

            window.MouseButtonPressed += MousePressed;
            window.MouseButtonReleased += MouseReleased;
            window.MouseMoved += MouseMove;
        }

        //init form Graphics
        public GuiBase InitFormSettingGraphics() {
            Rectangle FormSettingGraphics = new Rectangle();
            FormSettingGraphics.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\1.png"));
            FormSettingGraphics.SetSize(new Vector2f(930, 595));

            //CheckBox
            CheckBox checkBoxFPS = new CheckBox(
                () => { Config.config.isDisplayFPS = true;},

                () => { Config.config.isDisplayFPS = false;},

                ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckF.png"),
                ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckT.png"),
                Config.config.isDisplayFPS
            );

            checkBoxFPS.SetSize(new Vector2f(70, 70));
            checkBoxFPS.SetPosition(new Vector2f((FormSettingGraphics.size.X - checkBoxFPS.size.X) - 20, 20));

            CheckBox CheckBoxTransparantTextures = new CheckBox(
               () => { Config.config.isTransparantTextures = true;},

               () => { Config.config.isTransparantTextures = false;},

               ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckF.png"),
               ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckT.png"),
               Config.config.isTransparantTextures
           );

            CheckBoxTransparantTextures.SetSize(new Vector2f(70, 70));
            CheckBoxTransparantTextures.SetPosition(new Vector2f((FormSettingGraphics.size.X - checkBoxFPS.size.X) - 20, CheckBoxTransparantTextures.size.Y + 40));

            CheckBox CheckBoxFullScrean = new CheckBox(
               () => { Config.config.isFullScrean = true; Router.Init().InitFullScrean(); },

               () => { Config.config.isFullScrean = false; Router.Init().InitScrean(); },

               ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckF.png"),
               ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckT.png"),
               Config.config.isFullScrean
           );

            CheckBoxFullScrean.SetSize(new Vector2f(70, 70));
            CheckBoxFullScrean.SetPosition(new Vector2f((FormSettingGraphics.size.X - checkBoxFPS.size.X) - 20, (CheckBoxTransparantTextures.size.Y * 2)+ 60));

            // Border
            Button BorderFps = new Button();
            BorderFps.text = new Text("FPS", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            BorderFps.text.FillColor = new Color(122, 68, 74);
            BorderFps.text.CharacterSize = 32;

            BorderFps.SetSize(new Vector2f(245, 70));
            BorderFps.SetPosition(new Vector2f(20, 20));
            BorderFps.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Table.png"));

            Button BorderTransparantTextures = new Button();
            BorderTransparantTextures.text = new Text("Trans Textures", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            BorderTransparantTextures.text.FillColor = new Color(122, 68, 74);
            BorderTransparantTextures.text.CharacterSize = 29;

            BorderTransparantTextures.SetSize(new Vector2f(245, 70));
            BorderTransparantTextures.SetPosition(new Vector2f(20, BorderTransparantTextures.size.Y + 40));
            BorderTransparantTextures.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Table.png"));

            Button FullScrean = new Button();
            FullScrean.text = new Text("Full Screan", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            FullScrean.text.FillColor = new Color(122, 68, 74);
            FullScrean.text.CharacterSize = 32;

            FullScrean.SetSize(new Vector2f(245, 70));
            FullScrean.SetPosition(new Vector2f(20, (BorderTransparantTextures.size.Y * 2) + 60));
            FullScrean.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Table.png"));

            Button BorderFov = new Button();
            BorderFov.text = new Text("Fov", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            BorderFov.text.FillColor = new Color(122, 68, 74);
            BorderFov.text.CharacterSize = 32;

            BorderFov.SetSize(new Vector2f(245, 70));
            BorderFov.SetPosition(new Vector2f(20, (BorderTransparantTextures.size.Y * 3) + 80));
            BorderFov.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Table.png"));

            Button BorderReycast = new Button();
            BorderReycast.text = new Text("Count Rey", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            BorderReycast.text.FillColor = new Color(122, 68, 74);
            BorderReycast.text.CharacterSize = 32;

            BorderReycast.SetSize(new Vector2f(245, 70));
            BorderReycast.SetPosition(new Vector2f(20, (BorderTransparantTextures.size.Y * 4) + 100));
            BorderReycast.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Table.png"));

            Button BorderFovView = new Button();
            BorderFovView.text = new Text("34", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            BorderFovView.text.FillColor = new Color(122, 68, 74);
            BorderFovView.text.CharacterSize = 32;

            BorderFovView.SetSize(new Vector2f(150, 70));
            BorderFovView.SetPosition(new Vector2f((FormSettingGraphics.size.X - BorderFovView.size.X) - 20, 290));
            BorderFovView.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\TableMin.png"));

            Button BorderReyView = new Button();
            BorderReyView.text = new Text("512", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            BorderReyView.text.FillColor = new Color(122, 68, 74);
            BorderReyView.text.CharacterSize = 32;

            BorderReyView.SetSize(new Vector2f(150, 70));
            BorderReyView.SetPosition(new Vector2f((FormSettingGraphics.size.X - BorderReyView.size.X) - 20, 390));
            BorderReyView.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\TableMin.png"));

            //Slidebar
            Slider sliderFov = new Slider();
            sliderFov.SetSize(new Vector2f(450, 36));
            sliderFov.SetPosition(new Vector2f(285, 310));
            sliderFov.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\LineP.png"));

            sliderFov.LoadTextureSlider(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\G.png"));

            Slider sliderRey = new Slider();
            sliderRey.SetSize(new Vector2f(455, 35));
            sliderRey.SetPosition(new Vector2f(280, 410));
            sliderRey.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\LineP.png"));
            sliderRey.LoadTextureSlider(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\G.png"));

            //Append Node
            FormSettingGraphics.addNode(sliderFov);
            FormSettingGraphics.addNode(sliderRey);

            FormSettingGraphics.addNode(BorderFovView);
            FormSettingGraphics.addNode(BorderReyView);

            FormSettingGraphics.addNode(FullScrean); 
            FormSettingGraphics.addNode(BorderFov);
            FormSettingGraphics.addNode(BorderFps);
            FormSettingGraphics.addNode(BorderReycast);
            FormSettingGraphics.addNode(BorderTransparantTextures);
            
            FormSettingGraphics.addNode(checkBoxFPS);
            FormSettingGraphics.addNode(CheckBoxFullScrean);
            FormSettingGraphics.addNode(CheckBoxTransparantTextures);

            FormSettingGraphics.SetPosition(new Vector2f((window.Size.X / 2) - FormSettingGraphics.size.X / 2, 100 + (window.Size.Y / 2) - FormSettingGraphics.size.Y / 2));

            return FormSettingGraphics;
        }

        // init form Game
        public GuiBase InitFormSettingGame() {
            return new Rectangle();
        }

        public void Render(){
            foreach (var el in Background.GetGraphicsPackages())
                window.Draw(el);
        }

        private void MousePressed(object sender, MouseButtonEventArgs @event){
            Background.EventMousePressed(sender,  @event);
        }
        private void MouseReleased(object sender, MouseButtonEventArgs @event){
            Background.EventMouseReleassed(sender, @event);
        }
        private void MouseMove(object sender, MouseMoveEventArgs @event){
            //Background.EventMousePressed(sender, @event);
        }

        //function diz connect event
        public void DizActivation(){
            window.MouseButtonPressed -= MousePressed; 
            window.MouseButtonReleased -= MouseReleased;
            window.MouseMoved -= MouseMove;
        }

    }
}
