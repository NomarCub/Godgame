using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Godgame.Model.API
{
    public abstract class Tile : IDrawable, INotifyPropertyChanged
    {
        //List<Structure> structures = new List<Structure>();
        //List<Actor> actors = new List<Actor>();
        protected static IDictionary<Type, IList<Type>> acceptableStructures = new Dictionary<Type, IList<Type>>();

        private Structure _Structure = null;
        public Structure Structure
        {
            get => _Structure;
            set
            {
                if (value != _Structure)
                {
                    if (value == null)
                    {
                        _Structure = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Structure)));
                    }
                    else if (acceptableStructures[this.GetType()].Contains(value.GetType()))
                    {
                        _Structure = value;
                        _Structure.Tile = this;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Structure)));
                    }
                }
            }
        }

        private Actor _Actor = null;
        public Actor Actor
        {
            get => _Actor;
            set
            {
                if (value != _Actor)
                {
                    if (_Actor != null)
                    {
                        _Actor.CurrentTile._Actor = null;
                    }
                    _Actor = value;
                    if (_Actor != null)
                    {
                        _Actor.CurrentTile = this;
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Actor)));
                }
            }
        }

        public abstract string Path { get; }

        public readonly Coordinate Coordinate;
        public readonly World World;

        public event PropertyChangedEventHandler PropertyChanged;

        public Tile(Coordinate coordinate, World world)
        {
            this.Coordinate = coordinate;
            this.World = world;
        }

        public Tile GetNeighbour(Direction dir) => World[Coordinate.GetNeighbour(dir)];


        //public virtual bool MoveHere(Actor actor)
        //{
        //    if (Actor == null)
        //    {
        //        actor.CurrentTile.Actor = null;
        //        actor.CurrentTile = this;
        //        Actor = actor;
        //        return true;
        //    }
        //    return false;
        //}

    }
}
