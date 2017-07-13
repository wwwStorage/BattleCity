﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class MoveTank : MoveCommand
    {
        private IScene scene;

        public MoveTank(Unit unit, IScene scene)
            : base(unit)
        {
            this.scene = scene;
        }

        public override void Move(int spead)
        {
            base.Move(spead);
            MoveColision();
        }

        private void MoveColision()
        {
            while (scene.ColisionBoard(Unit)) base.Move(-1);

            Unit colision = scene.Colision(Unit);
            if (colision != null && colision.Type != TypeUnit.Shell)
            {
                do {
                    base.Move(-1);
                } while (Unit.BoundingBox.IntersectsWith(colision.BoundingBox)) ;
            }
        }
    }
}