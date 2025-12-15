using Celeste.Mod.Entities;
using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Monocle;
using Celeste.Mod.Rug.Module;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Celeste.Mod.Rug.Entities;

public class colorfulDummy : Entity
{
    public PlayerSprite Spritealt;

    public BadelineSpriteModule Sprite;

    public PlayerHair Hair;

    public Color Color;

    private Scene scene;
    public string flag = "";
    public bool ifset = false;

    private bool Debug = false;

    public BadelineAutoAnimator AutoAnimator;

    public Image white;
    public BloomPoint bloom;
    public VertexLight light;
    public List<Orb> orbs = new List<Orb>();

    public SineWave Wave;

    public VertexLight Light;

    public float FloatSpeed = 120f;

    public float FloatAccel = 240f;

    public float Floatness = 2f;

    public string variant;

    public Vector2 floatNormal = new Vector2(0f, 1f);

    public static colorfulDummy FindFirst(string variant = "blue")
    {
        foreach (var i in Engine.Scene.Entities.FindAll<colorfulDummy>())
        {
            if (i.variant == variant) return i as colorfulDummy;
        }
        return null;
    }
    public static List<colorfulDummy> FindAll(string variant = "blue")
    {
        List<colorfulDummy> l = new();
        foreach (var i in Engine.Scene.Entities.FindAll<colorfulDummy>())
        {
            if (i.variant == variant) l.Add(i);
        }
        return l;
    }

