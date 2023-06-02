using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace GraphicsEngine.Shared{
    public class ListBox : UIObject{

        private int enumCount;
        private Action<int> Pressed;
        public string[] dinamicEnum { get; }

        public ListBox(string[] dinamicEnum,int enumCount, Action<int> Pressed) {

            this.enumCount = enumCount;
            this.dinamicEnum = dinamicEnum;
            this.Pressed = Pressed;
            base.MousePressed = (object sender, MouseButtonEventArgs @event) =>{
                enumCount++;
                if (enumCount > dinamicEnum.Length - 1) enumCount = 0;
                Pressed?.Invoke(enumCount);
            };

        }

       
    }
}
