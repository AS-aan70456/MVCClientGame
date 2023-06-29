
# MVCClientServerGame

This is a simple game based on raycasting algorithms, the main feature of which is the MVC structure of the project, which gives a greater possibility to expand the project

![Сни1мок](https://github.com/AS-aan70456/MVCClientServerGame/assets/107953303/0ba25dc7-66f3-402c-9221-a3a6a7b00392)

---

### Algorithm

 The basis of the raycasting algorithm is to throw rays at the player's angle plus the rotation of the viewing angle, and to measure the distance when hitting a wall, according to this algorithm, the closer the wall is, the bigger it is.

- https://habr.com/ru/articles/515256/ - link to simplified algorithm.



the game implements an optimal beam shooting algorithm, based on throwing two beams in the same direction, but with an optimal interval that gives maximum performance.
formula:

```sh
  Position.X += -deltePosition.X;
  Position.Y += ((-deltePosition.X) / MathF.Tan((angle * MathF.PI) / 180));
```
- https://lodev.org/cgtutor/raycasting.html - link to optimal algorithm.

entity rendering is also implemented.

- https://lodev.org/cgtutor/raycasting3.html - link to tried to draw entities.

---

### Project Structure

The project is divided into four parts of the project:

- Client
- CoreEngine
- GraphicsEngine
- Server

#### Client
<dl>
  
  <dd>Client - all the content of the game is written here,
all the initialization of the systems is here,
and the game itself is launched. lso here is the block of creation and generation of the dungeon, the features here are the business logic and play settings. </dd>

  <dd>Generation algorithm:
the basic principle of the algorithm for breaking the game field into chunks, in the skin from the cis chaniks, the kimnatka is generated according to an approximate proportion, then the massif of the usima centrama kimnat is transferred and the graph is built, corridors are built according to this graph, near empty spaces.
The main feature is the proportion itself, give the zest to the algorithm, destroy the diversity of crossroads and stikof rooms.
<p></p>
  <p>int minRoom = (int)(chankSize * 1.1f);</p>
  <p>int maxRoom = (int)(chankSize * 1.6f);</p>
<p></p>

as you can see, even the minimum size of the room is larger than the chunk, and the maximum is one and a half times larger, this gives the possibility of the rooms touching, but in a non-critical form.

  </dd>

</dl>

#### CoreEngine

<dl>
  <dd>
    Еhe entire algorithm of the raycasting engine is implemented here, plus the game logic is located here, and features and interaction with the map are prescribed
  </dd>
</dl>

#### GraphicsEngine

<dl>
  <dd>  
In this block, the UI part of the game is implemented, also here they produce movements for the height of the walls. the main mechanics of Ui are nodes, each object is attached to another, nodes can be easily added, which makes the UI system expandable, it is also necessary to consider that each node has both a global position - a position on the screens, and a relative position - a position relative to the horizontal node, this allows make responsive UI.
  </dd>
</dl>


#### Server

<dl>
  <dd>
    the server part is not yet implemented.
  </dd>
</dl>

---

### Implemented functions

- MVC structure
- adaptive UI
- Canvas system
- Optimal reicast
- Dungeon generation
- Drawing entities
- menu pages
- setting pagers
<p></p>

---

### Function scheduling

- Multipla
- Extended scripting capabilities
- Map editor 
