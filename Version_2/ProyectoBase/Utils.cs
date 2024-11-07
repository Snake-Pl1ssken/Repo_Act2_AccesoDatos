using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasilioMixer
{
    internal class Utils
    {
        public static Vector2f Oscillator(float f, float rX, float rY)
        {
            float deg2Rad = MathF.PI / 180.0f;
            return new Vector2f(MathF.Cos(f * 360 * deg2Rad) * rX, MathF.Sin(f * 360 * deg2Rad) * rY);
        }

    }
}
