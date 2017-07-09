﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class Tank : ITank
    {
        private MoveObj movable;
        private IDriwer driver;
        private IShooter shooter;

        public Tank(MoveObj movable, IDriwer driver, IShooter shooter)
        {
            this.movable = movable;
            this.driver = driver;
            this.shooter = shooter;
        }

        public void Update()
        {
            driver.Update();
            shooter.Update();
        }
    }
}