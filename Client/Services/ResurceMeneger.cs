using SFML;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services{

    //makes sure that only one texture is loaded into applications
    static class ResurceMeneger{

        static private List<IResurce> resurce;

        static ResurceMeneger() {
            resurce = new List<IResurce>();
        }

        static public Texture LoadTexture(string path) {
            TextureItem texture = (TextureItem)resurce.FirstOrDefault(e => e.path == path);

            if (texture != default)
                return (Texture)texture.resurce;

            texture = new TextureItem{
                resurce = new Texture(AppDomain.CurrentDomain.BaseDirectory + path),
                path = path
            };

            resurce.Add(texture);
            return (Texture)texture.resurce;
        }

        static public Font LoadFont(string path){
            FontItem font = (FontItem)resurce.FirstOrDefault(e => e.path == path);

            if (font != default)
                return (Font)font.resurce;

            font = new FontItem{
                resurce = new Font(AppDomain.CurrentDomain.BaseDirectory + path),
                path = path
            };

            resurce.Add(font);
            return (Font)font.resurce;
        }
    }

    interface IResurce{
        public ObjectBase resurce { get; set; }
        public string path { get; set; }
    } 

    class TextureItem : IResurce{
        private Texture texture;
        public string path { get; set; }

        public ObjectBase resurce { get { return texture; } set { texture = (Texture)value;}}
    }

    class FontItem : IResurce{
        private Font font;
        public string path { get; set; }

        public ObjectBase resurce { get { return font; } set { font = (Font)value; } }
    }
}
