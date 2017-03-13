using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Wheel_Of_Fortune.Prizes {
    class ColorChanger {
        Color stopColor;
        Color startColor;

        int interval = 0;
        int currentFrame = 0;
        int sinceLastInterval = 0;

        double rChange = 0;
        double gChange = 0;
        double bChange = 0;

        double rCurrent = 0;
        double gCurrent = 0;
        double bCurrent = 0;

        public ColorChanger(int speed) {
            interval = speed;
        }

        public Color tick() {
            if (currentFrame == 0 || sinceLastInterval == 0) {

                if (currentFrame == 0) {
                    startColor = getRandomColor();
                } else {
                    startColor = stopColor;
                }
                
                stopColor = getRandomColor();

                rChange = (double) (stopColor.R - startColor.R) / interval;
                gChange = (double) (stopColor.G - startColor.G) / interval;
                bChange = (double) (stopColor.B - startColor.B) / interval;

                rCurrent = startColor.R;
                gCurrent = startColor.G;
                bCurrent = startColor.B;

                currentFrame++;
                sinceLastInterval = currentFrame % interval;
                return startColor;
            }

            rCurrent += rChange;
            gCurrent += gChange;
            bCurrent += bChange;
            
            currentFrame++;
            sinceLastInterval = currentFrame % interval;

            int rRounded = (int)Math.Round(rCurrent);
            int gRounded = (int)Math.Round(gCurrent);
            int bRounded = (int)Math.Round(bCurrent);

            return Color.FromRgb((byte)rRounded, (byte)gRounded, (byte)bRounded);
        }

        public static Color getRandomColor() {
            return Color.FromRgb((byte)Utilities.rnd.Next(0, 256), (byte)Utilities.rnd.Next(0, 256), (byte)Utilities.rnd.Next(0, 256));
        }
    }
}
