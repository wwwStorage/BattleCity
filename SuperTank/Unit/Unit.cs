﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;

namespace SuperTank
{
    public class Unit
    {
        private static int nextId = 0;

        private Rectangle boundingBox;
        private TypeUnit type;
        private readonly PropertiesUnit properties;
        private readonly int id;

        public Unit(int id, int x, int y, int width, int height, TypeUnit type)
        {
            this.id = id;
            this.type = type;
            boundingBox = new Rectangle(x, y, width, height);
            properties = new PropertiesUnit(this);
            nextId++;
        }

        public static int NextID { get { return nextId; } }
        public event Action<int, PropertiesType, object> PropertyChanged;

        public int ID { get { return id; } }
        public int X
        {
            get { return boundingBox.X; }
            set
            {
                if (boundingBox.X != value)
                {
                    boundingBox.X = value;
                    OnPropertyChenges(PropertiesType.X, value);
                }
            }
        }
        public int Y
        {
            get { return boundingBox.Y; }
            set
            {
                if (boundingBox.Y != value)
                {
                    boundingBox.Y = value;
                    OnPropertyChenges(PropertiesType.Y, value);
                }
            }
        }
        public int Width
        {
            get { return boundingBox.Width; }
            set { boundingBox.Width = value; }
        }
        public int Height
        {
            get { return boundingBox.Height; }
            set { boundingBox.Height = value; }
        }
        public Rectangle BoundingBox { get { return boundingBox; } }
        public TypeUnit Type { get { return type; } }
        public IDictionary<PropertiesType, object> Properties { get { return properties; } }
        
        protected void OnPropertyChenges(PropertiesType type, Object value)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(ID, type, value);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Unit unit = obj as Unit;
            if (unit == null)
                return false;
            return unit.ID.Equals(ID) && Type.Equals(unit.Type) && BoundingBox.Equals(unit.BoundingBox);
        }

        private class PropertiesUnit : Dictionary<PropertiesType, object>, IDictionary<PropertiesType, object>
        {
            private Unit unit;

            public PropertiesUnit(Unit unit)
            {
                this.unit = unit;
            }

            public new object this[PropertiesType key]
            {
                get
                {
                    return base[key];
                }
                set
                {
                    object obj;
                    unit.Properties.TryGetValue(key, out obj);
                    if (!value.Equals(obj))
                    {
                        base[key] = value;
                        unit.OnPropertyChenges(key, value);
                    }
                }
            }
        }
    }
}
