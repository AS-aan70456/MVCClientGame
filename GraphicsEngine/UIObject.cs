using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEngine{
    public class UIObject{

        public Action<object, MouseButtonEventArgs> MousePressed;
        public Action<object, MouseButtonEventArgs> MouseRealessed;

        public Action<object, MouseMoveEventArgs> MouseMoved;
        public Action Updata;

        private List<Transformable> Drawables;
        private List<UIObject> nods;

        public Vector2f AbsolutPosition { get; private set; }

        public Vector2f Position { get; private set; }
        public Vector2f Size { get; private set; }

        public UIObject() {
            nods = new List<UIObject>();
            Drawables = new List<Transformable>();
        }

        public UIObject(Vector2f Position, Vector2f Size){
            nods = new List<UIObject>();
            Drawables = new List<Transformable>();

            SetPosition(Position);
            SetSize(Size);
        }

        private bool CheckPressed(Vector2i MousePos){
            if ((MousePos.X > AbsolutPosition.X && MousePos.Y > AbsolutPosition.Y) && (MousePos.X < AbsolutPosition.X + Size.X && MousePos.Y < AbsolutPosition.Y + Size.Y))
                return true;
            
            return false;
        }

        public void SetPosition(Vector2f Position){
            this.Position = Position;
            this.AbsolutPosition = Position;

            SetPositionUI();

            foreach (var el in nods)
                el.SetPositionAbsolute(Position);
        }

        protected void SetPositionAbsolute(Vector2f Position){
            this.AbsolutPosition = Position += this.Position;
            SetPositionUI();
            foreach (var el in nods)
                el.SetPositionAbsolute(AbsolutPosition);
        }

        public void SetSize(Vector2f Size){
            this.Size = Size;
            foreach(var el in Drawables)
                if (el.GetType() == new RectangleShape().GetType())
                    ((RectangleShape)el).Size = Size;
            SetPositionUI();
        }

        public void SetPositionUI() {
            foreach (var el in Drawables){
                if (el.GetType() == new Text().GetType()){
                    el.Position = new Vector2f((Size / 2).X - ((Text)el).GetGlobalBounds().Width / 2, (Size / 2).Y - ((Text)el).GetGlobalBounds().Height * 1.5f) + AbsolutPosition;
                }
                if (el.GetType() == new RectangleShape().GetType()){
                    el.Position = AbsolutPosition;
                }
            }
        }

        public virtual IEnumerable<Transformable> GiveAweyGraphicsPackages(){ return new List<Transformable>(); }

        public IEnumerable<Transformable> GetGraphicsPackages(){
            List<Transformable> Packages = new List<Transformable>();
            Packages.AddRange(Drawables);
            Packages.AddRange(GiveAweyGraphicsPackages());
            foreach (var el in nods)
                Packages.AddRange(el.GetGraphicsPackages());
            return Packages;
        }

        public void EventMousePressed(object sender, MouseButtonEventArgs e){
            if (CheckPressed(new Vector2i(e.X,e.Y))) 
                MousePressed?.Invoke(sender, e);
            for (int i = 0; i < nods.Count; i++)
                nods[i].EventMousePressed(sender, e);
        }

        public void EventMouseReleassed(object sender, MouseButtonEventArgs e){
            MouseRealessed?.Invoke(sender, e);
            for (int i = 0; i < nods.Count; i++)
                nods[i].EventMouseReleassed(sender, e);
        }

        public void EventMouseMoved(object sender, MouseMoveEventArgs e){
            MouseMoved?.Invoke(sender, e);
            for (int i = 0; i < nods.Count; i++)
                nods[i].EventMouseMoved(sender, e);
        }

        public void EventUpdata(){
            Updata?.Invoke();
            for (int i = 0; i < nods.Count; i++)
                nods[i].EventUpdata();
        }

        public UIObject GetNode(int Code){
            foreach (var el in nods)
                if (el.GetHashCode() == Code) return el;
            foreach (var el in nods)
                if (el.GetNode(Code) != null) return el.GetNode(Code);
            throw new Exception();
        }

        public void SetNode(int Code, UIObject overNode){
            for (int i = 0; i < nods.Count; i++)
                if (nods[i].GetHashCode() == Code)
                    nods[i] = overNode;
            for (int i = 0; i < nods.Count; i++)
                nods[i].SetNode(Code, overNode);
        }

        public void AddDrawble(Transformable Drawble){
            Drawables.Add(Drawble);
            foreach (var el in Drawables)
                if (el.GetType() == new RectangleShape().GetType())
                    ((RectangleShape)el).Size = Size;
            SetPositionUI();
        }

        public void addNode(UIObject newNode){
            nods.Add(newNode);
        }

        public void removeNode(UIObject newNode) => nods.Remove(newNode);
    }
}
