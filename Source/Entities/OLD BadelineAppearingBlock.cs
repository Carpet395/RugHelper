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

[Tracked]
public class apearblockManager : Entity
{
    public apearblockManager()
        : base()
    {
        base.Tag = Tags.Global | Tags.Persistent;
        instance = this;
    }

    public static Level level
    {
        get
        {
            return Engine.Scene as Level;
        }
    }

    public static apearblockManager instance;

    public static VirtualMap<char> Map { set { map = value; } get { return map == null ? null : map;  } }

    private static VirtualMap<char> map = null; //= level.SolidsData;

    private bool initialized = false;

    private VirtualMap<char> template;

    public override void Awake(Scene scene)
    {
        base.Awake(scene);
        Level level = scene as Level;
        Rectangle tileBounds = level.Session.MapData.TileBounds;
        //if (template == null)
        //{
        VirtualMap<char> solidsData = level.SolidsData;
            Map = new VirtualMap<char>(solidsData.Columns, solidsData.Rows, '0');
            current = 0;
            for (int tx = 0; tx < solidsData.Columns; tx++)
                for (int ty = 0; ty < solidsData.Rows; ty++)
                    Map[tx, ty] = solidsData[tx, ty];
            template = Map;
        //}
        foreach (AppearingBlock i in Scene.Entities.FindAll<AppearingBlock>())
        {
            if ((i.DisappearMode && i.count <= current) || ((i.onNode && i.count > current) && i.count != 0 && !i.DisappearMode) || (!i.onNode && !i.Collidable)) continue;
            //if (block.onNode &&)
            //{
            int bx = (int)i.X / 8 - tileBounds.Left;
            int by = (int)i.Y / 8 - tileBounds.Top;
            int bw = (int)i.Width / 8;
            int bh = (int)i.Height / 8;
            for (int xx = 0; xx < bw; xx++)
                for (int yy = 0; yy < bh; yy++)
                    Map[bx + xx, by + yy] = i.tileType;
            //}
        }

    }

    public override void Update()
    {
        base.Update();
        if (!initialized)
        {
            initialized = true;
            //lastNode = GetNode() - 1;
            return;
        }
        current = GetNode();
        List<AppearingBlock> blocks = Engine.Scene.Entities.FindAll<AppearingBlock>();
        if (blocks.Count == 0) { instance = null; RemoveSelf(); return; }
        //apearblockManager goober = Engine.Scene.Entities.FindFirst<apearblockManager>();
        ///else if (goober != null && goober != ) { instance = null; RemoveSelf(); }
        Rectangle tileBounds = level.Session.MapData.TileBounds;
        //VirtualMap<char> map;
        if (current != lastNode && current != -1)
        {
            Map = template;
            foreach (AppearingBlock i in Scene.Entities.FindAll<AppearingBlock>())
            {
                if ((i.DisappearMode && i.count <= current) || ((i.onNode && i.count > current) && i.count != 0 && !i.DisappearMode) || (!i.onNode && !i.Collidable)) continue;
                //if (block.onNode &&)
                //{
                int bx = (int)i.X / 8 - tileBounds.Left;
                int by = (int)i.Y / 8 - tileBounds.Top;
                int bw = (int)i.Width / 8;
                int bh = (int)i.Height / 8;
                for (int xx = 0; xx < bw; xx++)
                    for (int yy = 0; yy < bh; yy++)
                        Map[bx + xx, by + yy] = i.tileType;
                //}
            }
            //if (current == -1) current = apearblockManager.current;
        }
        foreach (var i in blocks)
        {
            if (i.onNode)
            {
                if (current == lastNode) continue;

                if (i.DisappearMode)
                {
                    if (i.count == current) Add(new Coroutine(i.waitForDie(false)));
                    else if (i.count > current) i.RebuildOverlay(false, false, level, current, Map);
                }
                else
                {
                    if (i.count == current) Add(new Coroutine(i.waitForClear(false, Map)));
                    else if (i.count < current) i.RebuildOverlay(false, false, level, current, Map);
                }
            }
            else
            {
                if (i.BlockedCheck<FinalBoss>(true)) Add(new Coroutine(i.waitForClear(false, Map)));
            }
        }
        lastNode = current;
    }
    public static int current;
    public int lastNode = 0;

