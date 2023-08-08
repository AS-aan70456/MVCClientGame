using SFML.System;

namespace CoreEngine.ReyCast
{
    interface IStrategyReyCanculate
    {

        // Determine the starting position of a ray based on the player's position and angle.
        Vector2f StartReyPos(Vector2f Position, float angle);

        // Calculate the next position of the ray based on the current angle.
        Vector2f NextReyPos(float angle);

        // Compute an offset value for the ray based on the hit position.
        float GetOfset(Vector2f pos);

        // Return a vector indicating the side the ray is pointing towards.
        Vector2f GetSide();
    }
}
