using GraphicsEngine.Resurces;
using SFML.Graphics;


namespace GraphicsEngine.Bilder{
    public class RectBilder{

        RectangleShape Rectangle;

        public RectBilder(string path){
            Rectangle = new RectangleShape();
            Rectangle.Texture = ResurceMeneger.LoadTexture(path);
        }

        public RectBilder() {
            Rectangle = new RectangleShape();
        }

        public RectBilder LoadTexture(string path){
            Rectangle.Texture = ResurceMeneger.LoadTexture(path);
            return this;
        }

        public RectBilder FillColor(Color color){
            Rectangle.FillColor = color;
            return this;
        }

        public RectangleShape Init() {
            return Rectangle;
        }

    }
}
