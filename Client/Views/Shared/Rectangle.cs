using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Shared{
    class Rectangle : GuiBase{

        private RectangleShape rectangle;

        public Rectangle() {
            rectangle = new RectangleShape();
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

        protected override void Updata(){}

        protected override IEnumerable<Drawable> GiveAwayGraphicsPackages(){
            List<Drawable> drawables = new List<Drawable>();
            drawables.Add(rectangle);
            return drawables;
        }

        protected override void MousePressed(object sender, MouseButtonEventArgs @event){}
    }
}