    //public void Rebuild()
    //{
        //bool hm = GetNodeMatch(lastNode, out int current);
        //Logger.Log(LogLevel.Info, hm.ToString(), current.ToString());
        //Level level = Engine.Scene as Level;
        //if (!hm)
        //{
            //foreach (var i in Engine.Scene.Entities.FindAll<AppearingBlock>())
            //{
                //if ((i.DisappearMode && i.count < current) || (!i.DisappearMode && i.onNode && i.count >= current) || (!i.onNode && i.Collidable))
                //{
                    //i.RebuildOverlay(true, true, level, current);
                //}
            //}
        //}
        //lastNode = current;
    //}

    public static int GetNode()
    {
        //Currentnode = -1;
        //foreach (var i in Engine.Scene.Entities.FindAll<FinalBoss>())
        //{
        //if (bossData == null)
        //{
        //bossData = new DynamicData(i);
        //}
        //dynamiDynamicData.For(i); localbossData = bossData[i]; }
        FinalBoss goober = Engine.Scene.Entities.FindFirst<FinalBoss>();
        if (goober != null)
        {
            DynamicData localbossData = DynamicData.For(goober);
            //var field = typeof(FinalBoss).GetField("nodeIndex", BindingFlags.Instance | BindingFlags.NonPublic);
            //int index = (int)field.GetValue(i);
            int index = localbossData.Get<int>("nodeIndex"); //.nodeIndex;
            return index;
        }
        return -1;
            //if (index == toMatch) { Currentnode = index; return true; }
        //}
        //Currentnode = -1;
        //return false;
    }

}


