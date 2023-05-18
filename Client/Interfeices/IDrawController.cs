using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfeices{
    public interface IDrawController{

         void Draw();
         void Updata();

        //function connect event
        void Activation(RenderWindow window);

        //function diz connect event
        void DizActivation();

    }
}
