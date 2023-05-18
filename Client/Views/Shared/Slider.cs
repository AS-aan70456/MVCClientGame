using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Shared{
    class Slider : GuiBase{

        private RectangleShape rectangle;
        private RectangleShape slider;

        private bool isPressetSlider;

        public Slider() {
            rectangle = new RectangleShape();
            slider = new RectangleShape();

        }

        public void LoadTexture(Texture texture){
            rectangle.Texture = texture;
        }

        public void LoadTextureSlider(Texture texture){
            slider.Texture = texture;
        }

        public override void SetPosition(Vector2f Position) {
            base.SetPosition(Position);
            rectangle.Position = base.Position;
        }

        public override void SetSize(Vector2f Size){
            base.SetSize(Size);
            rectangle.Size = base.Size;
        }

        protected override void Updata(){
            
        }

        protected override IEnumerable<Drawable> GiveAwayGraphicsPackages(){
            List<Drawable> drawables = new List<Drawable>();
            drawables.Add(rectangle);
            return drawables;
        }

        protected override void MousePressed(object sender, MouseButtonEventArgs @event)  {
            isPressetSlider = true;
        }

        protected override void MouseReleassed(object sender, MouseButtonEventArgs @event) {
            isPressetSlider = false;
        }

        protected override void MouseMoved(object sender, MouseMoveEventArgs e){
            if (!isPressetSlider) return;



        }
    }
}
