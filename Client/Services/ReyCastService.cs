using Client.Models;
using SFML.System;
using System;
using System.Collections.Generic;

namespace Services{
    class ReyCastService {

        private Maps map;

        public float[] ReyDistance { get; private set; }
        public Vector2f[] ReyPoint { get; private set; }

        public ReyCastService(Maps map) {
            this.map = map;


        }

        public void ReyCast(Entity entity) {

            ReyDistance = new float[100];
            ReyPoint = new Vector2f[100];

            float ReySpead = 0.2f;
            float fov = entity.fov;

            float depth = 100;

            for (int i = 0; i < depth; i++) {

                float ReyAngle = entity.angle + fov / 2 - i * fov / depth;

                float ReyX = (float)Math.Cos(((ReyAngle * Math.PI) / 180));
                float ReyY = (float)Math.Sin(((ReyAngle * Math.PI) / 180));

                float distance = 0;

                while (true) { 

                    distance += ReySpead;

                    float testX = (entity.Position.X + ReyX * distance);
                    float testY = (entity.Position.Y + ReyY * distance);

                    char Cell = map.GetMap()[(int)testY * map.Size.X + (int)testX];

                    if (testX < 0 || testX >= depth + entity.Position.X || testY < 0 || testY >= depth + entity.Position.Y) {
                        ReyDistance[i] = depth;
                        break;
                    }
                    else {
                        if (Cell == '#'){
                            ReyDistance[i] = distance;
                            ReyPoint[i] = new Vector2f(testX, testY);
                            break;
                        }
                    }
                }
            }
        }

        
    }
}
