using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Celeste.Mod.Entities;
using CelesteMod.Publicizer;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.Partline.Entities;

[CustomEntity("Rug/invisBarrier")]
public class InvisibleBarrier : Solid
{

    // -- idk i dont need more from it -- //

    string Flag = "";
    bool State = true;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public InvisibleBarrier(Vector2 position, float width, float height, string flag, bool state)
        : base(position, width, height, safe: true)
    {
        this.Flag = flag;
        this.State = state;
        base.Tag = Tags.TransitionUpdate;
        Collidable = false;
        Visible = false;
        Add(new ClimbBlocker(edge: true));
        SurfaceSoundIndex = 33;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public InvisibleBarrier(EntityData data, Vector2 offset)
        : this(data.Position + offset, data.Width, data.Height, data.String("flag"), data.Bool("state"))
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public override void Update()
    {
        if (Flag == "" || (Engine.Scene as Level).Session.GetFlag(Flag) == State)
        {
            Collidable = true;
            if (CollideCheck<Player>())
            {
                Collidable = false;
            }

            if (!Collidable)
            {
                //Active = false;
            }
        }
        else
        {
            Collidable = false;
        }
    }
}