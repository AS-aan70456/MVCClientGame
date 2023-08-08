using GraphicsEngine.Resurces;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace GraphicsEngine.Shared
{
    public class CheckBox : UIObject
    { // Inherits from UIObject class

        public RectangleShape rectangle; // Rectangle shape for the checkbox

        private Texture OnCheck { get; set; } // Texture for the checked state
        private Texture OffCheck { get; set; } // Texture for the unchecked state

        public bool IsAction { get; private set; } // Indicates whether the checkbox is checked or not

        private Action Action; // Action to be performed when the checkbox is checked
        private Action DizAction; // Action to be performed when the checkbox is unchecked

        // Constructor for CheckBox class
        public CheckBox(Action Action, Action DizAction, string OnCheckTexture, string OffCheckTexture, bool IsAction)
        {
            rectangle = new RectangleShape(); // Create a new rectangle shape

            this.Action = Action; // Store the checked action
            this.DizAction = DizAction; // Store the unchecked action

            this.OnCheck = ResurceMeneger.LoadTexture(OnCheckTexture); // Load the texture for the checked state
            this.OffCheck = ResurceMeneger.LoadTexture(OffCheckTexture); // Load the texture for the unchecked state

            this.IsAction = IsAction; // Store the initial checkbox state

            // Event handler for mouse press
            base.MousePressed =
                (object sender, MouseButtonEventArgs @event) => {
                    this.IsAction = !this.IsAction; // Toggle the checkbox state
                    if (this.IsAction)
                    {
                        rectangle.Texture = OnCheck; // Set the texture for checked state
                        Action.Invoke(); // Invoke the checked action
                    }
                    else
                    {
                        rectangle.Texture = OffCheck; // Set the texture for unchecked state
                        DizAction.Invoke(); // Invoke the unchecked action
                    }
                };

            if (IsAction)
                rectangle.Texture = OnCheck; // Set the initial texture based on the initial state
            else
                rectangle.Texture = OffCheck;
            AddDrawble(rectangle); // Add the rectangle shape to the UIObject's drawables
        }
    }
}
