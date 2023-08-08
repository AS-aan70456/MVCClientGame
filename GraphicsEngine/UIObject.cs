using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEngine
{
    public class UIObject
    {

        // Events for mouse interactions and updates
        public Action<object, MouseButtonEventArgs> MousePressed;
        public Action<object, MouseButtonEventArgs> MouseRealessed;
        public Action<object, MouseMoveEventArgs> MouseMoved;
        public Action Updata;
        public Action Start;

        // Lists to store drawable elements and child UI nodes
        private List<Transformable> Drawables;
        private List<UIObject> nods;

        // Properties for position and size
        public Vector2f AbsolutPosition { get; private set; }
        public Vector2f Position { get; private set; }
        public Vector2f Size { get; private set; }

        // Constructor without parameters
        public UIObject()
        {
            nods = new List<UIObject>();
            Drawables = new List<Transformable>();
        }

        // Constructor with position and size parameters
        public UIObject(Vector2f Position, Vector2f Size)
        {
            nods = new List<UIObject>();
            Drawables = new List<Transformable>();

            SetPosition(Position);
            SetSize(Size);
        }

        // Check if the UI object is pressed by the mouse
        public bool CheckPressed(Vector2i MousePos)
        {
            if ((MousePos.X > AbsolutPosition.X && MousePos.Y > AbsolutPosition.Y) &&
                (MousePos.X < AbsolutPosition.X + Size.X && MousePos.Y < AbsolutPosition.Y + Size.Y))
                return true;
            return false;
        }

        // Set the position of the UI object
        public void SetPosition(Vector2f Position)
        {
            this.Position = Position;
            this.AbsolutPosition = Position;
            SetPositionUI();
            foreach (var el in nods)
                el.SetPositionAbsolute(Position);
        }

        // Set the absolute position of the UI object
        protected void SetPositionAbsolute(Vector2f Position)
        {
            this.AbsolutPosition = Position + this.Position;
            SetPositionUI();
            foreach (var el in nods)
                el.SetPositionAbsolute(AbsolutPosition);
        }

        // Set the size of the UI object
        public void SetSize(Vector2f Size)
        {
            this.Size = Size;
            foreach (var el in Drawables)
                if (el.GetType() == new RectangleShape().GetType())
                    ((RectangleShape)el).Size = Size;
            SetPositionUI();
        }

        // Set the position of UI elements based on their type
        public void SetPositionUI()
        {
            foreach (var el in Drawables)
            {
                if (el.GetType() == new Text().GetType())
                {
                    el.Position = new Vector2f((Size / 2).X - ((Text)el).GetGlobalBounds().Width / 2,
                        (Size / 2).Y - ((Text)el).GetGlobalBounds().Height * 1.5f) + AbsolutPosition;
                }
                if (el.GetType() == new RectangleShape().GetType())
                {
                    el.Position = AbsolutPosition;
                }
            }
        }

        // Get all graphics packages associated with the UI object and its child nodes
        public IEnumerable<Transformable> GetGraphicsPackages()
        {
            List<Transformable> Packages = new List<Transformable>();
            Packages.AddRange(Drawables);
            foreach (var el in nods)
                Packages.AddRange(el.GetGraphicsPackages());
            return Packages;
        }

        // Event handlers for mouse interactions and updates
        public void EventMousePressed(object sender, MouseButtonEventArgs e)
        {
            if (CheckPressed(new Vector2i(e.X, e.Y)))
                MousePressed?.Invoke(sender, e);
            for (int i = 0; i < nods.Count; i++)
                nods[i].EventMousePressed(sender, e);
        }

        public void EventMouseReleassed(object sender, MouseButtonEventArgs e)
        {
            MouseRealessed?.Invoke(sender, e);
            for (int i = 0; i < nods.Count; i++)
                nods[i].EventMouseReleassed(sender, e);
        }

        public void EventMouseMoved(object sender, MouseMoveEventArgs e)
        {
            MouseMoved?.Invoke(sender, e);
            for (int i = 0; i < nods.Count; i++)
                nods[i].EventMouseMoved(sender, e);
        }

        public void EventUpdata()
        {
            Updata?.Invoke();
            for (int i = 0; i < nods.Count; i++)
                nods[i].EventUpdata();
        }

        // Retrieve a child UI node based on a code
        public UIObject GetNode(int Code)
        {
            foreach (var el in nods)
                if (el.GetHashCode() == Code) return el;
            foreach (var el in nods)
                if (el.GetNode(Code) != null) return el.GetNode(Code);
            throw new Exception();
        }

        // Set a child UI node based on a code
        public void SetNode(int Code, UIObject overNode)
        {
            for (int i = 0; i < nods.Count; i++)
                if (nods[i].GetHashCode() == Code)
                    nods[i] = overNode;
            for (int i = 0; i < nods.Count; i++)
                nods[i].SetNode(Code, overNode);
        }

        // Add a drawable element to the UI object
        public void AddDrawble(Transformable Drawble)
        {
            Drawables.Add(Drawble);
            foreach (var el in Drawables)
                if (el.GetType() == new RectangleShape().GetType())
                    ((RectangleShape)el).Size = Size;
            SetPositionUI();
        }

        // Add a child UI node to the UI object
        public void addNode(UIObject newNode)
        {
            newNode.SetPositionAbsolute(AbsolutPosition);
            nods.Add(newNode);
            newNode.Start?.Invoke();
        }

        // Remove a child UI node from the UI object
        public void removeNode(UIObject newNode) => nods.Remove(newNode);
    }
}
