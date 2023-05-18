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

            //init Header
            Header = new Rectangle();

            Button graphicsButton = new Button(() => {
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

            Button GameButton = new Button(() => {
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

            Button exitButton = new Button(() => {
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
            Body = InitFormSettingGraphics();

            //Add nods. Header end Body
            Background.addNode(Header);
            Background.addNode(Body);

            window.MouseButtonPressed += MousePressed;

        }

        //init form Graphics
        public GuiBase InitFormSettingGraphics() {
            Rectangle FormSettingGraphics = new Rectangle();

            CheckBox checkBoxFPS = new CheckBox(
                () => { Config.config.isDisplayFPS = true;},

                () => { Config.config.isDisplayFPS = false;},

                ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckF.png"),
                ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckT.png"),
                Config.config.isDisplayFPS
            );

            checkBoxFPS.SetSize(new Vector2f(70, 70));
            checkBoxFPS.SetPosition(new Vector2f((window.Size.X - checkBoxFPS.size.X) - 50, 220));

            CheckBox CheckBoxTransparantTextures = new CheckBox(
               () => { Config.config.isTransparantTextures = true;},

               () => { Config.config.isTransparantTextures = false;},

               ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckF.png"),
               ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckT.png"),
               Config.config.isTransparantTextures
           );

            CheckBoxTransparantTextures.SetSize(new Vector2f(70, 70));
            CheckBoxTransparantTextures.SetPosition(new Vector2f((window.Size.X - checkBoxFPS.size.X) - 50, 320));

            FormSettingGraphics.addNode(checkBoxFPS);
            FormSettingGraphics.addNode(CheckBoxTransparantTextures);

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

        //function diz connect event
        public void DizActivation(){
            window.MouseButtonPressed -= MousePressed;
        }

    }
}
