using SFML.System;

namespace CoreEngine.ReyCast{
    public struct HitWall : Hit{

        public HitWall(Vector2f Point) {
            ReyPoint = Point;
            ReyDistance = 0;
            offset = 0;
            Wall = '0';
        }

        public Vector2f ReyPoint { get; set; }
        public float ReyDistance { get; set; }
        public char Wall { get; set; }

        public float offset { get; set; }
    }
}
