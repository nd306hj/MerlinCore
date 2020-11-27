using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game
{
    public class Color
    {
        public static Color Skyblue { get { return Raylib_cs.Color.SKYBLUE; } }
        public static Color Brown { get { return Raylib_cs.Color.BROWN; } }
        public static Color Beige { get { return Raylib_cs.Color.BEIGE; } }
        public static Color Darkpurple { get { return Raylib_cs.Color.DARKPURPLE; } }
        public static Color Violet { get { return Raylib_cs.Color.VIOLET; } }
        public static Color Purple { get { return Raylib_cs.Color.PURPLE; } }
        public static Color Darkblue { get { return Raylib_cs.Color.DARKBLUE; } }
        public static Color Blue { get { return Raylib_cs.Color.BLUE; } }
        public static Color Black { get { return Raylib_cs.Color.BLACK; } }
        public static Color Darkgreen { get { return Raylib_cs.Color.DARKGREEN; } }
        public static Color Lime { get { return Raylib_cs.Color.LIME; } }
        public static Color Green { get { return Raylib_cs.Color.GREEN; } }
        public static Color Maroon { get { return Raylib_cs.Color.MAROON; } }
        public static Color Red { get { return Raylib_cs.Color.RED; } }
        public static Color Pink { get { return Raylib_cs.Color.PINK; } }
        public static Color Orange { get { return Raylib_cs.Color.ORANGE; } }
        public static Color Gold { get { return Raylib_cs.Color.GOLD; } }
        public static Color Yellow { get { return Raylib_cs.Color.YELLOW; } }
        public static Color Darkgray { get { return Raylib_cs.Color.DARKGRAY; } }
        public static Color Gray { get { return Raylib_cs.Color.GRAY; } }
        public static Color Lightgray { get { return Raylib_cs.Color.LIGHTGRAY; } }
        public static Color Blank { get { return Raylib_cs.Color.BLANK; } }
        public static Color Magenta { get { return Raylib_cs.Color.MAGENTA; } }
        public static Color Raywhite { get { return Raylib_cs.Color.RAYWHITE; } }
        public static Color Darkbrown { get { return Raylib_cs.Color.DARKBROWN; } }
        public static Color White { get { return Raylib_cs.Color.WHITE; } }
        public int R { get; private set; }
        public int A { get; private set; }
        public int B { get; private set; }
        public int G { get; private set; }
        public Color(int r, int g, int b) : this(r, g, b, 255)
        {

        }
        public Color(int r, int g, int b, int a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static implicit operator Raylib_cs.Color(Color c) => new Raylib_cs.Color(c.R, c.G, c.B, c.A);
        public static implicit operator Color(Raylib_cs.Color c) => new Color(c.r, c.g, c.b, c.a);
    }
}
