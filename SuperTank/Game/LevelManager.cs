﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class LevelManager
    {
        private readonly IScene scene;
        private readonly IFactoryUnit factoryUnit;
        private readonly IPlaeyr plaeyr;

        public LevelManager(IScene scene, IFactoryUnit factoryUnit, IPlaeyr plaeyr)
        {
            this.scene = scene;
            this.factoryUnit = factoryUnit;
            this.plaeyr = plaeyr;
        }

        public void CreateLevel(int level)
        {
            scene.Clear();

            string[] linesTileMap = File.ReadAllLines(ConfigurationGme.Maps + level);
            int x = 0, y = 0;
            foreach (string line in linesTileMap)
            {
                foreach (char c in line)
                {
                    switch (c)
                    {
                        case '#':
                            scene.Add(factoryUnit.Create(x, y, TypeUnit.BrickWall));
                            break;
                    }
                    x += ConfigurationGme.WidthTile;
                }
                x = 0;
                y += ConfigurationGme.HeightTile;
            }
            plaeyr.Unit.X = 9 * ConfigurationGme.WidthTile;
            plaeyr.Unit.Y = ConfigurationGme.HeightBoard - ConfigurationGme.HeigthTank;
            scene.Add(plaeyr.Unit);
        }
    }
}