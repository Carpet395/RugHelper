using Celeste.Mod.Entities;
using Celeste.Mod.Rug.Module;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.Rug.Entities;

[CustomEntity("Rug/ColorfulBadelineBoss")]
[Tracked]
public class ColorfulBadelineBoss : FinalBoss
{

    public string flag = "";
    public bool setTo = true;

    public Color color;

    public BadelineSpriteModule sprite;

    public bool no_be_dumbass = false;

    public ColorfulBadelineBoss(EntityData data, Vector2 offset)
      : base(data, offset)
    {
        flag = data.Attr("flag");
        color = data.HexColor("color");
        setTo = data.Bool("setTo", true);
        Add(sprite = new BadelineSpriteModule("Wbadeline_boss"));
        //Sprite.Visible = false;

        //Hair.Color
    }

    public static Entity Load(EntityData data, Vector2 offset)
    {
        return new ColorfulBadelineChaser(data,offset);
    }

    private void Trail()
    {
        if (base.Scene.OnInterval(0.1f))
        {
            TrailManager.Add(this, color, 1);
        }
    }
    public override void Added(Scene scene)
    {
        base.Added(scene);
        //sprite = new BadelineSpriteModule("Wbadeline_boss");
        sprite.Color = color;
        sprite.Position = Sprite.Position;
        sprite.RenderPosition = Sprite.RenderPosition;
        Sprite.Visible = false;
        if (!no_be_dumbass)
        {
            Sprite.OnFrameChange = delegate (string anim)
            {
                sprite.PlayOffset(anim, Sprite.CurrentAnimationFrame);
            };
            no_be_dumbass = true;
        }
    }

    public override void Update()
    {
        //if (no_be_dumbass || SceneAs<Level>().Session.GetFlag(flag))
        //{
        base.Update();
        if (Sprite != null)
        {
            sprite.Color = color;
            sprite.Position = Sprite.Position;
            sprite.RenderPosition = Sprite.RenderPosition;
            Sprite.Visible = false;
            if (!no_be_dumbass) {
                Sprite.OnFrameChange = delegate (string anim)
                {
                    sprite.PlayOffset(anim, Sprite.CurrentAnimationFrame);
                };
                no_be_dumbass = true;
            }
        }

            //if (Sprite.)

            //Hair.Color = color;
            //Sprite.Visible = false;
            //sprite.Scale = Sprite.Scale;
            //Trail();
        //}
        //if (no_be_dumbass && SceneAs<Level>().Session.GetFlag(flag) != setTo)
        //{
            //Level obj = base.Scene as Level;
            //Audio.Play("event:/char/badeline/disappear", Position);
            //obj.Displacement.AddBurst(base.Center, 0.5f, 24f, 96f, 0.4f);
            //obj.Particles.Emit(BadelineOldsite.P_Vanish, 12, base.Center, Vector2.One * 6f);
            //RemoveSelf();
        //}
    }

}
