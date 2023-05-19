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

        private Vector2i MousePosition;
        private bool isPressetSlider;

        private Action<int> MovedSlider;

        public float MaxVaule { get; }
        public float MinVaule { get; }
        public float Vaule { get; private set; }

        public Slider(int MinVaule, int MaxVaule, int Vaule, Action<int> MovedSlider) {
            rectangle = new RectangleShape();
            slider = new RectangleShape();
            slider.Size = new Vector2f(32,56);

            this.MaxVaule = MaxVaule;
            this.MinVaule = MinVaule;
            this.Vaule = Vaule;

            slider.Position = new Vector2f(((Vaule - MinVaule) / (MaxVaule - MinVaule)) + rectangle.Position.X, rectangle.Position.Y);

            this.MovedSlider = MovedSlider;
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

            if (Vaule < MinVaule) Vaule = MinVaule;
            if (Vaule > MaxVaule) Vaule = MaxVaule;

            float delta = ((Vaule - MinVaule) / (MaxVaule - MinVaule));
            slider.Position = new Vector2f((delta * (rectangle.Size.X - slider.Size.X)) + Position.X, base.Position.Y - 10);

        }

        public override void SetSize(Vector2f Size){
            base.SetSize(Size);
            rectangle.Size = base.Size;
        }

        private bool CheckPressed(Vector2i MousePos){
            if ((MousePos.X > slider.Position.X && MousePos.Y > slider.Position.Y) && (MousePos.X < slider.Position.X + slider.Size.X && MousePos.Y < slider.Position.Y + slider.Size.Y)) {

                return true;
            }
            return false;
        }

        protected override IEnumerable<Drawable> GiveAwayGraphicsPackages(){
            List<Drawable> drawables = new List<Drawable>();
            drawables.Add(rectangle);
            drawables.Add(slider);
            return drawables;
        }

        protected override void MousePressed(object sender, MouseButtonEventArgs @event)  {
            isPressetSlider = CheckPressed(new Vector2i(@event.X, @event.Y));
        }

        protected override void MouseReleassed(object sender, MouseButtonEventArgs @event) {
            isPressetSlider = false;
        }

        protected override void MouseMoved(object sender, MouseMoveEventArgs @event){
            if (!isPressetSlider) {
                MousePosition = new Vector2i(@event.X, @event.Y);
                return; 
            }
            Vaule -= (MousePosition.X - @event.X) * ((MaxVaule - MinVaule)/ (rectangle.Size.X - slider.Size.X));
            MousePosition = new Vector2i(@event.X, @event.Y);

            if (Vaule < MinVaule) Vaule = MinVaule;
            if (Vaule > MaxVaule) Vaule = MaxVaule;

            float delta = ((Vaule - MinVaule) / (MaxVaule - MinVaule));
            slider.Position = new Vector2f((delta * (rectangle.Size.X - slider.Size.X)) + Position.X, base.Position.Y - 10);

            MovedSlider.Invoke((int)Vaule);
        }

        protected override void Updata(){}
    }
}
