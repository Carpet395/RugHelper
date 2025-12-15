using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Celeste.Mod.Entities;
using Celeste;
using Microsoft.Xna.Framework;
using Monocle;
using static Celeste.TrackSpinner;
using FrostHelper.Entities.Boosters;
using MonoMod.Utils;
using System.Text.RegularExpressions;
using Iced.Intel;
using System.Diagnostics;
using MonoMod.Core.Utils;

namespace Celeste.Mod.Rug.Entities;

[Tracked()]
[CustomEntity("Rug/BadAppearBlock")]
public class NewAppearingBlock : Solid
{

    // -- idk how tf does it work but i know it does so yeppe!! -- //

    // tile sprites
    public TileGrid sprite;
    //public TileGrid highlight;

    // nodex
    public Vector2[] nodes;

    // sine wave
    //public SineWave sine;

    //public float freq;
    //public float peak;

    public bool Appear = false;

    // tile
    public char tileType;
    //public char HightileType;

    //public static List<AppearingBlock> dic = new List<AppearingBlock>();

    // flag logic
    public string Flag;
    public bool OnFlag;

    public bool DisappearMode = false;
    
    public bool onNode;
    public int count;

    public NewAppearingBlock(Vector2[] nodes, float width, float height, char tileType, string flag, bool state, bool onNode = false, int count = 0, bool disappearMode = false)
        : base(nodes[0], width, height, safe: false)
    {
        this.onNode = onNode;
        this.count = count;
        this.DisappearMode = disappearMode;
        //BossNodeIndex = bossNodeIndex;
        //this.freq = freq;
        //peak = 1f;
        Flag = flag;
        OnFlag = state;
        this.nodes = nodes;
        //int newSeed = Calc.Random.Next();
        //Calc.PushRandom(newSeed);
        this.tileType = tileType;
        //Calc.PopRandom();
        //Calc.PushRandom(newSeed);
        //highlight = GFX.FGAutotiler.GenerateBox(highlightTileType, (int)(base.Width / 8f), (int)base.Height / 8).TileGrid;
        //highlight.Alpha = 0f;
        //Add(highlight);
        //Calc.PopRandom();
        //if ((count == 0 && onNode) || DisappearMode)
        //{
            //Collidable = true;
        //}
        Add(new LightOcclude());
    }

    public NewAppearingBlock(EntityData data, Vector2 offset)
    : this(data.NodesWithPosition(offset), data.Width, data.Height, data.Char("tiletype"), data.String("flag"), data.Bool("state"), data.Bool("onNode", false), data.Int("count", 0), data.Bool("Disappear", false))
    {
    }

    public static Dictionary<FinalBoss, DynamicData> bossData = null;

    public bool fade = false;
    public float TargetAlpha = 0;

    public int Lastcount = 0;

    public override void Update()
    {
        //if ()
        //bool hm = GetNodeMatch(count, out int nodes);
        //if (nodes != Lastcount) Rebuild(); //JustRebuilt = false;
        //Lastcount = nodes;
        //if ((BlockedCheck<FinalBoss>(true) && !onNode))
            //Add(new Coroutine(waitForClear(onNode && hm)));
        //else if (sprite != null && DisappearMode && ((BlockedCheck<FinalBoss>(true) && !onNode) || (onNode && hm)))
            //Add(new Coroutine(waitForDie(onNode && hm)));
        if (sprite != null)
        {

            fade = sprite.Alpha != TargetAlpha;
            //Logger.Log(LogLevel.Info, "neiw", fade.ToString());
            if (fade)
            {

                //sprite.Alpha = a;
                //while (current != to)
                //{
                //CassetteBlock
                sprite.Alpha = Calc.Approach(sprite.Alpha, TargetAlpha, Engine.DeltaTime);
                StartShaking(Engine.DeltaTime);
                if (sprite.Alpha == TargetAlpha)
                {
                    fade = false;
                    sprite.Position = Vector2.Zero;
                }
                //sprite.Alpha = current;
                //yield return null;
                //}
                //sprite.Alpha = to;
            }
            else
            {
                sprite.Position = Vector2.Zero;
            }
        }
        base.Update();
    }

    public override void Render()
    {
        if (sprite != null && !fade) sprite.Position = Vector2.Zero;
        base.Render();
    }

    public bool JustRebuilt = false;

    public override void SceneEnd(Scene scene)
    {
        //lastNode = 0;
        base.SceneEnd(scene);
    }

    public override void OnShake(Vector2 amount)
    {
        base.OnShake(amount);
        if (fade) sprite.Position = amount;
    }

