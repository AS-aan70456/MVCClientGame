using Client.Models;
using SFML.System;
using System;
using System.Collections.Generic;

namespace Services{
    class ReyCastService{

        private Maps map;

        public ReyCastService(Maps map){
            this.map = map;
        }

        public Rey[] ReyCastWall(Entity entity, float fov, float depth, int CountRey){
            Rey[] result = new Rey[CountRey];

            for (int i = 0; i < CountRey; i++){
                float ReyAngle = (entity.angle + fov / 2 - i * fov / CountRey);
                result[i] = ReyPush(entity.Position, ReyAngle, depth);
                //result[i].ReyDistance /= MathF.Cos(i/fov);
            }

            return result;
        }

        private Rey ReyPush( Vector2f Position, float angle, float depth){

            Rey ReyVertical = new Rey();
            Rey ReyHorizontal = new Rey();

            if (MathF.Sin((angle * MathF.PI) / 180) > 0) {
                if (MathF.Cos((angle * MathF.PI) / 180) > 0) {
                    ReyVertical = ReyPushStrategy(new LeftRey(), Position, -(angle + 90));
                    ReyHorizontal = ReyPushStrategy(new TopRey(), Position, angle);
                }
                else {
                    ReyVertical = ReyPushStrategy(new TopRey(), Position, angle);
                    ReyHorizontal = ReyPushStrategy(new RightRey(), Position, -(angle - 90));
                }
            }
            else {
                if (MathF.Cos((angle * MathF.PI) / 180) < 0){
                     ReyVertical = ReyPushStrategy(new RightRey(), Position, -(angle - 90));
                     ReyHorizontal = ReyPushStrategy(new DownRey(), Position, angle - 180);
                }
                else{
                    ReyVertical = ReyPushStrategy(new DownRey(), Position, angle - 180);
                    ReyHorizontal = ReyPushStrategy(new LeftRey(), Position, -(angle + 90));
                }
            }

            //return new Rey[] {
            //    ReyPushStrategy(new LeftRey(), Position,-angle ),
            //    ReyPushStrategy(new TopRey(), Position, angle)
            //};

            if (ReyVertical.ReyDistance < ReyHorizontal.ReyDistance)
                return ReyVertical;
            else
                return ReyHorizontal;
        }

        private Rey ReyPushStrategy(IStrategyReyCanculate strategy, Vector2f Position, float angle){
            Rey result = new Rey();

            Vector2f ReyPos = strategy.StartReyPos(Position, angle);

            for (int i = 0; i < 64; i++){

                if (ReyPos.X < 0 || ReyPos.Y < 0 || ReyPos.X >= map.Size.X || ReyPos.Y >= map.Size.Y){
                    result.ReyDistance = 1512;
                    result.ReyPoint = ReyPos;
                    return result;
                }

                result.Wall = map.Map[(int)ReyPos.Y * map.Size.X + (int)ReyPos.X]; 

                if (!map.IsVoid(result.Wall)){

                    result.offset = strategy.GetOfset(ReyPos);

                    float a = (ReyPos.Y - Position.Y);
                    float b = (ReyPos.X - Position.X);
                    result.ReyDistance = MathF.Abs(MathF.Sqrt((a * a) + (b * b)));

                    result.ReyPoint = ReyPos;

                    //if (map.IsTransparent(result.Wall)){
                    //    result.rey = ReyPush(new Vector2f(testX, testY), angle, depth);
                    //    result.rey.ReyDistance += result.ReyDistance;
                    //}
                    return result;
                }
                ReyPos += strategy.NextReyPos(angle);
            }
            return result;
        }

    }

    interface IStrategyReyCanculate {

        Vector2f StartReyPos(Vector2f Position, float angle);
        Vector2f NextReyPos(float angle);
        float GetOfset(Vector2f pos);
    }

    class LeftRey : IStrategyReyCanculate{

        public Vector2f StartReyPos(Vector2f Position, float angle){

            Vector2f deltePosition = new Vector2f(){
                X = (Position.X - (int)Position.X),
                Y = (Position.Y - (int)Position.Y)
            };

            Position.X += -deltePosition.X;
            Position.Y += ((-deltePosition.X) / MathF.Tan((angle * MathF.PI) / 180));

            Position -= new Vector2f(0.00001f, 0.00001f);

            return Position;
        }

        public Vector2f NextReyPos(float angle){
            return new Vector2f(
                -1f,
                -(1f / MathF.Tan((angle * MathF.PI) / 180))
            );
        }

        public float GetOfset(Vector2f pos){
            return (float)(pos.Y - (int)pos.Y);
        }
    }

    class TopRey : IStrategyReyCanculate{

        public Vector2f StartReyPos(Vector2f Position, float angle){
            Vector2f deltePosition = new Vector2f(){
                X = (Position.X - (int)Position.X),
                Y = (Position.Y - (int)Position.Y),
            };

            Position.X += ((-deltePosition.Y) / MathF.Tan((angle * MathF.PI) / 180));
            Position.Y += -deltePosition.Y;

            Position -= new Vector2f(0.00001f, 0.00001f);

            return Position;
        }

        public Vector2f NextReyPos(float angle){
            return new Vector2f(
                -(1f / MathF.Tan((angle * MathF.PI) / 180)),
                -1f
            );
        }

        public float GetOfset(Vector2f pos)
        {
            return (float)(pos.X - (int)pos.X);
        }
    }

    class RightRey : IStrategyReyCanculate{

        public Vector2f StartReyPos(Vector2f Position, float angle){
            Vector2f deltePosition = new Vector2f(){
                X = 1 - (Position.X - (int)Position.X),
                Y = 1 - (Position.Y - (int)Position.Y),
            };

            Position.X -= (-deltePosition.X);
            Position.Y -= (((-deltePosition.X)) / MathF.Tan((angle * MathF.PI) / 180));

            return Position;
        }

        public Vector2f NextReyPos(float angle){
            return new Vector2f(
                1f,
                (1f / MathF.Tan((angle * MathF.PI) / 180))
            );
        }

        public float GetOfset(Vector2f pos){
            return (float)(pos.Y - (int)pos.Y);
        }
    }

    class DownRey : IStrategyReyCanculate{

        public Vector2f StartReyPos(Vector2f Position, float angle){
            Vector2f deltePosition = new Vector2f(){
                X = 1 - (Position.X - (int)Position.X),
                Y = 1 - (Position.Y - (int)Position.Y),
            };

            Position.X -= (((-deltePosition.Y)) / MathF.Tan((angle * MathF.PI) / 180));
            Position.Y -= (-deltePosition.Y);

            return Position;
        }

        public Vector2f NextReyPos(float angle){
            return new Vector2f(
                (1f / MathF.Tan((angle * MathF.PI) / 180)),
                1f
            );
        }

        public float GetOfset(Vector2f pos){
            return (float)(pos.X - (int)pos.X);
        }
    }

}