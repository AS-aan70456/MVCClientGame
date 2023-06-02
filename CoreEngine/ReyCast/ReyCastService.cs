using Client.Models;
using CoreEngine.Entitys;
using CoreEngine.System;
using SFML.System;
using System;

namespace CoreEngine.ReyCast{

    // service for calculating the length distance between the player and the walls
    public class ReyCastService {

        private Level level;
        private bool isTransparantTextures;

        public ReyCastService(Level level, bool isTransparantTextures) {
            this.level = level;
            this.isTransparantTextures = isTransparantTextures;
        }


        //the service interface executes the firing of beams according to the given instructions
        public Rey[] ReyCastWall(Entity entity, float fov, float depth, int CountRey) {
            Rey[] result = new Rey[CountRey];

            for (int i = 0; i < CountRey; i++) {
                float ReyAngle = (entity.angle + fov / 2 - i * fov / CountRey);
                Vector3f vector3F = entity.Position + (entity.Size / 2);
                ReySettings reySettings = new ReySettings(){
                    Position = new Vector2f(vector3F.X, vector3F.Y),
                    angle = ReyAngle,
                    originAngle = ReyAngle,
                    entityAngle = entity.angle,
                    depth = depth
                };

                result[i] = ReyPush(reySettings);
                //result[i].ReyDistance /= MathF.Cos(i/fov);
            }

            return result;
        }

        //internal function for emitting rays
        private Rey ReyPush(ReySettings settings) {

            Rey ReyVertical = new Rey();
            Rey ReyHorizontal = new Rey();

            if (MathF.Sin((settings.angle * MathF.PI) / 180) > 0) {
                if (MathF.Cos((settings.angle * MathF.PI) / 180) > 0) {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(new LeftRey()));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(new TopRey()));
                }
                else {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(new TopRey()));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(new RightRey()));
                }
            }
            else {
                if (MathF.Cos((settings.angle * MathF.PI) / 180) < 0) {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(new RightRey()));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(new DownRey()));
                }
                else {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(new DownRey()));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(new LeftRey()));
                }
            }

            ReyVertical = GetDistance(settings.Position, ReyVertical, ((settings.angle - settings.entityAngle) * MathF.PI) / 180);
            ReyHorizontal = GetDistance(settings.Position, ReyHorizontal, ((settings.angle - settings.entityAngle) * MathF.PI) / 180);


            //length check between vertical and horizontal reys
            if (ReyVertical.ReyDistance < ReyHorizontal.ReyDistance)
                return ReyVertical;
            else
                return ReyHorizontal;
        }

        //function iterates beam radiation
        private Rey ReyPushStrategy(ReySettings settings) {
            Rey result = new Rey();

            Vector2f ReyPos = settings.strategy.StartReyPos(settings.Position, settings.angle);

            for (int i = 0; i < settings.depth; i++) {

                if (ReyPos.X < 0 || ReyPos.Y < 0 || ReyPos.X > level.Size.Y - 1|| ReyPos.Y > level.Size.X - 1) {
                    result.ReyDistance = settings.depth;
                    result.ReyPoint = ReyPos;
                    return result;
                }

                try { result.Wall = level.Map[(int)ReyPos.Y, (int)ReyPos.X]; }
                catch (Exception) {
                    break;
                }

                if (!Level.IsVoid(result.Wall)) {

                    result.offset = settings.strategy.GetOfset(ReyPos);
                    result.ReyPoint = ReyPos;

                    if (Level.IsTransparent(result.Wall) && isTransparantTextures){
                        settings.Position = ReyPos;
                        settings.angle = settings.originAngle;
                        result.rey = ReyPush(settings);
                    }

                    return result;
                }
                ReyPos += settings.strategy.NextReyPos(settings.angle);
            }
            result.offset = settings.strategy.GetOfset(ReyPos);
            result.ReyPoint = ReyPos;

            return result;
        }

        //length check between vertical and horizontal beam reys
        private Rey GetDistance(Vector2f Position, Rey rey, float angle) {

            if (rey.rey != null) {
                rey.rey = GetDistance(Position, rey.rey, angle);
            }

            float a = (rey.ReyPoint.Y - Position.Y);
            float b = (rey.ReyPoint.X - Position.X);
            rey.ReyDistance = MathF.Abs(MathF.Sqrt((a * a) + (b * b))) * MathF.Cos(angle);
            return rey;
        }

    }

    class ReySettings {
        public IStrategyReyCanculate strategy { get; private set; }
        public Vector2f Position { get; set; }
        public float angle { get; set; }
        public float originAngle { get; set; }
        public float entityAngle { get; set; }
        public float depth { get; set; }

        public ReySettings() {


        }

        public ReySettings(ReySettings settings) {
            Position = settings.Position;
            angle = settings.angle;
            entityAngle = settings.entityAngle;
            originAngle = settings.originAngle;
            depth = settings.depth;
        }

        public ReySettings SetStrategy(IStrategyReyCanculate strategy) {
            this.strategy = strategy;
            if (strategy.GetType() == new LeftRey().GetType())
                angle = -(angle + 90);
            else if (strategy.GetType() == new RightRey().GetType())
                angle = -(angle - 90);
            else if (strategy.GetType() == new DownRey().GetType())
                angle -= 180;
            return this;
        }
    }

}