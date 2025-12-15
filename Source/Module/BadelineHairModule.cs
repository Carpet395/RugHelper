using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.Rug.Module;

[Tracked(false)]
public class BadelineHairModule : Component
{
    public const string Hair = "characters/player/hair00";

    public Color Color = Player.NormalHairColor;

    public Color Border = Color.Black;

    public float Alpha = 1f;

    public Facings Facing;

    public bool DrawPlayerSpriteOutline;

    public bool SimulateMotion = true;

    public Vector2 StepPerSegment = new Vector2(0f, 2f);

    public float StepInFacingPerSegment = 0.5f;

    public float StepApproach = 64f;

    public float StepYSinePerSegment;

    public BadelineSpriteModule Sprite;

    public List<Vector2> Nodes = new List<Vector2>();

    private List<MTexture> bangs = GFX.Game.GetAtlasSubtextures("characters/player/bangs");

    private float wave;

    public BadelineHairModule(BadelineSpriteModule sprite)
        : base(active: true, visible: true)
    {
        Sprite = sprite;
        for (int i = 0; i < sprite.HairCount; i++)
        {
            Nodes.Add(Vector2.Zero);
        }
    }

    public void Start()
    {
        Vector2 value = base.Entity.Position + new Vector2((0 - Facing) * 200, 200f);
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i] = value;
        }
    }

    public void AfterUpdate()
    {
        Vector2 vector = Sprite.HairOffset * new Vector2((float)Facing, 1f);
        Nodes[0] = Sprite.RenderPosition + new Vector2(0f, -9f * Sprite.Scale.Y) + vector;
        Vector2 target = Nodes[0] + new Vector2((float)(0 - Facing) * StepInFacingPerSegment * 2f, (float)Math.Sin(wave) * StepYSinePerSegment) + StepPerSegment;
        Vector2 vector2 = Nodes[0];
        float num = 3f;
        for (int i = 1; i < Sprite.HairCount; i++)
        {
            if (i >= Nodes.Count)
            {
                Nodes.Add(Nodes[i - 1]);
            }
            if (SimulateMotion)
            {
                float num2 = (1f - (float)i / (float)Sprite.HairCount * 0.5f) * StepApproach;
                Nodes[i] = Calc.Approach(Nodes[i], target, num2 * Engine.DeltaTime);
            }
            if ((Nodes[i] - vector2).Length() > num)
            {
                Nodes[i] = vector2 + (Nodes[i] - vector2).SafeNormalize() * num;
            }
            target = Nodes[i] + new Vector2((float)(0 - Facing) * StepInFacingPerSegment, (float)Math.Sin(wave + (float)i * 0.8f) * StepYSinePerSegment) + StepPerSegment;
            vector2 = Nodes[i];
        }
    }

    public override void Update()
    {
        wave += Engine.DeltaTime * 4f;
        base.Update();
    }

    public void MoveHairBy(Vector2 amount)
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i] += amount;
        }
    }

    public override void Render()
    {
        if (!Sprite.HasHair)
        {
            return;
        }
        Vector2 origin = new Vector2(5f, 5f);
        Color color = Border * Alpha;
        Color color2 = Color * Alpha;
        if (DrawPlayerSpriteOutline)
        {
            Color color3 = Sprite.Color;
            Vector2 position = Sprite.Position;
            Sprite.Color = color;
            Sprite.Position = position + new Vector2(0f, -1f);
            Sprite.Render();
            Sprite.Position = position + new Vector2(0f, 1f);
            Sprite.Render();
            Sprite.Position = position + new Vector2(-1f, 0f);
            Sprite.Render();
            Sprite.Position = position + new Vector2(1f, 0f);
            Sprite.Render();
            Sprite.Color = color3;
            Sprite.Position = position;
        }
        Nodes[0] = Nodes[0].Floor();
        if (color.A > 0)
        {
            for (int i = 0; i < Sprite.HairCount; i++)
            {
                int hairFrame = Sprite.HairFrame;
                MTexture obj = ((i == 0) ? bangs[hairFrame] : GFX.Game["characters/player/hair00"]);
                Vector2 hairScale = GetHairScale(i);
                obj.Draw(Nodes[i] + new Vector2(-1f, 0f), origin, color, hairScale);
                obj.Draw(Nodes[i] + new Vector2(1f, 0f), origin, color, hairScale);
                obj.Draw(Nodes[i] + new Vector2(0f, -1f), origin, color, hairScale);
                obj.Draw(Nodes[i] + new Vector2(0f, 1f), origin, color, hairScale);
            }
        }
        for (int num = Sprite.HairCount - 1; num >= 0; num--)
        {
            int hairFrame2 = Sprite.HairFrame;
            ((num == 0) ? bangs[hairFrame2] : GFX.Game["characters/player/hair00"]).Draw(Nodes[num], origin, color2, GetHairScale(num));
        }
    }

    private Vector2 GetHairScale(int index)
    {
        float num = 0.25f + (1f - (float)index / (float)Sprite.HairCount) * 0.75f;
        return new Vector2(((index == 0) ? ((float)Facing) : num) * Math.Abs(Sprite.Scale.X), num);
    }
}
