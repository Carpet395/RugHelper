using System;
using System.Collections;
//using Celeste.Mod.Addiction.Module;
using Celeste.Mod.Entities;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;
//using Celeste.Mod.Rug.Module;
//using Celeste.Mod.Addiction.Module;

namespace Celeste.Mod.Rug.Triggers;

[CustomEntity(new string[] { "Rug/DisableCassetteBlocksTrigger" })]
public class DisableCassetteBlocksTrigger : Trigger
{
    //public string Event;

    //public bool OnSpawnHack;

    public bool OnlyOnce;

    private bool triggered;

    private int id;

    public DisableCassetteBlocksTrigger(EntityData data, Vector2 offset)
        : base(data, offset)
    {
        //Event = data.Attr("event");
        //OnSpawnHack = data.Bool("onSpawn");
        id = data.ID;
        OnlyOnce = data.Bool("onlyOnce");
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);
        if (OnlyOnce && (scene as Level).Session.GetFlag($"DisableCBTrigger_{id}"))
        {
            CassetteBlockManager cbm = base.Scene.Tracker.GetEntity<CassetteBlockManager>();
            cbm.StopBlocks();
        }
        /*if (OnSpawnHack)
        //{
            Player player = CollideFirst<Player>();
            if (player != null)
            {
                OnEnter(player);
            }
        }*/
    }

    public override void Update()
    {
        base.Update();
    }

    public Player player;

    public override void OnEnter(Player player)
    {
        Level level = base.Scene as Level;

        if (triggered || (OnlyOnce && level.Session.GetFlag($"DisableCBTrigger_{id}")))
        {
            return;
        }
        triggered = true;

        CassetteBlockManager cbm = base.Scene.Tracker.GetEntity<CassetteBlockManager>();
        cbm?.StopBlocks();
        cbm?.Finish();
        level.Session.SetFlag($"cutscene_trigger_{id}");
    }

    public override void Removed(Scene scene)
    {
        base.Removed(scene);
    }

    public override void SceneEnd(Scene scene)
    {
        base.SceneEnd(scene);
    }
}
