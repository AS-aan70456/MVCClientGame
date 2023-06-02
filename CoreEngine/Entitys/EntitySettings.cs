using CoreEngine.System;
using SFML.System;

namespace CoreEngine.Entitys{
    public class EntitySettings
    {
        public Level Level { get; set; }

        public Vector3f Position { get; set; }
        public Vector3f Size { get; set; }
        public float angle { get; set; }
    }
}
