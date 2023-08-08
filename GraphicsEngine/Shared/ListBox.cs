using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace GraphicsEngine.Shared
{
    public class ListBox : UIObject
    { // Inherits from UIObject class

        private int enumCount; // Store the current enum count
        private Action<int> Pressed; // Delegate for when an item is pressed
        public string[] dinamicEnum { get; } // Array to store dynamic enum values

        // Constructor for ListBox class
        public ListBox(string[] dinamicEnum, int enumCount, Action<int> Pressed)
        {

            this.enumCount = enumCount; // Store initial enum count
            this.dinamicEnum = dinamicEnum; // Store dynamic enum values
            this.Pressed = Pressed; // Store the Pressed delegate

            // Event handler for mouse press
            base.MousePressed = (object sender, MouseButtonEventArgs @event) => {
                enumCount++;
                if (enumCount > dinamicEnum.Length - 1) enumCount = 0; // Loop back to the first enum if it exceeds the array length
                Pressed?.Invoke(enumCount); // Invoke the Pressed delegate with the updated enum count
            };

        }
    }
}
