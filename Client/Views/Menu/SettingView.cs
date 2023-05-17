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

        private Rectangle Background;
        private Rectangle FormSettingGame;

        private CheckBox checkBoxFPS;
        private CheckBox CheckBoxTransparantTextures;

        public SettingView(RenderWindow window){
            this.window = window;

            //Background = new RectangleShape();
            //Background.Size = (SFML.System.Vector2f)window.Size;
           // Background.Texture = ResurceMeneger.LoadTexture(@"Resurces\Img\UI\BackGroundSetting.png");

            checkBoxFPS = new CheckBox(
                () => { Config.config.isDisplayFPS = true; Config.Save(); },

                () => { Config.config.isDisplayFPS = false; Config.Save(); },

                ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckF.png"),
                ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckT.png"),
                Config.config.isDisplayFPS
            );

           // checkBoxFPS.Size = new Vector2f(70, 70);
            //checkBoxFPS.Position = new Vector2f((window.Size.X - checkBoxFPS.Size.X) - 30, 210);

            // CheckBox TransparantTextures 

            CheckBoxTransparantTextures = new CheckBox(
                () => { Config.config.isTransparantTextures = true; Config.Save(); },

                () => { Config.config.isTransparantTextures = false; Config.Save(); },

                ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckF.png"),
                ResurceMeneger.LoadTexture(@"Resurces\Img\UI\CheckT.png"),
                Config.config.isTransparantTextures
            );

            //CheckBoxTransparantTextures.Size = new Vector2f(70, 70);
           // CheckBoxTransparantTextures.Position = new Vector2f((window.Size.X - checkBoxFPS.Size.X) - 30, 320);

            window.MouseButtonPressed += MousePressed;

        }

        public void Render(){
            
        //    window.Draw(background);
        //    window.Draw(checkBoxFPS);
        //    window.Draw(CheckBoxTransparantTextures);
        }

        private void MousePressed(object sender, MouseButtonEventArgs @event){
            checkBoxFPS.CheckPressed(new Vector2i(@event.X, @event.Y));
            CheckBoxTransparantTextures.CheckPressed(new Vector2i(@event.X, @event.Y));
        }

        public void DizActivation(){
            window.MouseButtonPressed -= MousePressed;
        }

    }
}
