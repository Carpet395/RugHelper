using System;
using System.Collections;
using System.Collections.Generic;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using static MonoMod.InlineRT.MonoModRule;

namespace Celeste.Mod.Rug.Entities;

[CustomEntity("Rug/CustomBadelineFallingBlock")]
[TrackedAs(typeof(global::Celeste.FallingBlock))]
[Tracked(true)]
public class RugFallingBlock : FallingBlock
{
    /*public static ParticleType P_FallDustA = FallingBlock.P_FallDustA;

    public static ParticleType P_FallDustB = FallingBlock.P_FallDustB;

    public static ParticleType P_LandDust = FallingBlock.P_LandDust;

    public bool Triggered;

    public float FallDelay;

    private char TileType;

    private TileGrid tiles;

    private TileGrid highlight;



    private bool finalBoss = true;

    public bool HasStartedFalling { get; private set; }*/

    public TileGrid Ptiles;

    public TileGrid PHtiles;

    public Color TileColor;
    public Color HighlightColor;

    public RugFallingBlock(Vector2 position, char tile, char highlight, int width, int height, bool behind, string hexColorTiles, string hexColorHighlights)
    : base(position, 'g', width, height, true, behind, false)
    {
        TileColor = hexColorTiles.ToColor();
        HighlightColor = hexColorHighlights.ToColor();
        //base.finalBoss = true;
        DynamicData Ddata = DynamicData.For(this);
        Ptiles = Ddata.Get<TileGrid>("tiles");
        PHtiles = Ddata.Get<TileGrid>("highlight");
        Remove(Ptiles);
        Remove(PHtiles);
        int newSeed = Calc.Random.Next();
        Calc.PushRandom(newSeed);
        Add(Ptiles = GFX.FGAutotiler.GenerateBox(tile, width / 8, height / 8).TileGrid);
        Add(PHtiles = GFX.FGAutotiler.GenerateBox(highlight, width / 8, height / 8).TileGrid);
        PHtiles.Alpha = 0f;
        Ddata.Set("tiles", Ptiles);
        Ddata.Set("highlight", PHtiles);
        Calc.PopRandom();
        Ptiles.Color = TileColor;
        PHtiles.Color = HighlightColor;
    }

    public RugFallingBlock(EntityData data, Vector2 offset)
    : this(data.Position + offset, data.Char("tiletype", '3'), data.Char("HighlightTiletype", '3'), data.Width, data.Height, data.Bool("behind"), data.String("TileColor"), data.String("HighlightColor"))
    {
    }
    public static RugFallingBlock Load(EntityData data, Vector2 offset)
    {
        return new RugFallingBlock(data, offset);
    }
}
