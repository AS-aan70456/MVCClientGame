using Client.Controllers;
using Client.Interfeices;
using Client.Services;
using Client.Views.Shared;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Linq;


namespace Client.Views{
    class MenuView : IRender{
        public RenderWindow window { get; }

        // this is core node
        private Rectangle Background;

        public MenuView(RenderWindow window) {
            this.window = window;

            Background = new Rectangle();
            Background.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Background.png"));
            Background.SetSize((Vector2f)window.Size);

            Router router = Router.Init();
            Button PlayButton = new Button((Vector2i mousePos) => {
                router.graphicsControllers.SetController(new GameController());
            });
            PlayButton.text = new Text("Play", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            PlayButton.text.FillColor = new Color(122, 68, 74);
            PlayButton.text.CharacterSize = 64;

            PlayButton.SetSize(new Vector2f(400, 80));
            PlayButton.SetPosition(new Vector2f((window.Size.X / 2) - 200, 80));
            PlayButton.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Button.png"));     

            Button OnlineButton = new Button((Vector2i mousePos) => {
                router.graphicsControllers.SetController(new GameController());
            });
            OnlineButton.text = new Text("Online", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            OnlineButton.text.FillColor = new Color(122, 68, 74);
            OnlineButton.text.CharacterSize = 64;

            OnlineButton.SetSize(new Vector2f(400, 80));
            OnlineButton.SetPosition(new Vector2f((window.Size.X / 2) - 200, 180));
            OnlineButton.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Button.png"));

            Button SettingButton = new Button((Vector2i mousePos) => {
                router.graphicsControllers.SetController(new SettingController());
            });
            SettingButton.text = new Text("Setting", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            SettingButton.text.FillColor = new Color(122, 68, 74);
            SettingButton.text.CharacterSize = 64;

            SettingButton.SetSize(new Vector2f(400, 80));
            SettingButton.SetPosition(new Vector2f((window.Size.X / 2) - 200, 280));
            SettingButton.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Button.png"));


            Button ExitButton = new Button((Vector2i mousePos) => {
                window.Close();
            });
            ExitButton.text = new Text("Exit", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            ExitButton.text.FillColor = new Color(122, 68, 74);
            ExitButton.text.CharacterSize = 64;

            ExitButton.SetSize(new Vector2f(400, 80));
            ExitButton.SetPosition(new Vector2f((window.Size.X / 2) - 200, 380));
            ExitButton.LoadTexture(ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Button.png"));

            Background.addNode(PlayButton);
            Background.addNode(OnlineButton);
            Background.addNode(SettingButton);
            Background.addNode(ExitButton);

            window.MouseButtonPressed += MousePressed;
            window.MouseButtonReleased += MouseReleased;
        }

        public void Render(){
            foreach (var el in Background.GetGraphicsPackages())
                window.Draw(el);
        }

        private void MousePressed(object sender, MouseButtonEventArgs @event) {
            Background.EventMousePressed(sender, @event);
        }

        private void MouseReleased(object sender, MouseButtonEventArgs @event){

        }

        public void DizActivation(){
            window.MouseButtonPressed -= MousePressed;
            window.MouseButtonReleased -= MouseReleased;
        }
    }
}
