using GraphicsEngine.Bilder;
using SFML.System;
using SFML.Window;
using System;

namespace GraphicsEngine.Shared{
    public class Slider : UIObject{

        private UIObject slider;

        private Vector2i MousePosition;
        private bool isPressetSlider;

        private Action<int> MovedSlider;

        public float MaxVaule { get; }
        public float MinVaule { get; }
        public float Vaule { get; private set; }

        public Slider(int MinVaule, int MaxVaule, int vaule, string path, Action<int> MovedSlider) {
            slider = new UIObject();
            
            this.MaxVaule = MaxVaule;
            this.MinVaule = MinVaule;
            this.Vaule = vaule;

            base.Start =
                () => {
                    float delta = ((Vaule - MinVaule) / (MaxVaule - MinVaule));
                    slider.SetSize(new Vector2f(32, 56));
                    slider.SetPosition(new Vector2f((delta * (Size.X - slider.Size.X)), -10));
                    slider.AddDrawble(new RectBilder(path).Init());
                    addNode(slider);
                };

            base.MousePressed =
                (object sender, MouseButtonEventArgs @event) =>{
                    isPressetSlider = slider.CheckPressed(new Vector2i(@event.X, @event.Y));
                };

            base.MouseRealessed =
                (object sender, MouseButtonEventArgs @event) => {
                    isPressetSlider = false;
                };

            base.MouseMoved =
                (object sender, MouseMoveEventArgs @event) => {
                    if (!isPressetSlider){
                        MousePosition = new Vector2i(@event.X, @event.Y);
                        return;
                    }
                    Vaule -= (MousePosition.X - @event.X) * ((MaxVaule - MinVaule) / (Size.X - slider.Size.X));
                    MousePosition = new Vector2i(@event.X, @event.Y);

                    if (Vaule < MinVaule) Vaule = MinVaule;
                    if (Vaule > MaxVaule) Vaule = MaxVaule;

                    float delta = ((Vaule - MinVaule) / (MaxVaule - MinVaule));
                    slider.SetPosition(new Vector2f((delta * (Size.X - slider.Size.X)) + AbsolutPosition.X, base.AbsolutPosition.Y - 10));

                    MovedSlider.Invoke((int)Vaule);
                };
            this.MovedSlider = MovedSlider;  
        }
    }
}
