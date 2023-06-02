using GraphicsEngine.Resurces;
using SFML.Graphics;

namespace GraphicsEngine.Bilder{
    public class TextBilder{
        Text text;

        public TextBilder(string str, string pathFont){
            this.text = new Text(str, ResurceMeneger.LoadFont(pathFont));
        }

        public TextBilder() {
            text = new Text();
        }

        public TextBilder DisplayedString(string str){
            text.DisplayedString = str;
            return this;
        }

        public TextBilder CharacterSize(uint size){
            text.CharacterSize = size;
            return this;
        }

        public TextBilder FillColor(Color color){
            text.FillColor = color;
            return this;
        }

        public TextBilder Font(string path){
            text.Font = ResurceMeneger.LoadFont(path);
            return this;
        }

        public Text Init() {
            return text;
        }
    }
}
