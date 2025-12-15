//using Celeste.Mod.DJMapHelper.Entities;
//using System;
//using Celeste.Mod.DJMapHelper.Entities;
using System.Collections.Generic;
using System.Linq;
using Monocle;
using Microsoft.Xna.Framework;
using Celeste.Mod.Entities;
using MonoMod.RuntimeDetour;
using System.Collections;

namespace Celeste.Mod.Rug.Triggers;

[CustomEntity("Rug/ShaderOverrider")]
[Tracked]
public class ShaderOverrider : Trigger
{
    public List<Entity> entities;
    //public List<Solid> solids = new List<Solid>();
    public Vector2[] nodeslist;
    public bool multiples;
    public bool includePlayer;
    public bool patch;

    public ShaderOverrider(EntityData data, Vector2 offset, Vector2[] nodes, bool multiples, bool includePlayer, bool patch)
        : base(data, offset)
    {
        this.includePlayer = includePlayer;
        if (entities == null) entities = new List<Entity>();
        nodeslist = nodes;
        //nodeslist.RemoveAt(0);
        this.multiples = multiples;
        int z = 0;
        ColliderList colliders = new ColliderList(base.Collider);
        //colliders.Add(base.Collider);
        foreach (Vector2 i in nodeslist)
        {
            //Logger.Log(LogLevel.Info, "rug", i.ToString());
            //if (z != 0)
            //{
                Hitbox col = new(8f, 8f, i.X, i.Y);
                colliders.Add(col);
            //}
            //z++;
        }
        base.Collider = colliders;
        // On.Celeste.Level.AfterRender += hm;
    }
    

    public ShaderOverrider(EntityData data, Vector2 offset) 
        :this(data, offset, data.NodesOffset(-data.Position), data.Bool("multiples"), data.Bool("includePlayer"), data.Bool("lightingPatch"))
    {
        //On.Celeste.Level.AfterRender += hm;
    }

    /*public override void OnEnter(Player player)
    {
        base.OnEnter(player);

        foreach (var i in nodeslist)
        {
            foreach (Entity e in Scene)
            {
                if (!Collide.Check(this, e))
                    continue;
                if (!entities.Contains(e)) entities.Add(e);
                e.Visible = false;
                Logger.Log(LogLevel.Info, "Rug Helper", e.GetType().ToString());
                if (!multiples)
                    break;
            }
        }
        //On.Celeste.Glitch.Apply += hmmer;
    }*/
    public override void OnEnter(Player player)
    {
        base.OnEnter(player);
        Check(Scene);
    }

    public void Check(Scene scene)
    {
        foreach (Entity e in scene)
        {
            if (e == null) continue;
            if (e is Player || e is Trigger) continue;
            if (e.Collider == null || !Collide.Check(this, e))
            {
                foreach (Vector2 i in nodeslist)
                {
                    float boxLeft = i.X - 4f;
                    float boxRight = i.X + 4f;
                    float boxTop = i.Y - 4f;
                    float boxBottom = i.Y + 4f;

                    float entityLeft = e.Left;
                    float entityRight = e.Right;
                    float entityTop = e.Top;
                    float entityBottom = e.Bottom;
                    if (entityLeft <= boxRight && boxLeft <= entityRight && entityTop <= boxBottom && boxTop <= entityBottom)
                    {
                        Logger.Log(LogLevel.Info, "Rug Helper but cooler", e.GetType().ToString());
                        if (!entities.Contains(e)) entities.Add(e);
                        continue;
                    }
                    else continue;
                }
                continue;
            }
            else
            {
                if (!entities.Contains(e)) entities.Add(e);
                if (!patch) e.Visible = false;
                Logger.Log(LogLevel.Info, "Rug Helper", e.GetType().ToString());
                if (!multiples)
                    break;
            }
        }
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);
        Check(scene);
    }

    public IEnumerator meow(Scene scene)
    {
        Level level = (scene as Level);
        while (level.Transitioning)
        {
            yield return null;
        }
        yield return null;
        if (includePlayer) entities.Add(scene.Entities.FindFirst<Player>());
        Check(scene);
    }

    public override void Added(Scene scene)
    {
        base.Added(scene);
        Add(new Coroutine(meow(scene)));
        //Player player = CollideFirst<Player>();
        //entities.Add(player);
        //Solid solid = CollideFirst<Solid>(i);
        //if (solid != null) solids.Add(solid);


        using (new DetourConfigContext(
    new DetourConfig("RugHelper")
        .WithPriority(int.MaxValue)).Use())
        {
            On.Celeste.Glitch.Apply += hmmer;
        }
        //On.Celeste.Glitch.Apply += hmmer;
    }

    public override void Removed(Scene scene)
    {
        On.Celeste.Glitch.Apply -= hmmer;
        foreach (var entity in entities)
        {
            if (scene.Contains(entity))
            {
                entity.Visible = true;
                //for (int i = 0; i < entity.Components.Count; i++)
                //{
                    //var component = entity.Components[i];
                    //component.RemoveSelf();
                //}
                //entity.RemoveSelf();
            }
        }
        //entities.RemoveAll(entity => entity == null || scene.Contains(entity));
        base.Removed(scene);
    }
    
    public override void SceneEnd(Scene scene)
    {
        //On.Celeste.Glitch.Apply -= hmmer;
        /*foreach (var entity in entities)
        {
            if (scene.Contains(entity))
            {
                entity.Visible = false;
                for (int i = 0; i < entity.Components.Count; i++)
                {
                    var component = entity.Components[i];
                    component.RemoveSelf();
                }
                entity.RemoveSelf();
            }
        }
        entities.RemoveAll(entity => entity == null || scene.Contains(entity));*/
        base.SceneEnd(scene);
    }

    void hmmer(On.Celeste.Glitch.orig_Apply orig, VirtualRenderTarget source, float timer, float seed, float amplitude)
    {
        orig(source, timer, seed, amplitude);
        Engine.Instance.GraphicsDevice.SetRenderTarget(source);
        GameplayRenderer.Begin();
        foreach (var i in entities)
        {
            if (i == null || Scene == null || i.Scene != Scene) continue;
            if (i is Player && ((i as Player).Dead || (i as Player).Scene == null)) continue;
            if (!patch) i.Visible = true;
            i.Render();
            if (!patch) i.Visible = false;
        }
        Draw.SpriteBatch.End();
    }

    public static Entity Load(EntityData data, Vector2 offset)
    {
        return new ShaderOverrider(data, offset);
    }

}