    public bool BlockedCheck(bool doShit = false)
    {
        TheoCrystal theoCrystal = CollideFirst<TheoCrystal>();
        if (doShit ? theoCrystal != null && TryActorWiggleUp(theoCrystal) : theoCrystal != null)
        {
            return true;
        }

        Player player = CollideFirst<Player>();
        if (doShit ? player != null && TryActorWiggleUp(player) : player != null)
        {
            return true;
        }

        return false;
    }

    public bool BlockedCheck<T>(bool doShit = false) where T : Entity
    {
        bool cl = Collidable;
        List<T> disabled = new List<T>();
        foreach (var i in Engine.Scene.Entities.FindAll<T>())
        {
            if (i.Collidable == false) disabled.Add(i);
        }
        foreach (var item in disabled)
        {
            item.Collidable = true;
        }
        Collidable = true;
        T Tcollider = CollideFirst<T>();
        if (Tcollider != null)
        {
            foreach (var item in disabled)
            {
                item.Collidable = false;
            }
            Collidable = cl;
            return true;
        }
        foreach (var item in disabled)
        {
            item.Collidable = false;
        }
        Collidable = cl;
        return false;
    }

    public bool TryActorWiggleUp(Entity actor)
    {
        bool collidable = Collidable;
        Collidable = true;
        for (int i = 1; i <= 4; i++)
        {
            if (!actor.CollideCheck<Solid>(actor.Position - Vector2.UnitY * i))
            {
                actor.Position -= Vector2.UnitY * i;
                Collidable = collidable;
                return true;
            }
        }

        Collidable = collidable;
        return false;
    }


    public bool MasterOfGroup;
    public bool HasGroup;
    public List<NewAppearingBlock> group;
    public Dictionary<Platform, Vector2> Moves;

    public TileGrid tiles;

    public Point GroupBoundsMin;
    public Point GroupBoundsMax;

    public void FindInGroup(NewAppearingBlock block)
    {
        block.HasGroup = true;
        //block.OnDashCollide = OnDash;
        group.Add(block);
        Moves.Add(block, block.Position);
        foreach (NewAppearingBlock entity in base.Scene.Tracker.GetEntities<NewAppearingBlock>())
        {
            if (entity != this && entity != block && ((entity.onNode && entity.DisappearMode == this.DisappearMode && entity.count == this.count)) && (entity.CollideRect(new Rectangle((int)block.X - 1, (int)block.Y, (int)block.Width + 2, (int)block.Height)) || entity.CollideRect(new Rectangle((int)block.X, (int)block.Y - 1, (int)block.Width, (int)block.Height + 2))) && !group.Contains(entity))
            {
                //group.Add(entity);
                FindInGroup(entity);
                entity.group = group;
            }
        }
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);
        if (!HasGroup)
        {
            MasterOfGroup = true;
            Moves = new Dictionary<Platform, Vector2>();
            group = new List<NewAppearingBlock>();
            //Jumpthrus = new List<JumpThru>();
            GroupBoundsMin = new Point((int)base.X, (int)base.Y);
            GroupBoundsMax = new Point((int)base.Right, (int)base.Bottom);
            FindInGroup(this);
            _ = base.Scene;
            Rectangle rectangle = new Rectangle(GroupBoundsMin.X / 8, GroupBoundsMin.Y / 8, (GroupBoundsMax.X - GroupBoundsMin.X) / 8 + 1, (GroupBoundsMax.Y - GroupBoundsMin.Y) / 8 + 1);
            VirtualMap<char> virtualMap = new VirtualMap<char>(rectangle.Width, rectangle.Height, '0');
            foreach (NewAppearingBlock item in group)
            {
                int num = (int)(item.X / 8f) - rectangle.X;
                int num2 = (int)(item.Y / 8f) - rectangle.Y;
                int num3 = (int)(item.Width / 8f);
                int num4 = (int)(item.Height / 8f);
                for (int i = num; i < num + num3; i++)
                {
                    for (int j = num2; j < num2 + num4; j++)
                    {
                        virtualMap[i, j] = tileType;
                    }
                }
            }

            tiles = GFX.FGAutotiler.GenerateMap(virtualMap, new Autotiler.Behaviour
            {
                EdgesExtend = false,
                EdgesIgnoreOutOfLevel = false,
                PaddingIgnoreOutOfLevel = false
            }).TileGrid;
            tiles.Position = new Vector2((float)GroupBoundsMin.X - base.X, (float)GroupBoundsMin.Y - base.Y);
            Add(tiles);
        }
    }
}