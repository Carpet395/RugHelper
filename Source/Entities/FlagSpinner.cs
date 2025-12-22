using System;
using System.Collections.Generic;
using Celeste.Mod.Entities;
using CelesteMod.Publicizer;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;

namespace Celeste.Mod.Rug.Entities;

[CustomEntity("Rug/FlagSpinner")]
[TrackedAs(typeof(CrystalStaticSpinner))]
public class FlagCrystalStaticSpinner : Entity
{

    // -- to add later: option for it to shatter when disappearing -- //

    private class CoreModeListener : Component
    {
        public FlagCrystalStaticSpinner Parent;

        public CoreModeListener(FlagCrystalStaticSpinner parent)
            : base(active: true, visible: false)
        {
            Parent = parent;
        }

        public override void Update()
        {
            Level level = base.Scene as Level;
            if ((Parent.color == CrystalColor.Blue && level.CoreMode == Session.CoreModes.Hot) || (Parent.color == CrystalColor.Red && level.CoreMode == Session.CoreModes.Cold))
            {
                if (Parent.color == CrystalColor.Blue)
                {
                    Parent.color = CrystalColor.Red;
                }
                else
                {
                    Parent.color = CrystalColor.Blue;
                }
                Parent.ClearSprites();
                Parent.CreateSprites();
            }
        }
    }

    private class Border : Entity
    {
        private Entity[] drawing = new Entity[2];

        public Border(Entity parent, Entity filler)
        {
            drawing[0] = parent;
            drawing[1] = filler;
            base.Depth = parent.Depth + 2;
        }

        public override void Render()
        {
            if (drawing[0].Visible)
            {
                DrawBorder(drawing[0]);
                DrawBorder(drawing[1]);
            }
        }

        private void DrawBorder(Entity entity)
        {
            if (entity == null)
            {
                return;
            }
            foreach (Component component in entity.Components)
            {
                if (component is Image { Color: var color, Position: var position } image)
                {
                    image.Color = Color.Black;
                    image.Position = position + new Vector2(0f, -1f);
                    image.Render();
                    image.Position = position + new Vector2(0f, 1f);
                    image.Render();
                    image.Position = position + new Vector2(-1f, 0f);
                    image.Render();
                    image.Position = position + new Vector2(1f, 0f);
                    image.Render();
                    image.Color = color;
                    image.Position = position;
                }
            }
        }
    }

    public static ParticleType P_Move;

    public const float ParticleInterval = 0.02f;

    private static Dictionary<CrystalColor, string> fgTextureLookup = new Dictionary<CrystalColor, string>
    {
        {
            CrystalColor.Blue,
            "danger/crystal/fg_blue"
        },
        {
            CrystalColor.Red,
            "danger/crystal/fg_red"
        },
        {
            CrystalColor.Purple,
            "danger/crystal/fg_purple"
        },
        {
            CrystalColor.Rainbow,
            "danger/crystal/fg_white"
        }
    };

    private static Dictionary<CrystalColor, string> bgTextureLookup = new Dictionary<CrystalColor, string>
    {
        {
            CrystalColor.Blue,
            "danger/crystal/bg_blue"
        },
        {
            CrystalColor.Red,
            "danger/crystal/bg_red"
        },
        {
            CrystalColor.Purple,
            "danger/crystal/bg_purple"
        },
        {
            CrystalColor.Rainbow,
            "danger/crystal/bg_white"
        }
    };

    public bool AttachToSolid;

    private Entity filler;

    private Border border;

    private float offset = Calc.Random.NextFloat();

    private bool expanded;

    private int randomSeed;

    private CrystalColor color;

    public FlagCrystalStaticSpinner(Vector2 position, bool attachToSolid, CrystalColor color, string flag, bool needTo)
        : base(position)
    {
        this.Flag = flag;
        this.needTo = needTo;
        this.color = color;
        base.Tag = Tags.TransitionUpdate;
        base.Collider = new ColliderList(new Circle(6f), new Hitbox(16f, 4f, -8f, -3f));
        Visible = false;
        Add(new PlayerCollider(OnPlayer));
        Add(new HoldableCollider(OnHoldable));
        Add(new LedgeBlocker());
        base.Depth = -8500;
        AttachToSolid = attachToSolid;
        if (attachToSolid)
        {
            Add(new StaticMover
            {
                OnShake = OnShake,
                SolidChecker = IsRiding,
                OnDestroy = base.RemoveSelf
            });
        }
        randomSeed = Calc.Random.Next();
    }

    public FlagCrystalStaticSpinner(EntityData data, Vector2 offset, CrystalColor color)
        : this(data.Position + offset, data.Bool("attachToSolid"), color, data.String("Flag"), data.Bool("State"))
    {
    }

