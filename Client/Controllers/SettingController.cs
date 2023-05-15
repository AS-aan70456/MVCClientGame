using Client.Interfeices;
using Client.Views;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers{
    class SettingController : IDrawController{

        SettingView View;

        public void Activation(RenderWindow window){
            View =  new SettingView(window);
        }

        public void DizActivation(){
            View.DizActivation();
        }

        public void Draw(){
            View.Render();
        }

        public void Updata(){
            
        }
    }
}
