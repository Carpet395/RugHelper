using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;


namespace Celeste.Mod.Rug.Entities;

[Tracked()]
[CustomEntity("Rug/BadAppearBlock")]
public class NewAppearingBlock : Solid
{

    // -- idk how tf does it work but i know it does so yeppe!! -- //

    // tile sprites
    //public TileGrid sprite;
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

    public override void DebugRender(Camera camera)
    {
        base.DebugRender(camera);

        if (Collider != null)
        {
            Collider.Render(camera, TrueMasterOfGroup ? Collidable ? Color.Cyan : Color.DarkCyan : MasterOfGroup ? Collidable ? Color.Gold : Color.DarkGoldenrod : Collidable ? Color.Red : Color.DarkRed);
        }
    }

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
        if (MasterOfGroup)
        {
            if (onNode)
            {
                if (GetNodeMatch(count, out int nodes) && Lastcount != nodes)
                {
                    if (DisappearMode)
                    {
                        if (tiles != null) tiles.RemoveSelf();
                        foreach (NewAppearingBlock item in group)
                        {
                            item.Collidable = false;
                        }
                    }
                    else if (!DisappearMode)
                    {
                        GroupBoundsMin = new Point((int)base.X, (int)base.Y);
                        GroupBoundsMax = new Point((int)base.Right, (int)base.Bottom);
                        if (nodes == count)
                        {
                            //if (tiles != null) tiles.RemoveSelf();
                            Actualgroup = new List<List<NewAppearingBlock>>();
                            FindInGroupDiffrent(this, this);
                        }
                        foreach (List<NewAppearingBlock> agroup in Actualgroup)
                        {
                            foreach (NewAppearingBlock i in agroup)
                            {
                                if (i.Y < GroupBoundsMin.Y) GroupBoundsMin.Y = (int)i.Y;
                                if (i.X < GroupBoundsMin.X) GroupBoundsMin.X = (int)i.X;
                                if (i.X + i.Width > GroupBoundsMax.X) GroupBoundsMax.X = (int)(i.X + i.Width);
                                if (i.Y + i.Height > GroupBoundsMax.Y) GroupBoundsMax.Y = (int)(i.Y + i.Height);
                            }
                        }
                        Rectangle rectangle = new Rectangle(GroupBoundsMin.X / 8, GroupBoundsMin.Y / 8, (GroupBoundsMax.X - GroupBoundsMin.X) / 8 + 1, (GroupBoundsMax.Y - GroupBoundsMin.Y) / 8 + 1);
                        VirtualMap<char> virtualMap = new VirtualMap<char>(rectangle.Width, rectangle.Height, '0');
                        foreach (List<NewAppearingBlock> agroup in Actualgroup)
                        {
                            foreach (NewAppearingBlock item in agroup)
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
                        }

                        tiles = GFX.FGAutotiler.GenerateMap(virtualMap, new Autotiler.Behaviour
                        {
                            EdgesExtend = false,
                            EdgesIgnoreOutOfLevel = false,
                            PaddingIgnoreOutOfLevel = false
                        }).TileGrid;
                        tiles.Position = new Vector2((float)GroupBoundsMin.X - base.X, (float)GroupBoundsMin.Y - base.Y);
                        Add(tiles);
                        if (nodes == count)
                        {
                            tiles.Alpha = 0;
                            TargetAlpha = 1;
                        }
                        foreach (NewAppearingBlock item in group)
                        {
                            item.Collidable = true;
                        }
                    }

                    if (Flag != "")
                    {
                        SceneAs<Level>().Session.SetFlag(Flag, OnFlag);
                    }
                }
                Lastcount = nodes;
            }
            else
            {
                if (BlockedCheck<FinalBoss>(false) || BlockedCheck<FlagBadeline>(false))
                {
                    if (Flag != "")
                    {
                        SceneAs<Level>().Session.SetFlag(Flag, OnFlag);
                    }
                    GroupBoundsMin = new Point((int)base.X, (int)base.Y);
                    GroupBoundsMax = new Point((int)base.Right, (int)base.Bottom);
                    foreach (NewAppearingBlock i in group)
                    {
                        if (i.Y < GroupBoundsMin.Y) GroupBoundsMin.Y = (int)i.Y;
                        if (i.X < GroupBoundsMin.X) GroupBoundsMin.X = (int)i.X;
                        if (i.X + i.Width > GroupBoundsMax.X) GroupBoundsMax.X = (int)(i.X + i.Width);
                        if (i.Y + i.Height > GroupBoundsMax.Y) GroupBoundsMax.Y = (int)(i.Y + i.Height);
                    }
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
                    tiles.Alpha = 0.5f;
                    TargetAlpha = 1;
                    foreach (NewAppearingBlock item in group)
                    {
                        item.Collidable = true;
                    }
                }
            }
        }


