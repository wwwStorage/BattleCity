﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class Shell : MovableUnit, IUpdatable
    {
        private IScene scene;
        private int delayDetonation = 0;
        private Tank ownerTank;

        public Shell(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, Tank ownerTank, IScene scene) : base(id, x, y, width, height, type, velosity, direction)
        {
            this.scene = scene;
            this.ownerTank = ownerTank;
            Owner = (Owner)ownerTank.Properties[PropertiesType.Owner];
            ownerTank.Shell = null;
            IsDetonation = false;
        }

        public void Start()
        {
            Game.Updatable.Add(this);
            scene.Add(this);
        }

        public void Update()
        {
            if (IsDetonation)
            {
                delayDetonation++;
                if (delayDetonation == ConfigurationGame.DelayDetonation)
                    OnShellDestroy();

                return;
            }

            Move(Velosity);

            if (scene.ColisionBoard(this))
                Detonation(null, false);
            else
                ColisionWichUnit();
        }

        protected Owner Owner
        {
            set { Properties[PropertiesType.Owner] = value; }
            get { return (Owner)Properties[PropertiesType.Owner]; }
        }

        protected bool IsDetonation
        {
            set { Properties[PropertiesType.Detonation] = value; }
            get { return (bool)Properties[PropertiesType.Detonation]; }
        }

        private void ColisionWichUnit()
        {
            for (int i = 0; i < scene.Units.Count; i++)
            {
                Unit item = scene.Units[i];
                if (item.BoundingBox.IntersectsWith(this.BoundingBox) && !item.Equals(this))
                {
                    switch (item.Type)
                    {
                        case TypeUnit.PainTank:
                            if (!Owner.Equals(item.Properties[PropertiesType.Owner]))
                            {
                                item.Properties[PropertiesType.Detonation] = true;
                                Detonation(item, true);
                                return;
                            }
                            break;
                        case TypeUnit.Shell:
                            if (!Owner.Equals(item.Properties[PropertiesType.Owner]))
                            {
                                ((Shell)item).OnShellDestroy();
                                OnShellDestroy();
                                return;
                            }
                            break;
                        case TypeUnit.BrickWall:
                            Detonation(item, true);
                            return;
                        case TypeUnit.ConcreteWall:
                            Detonation(item, false);
                            break;
                    }
                }
            }
        }

        protected virtual void Detonation(Unit item, bool removeItem)
        {
            if (item != null && removeItem) scene.Remove(item);
            IsDetonation = true;
        }


        protected virtual void OnShellDestroy()
        {
            scene.Remove(this);
            Game.Updatable.Remove(this);
            ownerTank.Shell = null;
        }
    }
}
