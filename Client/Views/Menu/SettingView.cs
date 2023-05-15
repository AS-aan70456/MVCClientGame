using Client.Interfeices;
using Client.Services;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views{
    class SettingView : IRender{
        public RenderWindow window { get; }

        Texture Background;
        Texture Button;
        Texture CheckTrue;
        Texture CheckFalse;

        public SettingView(RenderWindow window){
            this.window = window;

            Background = ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Background.png");
            Button = ResurceMeneger.LoadTexture(@"Resurces\Img\UI\Background.png");
        }

        public void Render(){
            RectangleShape Background = new RectangleShape();
            Background.Size = (SFML.System.Vector2f)window.Size;
            Background.Texture = new Texture(this.Background);
        }

        public void DizActivation(){
            
        }
    }
}
