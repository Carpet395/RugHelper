using System.Collections.Generic;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste.Mod.Rug.Module;

[CustomEntity("Rug/underererer")]
[Tracked]
public class Undertaler : Trigger
{

    public Undertale under;

    public Undertaler(EntityData data, Vector2 offset)
        : base(data, offset)
    {

    }

    public override void OnEnter(Player player)
    {
        Scene.Add(under = new Undertale());
        base.OnEnter(player);
    }

    public override void OnLeave(Player player)
    {
        Scene.Remove(under);
        base.OnLeave(player);
    }

    public Entity Load(EntityData data, Vector2 offset)
    {
        return new Undertaler(data, offset);
    }

}

public class Undertale : Entity
{
    public string name = "testmac+cheese/fog";

    public MTexture Enemysprite;


    public Undertale() 
        :base(Vector2.Zero)
    {
        base.Tag = Tags.HUD;
        //Add(Enemysprite = new Sprite(GFX.Portraits, name));
        //Enemysprite.Position = new Vector2(1920f, 1080f) / 2f;
        //Enemysprite.CenterOrigin();
        Enemysprite = GFX.Portraits[name];
    }

    public override void Render()
    {
        HiresRenderer.EndRender();
        HiresRenderer.BeginRender(BlendState.AlphaBlend, SamplerState.PointClamp);
        Draw.Rect(0, 0, 1920, 1080, Color.Black);
        Vector2 rawpos = new Vector2(1920f, 1080f);
        Vector2 position = rawpos / 2f;
        Enemysprite.DrawCentered(position - new Vector2(0, rawpos.Y / 11.5f), Color.White, 2.5f);
        Vector2 box_Spos = position - new Vector2(position.X / 1.25f, -50);
        utils.DrawHollowRect(box_Spos, (float)((position.X - box_Spos.X) * 2), 300, Color.White, Color.Black, 10);
        HiresRenderer.EndRender();
        HiresRenderer.BeginRender();
        //Enemysprite.Render();
        //base.Render();
    }
}

public static class utils
{
    public static void DrawHollowRect(Vector2 position, float width, float height, Color color, Color backgroundColor, float Thickness)
    {
        Draw.Rect(position - new Vector2(Thickness / 2f), width + Thickness, height + Thickness, color);
        Draw.Rect(position + new Vector2(Thickness / 2f), width - Thickness, height - Thickness, backgroundColor);
    }
}