    public FlagCrystalStaticSpinner(EntityData data, Vector2 offset) 
        : this(data, offset, ParseEnum(data.String("color"), CrystalColor.Rainbow))
    {
        color2 = data.String("color");
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);
        if ((scene as Level).Session.Area.LevelSet.Equals("Celeste") && (scene as Level).Session.Area.ID == 9)
        {
            Add(new CoreModeListener(this));
            if ((scene as Level).CoreMode == Session.CoreModes.Cold)
            {
                color = CrystalColor.Blue;
            }
            else
            {
                color = CrystalColor.Red;
            }
        }
        //if (InView())
        //{
            CreateSprites();
        //}
    }

    public void ForceInstantiate()
    {
        CreateSprites();
        Visible = true;
    }

    public override void Update()
    {
        //color = ParseEnum(color2, CrystalColor.Rainbow);

        // Visible = false;

        if (Flag == "" || SceneAs<Level>().Session.GetFlag(Flag) == needTo)
        {
            activeHmmm = true;
            Visible = true;
            if (filler != null)
            filler.Visible = false;
            Collidable = true;
        }
        else
        {
            if (activeHmmm)
            {
                Destroyfx();
            }
            Collidable = false;
            Visible = false;
            if (filler != null)
                filler.Visible = false;
            activeHmmm = false;
        }
        if (!Visible)
        {
            Collidable = activeHmmm;
            if (InView() && activeHmmm)
            {
                Visible = true;
                if (!expanded)
                {
                    CreateSprites();
                }
                if (color == CrystalColor.Rainbow)
                {
                    UpdateHue();
                }
            }
        }
        else
        {
            base.Update();
            if (color == CrystalColor.Rainbow && base.Scene.OnInterval(0.08f, offset))
            {
                UpdateHue();
            }
            if (base.Scene.OnInterval(0.25f, offset) && !InView())
            {
                Visible = false;
            }
            if (base.Scene.OnInterval(0.05f, offset))
            {
                Player entity = base.Scene.Tracker.GetEntity<Player>();
                if (entity != null)
                {
                    Collidable = Math.Abs(entity.X - base.X) < 128f && Math.Abs(entity.Y - base.Y) < 128f;
                }
            }
        }
        if (filler != null)
        {
            filler.Position = Position;
        }
        if (Flag == "" || SceneAs<Level>().Session.GetFlag(Flag) == needTo)
        {
            activeHmmm = true;
            Visible = true;
            if (filler != null)
                filler.Visible = true;
            Collidable = true;
        }
        else
        {
            Collidable = false;
            Visible = false;
            if (filler != null)
                filler.Visible = false;
            activeHmmm = false;
        }
    }

    private void UpdateHue()
    {
        foreach (Component component in base.Components)
        {
            if (component is Image image)
            {
                image.Color = GetHue(Position + image.Position);
            }
        }
        if (filler == null)
        {
            return;
        }
        foreach (Component component2 in filler.Components)
        {
            if (component2 is Image image2)
            {
                image2.Color = GetHue(Position + image2.Position);
            }
        }
    }

    private bool InView()
    {
        Camera camera = (base.Scene as Level).Camera;
        if (base.X > camera.X - 16f && base.Y > camera.Y - 16f && base.X < camera.X + 320f + 16f)
        {
            return base.Y < camera.Y + 180f + 16f;
        }
        return false;
    }

    private void CreateSprites()
    {
        if (expanded)
        {
            return;
        }
        Calc.PushRandom(randomSeed);
        List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures(fgTextureLookup[this.color]);
        MTexture mTexture = Calc.Random.Choose(atlasSubtextures);
        Color color = Color.White;
        if (this.color == CrystalColor.Rainbow)
        {
            color = GetHue(Position);
        }
        if (!SolidCheck(new Vector2(base.X - 4f, base.Y - 4f)))
        {
            Add(new Image(mTexture.GetSubtexture(0, 0, 14, 14)).SetOrigin(12f, 12f).SetColor(color));
        }
        if (!SolidCheck(new Vector2(base.X + 4f, base.Y - 4f)))
        {
            Add(new Image(mTexture.GetSubtexture(10, 0, 14, 14)).SetOrigin(2f, 12f).SetColor(color));
        }
        if (!SolidCheck(new Vector2(base.X + 4f, base.Y + 4f)))
        {
            Add(new Image(mTexture.GetSubtexture(10, 10, 14, 14)).SetOrigin(2f, 2f).SetColor(color));
        }
        if (!SolidCheck(new Vector2(base.X - 4f, base.Y + 4f)))
        {
            Add(new Image(mTexture.GetSubtexture(0, 10, 14, 14)).SetOrigin(12f, 2f).SetColor(color));
        }
        foreach (CrystalStaticSpinner entity in base.Scene.Entities.FindAll<CrystalStaticSpinner>())
        {
            if (entity.AttachToSolid == AttachToSolid && entity.X >= base.X && (entity.Position - Position).Length() < 24f)
            {
                AddSprite((Position + entity.Position) / 2f - Position);
            }
            else if (entity.AttachToSolid == AttachToSolid && entity.X < base.X && (entity.Position - Position).Length() < 24f)
            {
                DynamicData.For(entity).Invoke("AddSprite", (Position + entity.Position) / 2f - entity.Position);
            }
        }
        foreach (FlagCrystalStaticSpinner entity in base.Scene.Entities.FindAll<FlagCrystalStaticSpinner>())
        {
            if (entity != this && entity.AttachToSolid == AttachToSolid && entity.X >= base.X && (entity.Position - Position).Length() < 24f)
            {
                AddSprite((Position + entity.Position) / 2f - Position);
            }
        }
        base.Scene.Add(border = new Border(this, filler));
        expanded = true;
        Calc.PopRandom();
    }

    private void AddSprite(Vector2 offset)
    {
        if (filler == null)
        {
            base.Scene.Add(filler = new Entity(Position));
            filler.Depth = base.Depth + 1;
        }
        List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures(bgTextureLookup[color]);
        Image image = new Image(Calc.Random.Choose(atlasSubtextures));
        image.Position = offset;
        image.Rotation = (float)Calc.Random.Choose(0, 1, 2, 3) * ((float)Math.PI / 2f);
        image.CenterOrigin();
        if (color == CrystalColor.Rainbow)
        {
            image.Color = GetHue(Position + offset);
        }
        filler.Add(image);
    }

    private bool SolidCheck(Vector2 position)
    {
        if (AttachToSolid)
        {
            return false;
        }
        foreach (Solid item in base.Scene.CollideAll<Solid>(position))
        {
            if (item is SolidTiles)
            {
                return true;
            }
        }
        return false;
    }

    private void ClearSprites()
    {
        if (filler != null)
        {
            filler.RemoveSelf();
        }
        filler = null;
        if (border != null)
        {
            border.RemoveSelf();
        }
        border = null;
        foreach (Image item in base.Components.GetAll<Image>())
        {
            item.RemoveSelf();
        }
        expanded = false;
    }

    private void OnShake(Vector2 pos)
    {
        foreach (Component component in base.Components)
        {
            if (component is Image)
            {
                (component as Image).Position = pos;
            }
        }
    }

    private bool IsRiding(Solid solid)
    {
        return CollideCheck(solid);
    }

    private void OnPlayer(Player player)
    {
        player.Die((player.Position - Position).SafeNormalize());
    }

    private void OnHoldable(Holdable h)
    {
        h.HitSpinner(this);
    }

    public override void Removed(Scene scene)
    {
        if (filler != null && filler.Scene == scene)
        {
            filler.RemoveSelf();
        }
        if (border != null && border.Scene == scene)
        {
            border.RemoveSelf();
        }
        base.Removed(scene);
    }

    public void Destroy(bool boss = false)
    {
        if (InView())
        {
            Audio.Play("event:/game/06_reflection/fall_spike_smash", Position);
            Color color = Color.White;
            if (this.color == CrystalColor.Red)
            {
                color = Calc.HexToColor("ff4f4f");
            }
            else if (this.color == CrystalColor.Blue)
            {
                color = Calc.HexToColor("639bff");
            }
            else if (this.color == CrystalColor.Purple)
            {
                color = Calc.HexToColor("ff4fef");
            }
            CrystalDebris.Burst(Position, color, boss, 8);
        }
        RemoveSelf();
    }
    public void Destroyfx(bool boss = false)
    {
        if (InView())
        {
            Audio.Play("event:/game/06_reflection/fall_spike_smash", Position);
            Color color = GetHue(Position);
            if (this.color == CrystalColor.Red)
            {
                color = Calc.HexToColor("ff4f4f");
            }
            else if (this.color == CrystalColor.Blue)
            {
                color = Calc.HexToColor("639bff");
            }
            else if (this.color == CrystalColor.Purple)
            {
                color = Calc.HexToColor("ff4fef");
            }
            CrystalDebris.Burst(Position, color, boss, 8);
        }
    }

    private Color GetHue(Vector2 position)
    {
        float num = 280f;
        float value = (position.Length() + base.Scene.TimeActive * 50f) % num / num;
        return Calc.HsvToColor(0.4f + Calc.YoYo(value) * 0.4f, 0.4f, 0.9f);
    }
    public string Flag = "";
    public Collider collider2;

    public bool activeHmmm = false;
    public bool needTo = true;
    public string color2 = "";

    /*public static Entity Load(EntityData data, Vector2 offset)
    {
        CrystalColor color;
        if (Enum.TryParse<CrystalColor>(data.String("color"), true, out color))
            return new FlagCrystalStaticSpinner(data, offset, color);
    }*/

    public static T ParseEnum<T>(string value, T fallback) where T : struct, Enum
    {
        return Enum.TryParse<T>(value, true, out var result) ? result : fallback;
    }

    public void ClearSprites2()
    {
        if (filler != null) filler.Visible = false;
        if (border != null) border.Visible = false;
    }

    public override void Render()
    {
        //if (!activeHmmm)
        //(Engine.Scene as Level).OnEndOfFrame += ClearSprites2;
        //{
        base.Render();
        //}
    }

}