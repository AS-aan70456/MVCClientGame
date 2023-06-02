using GraphicsEngine.Resurces;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace GraphicsEngine.Shared{
    public class CheckBox : UIObject{

        public RectangleShape rectangle;

        private Texture OnCheck { get; set; }
        private Texture OffCheck { get; set; }

        public bool IsAction { get; private set; }
        
        private Action Action;
        private Action DizAction;

        public CheckBox(Action  Action, Action DizAction, string OnCheckTexture, string OffCheckTexture, bool IsAction){
            rectangle = new RectangleShape();

            this.Action = Action;
            this.DizAction = DizAction;

            this.OnCheck = ResurceMeneger.LoadTexture(OnCheckTexture);
            this.OffCheck = ResurceMeneger.LoadTexture(OffCheckTexture);

            this.IsAction = IsAction;

            base.MousePressed = 
                (object sender, MouseButtonEventArgs @event) => {
                        this.IsAction = !this.IsAction;
                    if (this.IsAction){
                        rectangle.Texture = OnCheck;
                        Action.Invoke();
                    }
                    else{
                        rectangle.Texture = OffCheck;
                        DizAction.Invoke();
                    }
                };

            if (IsAction)
                rectangle.Texture = OnCheck;
            else
                rectangle.Texture = OffCheck;
            AddDrawble(rectangle);

        }
    }
}
