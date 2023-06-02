using GraphicsEngine.Resurces.DTO;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicsEngine.Resurces{

    //makes sure that only one texture is loaded into applications
    public static class ResurceMeneger{

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

        public static Texture GetTexture(char ch){
            switch (ch){
                case '1':
                    return LoadTexture(@"Resurces\Img\Walss\Wall2.png");
                case '2':
                    return LoadTexture(@"Resurces\Img\Walss\Wall.png");
                case '3':
                    return LoadTexture(@"Resurces\Img\Walss\Window.png");
                case '4':
                    return LoadTexture(@"Resurces\Img\Walss\Door.png");
                case '5':
                    return LoadTexture(@"Resurces\Img\Walss\DoorOpen.png");
                default:
                    return LoadTexture(@"Resurces\Img\Walss\Flore.jpg");
            }
        }
    }
}
