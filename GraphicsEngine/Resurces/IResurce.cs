using SFML; // Include the appropriate namespace for SFML

namespace GraphicsEngine.Resurces
{
    // Interface for resource items
    interface IResurce
    {
        ObjectBase resource { get; set; } // Actual resource object
        string path { get; set; } // Path to the resource
    }
}