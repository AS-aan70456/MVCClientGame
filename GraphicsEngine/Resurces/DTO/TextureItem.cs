﻿using SFML;
using SFML.Graphics;

namespace GraphicsEngine.Resurces.DTO{
    class TextureItem : IResurce{
        private Texture texture;
        public string path { get; set; }

        public ObjectBase resource { get { return texture; } set { texture = (Texture)value; } }
    }
}
