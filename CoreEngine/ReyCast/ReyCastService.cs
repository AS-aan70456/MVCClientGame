using CoreEngine.Entitys;
using CoreEngine.System;
using SFML.System;
using System;

namespace CoreEngine.ReyCast{

    // service for calculating the length distance between the player and the walls
    public class ReyCastService {


        private TopRey topReyStrategic;
        private DownRey downReyStrategic;
        private LeftRey leftReyStrategic;
        private RightRey rightReyStrategic;


        private Level level;
        private bool isTransparantTextures;

        public ReyCastService(Level level, bool isTransparantTextures) {
            this.level = level;
            this.isTransparantTextures = isTransparantTextures;

            topReyStrategic = new TopRey();
            downReyStrategic = new DownRey();
            leftReyStrategic = new LeftRey();
            rightReyStrategic = new RightRey();
        }


        //the service interface executes the firing of beams according to the given instructions
        public ReyContainer[] ReyCastWall(Entity entity, float fov, float depth, int CountRey) {
            Rey[] result = new Rey[CountRey];

            for (int i = 0; i < CountRey; i++) {
                float ReyAngle = (entity.angle + fov / 2 - i * fov / CountRey);

                ReySettings reySettings = new ReySettings(){
                    Position = entity.Position + (entity.Size / 2),
                    angle = ReyAngle,
                    originAngle = ReyAngle,
                    entityAngle = entity.angle,
                    depth = depth
                };

                result[i] = ReyPush(reySettings);
            }

            return result;
        }

        //internal function for emitting rays
        private Rey ReyPush(ReySettings settings) {

            Rey ReyVertical = new Rey();
            Rey ReyHorizontal = new Rey();

            if (MathF.Sin((settings.angle * MathF.PI) / 180) > 0) {
                if (MathF.Cos((settings.angle * MathF.PI) / 180) > 0) {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(leftReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(topReyStrategic));
                }
                else {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(topReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(rightReyStrategic));
                }
            }
            else {
                if (MathF.Cos((settings.angle * MathF.PI) / 180) < 0) {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(rightReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(downReyStrategic));
                }
                else {
                    ReyVertical = ReyPushStrategy(new ReySettings(settings).SetStrategy(downReyStrategic));
                    ReyHorizontal = ReyPushStrategy(new ReySettings(settings).SetStrategy(leftReyStrategic));
                }
            }

            //ReyVertical = GetDistance(settings.Position, ReyVertical, ((settings.angle - settings.entityAngle) * MathF.PI) / 180);
            //ReyHorizontal = GetDistance(settings.Position, ReyHorizontal, ((settings.angle - settings.entityAngle) * MathF.PI) / 180);


            //length check between vertical and horizontal reys
            return Rey.HitDistribution(ReyHorizontal, ReyVertical, settings);
        }

        //function iterates beam radiation
        private Rey ReyPushStrategy(ReySettings settings) {
            Rey result = new Rey();

            Vector2f reyPos = settings.strategy.StartReyPos(settings.Position, settings.angle);
            char reyWall = ' ';

            for (int i = 0; i < settings.depth; i++) {

                if (reyPos.X < 0 || reyPos.Y < 0 || reyPos.X > level.Size.Y - 1|| reyPos.Y > level.Size.X - 1) {
                    result.Hit(new HitWall() {
                        ReyDistance = GetDistance(reyPos, settings.Position),
                        ReyPoint = reyPos,
                        Wall = '0'
                    });
                    return result;
                }

                reyWall = level.Map[(int)reyPos.Y, (int)reyPos.X]; 
                

                if (!Level.IsVoid(reyWall)) {

                    HitWall hit = new HitWall(reyPos);

                    if (Level.Ishalf(reyWall))
                        hit.ReyPoint += settings.strategy.NextReyPos(settings.angle) / 2;
                    
                    hit.ReyDistance = GetDistance(hit.ReyPoint, settings.Position);
                    hit.offset = settings.strategy.GetOfset(hit.ReyPoint);
                    hit.Wall = reyWall;

                    result.Hit(hit);

                    if (!Level.IsTransparent(reyWall) || !isTransparantTextures)
                        return result;

                }

                result.Hit(new HitFlore(){
                    ReyDistance = GetDistance(reyPos, settings.Position),
                    ReyPoint = reyPos,
                    Wall = reyWall,
                });

                reyPos += settings.strategy.NextReyPos(settings.angle);

            }

            result.Hit(new HitWall(){
                ReyDistance = GetDistance(
                    new Vector3f(reyPos.X, reyPos.Y, 0),
                    new Vector3f(settings.Position.X, settings.Position.Y, 0.5f)
                ),
                ReyPoint = reyPos,
                Wall = '0'
            });

            return result;
        }

        //length check between vertical and horizontal beam reys

        private float GetDistance(Vector2f PosA, Vector2f PosB) {
            return MathF.Abs(MathF.Sqrt(MathF.Pow(PosA.X - PosB.X, 2) + MathF.Pow(PosA.Y - PosB.Y, 2)));
        }

        private float GetDistance(Vector3f PosA, Vector3f PosB){
            return MathF.Abs(MathF.Sqrt(MathF.Pow(PosA.X - PosB.X, 2) + MathF.Pow(PosA.Y - PosB.Y, 2) + MathF.Pow(PosA.Z - PosB.Z, 2)));
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