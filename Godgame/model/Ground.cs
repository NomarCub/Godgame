﻿using Godgame.Model.API;
using System;
using System.Collections.Generic;

namespace Godgame.Model
{
    class Ground : Tile
    {
        public Ground(Coordinate coordinate, World world) : base(coordinate, world) { }

        static Ground()
        {
            var list = new List<Type>();
            acceptableStructures[typeof(Ground)] = list;
            list.Add(typeof(Tree));
            list.Add(typeof(Chest));
        }
        public override string Path => "grass.png";
    }
}