using SFML.Graphics;

namespace Client.Interfeices{
    public interface IRender{

        //access to events
        public RenderWindow window { get; }

        public void Render();

    }
}
