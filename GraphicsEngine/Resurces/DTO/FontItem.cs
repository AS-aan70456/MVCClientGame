using SFML;
using SFML.Graphics;

namespace GraphicsEngine.Resurces.DTO{
    class FontItem : IResurce{
        private Font font;
        public string path { get; set; }

        public ObjectBase resource { get { return font; } set { font = (Font)value; } }
    }
}
