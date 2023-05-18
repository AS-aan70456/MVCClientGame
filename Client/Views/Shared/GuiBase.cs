using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Shared{
    abstract class GuiBase{
        private List<GuiBase> nods;

        protected Vector2f Position;
        protected Vector2f Size;

        public Vector2f size { get { return Size; } }
        public Vector2f position { get { return Position; } }

        public GuiBase() {
            nods = new List<GuiBase>();
        }

        public virtual void SetPosition(Vector2f Position) {
            this.Position = Position;
            foreach (var el in nods)
                el.SetPosition(this.Position + el.Position);
        }

        public virtual void SetSize(Vector2f Size){
            this.Size = Size;
        }

        protected abstract void Updata();
        protected abstract IEnumerable<Drawable> GiveAwayGraphicsPackages();
        protected abstract void MousePressed(object sender, MouseButtonEventArgs e);

        public IEnumerable<Drawable> GetGraphicsPackages(){
            List<Drawable> Packages = new List<Drawable>();
            Packages.AddRange(GiveAwayGraphicsPackages());
            foreach (var el in nods)
                Packages.AddRange(el.GetGraphicsPackages());
            return Packages;
        }

        public void EventMousePressed(object sender, MouseButtonEventArgs e){
            MousePressed(sender, e);
            for (int i = 0; i < nods.Count; i++)
                nods[i].EventMousePressed(sender, e);
        }

        public void addNode(GuiBase newNode){
            Console.WriteLine(newNode.GetHashCode());

            nods.Add(newNode);
            foreach (var el in nods) el.SetPosition(this.Position + el.Position);
        }

        public GuiBase GetNode(int Code){
            foreach (var el in nods)
                if (el.GetHashCode() == Code) return el;
            foreach (var el in nods)
                if (el.GetNode(Code) != null) return el.GetNode(Code);
            throw new Exception();
        }

        public void SetNode(int Code, GuiBase overNode){
            for (int i = 0; i < nods.Count; i++)
                if (nods[i].GetHashCode() == Code)
                    nods[i] = overNode;
            for (int i = 0; i < nods.Count; i++)
                nods[i].SetNode(Code, overNode);
        }

        public void removeNode(GuiBase newNode) => nods.Remove(newNode);
    }
}
