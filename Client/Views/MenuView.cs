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

        Button PlayButton;
        Button OnlineButton;
        Button SettingButton;
        Button ExitButton;

        Texture Background;
        Texture Button;



        public MenuView(RenderWindow window) {
            this.window = window;

            Background = ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Background.png"); 
            Button = ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Button.png"); 

            Router router = Router.Init();
            PlayButton = new Button(() => {
                router.graphicsControllers.SetController(new GameController());
            });
            PlayButton.Size = new Vector2f(400, 80);
            PlayButton.Position = new Vector2f((window.Size.X / 2) - 200, 80);
            PlayButton.Texture = Button;

            PlayButton.Text = new Text("Play", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            PlayButton.Text.Position = new Vector2f((window.Size.X / 2) - 58, 80 - 10);
            PlayButton.Text.FillColor = new Color(122, 68, 74);
            PlayButton.Text.CharacterSize = 64;

            OnlineButton = new Button(() => {
                router.graphicsControllers.SetController(new GameController());
            });
            OnlineButton.Size = new Vector2f(400, 80);
            OnlineButton.Position = new Vector2f((window.Size.X / 2) - 200, 180);
            OnlineButton.Texture = Button;

            OnlineButton.Text = new Text("Online", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            OnlineButton.Text.Position = new Vector2f((window.Size.X / 2) - 85, 180 - 10);
            OnlineButton.Text.FillColor = new Color(122, 68, 74);
            OnlineButton.Text.CharacterSize = 64;

            SettingButton = new Button(() => {
                router.graphicsControllers.SetController(new GameController());
            });
            SettingButton.Size = new Vector2f(400, 80);
            SettingButton.Position = new Vector2f((window.Size.X / 2) - 200, 280);
            SettingButton.Texture = Button;

            SettingButton.Text = new Text("Setting", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            SettingButton.Text.Position = new Vector2f((window.Size.X / 2) - 100, 280 - 10);
            SettingButton.Text.FillColor = new Color(122, 68, 74);
            SettingButton.Text.CharacterSize = 64;

            ExitButton = new Button(() => {
                window.Close();
            });
            ExitButton.Size = new Vector2f(400, 80);
            ExitButton.Position = new Vector2f((window.Size.X / 2) - 200, 380);
            ExitButton.Texture = Button;

            ExitButton.Text = new Text("Exit", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
            ExitButton.Text.Position = new Vector2f((window.Size.X / 2) - 60, 380 - 10);
            ExitButton.Text.FillColor = new Color(122, 68, 74);
            ExitButton.Text.CharacterSize = 64;

            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(MousePressed);
            window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(MouseReleased);
        }

        public void Render(){
            RectangleShape Background = new RectangleShape();
            Background.Size = (SFML.System.Vector2f)window.Size;
            Background.Texture = new Texture(this.Background);


            window.Draw(Background);

            window.Draw(PlayButton);
            window.Draw(OnlineButton);
            window.Draw(SettingButton);
            window.Draw(ExitButton);

            window.Draw(PlayButton.Text);
            window.Draw(OnlineButton.Text);
            window.Draw(SettingButton.Text);
            window.Draw(ExitButton.Text);
        }

        private void MousePressed(object sender, MouseButtonEventArgs @event) {
            PlayButton.CheckPressed(new Vector2i(@event.X, @event.Y));
            OnlineButton.CheckPressed(new Vector2i(@event.X, @event.Y));
            SettingButton.CheckPressed(new Vector2i(@event.X, @event.Y));
            ExitButton.CheckPressed(new Vector2i(@event.X, @event.Y));
        }

        private void MouseReleased(object sender, MouseButtonEventArgs @event){

        }
    }
}
