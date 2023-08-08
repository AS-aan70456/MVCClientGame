using GraphicsEngine.Resurces; // Import the ResurceMeneger namespace
using SFML.Graphics; // Import the SFML.Graphics namespace

namespace GraphicsEngine.Bilder
{
    public class TextBilder
    {
        Text text;

        public TextBilder(string str, string pathFont)
        {
            this.text = new Text(str, ResurceMeneger.LoadFont(pathFont)); // Load font from the resource manager
        }

        public TextBilder()
        {
            text = new Text();
        }

        public TextBilder DisplayedString(string str)
        {
            text.DisplayedString = str; // Set the displayed string
            return this;
        }

        public TextBilder CharacterSize(uint size)
        {
            text.CharacterSize = size; // Set character size
            return this;
        }

        public TextBilder FillColor(Color color)
        {
            text.FillColor = color; // Set fill color
            return this;
        }

        public TextBilder Font(string path)
        {
            text.Font = ResurceMeneger.LoadFont(path); // Load font from the resource manager
            return this;
        }

        public Text Init()
        {
            return text; // Return the built Text
        }
    }
}