[Tracked()]
//[CustomEntity("Rug/BadAppearBlock")]
public class AppearingBlock : Solid
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

    public AppearingBlock(Vector2[] nodes, float width, float height, char tileType, string flag, bool state, bool onNode = false, int count = 0, bool disappearMode = false)
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

    public AppearingBlock(EntityData data, Vector2 offset)
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
        //if ((BlockedCheck<FinalBoss>(true) && !onNode) || (onNode && hm))
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



    public void RebuildOverlay(bool outsider = false, bool ignoreCollide = false, Level level = null, int current = -1, VirtualMap<char> map = null)
    {
        //if ((!Collidable && !ignoreCollide))
            //return;
        if (level == null) level = Scene as Level;
        Rectangle tileBounds = level.Session.MapData.TileBounds;
        if (map == null)
        {

            VirtualMap<char> solidsData = level.SolidsData;
            map = new VirtualMap<char>(solidsData.Columns, solidsData.Rows, '0');

            for (int tx = 0; tx < solidsData.Columns; tx++)
                for (int ty = 0; ty < solidsData.Rows; ty++)
                    map[tx, ty] = solidsData[tx, ty];
            if (current == -1) current = apearblockManager.current;
            foreach (AppearingBlock block in Scene.Entities.FindAll<AppearingBlock>())
            {
                if ((block.DisappearMode && block.count >= current) || (block.onNode && block.count > current) || (!block.onNode && !block.Collidable)) continue;
                //if (block.onNode &&)
                //{
                int bx = (int)block.X / 8 - tileBounds.Left;
                int by = (int)block.Y / 8 - tileBounds.Top;
                int bw = (int)block.Width / 8;
                int bh = (int)block.Height / 8;
                for (int xx = 0; xx < bw; xx++)
                    for (int yy = 0; yy < bh; yy++)
                        map[bx + xx, by + yy] = tileType;
                //}
            }
        }

        int x = (int)X / 8 - tileBounds.Left;
        int y = (int)Y / 8 - tileBounds.Top;

        // Regenerate autotiler overlay
        TileGrid newTile = GFX.FGAutotiler.GenerateOverlay(tileType, x, y, (int)(Width / 8), (int)(Height / 8), map).TileGrid;
        if (sprite == null)
        {
            sprite = newTile;
            sprite.Position = Vector2.Zero;
            sprite.Alpha = outsider? 1 : 0;
            Add(sprite);
            Add(new TileInterceptor(sprite, highPriority: true));
            sprite.Visible = true;
            sprite.Active = true;
        }
        else
        {
            sprite.Tiles = newTile.Tiles;
        }
        //JustRebuilt = outsider ? true : JustRebuilt;
    }

    public IEnumerator waitForDie(bool shakeExtra)
    {
        //sprite.Visible = true;
        //sprite.Active = true;
        //TargetAlpha = 0.75f;
        //yield return null;
        if (Flag != "")
        {
            SceneAs<Level>().Session.SetFlag(Flag, OnFlag);
        }
        TargetAlpha = 0f;
        //if (count == 0) sprite.Alpha = 0f;
        Collidable = false;
        /*Rectangle rect = new Rectangle((int)X - 8, (int)Y - 8, (int)Width + 16, (int)Height + 16);
        foreach (AppearingBlock other in Scene.Entities.FindAll<AppearingBlock>())
        {
            if (other == this || other.sprite == null)
                continue;

            // this can be used to make the tiles node sensitive
            //bool connect =
            //(!onNode && !other.onNode) ||                   // both are global
            //(!onNode && other.onNode) ||                    // this is global
            //(onNode && !other.onNode) ||                    // other is global
            //(onNode && other.onNode && other.count == count); // both onNode and same count

            //bool connect = true;
            //if (other.Lastcount < cool)
            //{
            //other.Lastcount = cool;
            //other.JustRebuilt = false;
            //}
            // this calls all the tiles that are neighbours (should at least)
            if (other.Collidable &&
                other.CollideRect(rect))
            {
                other.RebuildOverlay(false);
            }
        }*/
        while (sprite.Alpha != 0) yield return null;
        RemoveSelf();
    }

    public IEnumerator waitForClear(bool shakeExtra, VirtualMap<char> map)
    {
        //int tilesX = (int)(Width / 8);
        //int tilesY = (int)(Height / 8);

        //Level level = Scene as Level;
        /*Rectangle tileBounds = level.Session.MapData.TileBounds;
        VirtualMap<char> solidsData = level.SolidsData;
        VirtualMap<char> map = new VirtualMap<char>(solidsData.Columns, solidsData.Rows, '0');
        //for (int tx = 0; tx < solidsData.Columns; tx++)
        //for (int ty = 0; ty < solidsData.Rows; ty++)
        //map[tx, ty] = solidsData[tx, ty];

        // we are now generating the sprite for the first time

        // this isnt needed 
        /*foreach (AppearingBlock block in Scene.Entities.FindAll<AppearingBlock>())
        {
            if (block.tileType == tileType)
            {
                if (block.Appear)
                {
                    int bx = (int)block.X / 8 - tileBounds.Left;
                    int by = (int)block.Y / 8 - tileBounds.Top;
                    int bw = (int)block.Width / 8;
                    int bh = (int)block.Height / 8;
                    for (int xx = 0; xx < bw; xx++)
                        for (int yy = 0; yy < bh; yy++)
                            map[bx + xx, by + yy] = tileType;
                }
            }
        }
        int x = (int)X / 8 - tileBounds.Left;
        int y = (int)Y / 8 - tileBounds.Top;
        sprite = GFX.FGAutotiler.GenerateOverlay(tileType, x, y, tilesX, tilesY, map).TileGrid;
        sprite.Position = Vector2.Zero;
        sprite.Alpha = 0;
        Add(sprite);
        Add(new TileInterceptor(sprite, highPriority: true));
        sprite.Visible = true;
        sprite.Active = true;*/
        //yield return null;
        while (map == null)
        {
            map = apearblockManager.Map;
            yield return null;
        }
        if (sprite == null) RebuildOverlay(false, true, null, apearblockManager.current, map);
        //Rebuild(
        TargetAlpha = 0.25f;
        if ((count == 0 && onNode) || DisappearMode) sprite.Alpha = 0.25f;
        //fade = true;
        //Add(new Coroutine(ChangeAlpha(0f, 0.25f, 0.25f)));
        //sprite.Color = new Color(1, 1, 1, 0.25f);
        // now it should all connect
        yield return null;
        //RebuildOverlay();
        //while (BlockedCheck(true)) //|| BlockedCheck<FinalBoss>(true))
        //{
            //StartShaking(0.05f);
            //yield return 0.05f;
        //}
        //SceneAs<Level>().Shake(0.25f);
        //StartShaking(shakeExtra? 1f : 0.5f);
        if (Flag != "" && !DisappearMode)
        {
            if (Scene != null)
            SceneAs<Level>().Session.SetFlag(Flag, OnFlag);
        }
        //sprite.Color = new Color(1, 1, 1, 1f);
        //Add(new Coroutine(ChangeAlpha(0.25f, 1, 0.75f)));
        TargetAlpha = 1f;
        if ((count == 0 && onNode) || DisappearMode) sprite.Alpha = 1f;
        //fade = true;
        Collidable = true;
        yield return null;
        //Rebuild();
        //GetNodeMatch(0, out int cool);
        //Rectangle rect = new Rectangle((int)X - 8, (int)Y - 8, (int)Width + 16, (int)Height + 16);
        //Rebuild();
        /*foreach (AppearingBlock other in Scene.Entities.FindAll<AppearingBlock>())
        {
            if (other == this || other.sprite == null)
                continue;

            // this can be used to make the tiles node sensitive
            //bool connect =
            //(!onNode && !other.onNode) ||                   // both are global
            //(!onNode && other.onNode) ||                    // this is global
            //(onNode && !other.onNode) ||                    // other is global
            //(onNode && other.onNode && other.count == count); // both onNode and same count

            bool connect = true;
            if (other.Lastcount < cool)
            {
                other.Lastcount = cool;
                other.JustRebuilt = false;
            }
            // this calls all the tiles that are neighbours (should at least)
            if (connect &&
                other.Collidable &&
                other.CollideRect(rect))
            {
                other.RebuildOverlay(false);
            }
        }
        // to make sure it all connects
        RebuildOverlay();*/
        if ((count == 0 && onNode) || DisappearMode) sprite.Alpha = 1f;
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

    public override void Awake(Scene scene)
    {

        base.Awake(scene);

        if (scene.Entities.FindFirst<apearblockManager>() == null)
        {
            //apearblockManager a;
            scene.Add(new apearblockManager());
            //apearblockManager.Map = (scene as Level).SolidsData;
        }

        //sine = new SineWave(freq, 1);
        if (bossData == null)
        {
            bossData = new Dictionary<FinalBoss, DynamicData>();
            foreach (var i in Engine.Scene.Entities.FindAll<FinalBoss>())
            {
                //if (bossData == null)
                //{
                //bossData = new DynamicData(i);
                //}
                bossData[i] = DynamicData.For(i);
                //var field = typeof(FinalBoss).GetField("nodeIndex", BindingFlags.Instance | BindingFlags.NonPublic);
                //int index = (int)field.GetValue(i);
                //int index = localbossData.Get<int>("nodeIndex"); //.nodeIndex;
                //if (index == toMatch) { Currentnode = index; return true; }
            }
        }
        if (!((count == 0 && onNode) || DisappearMode) || !onNode)
        {
            Collidable = false;
            if (sprite != null)
            {
                sprite.Visible = false;
                sprite.Active = false;
            }
        }
        //else
        //{
        //RebuildOverlay();
        //}
        //bool hm = GetNodeMatch(count, out int nodes);
        if (DisappearMode || (count == 0 && onNode))
            Add(new Coroutine(waitForClear(DisappearMode, apearblockManager.Map)));
        //else if (sprite != null && DisappearMode && ((BlockedCheck<FinalBoss>(true) && !onNode) || (onNode && hm)))
            //Add(new Coroutine(waitForDie(onNode && hm)));
    }
}