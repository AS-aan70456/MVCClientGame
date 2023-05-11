using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Shared{
    class Button : RectangleShape {

        public Text Text;

        private Action Pressed;
        private Action Released;

        public Button(Action Pressed, Action Released) {
            this.Pressed = Pressed;
            this.Released = Released;
        }

        public Button(Action Pressed) {
            this.Pressed = Pressed;
            this.Released = () => { };
        }

        public bool CheckPressed(Vector2i MousePos) {
            if ((MousePos.X > Position.X && MousePos.Y > Position.Y) && (MousePos.X < Position.X + Size.X && MousePos.Y < Position.Y + Size.Y)){
                Pressed.Invoke();
                return true;
            }

            return false;
        }
        public bool CheckReleased(Vector2i MousePos){
            if ((MousePos.X > Position.X && MousePos.Y > Position.Y) && (MousePos.X < Position.X + Size.X && MousePos.Y < Position.Y + Size.Y)){
                Released.Invoke();
                return true;
            }

            return false;
        }
    }
}
