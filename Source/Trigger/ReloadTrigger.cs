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

[CustomEntity(new string[] { "Rug/ReloadTrigger" })]
public class ReloadTrigger : Trigger
{
    //public string Event;

    //public bool OnSpawnHack;

    public bool OnlyOnce;

    private bool triggered;

    private EventInstance snapshot;

    public float Time { get; private set; }

    public ReloadTrigger(EntityData data, Vector2 offset)
        : base(data, offset)
    {
        //Event = data.Attr("event");
        //OnSpawnHack = data.Bool("onSpawn");
        OnlyOnce = data.Bool("onlyOnce");  // Get the onlyOnce flag from the data
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);
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
        Level level = Engine.Scene as Level;
        Logger.Log(LogLevel.Info, "meow", level?.Tracker.GetEntity<Player>()?.Position.ToString());
        base.Update();
    }

    public Player player;

    public override void OnEnter(Player player)
    {
        Level level = base.Scene as Level;

        // Check if this trigger should only activate once and if it has already been triggered
        //if (OnlyOnce && level.Session.GetFlag($"cutscene_trigger_{Event}"))
        //{
            //return;  // Skip if it has already been triggered and onlyOnce is true
        //}

        // Set the triggered flag and the session flag if OnlyOnce is true
        if (triggered)
        {
            //return;
        }
        triggered = true;

        //this.player = player;
        Everest.Events.Level.OnAfterUpdate += afterUpdate;
        //Level.
        //player.Position = pos;
        //if (OnlyOnce)
        //{
        // level.Session.SetFlag($"cutscene_trigger_{Event}");
        //}

        /*switch (Event)
        //{
            case "intro_breath":
                Scene.Add(new cs1_intro());
                break;
            case "intro_cutscene":
                Scene.Add(new intro_cutscene());
                break;
            default:
                Console.WriteLine($"tf is {Event} idk doesnt exist");
                break;
        }*/
    }

    public void afterUpdate(Level self)
    {
        //Session Session = self.Session;
        //Celeste.ReloadAssets(levels: true, graphics: false, hires: false, Session.Area);
        //Engine.Scene = new LevelLoader(Session);
        self.Reload();
        Everest.Events.Level.OnAfterUpdate -= afterUpdate;
    }

    public override void Removed(Scene scene)
    {
        base.Removed(scene);
        Audio.ReleaseSnapshot(snapshot);
    }

    public override void SceneEnd(Scene scene)
    {
        base.SceneEnd(scene);
        Audio.ReleaseSnapshot(snapshot);
    }

    private IEnumerator Brighten()
    {
        Level level = base.Scene as Level;
        float darkness = AreaData.Get(level).DarknessAlpha;
        while (level.Lighting.Alpha != darkness)
        {
            level.Lighting.Alpha = Calc.Approach(level.Lighting.Alpha, darkness, Engine.DeltaTime * 4f);
            yield return null;
        }
    }

    private IEnumerator Ch9HubTransitionBackgroundToBright(Player player)
    {
        Level level = base.Scene as Level;
        float start = base.Bottom;
        float end = base.Top;
        while (true)
        {
            float fadeAlphaMultiplier = Calc.ClampedMap(player.Y, start, end);
            foreach (Backdrop item in level.Background.GetEach<Backdrop>("bright"))
            {
                item.ForceVisible = true;
                item.FadeAlphaMultiplier = fadeAlphaMultiplier;
            }
            yield return null;
        }
    }
}
