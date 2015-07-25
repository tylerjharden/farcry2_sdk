using System;
using System.Collections.Generic;
using System.Text;

namespace FC2Shell.Helper
{
    public static class MathHelper
    {
        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
                return value;
            }
            if (value > max)
            {
                value = max;
            }
            return value;
        }

        public static float Deg2Rad(float angleDeg)
        {
            return (((angleDeg * 2f) * 3.141593f) / 360f);
        }

        public static float Rad2Deg(float angleRad)
        {
            return ((angleRad * 360f) / 6.283185f);
        }

    }
}
