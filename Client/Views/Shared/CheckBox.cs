using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Shared{
    class CheckBox : GuiBase{

        public RectangleShape rectangle;

        public Texture OnCheck { get; private set; }
        public Texture OffCheck { get; set; }

        public bool IsAction { get; private set; }
        
        private Action Action;
        private Action DizAction;

        public CheckBox(Action  Action, Action DizAction, Texture OnCheck, Texture OffCheck, bool IsAction){
            this.Action = Action;
            this.DizAction = DizAction;

            this.OnCheck = OnCheck;
            this.OffCheck = OffCheck;

            rectangle = new RectangleShape();

            rectangle.Texture = OffCheck;

            this.IsAction = IsAction;

            if (IsAction)
                rectangle.Texture = OnCheck;
            else
                rectangle.Texture = OffCheck;
            
        }

        public void LoadTexture(Texture texture){
            rectangle.Texture = texture;
        }

        public override void SetPosition(Vector2f Position){
            base.SetPosition(Position);
            rectangle.Position = base.Position;
        }

        public override void SetSize(Vector2f Size){
            base.SetSize(Size);
            rectangle.Size = base.Size;
        }

        private bool CheckPressed(Vector2i MousePos){
            if ((MousePos.X > Position.X && MousePos.Y > Position.Y) && (MousePos.X < Position.X + Size.X && MousePos.Y < Position.Y + Size.Y)){
                IsAction = !IsAction;
                if (IsAction){
                    rectangle.Texture = OnCheck;
                    Action.Invoke();
                }
                else {
                    rectangle.Texture = OffCheck;
                    DizAction.Invoke();
                }
                
            }
            return false;
        }

        protected override IEnumerable<Drawable> GiveAwayGraphicsPackages() {
            List<Drawable> drawables = new List<Drawable>();
            drawables.Add(rectangle);
            return drawables;
        }

        protected override void MousePressed(object sender, MouseButtonEventArgs @event){
            CheckPressed(new Vector2i(@event.X, @event.Y));
        }

        protected override void MouseReleassed(object sender, MouseButtonEventArgs @event){}

        protected override void MouseMoved(object sender, MouseMoveEventArgs e){}

        protected override void Updata(){}
    }
}
