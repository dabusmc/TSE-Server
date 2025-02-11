﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Helper.Math
{
    public class Vector4 : Vector
    {
        private float _x;
        private float _y;
        private float _z;
        private float _w;

        public float X { get { return _x; } set { _x = value; ChangeValues(); } }
        public float Y { get { return _y; } set { _y = value; ChangeValues(); } }
        public float Z { get { return _z; } set { _z = value; ChangeValues(); } }
        public float W { get { return _w; } set { _w = value; ChangeValues(); } }

        public Vector4() : base(4)
        {

        }

        public Vector4(float x, float y, float z, float w) : base(4)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
            ChangeValues();
        }

        protected override void ValuesUpdated()
        {
            _x = GetValues()[0];
            _y = GetValues()[1];
            _z = GetValues()[2];
            _w = GetValues()[3];
        }

        private void ChangeValues()
        {
            UpdateValues(new float[4] { _x, _y, _z, _w });
        }
    }
}
