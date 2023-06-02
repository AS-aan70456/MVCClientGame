using Client.Controllers;
using Client.Interfeices;
using GraphicsEngine;
using GraphicsEngine.Shared;
using GraphicsEngine.Bilder;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Client.Views{
    class MenuView : IRender{
        public RenderWindow window { get; }

        // this is core node
        private UIObject Background;

        public MenuView(RenderWindow window) {
            this.window = window;

            InitPage();

            window.MouseButtonPressed += MousePressed;
            window.MouseButtonReleased += MouseReleased;
        }

        private void InitPage() {
            Router router = Router.Init();

            Background = new UIObject();
            Background.AddDrawble(new RectBilder(@"Resurces\Img\UI\Background.png").Init());
            Background.SetSize((Vector2f)window.Size);

            RectangleShape button = new RectBilder(@"Resurces\Img\UI\Button.png").Init();

            UIObject PlayButton = new UIObject();
            PlayButton.SetPosition(new Vector2f((window.Size.X / 2) - 200, 80));
            PlayButton.SetSize(new Vector2f(400, 80));

            PlayButton.MousePressed = (object sender, MouseButtonEventArgs @event) => {
                router.graphicsControllers.SetController(new GameController());
            };
            PlayButton.AddDrawble(button);
            PlayButton.AddDrawble(
                new TextBilder("Play", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(64)
                .Init()
            );

            UIObject OnlineButton = new UIObject();
            OnlineButton.SetPosition(new Vector2f((window.Size.X / 2) - 200, 180));
            OnlineButton.SetSize(new Vector2f(400, 80));

            OnlineButton.MousePressed = (object sender, MouseButtonEventArgs @event) => {
                router.graphicsControllers.SetController(new GameController());
            };
            OnlineButton.AddDrawble(new RectangleShape(button));
            OnlineButton.AddDrawble(
                new TextBilder("Online", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(64)
                .Init()
            );

            UIObject SettingButton = new UIObject();
            SettingButton.SetPosition(new Vector2f((window.Size.X / 2) - 200, 280));
            SettingButton.SetSize(new Vector2f(400, 80));

            SettingButton.MousePressed = (object sender, MouseButtonEventArgs @event) => {
                router.graphicsControllers.SetController(new SettingController());
            };
            SettingButton.AddDrawble(new RectangleShape(button));
            SettingButton.AddDrawble(
                new TextBilder("Settings", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(64)
                .Init()
            );

            UIObject ExitButton = new UIObject();
            ExitButton.SetPosition(new Vector2f((window.Size.X / 2) - 200, 380));
            ExitButton.SetSize(new Vector2f(400, 80));

            ExitButton.MousePressed = (object sender, MouseButtonEventArgs @event) => {
                window.Close();
            };
            ExitButton.AddDrawble(new RectangleShape(button));
            ExitButton.AddDrawble(
                new TextBilder("Exit", @"Resurces\Font\Samson.ttf")
                .FillColor(new Color(122, 68, 74))
                .CharacterSize(64)
                .Init()
            );

            Background.addNode(PlayButton);
            Background.addNode(OnlineButton);
            Background.addNode(SettingButton);
            Background.addNode(ExitButton);

        }

        public void Render(){
            foreach (var el in Background.GetGraphicsPackages())
                window.Draw((Drawable)el);
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
