using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Shared{
    class CheckBox : RectangleShape{

        public Texture OnCheck { get; set; }
        public Texture OffCheck { get; set; }

        public bool IsAction { get; private set; }

        private Action Action;
        private Action DizAction;

        public CheckBox(Action  Action, Action DizAction, Texture OnCheck, Texture OffCheck, bool IsAction){
            this.Action = Action;
            this.DizAction = DizAction;

            this.OnCheck = OnCheck;
            this.OffCheck = OffCheck;

            base.Texture = OffCheck;

            this.IsAction = IsAction;

            if (IsAction)
                base.Texture = OnCheck;
            else
                base.Texture = OffCheck;
            
        }

        public bool CheckPressed(Vector2i MousePos){
            if ((MousePos.X > Position.X && MousePos.Y > Position.Y) && (MousePos.X < Position.X + Size.X && MousePos.Y < Position.Y + Size.Y)){
                IsAction = !IsAction;
                if (IsAction){
                    base.Texture = OnCheck;
                    Action.Invoke();
                }
                else {
                    base.Texture = OffCheck;
                    DizAction.Invoke();
                }
                
            }
            return false;
        }

    }
}
