using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Helper.Math
{
    public abstract class Vector
    {
        private int m_ElementCount = 0;
        private float[] m_Values;

        protected Vector(int elementCount)
        {
            m_Values = new float[m_ElementCount];
            for(int i = 0; i < m_ElementCount; i++)
            {
                m_Values[i] = 0.0f;
            }
        }

        protected float[] GetValues()
        {
            return m_Values;
        }

        protected void UpdateValues(float[] values)
        {
            if(values.Length != m_ElementCount)
            {
                return;
            }

            m_Values = values;
        }

        protected abstract void ValuesUpdated();
    }
}
