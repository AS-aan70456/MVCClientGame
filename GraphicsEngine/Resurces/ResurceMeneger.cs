using GraphicsEngine.Resurces.DTO;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicsEngine.Resurces
{

    // Resource Manager class for handling resources
    public static class ResurceMeneger
    {

        static private List<IResurce> resurce; // List to store loaded resources

        static ResurceMeneger()
        {
            resurce = new List<IResurce>(); // Initialize the list of resources
        }

        // Load a texture
        static public Texture LoadTexture(string path)
        {
            TextureItem texture = (TextureItem)resurce.FirstOrDefault(e => e.path == path);

            if (texture != default)
                return (Texture)texture.resource;

            texture = new TextureItem
            {
                resource = new Texture(AppDomain.CurrentDomain.BaseDirectory + path),
                path = path
            };

            resurce.Add(texture);
            return (Texture)texture.resource;
        }

        // Load a font
        static public Font LoadFont(string path)
        {
            FontItem font = (FontItem)resurce.FirstOrDefault(e => e.path == path);

            if (font != default)
                return (Font)font.resource;

            font = new FontItem
            {
                resource = new Font(AppDomain.CurrentDomain.BaseDirectory + path),
                path = path
            };

            resurce.Add(font);
            return (Font)font.resource;
        }

        // Get a texture based on character identifier
        public static Texture GetTexture(char ch)
        {
            switch (ch)
            {
                case '0':
                    return LoadTexture(@"Resurces\Img\Walss\Wall.png");
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
                case '6':
                    return LoadTexture(@"Resurces\Img\Walss\Column.png");
                case '7':
                    return LoadTexture(@"Resurces\Img\Walss\touches.png");
                default:
                    return LoadTexture(@"Resurces\Img\Flore\Flore.jpg");
            }
        }
    }
}
