using Microsoft.Xna.Framework;
using System;

namespace Celeste.Mod.Rug.Module;

public static class BadelineHairColors
{

    // -- color logic for badeline colors -- //

    // for easy identification
    public static readonly string[] Colors_Index = ["blue","orange", "yellow", "green", "gray", "red", "pink", "purple", "magenta"];
    // when recoloring with the .py script included in the mod, if recoloring magenta 3rd eye use #00B3C6
    public static readonly string[] HexColors = ["2F69CE", "E38444", "FFE800", "009E2A", "AAC1C1", "D30000", "FF7CA0", "9b41c1", "C43684"];
    // colors used for the example map 
    public static readonly string[] UnderTaleColors = ["0040ff", "ffa300", "ffee00", "009100", "31e9ff", "ff1900", "pink", "cd45c9"];
    public static readonly string[] Names = ["sadeline", "cadeline", "dadeline", "green", "ladeline", "hadeline", "nadeline", "badeline", "sixty"];
    // my self chosen names, feel free to disregard it completely
    public static readonly string[] Self_Chosen_Names = ["sadeline", "cadeline", "dadeline", "radeline", "ladeline", "hadeline", "nadeline", "badeline", "sixty"];

    public static Color ToColor(this string hex)
    {
        if (hex.StartsWith("#"))
        {
            hex = hex.Substring(1);
        }

        byte r = Convert.ToByte(hex.Substring(0, 2), 16);
        byte g = Convert.ToByte(hex.Substring(2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(4, 2), 16);

        return new Color(r, g, b);
    }
    public static string ToHex(this Color color)
    {
        string hex = "";
        
        byte r = color.R;
        byte g = color.G;
        byte b = color.B;

        hex += r.ToString();
        hex += g.ToString();
        hex += b.ToString();

        return hex;
    }
}
public enum BadelineVariants
{
    Blue,
    Orange,
    Yellow, 
    Green,
    Gray,
    Red,
    Pink,
    Purple,
    Magenta
}