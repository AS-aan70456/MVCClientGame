using GraphicsEngine.Resurces;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace GraphicsEngine.Shared{
    public class Slider : UIObject{

        private RectangleShape slider;

        private Vector2i MousePosition;
        private bool isPressetSlider;

        private Action<int> MovedSlider;

        public float MaxVaule { get; }
        public float MinVaule { get; }
        public float Vaule { get; private set; }

        public Slider(int MinVaule, int MaxVaule, int vaule, Action<int> MovedSlider) {
            slider = new RectangleShape();

            this.MaxVaule = MaxVaule;
            this.MinVaule = MinVaule;
            this.Vaule = vaule;

            base.MousePressed =
                (object sender, MouseButtonEventArgs @event) =>{
                    isPressetSlider = CheckPressed(new Vector2i(@event.X, @event.Y));
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
                    slider.Position = new Vector2f((delta * (Size.X - slider.Size.X)) + AbsolutPosition.X, base.AbsolutPosition.Y - 10);

                    MovedSlider.Invoke((int)Vaule);
                };
            this.MovedSlider = MovedSlider;


            
        }
        public void LoadTextureSlider(string path){
            slider.Texture = ResurceMeneger.LoadTexture(path);
        }

        private bool CheckPressed(Vector2i MousePos){
            if ((MousePos.X > slider.Position.X && MousePos.Y > slider.Position.Y) && (MousePos.X < slider.Position.X + slider.Size.X && MousePos.Y < slider.Position.Y + slider.Size.Y)) 
                return true;
            return false;
        }


        public void InitSlider() {
            AddDrawble(slider);
            slider.Size = new Vector2f(32, 56);
            float delta = ((Vaule - MinVaule) / (MaxVaule - MinVaule));
            slider.Position += new Vector2f((delta * (Size.X - slider.Size.X)) , -10);
        }




    }
}
