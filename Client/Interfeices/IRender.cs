using SFML.Graphics;

namespace Client.Interfeices{
    public interface IRender{

        public RenderWindow window { get; }

        public void Render();

    }
}
