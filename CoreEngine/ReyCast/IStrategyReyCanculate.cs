using SFML.System;

namespace CoreEngine.ReyCast{
    interface IStrategyReyCanculate{

        Vector2f StartReyPos(Vector2f Position, float angle);
        Vector2f NextReyPos(float angle);
        float GetOfset(Vector2f pos);
        Vector2f GetSide();
    }
}
