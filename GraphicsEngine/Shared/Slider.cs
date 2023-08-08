using GraphicsEngine.Bilder; // Importing necessary namespaces
using SFML.System;
using SFML.Window;
using System;

namespace GraphicsEngine.Shared
{
    public class Slider : UIObject
    { // Inherits from UIObject class

        private UIObject slider; // Private UIObject for the slider
        private Vector2i MousePosition; // Store mouse position
        private bool isPressetSlider; // Store if slider is being pressed

        private Action<int> MovedSlider; // Delegate for slider movement

        // Properties for maximum, minimum, and current value
        public float MaxVaule { get; }
        public float MinVaule { get; }
        public float Vaule { get; private set; }

        // Constructor for Slider class
        public Slider(int MinVaule, int MaxVaule, int vaule, string path, Action<int> MovedSlider)
        {
            slider = new UIObject(); // Create a UIObject for the slider

            this.MaxVaule = MaxVaule;
            this.MinVaule = MinVaule;
            this.Vaule = vaule;

            base.Start =
                () => {
                    float delta = ((Vaule - MinVaule) / (MaxVaule - MinVaule));
                    slider.SetSize(new Vector2f(32, 56)); // Set size of the slider
                    slider.SetPosition(new Vector2f((delta * (Size.X - slider.Size.X)), -10)); // Set initial position of the slider
                    slider.AddDrawble(new RectBilder(path).Init()); // Add a drawable to the slider
                    addNode(slider); // Add the slider as a node
                };

            // Event handler for mouse press
            base.MousePressed =
                (object sender, MouseButtonEventArgs @event) => {
                    isPressetSlider = slider.CheckPressed(new Vector2i(@event.X, @event.Y));
                };

            // Event handler for mouse release
            base.MouseRealessed =
                (object sender, MouseButtonEventArgs @event) => {
                    isPressetSlider = false;
                };

            // Event handler for mouse movement
            base.MouseMoved =
                (object sender, MouseMoveEventArgs @event) => {
                    if (!isPressetSlider)
                    {
                        MousePosition = new Vector2i(@event.X, @event.Y);
                        return;
                    }
                    Vaule -= (MousePosition.X - @event.X) * ((MaxVaule - MinVaule) / (Size.X - slider.Size.X));
                    MousePosition = new Vector2i(@event.X, @event.Y);

                    // Ensure value stays within limits
                    if (Vaule < MinVaule) Vaule = MinVaule;
                    if (Vaule > MaxVaule) Vaule = MaxVaule;

                    float delta = ((Vaule - MinVaule) / (MaxVaule - MinVaule));
                    slider.SetPosition(new Vector2f((delta * (Size.X - slider.Size.X)) + AbsolutPosition.X, base.AbsolutPosition.Y - 10));

                    MovedSlider.Invoke((int)Vaule); // Invoke the MovedSlider delegate
                };
            this.MovedSlider = MovedSlider; // Store the MovedSlider delegate
        }
    }
}