        if (tiles != null)
        {

            fade = tiles.Alpha != TargetAlpha;
            //Logger.Log(LogLevel.Info, "neiw", fade.ToString());
            if (fade)
            {

                //sprite.Alpha = a;
                //while (current != to)
                //{
                //CassetteBlock
                tiles.Alpha = Calc.Approach(tiles.Alpha, TargetAlpha, Engine.DeltaTime);
                StartShaking(Engine.DeltaTime);
                if (tiles.Alpha == TargetAlpha)
                {
                    fade = false;
                    //tiles.Position = new Vector2((float)GroupBoundsMin.X - base.X, (float)GroupBoundsMin.Y - base.Y);
                }
                //sprite.Alpha = current;
                //yield return null;
                //}
                //sprite.Alpha = to;
            }
            else
            {
                //tiles.Position = new Vector2((float)GroupBoundsMin.X - base.X, (float)GroupBoundsMin.Y - base.Y);
            }
            foreach (TileGrid i in Components.GetAll<TileGrid>().ToList<TileGrid>())
            {
                if (i != tiles) Remove(i);
            }
        }
        base.Update();
    }

    public bool GetNodeMatch(int target_node, out int current_node)
    {
        int node = -1;
        foreach (var i in Scene.Entities.FindAll<FinalBoss>())
        {
            if (i.Active)
            {
                node = DynamicData.For(i).Get<int>("nodeIndex");
                break;
            }
        }
        current_node = node;
        return node == -1? false : node == target_node;
    }


    public override void Render()
    {
        if (tiles != null && !fade) tiles.Position = new Vector2((float)GroupBoundsMin.X - base.X, (float)GroupBoundsMin.Y - base.Y);
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
        if (fade) tiles.Position = new Vector2((float)GroupBoundsMin.X - base.X, (float)GroupBoundsMin.Y - base.Y) + amount;
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

    public bool BlockedCheckAlt<T>(bool doShit = false) where T : FinalBoss
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
    public bool TrueMasterOfGroupEnd;
    public bool TrueMasterOfGroup { get
        {
            return trueMasterOfGroup;
        } 
        set
        {
            if (!TrueMasterOfGroupEnd)
            {
                trueMasterOfGroup = value;
                TrueMasterOfGroupEnd = true;
            }
        } 
    }
    private bool trueMasterOfGroup;
    public bool HasGroup;
    public bool HasTrueGroup;
    public List<NewAppearingBlock> group;
    public List<List<NewAppearingBlock>> Actualgroup;
    public List<NewAppearingBlock> Truegroup;
    public Dictionary<Platform, Vector2> Moves;

    public TileGrid tiles;

    public Point GroupBoundsMin;
    public Point GroupBoundsMax;

    public void FindInGroup(NewAppearingBlock block, NewAppearingBlock orig)
    {
        block.HasGroup = true;
        //block.OnDashCollide = OnDash;
        group.Add(block);
        Moves.Add(block, block.Position);
        foreach (NewAppearingBlock entity in base.Scene.Tracker.GetEntities<NewAppearingBlock>())
        {
            //if (entity != this && entity != block && entity != orig && ((entity.onNode == orig.onNode == true && entity.DisappearMode == orig.DisappearMode) || (entity.onNode == orig.onNode == false)) && (entity.CollideRect(new Rectangle((int)block.X - 1, (int)block.Y, (int)block.Width + 2, (int)block.Height)) || entity.CollideRect(new Rectangle((int)block.X, (int)block.Y - 1, (int)block.Width, (int)block.Height + 2))))
            //{
                //entity.TrueMasterOfGroupEnd = true;
                //entity.TrueMasterOfGroup = false;
            //}
            if (entity != this && entity != block && ((entity.onNode && entity.DisappearMode == this.DisappearMode && (entity.count == this.count))) && (entity.CollideRect(new Rectangle((int)block.X - 1, (int)block.Y, (int)block.Width + 2, (int)block.Height)) || entity.CollideRect(new Rectangle((int)block.X, (int)block.Y - 1, (int)block.Width, (int)block.Height + 2))) && !group.Contains(entity))
            {
                group.Add(entity);
                entity.group = group;
                FindInGroup(entity, orig);
            }
        }
    }
    public void FindInGroupTrueMaster(NewAppearingBlock block, NewAppearingBlock orig)
    {
        block.HasTrueGroup = true;
        //block.OnDashCollide = OnDash;
        Truegroup.Add(block);
        //Moves.Add(block, block.Position);
        foreach (NewAppearingBlock entity in base.Scene.Tracker.GetEntities<NewAppearingBlock>())
        {
            if (entity != this && entity != block && entity != orig && ((entity.onNode == orig.onNode == true && entity.DisappearMode == orig.DisappearMode) || (entity.onNode == orig.onNode == false)) && (entity.CollideRect(new Rectangle((int)block.X - 1, (int)block.Y, (int)block.Width + 2, (int)block.Height)) || entity.CollideRect(new Rectangle((int)block.X, (int)block.Y - 1, (int)block.Width, (int)block.Height + 2))) && !Truegroup.Contains(entity))
            {
                //entity.TrueMasterOfGroupEnd = true;
                entity.TrueMasterOfGroup = false;
                Truegroup.Add(entity);
                entity.Truegroup = Truegroup;
                orig.Truegroup = Truegroup;
                FindInGroupTrueMaster(entity, orig);
            }
        }
    }


    public void FindInGroupDiffrent(NewAppearingBlock block, NewAppearingBlock orig)
    {
        block.HasGroup = true;
        //block.OnDashCollide = OnDash;
        orig.Actualgroup.Add(block.group);
        //orig.Moves.TryAdd(block, block.Position);
        foreach (NewAppearingBlock entity in orig.Truegroup)
        {
            if (entity != orig && ((entity.onNode && entity.DisappearMode == orig.DisappearMode && (entity.count <= orig.count))) && (entity.group != null && !orig.Actualgroup.Contains(entity.group)))
            {
                if (entity.tiles != null && entity != orig)
                {
                    entity.Depth = orig.Depth + 1;
                    //entity.tiles.RemoveSelf();
                }
                /*if (entity.MasterOfGroup)
                {
                    entity.MasterOfGroup = false;
                }*/
                orig.Actualgroup.Add(entity.group);
                entity.Actualgroup = orig.Actualgroup;
                block.Actualgroup = orig.Actualgroup;
                //FindInGroupDiffrent(entity, orig);
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
            TrueMasterOfGroup = true;
            //TrueMasterOfGroupEnd = true;
            if (!HasTrueGroup)
            {
                Truegroup = new List<NewAppearingBlock>();
                FindInGroupTrueMaster(this, this);
            }
            FindInGroup(this, this);
            _ = base.Scene;
            if (onNode && (count == 0 || DisappearMode))
            {
                foreach (NewAppearingBlock i in group)
                {
                    if (i.Y < GroupBoundsMin.Y) GroupBoundsMin.Y = (int)i.Y;
                    if (i.X < GroupBoundsMin.X) GroupBoundsMin.X = (int)i.X;
                    if (i.X + i.Width > GroupBoundsMax.X) GroupBoundsMax.X = (int)(i.X + i.Width);
                    if (i.Y + i.Height > GroupBoundsMax.Y) GroupBoundsMax.Y = (int)(i.Y + i.Height);
                }
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
                tiles.Alpha = 1.0f;
                TargetAlpha = 1f;
            }
            else if (!onNode || (onNode && !DisappearMode))
            {
                foreach (NewAppearingBlock item in group)
                {
                    item.Collidable = false;
                }
            }
        }
    }
}