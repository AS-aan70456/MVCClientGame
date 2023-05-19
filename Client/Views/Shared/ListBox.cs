using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Shared{
    class ListBox : GuiBase{

        private RectangleShape rectangle;
        public Text text;

        private int enumCount;
        private Action<int> Pressed;
        public string[] dinamicEnum { get; }

        public ListBox(string[] dinamicEnum,int enumCount, Action<int> Pressed) {
            rectangle = new RectangleShape();

            this.enumCount = enumCount;
            this.dinamicEnum = dinamicEnum;
            this.Pressed = Pressed;
        }

        public void LoadTexture(Texture texture){
            rectangle.Texture = texture;
        }

        public void LoadText(Text text){
            this.text = text;
            text.DisplayedString = dinamicEnum[enumCount];
            text.Position = new Vector2f((base.Size / 2).X - text.GetGlobalBounds().Width / 2, (base.Size / 2).Y - text.GetGlobalBounds().Height * 1.5f) + base.Position;
        }

        public override void SetPosition(Vector2f Position){
            base.SetPosition(Position);
            rectangle.Position = base.Position;
            text.Position = new Vector2f((base.Size / 2).X - text.GetGlobalBounds().Width / 2, (base.Size / 2).Y - text.GetGlobalBounds().Height * 1.5f) + base.Position;
        }

        public override void SetSize(Vector2f Size){
            base.SetSize(Size);
            rectangle.Size = base.Size;
        }

        private bool CheckPressed(Vector2i MousePos) { 
            if ((MousePos.X > rectangle.Position.X && MousePos.Y > rectangle.Position.Y) && (MousePos.X < rectangle.Position.X + rectangle.Size.X && MousePos.Y < rectangle.Position.Y + rectangle.Size.Y)){
                return true;
            }
            return false;
        }

        protected override IEnumerable<Drawable> GiveAwayGraphicsPackages() {
            List<Drawable> drawables = new List<Drawable>();
            drawables.Add(rectangle);
            drawables.Add(text);
            return drawables;
        }

        protected override void MouseMoved(object sender, MouseMoveEventArgs @event){

        }

        protected override void MousePressed(object sender, MouseButtonEventArgs @event){
            if (CheckPressed(new Vector2i(@event.X, @event.Y))){
                enumCount++;

                if (enumCount > dinamicEnum.Length - 1) enumCount = 0;

                Pressed?.Invoke(enumCount);
                text.DisplayedString = dinamicEnum[enumCount];
                text.Position = new Vector2f((base.Size / 2).X - text.GetGlobalBounds().Width / 2, (base.Size / 2).Y - text.GetGlobalBounds().Height * 1.5f) + base.Position;
            }
        }

        protected override void MouseReleassed(object sender, MouseButtonEventArgs @event){

        }

        protected override void Updata(){}
    }
}
