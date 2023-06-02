using SFML.System;

namespace CoreEngine.System{
    public class Level {

        public Vector2i SpawnPoint { get; private set; }

        public Vector2i Size { get; private set; }
        public char[,] Map { get; set; }
        private static char[] Void;
        private static char[] Transparent;
        private static char[] Collision;

        public char this[int i,int j] { get { return Map[i, j]; } }

        public Level(char[,] Map, Vector2i Size, Vector2i SpawnPoint) {

            this.Map = Map;
            this.Size = Size;
            this.SpawnPoint = SpawnPoint;

            Void = new char[] { ' ' };
            Transparent = new char[] { '3', '5' };
            Collision = new char[] { '1', '2', '3', '4' };
        }

        public static bool IsVoid(char Cell) {
            for (int i = 0; i < Void.Length; i++) if (Cell == Void[i]) return true; return false;
        }

        public static bool IsTransparent(char Cell){
            for (int i = 0; i < Transparent.Length; i++) if (Cell == Transparent[i]) return true; return false;
        }

        public static bool IsCollision(char Cell){
            for (int i = 0; i < Collision.Length; i++) if (Cell == Collision[i]) return true; return false;
        }
    }
}
