using GraphicsEngine.Resurces; // Import the ResurceMeneger namespace
using SFML.Graphics; // Import the SFML.Graphics namespace

namespace GraphicsEngine.Bilder
{
    public class RectBilder
    {

        RectangleShape Rectangle;

        public RectBilder(string path)
        {
            Rectangle = new RectangleShape();
            Rectangle.Texture = ResurceMeneger.LoadTexture(path); // Load texture from the resource manager
        }

        public RectBilder()
        {
            Rectangle = new RectangleShape();
        }

        public RectBilder LoadTexture(string path)
        {
            Rectangle.Texture = ResurceMeneger.LoadTexture(path); // Load texture from the resource manager
            return this;
        }

        public RectBilder FillColor(Color color)
        {
            Rectangle.FillColor = color; // Set fill color
            return this;
        }

        public RectangleShape Init()
        {
            return Rectangle; // Return the built RectangleShape
        }
    }
}
