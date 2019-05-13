using Godgame.Model.API;
using System;
using System.Collections.Generic;

namespace Godgame.Model.Tiles
{
    class Water : Tile
    {
        public Water(Coordinate coordinate, World world) : base(coordinate, world) { }
        static Water()
        {
            var list = new List<Type>();
            acceptableStructures[typeof(Water)] = list;
        }
        public override string Path => "water.png";
    }
}