    public colorfulDummy(Vector2 position, string variant = "blue", bool left = false, bool floating = true, string animation = "", string flag = "", bool ifset = false)
        : base(position)
    {
        this.flag = flag;
        this.ifset = ifset;
        this.variant = variant;
        BadelineVariants color = new BadelineVariants();
        base.Collider = new Hitbox(6f, 6f, -3f, -7f);
        Spritealt = new PlayerSprite(PlayerSpriteMode.Badeline);
        Spritealt.Play("fallSlow");
        Spritealt.Scale.X = -1f;
        Spritealt.Visible = false;
        if (Enum.TryParse(variant, true, out color))
        {
            Sprite = new BadelineSpriteModule("whiteBadeline");
            Sprite.Color = BadelineHairColors.HexColors[(int)color].ToColor();
            Sprite.Play("fallSlow");
            Sprite.Scale.X = left ? -1 : 1;
            if (!floating)
            {
                Floatness = 0f;
                Sprite.Play("idle");
            }
            if (animation != "" && animation != null)
            {
                Sprite.Play(animation);
            }
            Sprite.Visible = false;
            //Color = colorHairColors.HexToColor(colorHairColors.HexColors[(int)color]);
            //Sprite.Color = Color;
            Hair = new PlayerHair(Spritealt);
            Hair.Color = BadelineHairColors.HexColors[(int)color].ToColor();
            Hair.Border = Color.Black;
            Hair.Facing = Facings.Left;
            Hair.Visible = false;
            Add(Hair);
            Add(Sprite);
            // debug
            if (Debug) Console.WriteLine($"hello, I am {BadelineHairColors.Names[(int)color]}, My color is {BadelineHairColors.HexColors[(int)color]}. But you can just say {variant}!");
        }
        else
        {
            Sprite = new BadelineSpriteModule("badeline");
            Sprite.Play("fallSlow");
            Sprite.Scale.X = left ? -1 : 1;
            if (!floating)
            {
                Floatness = 0f;
                Sprite.Play("idle");
            }
            if (animation != "" && animation != null)
            {
                Sprite.Play(animation);
            }
            //Color = colorHairColors.HexToColor(colorHairColors.HexColors[(int)color]);
            //Sprite.Color = Color;
            Hair = new PlayerHair(Spritealt);
            Hair.Color = BadelineHairColors.HexColors[(int)color].ToColor();
            Hair.Border = Color.Black;
            Hair.Facing = Facings.Left;
            Add(Hair);
            Add(Sprite);
        }
        Add(Spritealt);
        //Add(white = new Image(GFX.Game["characters/badelineBoss/calm_white"]));
        Add(white = new Image(GFX.Game["characters/white"]));
        white.Color = Color.White * 0f;
        white.Origin = Sprite.Origin;
        white.Position = Sprite.Position;
        //Console.WriteLine(white.Position + "," + Sprite.Position);
        Add(bloom = new BloomPoint(new Vector2(0f, -6f), 0f, 16f));
        Add(light = new VertexLight(new Vector2(0f, -6f), Color.White, 1f, 24, 64));
        /*Hair = new BadelineHairModule(Sprite);
		Hair.Color = colors.HexToColor(colors.HexColors[0]);
        Hair.Border = Color.Black;
        Hair.Facing = Facings.Left;*/
        Add(AutoAnimator = new BadelineAutoAnimator());
        Sprite.OnFrameChange = delegate (string anim)
        {
            int currentAnimationFrame = Sprite.CurrentAnimationFrame;
            if ((anim == "walk" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)) || (anim == "runSlow" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)) || (anim == "runFast" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)))
            {
                Audio.Play("event:/char/badeline/footstep", Position);
            }
        };
        Add(Wave = new SineWave(0.25f));
        Wave.OnUpdate = delegate (float f)
        {
            Sprite.Position = floatNormal * f * Floatness;
        };
        Add(Light = new VertexLight(new Vector2(0f, -8f), BadelineHairColors.HexColors[(int)color].ToColor(), 1f, 20, 60));
    }

    public colorfulDummy(EntityData data, Vector2 offset)
    : this(data.Position + offset + new Vector2(0, 16), data.Attr("variant"), data.Bool("left"), data.Bool("floating"), data.Attr("animation"), data.Attr("flag"), data.Bool("ifset"))
    {
    }

    public void ReLoad_Variant(bool left = false, bool floating = true, string animation = "")
    {
        BadelineVariants color = new BadelineVariants();
        //BadelineHairColors colorHairColors = new BadelineHairColors();

        if (Enum.TryParse(variant, true, out color))
        {
            Remove(Hair);
            Remove(Sprite);
            Sprite = new BadelineSpriteModule($"{BadelineHairColors.Names[(int)color]}");
            Sprite.Play("fallSlow");
            Sprite.Scale.X = left ? -1 : 1;
            if (!floating)
            {
                Floatness = 0f;
                Sprite.Play("idle");
            }
            if (animation != "" && animation != null)
            {
                Sprite.Play(animation);
            }

            //Color = colorHairColors.HexToColor(colorHairColors.HexColors[(int)color]);
            //Sprite.Color = Color;

            Hair = new PlayerHair(Spritealt);

            Hair.Color = BadelineHairColors.HexColors[(int)color].ToColor();
            Hair.Border = Color.Black;
            Hair.Facing = Facings.Left;
            Add(Hair);
            Add(Sprite);
            Console.WriteLine($"hello, I am {BadelineHairColors.Names[(int)color]} now!, My color is {BadelineHairColors.HexColors[(int)color]}. But you can just say {variant}!");
        }
        else
        {
            Sprite = new BadelineSpriteModule($"badeline");
            Sprite.Play("fallSlow");
            Sprite.Scale.X = left ? -1 : 1;
            if (!floating)
            {
                Floatness = 0f;
                Sprite.Play("idle");
            }
            if (animation != "" && animation != null)
            {
                Sprite.Play(animation);
            }

            //Color = colorHairColors.HexToColor(colorHairColors.HexColors[(int)color]);
            //Sprite.Color = Color;

            Hair = new PlayerHair(Spritealt);
            Hair.Color = BadelineHairColors.HexColors[(int)color].ToColor();
            Hair.Border = Color.Black;
            Hair.Facing = Facings.Left;
            Add(Hair);
            Add(Sprite);
        }
    }


    public void Appear(Level level, bool silent = false)
    {
        if (!silent)
        {
            Audio.Play("event:/char/badeline/appear", Position);
            Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
        }
        level.Displacement.AddBurst(base.Center, 0.5f, 24f, 96f, 0.4f);
        level.Particles.Emit(BadelineOldsite.P_Vanish, 12, base.Center, Vector2.One * 6f);
    }

    public void Vanish()
    {
        Audio.Play("event:/char/badeline/disappear", Position);
        Shockwave();
        SceneAs<Level>().Particles.Emit(BadelineOldsite.P_Vanish, 12, base.Center, Vector2.One * 6f);
        RemoveSelf();
    }

    private void Shockwave()
    {
        SceneAs<Level>().Displacement.AddBurst(base.Center, 0.5f, 24f, 96f, 0.4f);
    }

    public IEnumerator FloatTo(Vector2 target, int? turnAtEndTo = null, bool faceDirection = true, bool fadeLight = false, bool quickEnd = false)
    {
        Sprite.Play("fallSlow");
        if (faceDirection && Math.Sign(target.X - base.X) != 0)
        {
            Sprite.Scale.X = Math.Sign(target.X - base.X);
        }
        Vector2 vector = (target - Position).SafeNormalize();
        Vector2 perp = new Vector2(0f - vector.Y, vector.X);
        float speed = 0f;
        while (Position != target)
        {
            speed = Calc.Approach(speed, FloatSpeed, FloatAccel * Engine.DeltaTime);
            Position = Calc.Approach(Position, target, speed * Engine.DeltaTime);
            Floatness = Calc.Approach(Floatness, 4f, 8f * Engine.DeltaTime);
            floatNormal = Calc.Approach(floatNormal, perp, Engine.DeltaTime * 12f);
            if (fadeLight)
            {
                Light.Alpha = Calc.Approach(Light.Alpha, 0f, Engine.DeltaTime * 2f);
            }
            yield return null;
        }
        if (quickEnd)
        {
            Floatness = 2f;
        }
        else
        {
            while (Floatness != 2f)
            {
                Floatness = Calc.Approach(Floatness, 2f, 8f * Engine.DeltaTime);
                yield return null;
            }
        }
        if (turnAtEndTo.HasValue)
        {
            Sprite.Scale.X = turnAtEndTo.Value;
        }
        Spritealt.Play("fallSlow");
        if (faceDirection && Math.Sign(target.X - base.X) != 0)
        {
            Spritealt.Scale.X = Math.Sign(target.X - base.X);
        }
        vector = (target - Position).SafeNormalize();
        perp = new Vector2(0f - vector.Y, vector.X);
        speed = 0f;
        while (Position != target)
        {
            speed = Calc.Approach(speed, FloatSpeed, FloatAccel * Engine.DeltaTime);
            Position = Calc.Approach(Position, target, speed * Engine.DeltaTime);
            Floatness = Calc.Approach(Floatness, 4f, 8f * Engine.DeltaTime);
            floatNormal = Calc.Approach(floatNormal, perp, Engine.DeltaTime * 12f);
            if (fadeLight)
            {
                Light.Alpha = Calc.Approach(Light.Alpha, 0f, Engine.DeltaTime * 2f);
            }
            yield return null;
        }
        if (quickEnd)
        {
            Floatness = 2f;
        }
        else
        {
            while (Floatness != 2f)
            {
                Floatness = Calc.Approach(Floatness, 2f, 8f * Engine.DeltaTime);
                yield return null;
            }
        }
        if (turnAtEndTo.HasValue)
        {
            Spritealt.Scale.X = turnAtEndTo.Value;
        }
    }
    public IEnumerator WalkTo(float x, float speed = 64f)
    {
        Floatness = 0f;
        Sprite.Play("walk");
        if (Math.Sign(x - base.X) != 0)
        {
            Sprite.Scale.X = Math.Sign(x - base.X);
        }
        while (base.X != x)
        {
            base.X = Calc.Approach(base.X, x, Engine.DeltaTime * speed);
            yield return null;
        }
        Sprite.Play("idle");
        Floatness = 0f;
        Spritealt.Play("walk");
        if (Math.Sign(x - base.X) != 0)
        {
            Spritealt.Scale.X = Math.Sign(x - base.X);
        }
        while (base.X != x)
        {
            base.X = Calc.Approach(base.X, x, Engine.DeltaTime * speed);
            yield return null;
        }
        Spritealt.Play("idle");
    }

    public IEnumerator SmashBlock(Vector2 target)
    {
        SceneAs<Level>().Displacement.AddBurst(Position, 0.5f, 24f, 96f);
        Sprite.Play("dreamDashLoop");
        Vector2 from = Position;
        for (float p = 0f; p < 1f; p += Engine.DeltaTime * 6f)
        {
            Position = from + (target - from) * Ease.CubeOut(p);
            yield return null;
        }
        base.Scene.Entities.FindFirst<DashBlock>().Break(Position, new Vector2(0f, -1f), playSound: false);
        Sprite.Play("idle");
        for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
        {
            Position = target + (from - target) * Ease.CubeOut(p);
            yield return null;
        }
        Sprite.Play("fallSlow");
        SceneAs<Level>().Displacement.AddBurst(Position, 0.5f, 24f, 96f);
        Spritealt.Play("dreamDashLoop");
        from = Position;
        for (float p = 0f; p < 1f; p += Engine.DeltaTime * 6f)
        {
            Position = from + (target - from) * Ease.CubeOut(p);
            yield return null;
        }
        base.Scene.Entities.FindFirst<DashBlock>().Break(Position, new Vector2(0f, -1f), playSound: false);
        Spritealt.Play("idle");
        for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
        {
            Position = target + (from - target) * Ease.CubeOut(p);
            yield return null;
        }
        Spritealt.Play("fallSlow");
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);
    }

    public override void Update()
    {
        Level level = SceneAs<Level>();
        if (level != null)
        {
            if (flag != "" && level.Session.GetFlag(flag) != ifset) { Hair.Visible = false; Sprite.Visible = false; }
            else { Hair.Visible = true; Sprite.Visible = true; }
        }
        else Console.WriteLine("level is null");
        if (Sprite.CurrentAnimationID != null && Sprite.CurrentAnimationID != "")
            Spritealt.Play(Sprite.CurrentAnimationID, false, false);
        if (Sprite.Scale.X != 0f)
        {
            Hair.Facing = (Facings)Math.Sign(Sprite.Scale.X);
        }
        white.Position = Sprite.Position;
        base.Update();
    }


    public override void Render()
    {
        Vector2 renderPosition = Sprite.RenderPosition;
        Sprite.RenderPosition = Sprite.RenderPosition.Floor();
        Spritealt.RenderPosition = Spritealt.RenderPosition.Floor();
        //white.RenderPosition= white.RenderPosition.Floor();
        base.Render();
        //white.Render();
        Sprite.RenderPosition = renderPosition;
        //white.RenderPosition = renderPosition;
        Spritealt.RenderPosition = renderPosition;
    }
    public IEnumerator TurnWhite(float duration)
    {
        white.Visible = true;
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Engine.DeltaTime / duration;
            white.Color = Color.White * alpha;
            bloom.Alpha = alpha;
            yield return null;
        }
        Console.WriteLine(white.Position + "," + Sprite.Position);
        Sprite.Visible = false;
    }

    public IEnumerator Disperse()
    {
        Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
        float size = 1f;
        while (orbs.Count < 8)
        {
            float to = size - 0.125f;
            while (size > to)
            {
                white.Scale = Vector2.One * size;
                light.Alpha = size;
                bloom.Alpha = size;
                size -= Engine.DeltaTime;
                yield return null;
            }
            Orb orb = new Orb(Position);
            orb.Target = Position + new Vector2(-16f, -40f);
            base.Scene.Add(orb);
            orbs.Add(orb);
        }
        yield return 3.25f;
        int i = 0;
        foreach (Orb orb2 in orbs)
        {
            orb2.Routine.Replace(orb2.CircleRoutine((float)i / 8f * ((float)Math.PI * 2f)));
            i++;
            yield return 0.2f;
        }
        yield return 2f;
        foreach (Orb orb3 in orbs)
        {
            orb3.Routine.Replace(orb3.AbsorbRoutine());
        }
        yield return 1f;
    }
}
