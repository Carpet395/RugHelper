using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.Rug.Entities;

[Tracked(false)]
[CustomEntity("Rug/oscBlock")]
public class oscillatingBlock : Solid
{

    // -- ill finish making this one later lol -- //

    // tile sprites
    public TileGrid sprite;
    public TileGrid highlight;

    // nodex
    public Vector2[] nodes;

    // sine wave
    public SineWave sine;

    public float freq;
    public float peak;

    // tile
    public char tileType;
    public char HightileType;

    // flag logic
    public string Flag;
    public bool OnFlag;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public oscillatingBlock(Vector2[] nodes, float width, float height, string Flag, bool OnFlag, char tileType, char highlightTileType, float freq)
        : base(nodes[0], width, height, safe: false)
    {
        //BossNodeIndex = bossNodeIndex;
        this.freq = freq;
        peak = 1f;
        this.nodes = nodes;
        int newSeed = Calc.Random.Next();
        Calc.PushRandom(newSeed);
        sprite = GFX.FGAutotiler.GenerateBox(tileType, (int)base.Width / 8, (int)base.Height / 8).TileGrid;
        Add(sprite);
        Calc.PopRandom();
        Calc.PushRandom(newSeed);
        highlight = GFX.FGAutotiler.GenerateBox(highlightTileType, (int)(base.Width / 8f), (int)base.Height / 8).TileGrid;
        highlight.Alpha = 0f;
        Add(highlight);
        Calc.PopRandom();
        Add(new TileInterceptor(sprite, highPriority: false));
        Add(new LightOcclude());
    }

    public override void Awake(Scene scene)
    {
        sine = new SineWave(freq, 1);
        base.Awake(scene);
    }

    /*[MethodImpl(MethodImplOptions.NoInlining)]
    public oscillatingBlock(EntityData data, Vector2 offset)
        : this(data.NodesWithPosition(offset), data.Width, data.Height, data.String("Flag"), data.Bool("On"), data.String("tileType"))
    {
    }*/
}