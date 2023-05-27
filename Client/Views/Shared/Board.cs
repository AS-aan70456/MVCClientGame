using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace Client.Views.Shared{
    class Board : GuiBase{

        private RectangleShape rectangle;
        public Text text;

        public Board(){
            rectangle = new RectangleShape();
            text = new Text();
        }

        public void LoadTexture(Texture texture){
            rectangle.Texture = texture;
        }

        public void LoadText(Text text){
            this.text = text;
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

        protected override IEnumerable<Drawable> GiveAwayGraphicsPackages(){
            List<Drawable> drawables = new List<Drawable>();
            drawables.Add(rectangle);
            drawables.Add(text);
            return drawables;
        }

        protected override void MousePressed(object sender, MouseButtonEventArgs @event){}
        protected override void MouseReleassed(object sender, MouseButtonEventArgs @event){}
        protected override void MouseMoved(object sender, MouseMoveEventArgs e) { }
        protected override void Updata(){ text.DisplayedString = ((int)(Router.Init().graphicsControllers.fps)).ToString(); }
    }
}
