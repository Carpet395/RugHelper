using Celeste.Mod.Entities;
using Monocle;
using Microsoft.Xna.Framework;
using Celeste.Mod.Backdrops;
using System.Collections.Generic;

namespace Celeste.Mod.Rug.Stylegrounds;

[CustomBackdrop("Rug/ColorgradeStyleground")]
public class ColorgradeStyleground : Backdrop
{

    public string name;
    public bool immediate;
    public float duration;
    public bool onlyOnce;

    private bool triggered = false;

    //public List<List<MTexture>> textures;

    public ColorgradeStyleground(BinaryPacker.Element data)
    {
        name = data.Attr("colorgrade", "none");
        //Logger.Log(LogLevel.Info, "init", name);
        immediate = data.AttrBool("immediate", false);
        onlyOnce = data.AttrBool("onlyOnce", false);
        duration = data.AttrFloat("duration", 2f);
        FadeAlphaMultiplier = 1f;
        Scroll = Vector2.Zero;
        LoopX = LoopY = true;
        //textures = new List<List<MTexture>>();
    }

    public override void Update(Scene scene)
    {
        //if (immediate)
        base.Update(scene);
        if (Visible)
        {
            //triggered = lastScene == Engine.Scene ? true : false;
            if (onlyOnce && triggered) return;
            //Logger.Log(LogLevel.Info, "gsad", name);
            if (immediate)
                (Engine.Scene as Level)?.SnapColorGrade(name);
            else
                (Engine.Scene as Level)?.NextColorGrade(name, 1f / duration);
            triggered = true;
            //lastScene = Engine.Scene;
        }
        else
        {
            triggered = false;
        }
        //else
        //(scene as Level).NextColorGrade(name, 1f / duration);
    }

    //public override void Ended(Scene scene)
    //{
    //base.Ended(scene);
    //}
}