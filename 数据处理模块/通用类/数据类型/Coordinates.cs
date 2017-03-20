using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    public class Coordinates
    {
        private float x = 0.0f;
        private float y = 0.0f;
        public float X {
            get { return x; }
            set { X = value; }
        }
        public float Y
        {
            get { return y; }
            set { Y = value; }
        }
        public Coordinates() { }
        public Coordinates(float xvalue,float yvalue)
        {
            this.X = xvalue;
            this.Y = yvalue;
        }
    }
}
