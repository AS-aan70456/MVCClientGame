using Client.Interfeices;
using Client.Views;
using Client.Models;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using Client.Models.Dungeons;
using CoreEngine.ReyCast;
using CoreEngine.System;
using CoreEngine.Entitys;

namespace Client.Controllers
{
    class GameController : IDrawController
    {
        //Button array to track pressed keys.
        bool[] Button;

        // The game view that displays the game.
        private GameView View;

        // Service for raycasting.
        private ReyCastService reyCast;

        // List of entities in the game.
        private List<Entity> entities;

        // The player entity.
        private Player player;

        // Generator for creating dungeons.
        private DungeonsGenerator generator;

        // The generated level.
        private Level level;

        // Mouse position relative to the window.
        private Vector2i MousePosition;

        // Method to activate the game controller.
        public void Activation(RenderWindow window)
        {
            // Hide the mouse cursor.
            window.SetMouseCursorVisible(false);

            // Generate a dungeon and entities.
            generator = new DungeonsGenerator(DateTime.Now.Second);
            level = generator.GenerateDungeon(new Vector2i(32, 48), 8, 8);
            entities = generator.GenerateEntity();

            // Create the player entity.
            player = new Player(level);

            // Create the raycast service.
            reyCast = new ReyCastService(level, Config.config.isTransparantTextures);

            // Create the game view.
            View = new GameView(window, reyCast, player, entities, level);

            // Initialize the Button array to track key presses.
            Button = new bool[6];

            // Set the initial mouse position and handle mouse events.
            Vector2i newMousePos = View.window.Position + (Vector2i)(View.window.Size / 2);
            Mouse.SetPosition(newMousePos);
            MousePosition = newMousePos - View.window.Position;

            // Add event handlers for key and mouse events.
            window.MouseMoved += new EventHandler<MouseMoveEventArgs>(MouseMoved);
            window.KeyPressed += new EventHandler<KeyEventArgs>(KeyPressed);
            window.KeyReleased += new EventHandler<KeyEventArgs>(KeyReleased);
        }

        // Handle key press events.
        private void KeyPressed(object sender, KeyEventArgs @event)
        {
            // Check which key was pressed and update the Button array accordingly.
            if (@event.Code == Keyboard.Key.W)
                Button[0] = true;
            else if (@event.Code == Keyboard.Key.S)
                Button[1] = true;
            else if (@event.Code == Keyboard.Key.A)
                Button[2] = true;
            else if (@event.Code == Keyboard.Key.D)
                Button[3] = true;
            else if (@event.Code == Keyboard.Key.Q)
                Button[4] = true;
            else if (@event.Code == Keyboard.Key.E)
            {
                Button[5] = true;
                // Perform an action when the E key is pressed.
                ReyContainer[] actionRey = reyCast.ReyCastWall(player, 1, 1, 1);
                Vector2f ReyPosition = actionRey[0].GetLastHit(new HitWall()).ReyPoint;

                if (level.Map[(int)ReyPosition.Y, (int)ReyPosition.X] == '4')
                    level.Map[
                        (int)actionRey[0].GetLastHit(new HitWall()).ReyPoint.Y,
                        (int)actionRey[0].GetLastHit(new HitWall()).ReyPoint.X
                    ] = '5';
            }
            // Handle other key events.

            // Handle the Escape key to return to the main menu.
            else if (@event.Code == Keyboard.Key.Escape)
            {
                View.window.SetMouseCursorVisible(true);
                Router.Init().graphicsControllers.SetController(new MenuController());
            }
        }

        // Handle key release events.
        private void KeyReleased(object sender, KeyEventArgs @event)
        {
            // Update the Button array when a key is released.
            if (@event.Code == Keyboard.Key.W)
                Button[0] = false;
            else if (@event.Code == Keyboard.Key.S)
                Button[1] = false;
            else if (@event.Code == Keyboard.Key.A)
                Button[2] = false;
            else if (@event.Code == Keyboard.Key.D)
                Button[3] = false;
            else if (@event.Code == Keyboard.Key.Q)
                Button[4] = false;
            else if (@event.Code == Keyboard.Key.E)
                Button[5] = false;
        }

        // Draw method to render the game view.
        public void Draw()
        {
            View.Render();
        }

        // Update method to handle player movement based on button presses.
        public void Updata()
        {
            if (Button[0] == true)
                player.Move(new Vector2f(0.1f, 0));
            if (Button[1] == true)
                player.Move(new Vector2f(-0.1f, 0));
            if (Button[2] == true)
                player.Move(new Vector2f(0, 0.04f));
            if (Button[3] == true)
                player.Move(new Vector2f(0, -0.04f));
        }

        // Handle mouse movement events.
        private void MouseMoved(object sender, MouseMoveEventArgs @event)
        {
            // Rotate the player entity based on mouse movement.
            player.Rotate((MousePosition.X - @event.X) * 0.03f);
            float offsetY = (MousePosition.Y - @event.Y) * 0.2f;
            if (offsetY > 0)
                if (player.angleY < 100)
                    player.RotateY(offsetY);
            if (offsetY <= 0)
                if (player.angleY > -100)
                    player.RotateY(offsetY);

            // Update the mouse position.
            MousePosition = new Vector2i(@event.X, @event.Y);

            // Keep the mouse within certain bounds to prevent abrupt movements.
            if (@event.X < 500 || @event.X > View.window.Size.X - 500)
            {
                Vector2i newMousePos = new Vector2i((int)(View.window.Size.X / 2), MousePosition.Y);
                Mouse.SetPosition(newMousePos, View.window);
                MousePosition.X = newMousePos.X;
            }

            if (@event.Y < 200 || @event.Y > View.window.Size.Y - 200)
            {
                Vector2i newMousePos = new Vector2i(MousePosition.X, (int)(View.window.Size.Y / 2));
                Mouse.SetPosition(newMousePos, View.window);
                MousePosition.Y = newMousePos.Y;
            }
        }

        // Method to deactivate the game controller and remove event handlers.
        public void DizActivation()
        {
            View.window.MouseMoved -= new EventHandler<MouseMoveEventArgs>(MouseMoved);
            View.window.KeyPressed -= new EventHandler<KeyEventArgs>(KeyPressed);
            View.window.KeyReleased -= new EventHandler<KeyEventArgs>(KeyReleased);
        }
    }
}
