using Celeste.Mod.IsaGrabBag;
using Celeste.Mod.Rug.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;

namespace Celeste.Mod.Rug.Entities;


public class ChangedFollower : Entity
{
    public const string IsaGrabBag_HasBadelineFollower = "has_badeline_follower";

    private static BadelineBoost booster;

    private static bool firstBoost;

    private static bool boosting;

    private static Coroutine LookForBubble;

    private float previousPosition;

    public Follower follower { get; private set; }

    public static ChangedFollower instance { get; set; }

    public colorfulDummy dummy { get; private set; }

    public ChangedFollower(Level level, colorfulDummy _dummy, Vector2 position)
    : base(position)
    {
        level.Session.SetFlag("has_badeline_follower");
        level.Add(dummy = _dummy);
        dummy.Add(this.follower = new Follower());
        this.follower.PersistentFollow = true;
        this.follower.Added(dummy);
        AddTag(Tags.Persistent);
        dummy.AddTag(Tags.Persistent);
        instance = this;
    }

    public ChangedFollower(Level level, Vector2 position, string variant = "purple")
        : this(level, new colorfulDummy(position, variant), position)
    {
    }

    public static void SpawnColorfulBadelineFriendo(Level _level, string variant)
    {
        Player playerInstance = GrabBagModule.playerInstance;
        if (_level != null)
        {
            _level.Session.SetFlag("has_badeline_follower");
            if (playerInstance != null)
            {
                //ColorfulDummy CD = new ColorfulDummy(playerInstance.Position, variant);
                ChangedFollower badelineFollower = new ChangedFollower(_level, playerInstance.Position, variant);
                _level.Add(badelineFollower);
                playerInstance.Leader.GainFollower(badelineFollower.follower);
            }
        }
    }
    /*public static void CmdSpawnBadeline()
    {
        if (Engine.Scene is Level level)
        {
            SpawnBadelineFriendo(level);
        }
    }*/

    /*public static void Search()
    {
        if (LookForBubble != null && LookForBubble.Active)
        {
            LookForBubble.Cancel();
        }

        GrabBagModule.playerInstance.Add(LookForBubble = new Coroutine(SearchForBadeline(Engine.Scene as Level)));
    }*/

    public static void SpawnBadelineFriendo(Level _level)
    {
        Player playerInstance = GrabBagModule.playerInstance;
        if (_level != null)
        {
            _level.Session.SetFlag("has_badeline_follower");
            if (playerInstance != null)
            {
                BadelineFollower badelineFollower = new BadelineFollower(_level, playerInstance.Position);
                _level.Add(badelineFollower);
                playerInstance.Leader.GainFollower(badelineFollower.follower);
            }
        }
    }

    /*public static bool CheckBooster(Level lvl, bool onTransition)
    {
        if (lvl == null)
        {
            return false;
        }

        if (!lvl.Session.GetFlag("has_badeline_follower"))
        {
            return false;
        }

        Player playerInstance = GrabBagModule.playerInstance;
        booster = null;
        List<BadelineBoost> list = lvl.Entities.FindAll<BadelineBoost>();
        float num = float.MaxValue;
        if (list.Count > 0)
        {
            if (playerInstance != null)
            {
                Vector2 value = ((!lvl.Session.RespawnPoint.HasValue) ? playerInstance.Position : lvl.Session.RespawnPoint.Value);
                float num2 = float.MaxValue;
                foreach (BadelineBoost item in list)
                {
                    item.Visible = false;
                    num = Vector2.Distance(item.Position, value);
                    if (num < num2)
                    {
                        num2 = num;
                        if (booster != null)
                        {
                            booster.RemoveSelf();
                        }

                        booster = item;
                    }
                    else
                    {
                        item.RemoveSelf();
                    }
                }

                booster.Collidable = false;
                booster.Visible = true;
                if (lvl.Session.GetFlag("has_badeline_follower"))
                {
                    booster.Get<PlayerCollider>().OnCollide = NewBoostMechanic;
                    booster.Visible = false;
                    firstBoost = true;
                    booster.Position = instance.Position;
                    booster.Add(new Coroutine(NewBoost()));
                    if (!lvl.Session.Level.Contains("_sl"))
                    {
                        booster.Add(new Coroutine(Skip()));
                    }

                    return true;
                }

                booster.RemoveSelf();
            }
            else
            {
                foreach (BadelineBoost item2 in list)
                {
                    item2.RemoveSelf();
                }
            }
        }

        return false;
    }*/

    public override void Update()
    {
        if (GrabBagModule.playerInstance != null && (follower.Leader == null || follower.Leader.Entity != GrabBagModule.playerInstance))
        {
            GrabBagModule.playerInstance.Leader.GainFollower(follower);
        }

        if ((previousPosition - dummy.Position.X) * dummy.Sprite.Scale.X > 0f)
        {
            dummy.Sprite.Scale.X *= -1f;
        }

        previousPosition = dummy.Position.X;
        base.Update();
    }

    public override void SceneBegin(Scene scene)
    {
        boosting = false;
        base.SceneBegin(scene);
    }

    public void Readd(Level lvl, Player obj)
    {
        lvl.Add(this);
        lvl.Add(dummy);
        obj.Leader.GainFollower(follower);
        dummy.Position = obj.Position - new Vector2((obj.Facing == Facings.Left) ? (-5) : 5, 16f);
    }

    internal static void Load()
    {
        Everest.Events.Level.OnLoadEntity += Level_OnLoadEntity;
        On.Celeste.BadelineBoost.Awake += BadelineBoostAwake;
    }

    internal static void Unload()
    {
        Everest.Events.Level.OnLoadEntity -= Level_OnLoadEntity;
        On.Celeste.BadelineBoost.Awake -= BadelineBoostAwake;
    }

    private static bool Level_OnLoadEntity(Level level, LevelData levelData, Vector2 offset, EntityData entityData)
    {
        string name = entityData.Name;
        string text = name;
        if (text == "isaBag/baddyFollow")
        {
            if (!level.Session.GetFlag("has_badeline_follower"))
            {
                SpawnBadelineFriendo(level);
            }

            return true;
        }

        return false;
    }

    private static void BadelineBoostAwake(On.Celeste.BadelineBoost.orig_Awake orig, BadelineBoost self, Scene scene)
    {
        orig(self, scene);
        if ((scene as Level).Session.GetFlag("has_badeline_follower"))
        {
            self.Visible = false;
        }
    }

    /*private static IEnumerator Skip()
    {
        Player player = GrabBagModule.playerInstance;
        DynamicData boostData = DynamicData.For(booster);
        Vector2[] nodes = boostData.Get<Vector2[]>("nodes");
        yield return 1;
        int value = boostData.Get<int>("nodeIndex");
        while (value < nodes.Length - 1)
        {
            while (boosting)
            {
                yield return null;
            }

            float distNow = Vector2.Distance(nodes[value], player.Position);
            float distNext = Vector2.Distance(nodes[value + 1], player.Position);
            if (distNow > distNext * 1.3f)
            {
                boostData.Set("nodeIndex", value++);
                boostData.Invoke("Skip");
                boosting = true;
                do
                {
                    boosting = boostData.Get<bool>("travelling");
                    yield return null;
                }
                while (boosting);
            }

            yield return null;
        }
    }

    private static IEnumerator SearchForBadeline(Level level)
    {
        bool exit = false;
        while (!exit)
        {
            if (CheckBooster(level, onTransition: true))
            {
                exit = true;
            }

            yield return 0.05f;
        }
    }

    private static void NewBoostMechanic(Player obj)
    {
        if (booster == null)
        {
            throw new NullReferenceException("Badeline Booster is null?");
        }

        booster.Collidable = false;
        booster.Add(new Coroutine(NewBoost()));
    }

    private static IEnumerator NewBoost()
    {
        Player player = GrabBagModule.playerInstance;
        while (boosting)
        {
            yield return 0;
        }

        boosting = true;
        booster.Visible = true;
        DynamicData boostData = DynamicData.For(booster);
        int nodeIndex = boostData.Get<int>("nodeIndex");
        Vector2[] nodes = boostData.Get<Vector2[]>("nodes");
        Sprite sprite = boostData.Get<Sprite>("sprite");
        Image stretch = boostData.Get<Image>("stretch");
        Level level = booster.Scene as Level;
        if (instance.dummy.Visible)
        {
            instance.dummy.Visible = false;
        }

        if (!firstBoost)
        {
            int num2 = nodeIndex + 1;
            nodeIndex = num2;
            boostData.Set("nodeIndex", num2);
        }

        bool finalBoost = nodeIndex >= nodes.Length;
        if (!firstBoost)
        {
            sprite.Visible = false;
            sprite.Position = Vector2.Zero;
            booster.Collidable = false;
            Audio.Play("event:/char/badeline/booster_begin", booster.Position);
            player.StateMachine.State = 11;
            player.DummyAutoAnimate = false;
            player.DummyGravity = false;
            player.Dashes = 1;
            player.RefillStamina();
            player.Speed = Vector2.Zero;
            int num = Math.Sign(player.X - booster.X);
            if (num == 0)
            {
                num = -1;
            }

            BadelineDummy badeline = new BadelineDummy(booster.Position);
            booster.Scene.Add(badeline);
            player.Facing = (Facings)(-num);
            badeline.Sprite.Scale.X = num;
            Vector2 playerFrom = player.Position;
            Vector2 playerTo = booster.Position + new Vector2(num * 4, -3f);
            Vector2 badelineFrom = badeline.Position;
            Vector2 badelineTo = booster.Position + new Vector2((0f - (float)num) * 4f, 3f);
            for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.2f)
            {
                Vector2 vector = Vector2.Lerp(playerFrom, playerTo, p);
                if (player.Scene != null)
                {
                    player.MoveToX(vector.X);
                }

                if (player.Scene != null)
                {
                    player.MoveToY(vector.Y);
                }

                badeline.Position = Vector2.Lerp(badelineFrom, badelineTo, p);
                yield return null;
            }

            Audio.Play("event:/char/badeline/booster_throw", booster.Position);
            badeline.Sprite.Play("boost");
            yield return 0.1f;
            if (!player.Dead)
            {
                player.MoveV(5f);
            }

            yield return 0.1f;
            booster.Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
            {
                if (player.Dashes < player.Inventory.Dashes)
                {
                    player.Dashes++;
                }

                booster.Scene.Remove(badeline);
                (booster.Scene as Level).Displacement.AddBurst(badeline.Position, 0.25f, 8f, 32f, 0.5f);
            }, 0.15f, start: true));
            (booster.Scene as Level).Shake();
            player.BadelineBoostLaunch(booster.CenterX);
        }

        booster.Visible = true;
        Vector2 from = (firstBoost ? instance.dummy.Position : booster.Position);
        Vector2 to = (finalBoost ? instance.dummy.Position : nodes[nodeIndex]);
        float duration = Vector2.Distance(from, to) / 320f;
        stretch.Visible = true;
        stretch.Rotation = (to - from).Angle();
        Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, duration, start: true);
        tween.OnUpdate = delegate (Tween t)
        {
            if (finalBoost)
            {
                to = instance.dummy.Position;
            }

            booster.Position = Vector2.Lerp(from, to, t.Eased);
            stretch.Scale.X = 1f + Calc.YoYo(t.Eased) * 2f;
            stretch.Scale.Y = 1f - Calc.YoYo(t.Eased) * 0.75f;
            if (t.Eased < 0.9f && booster.Scene.OnInterval(0.03f))
            {
                TrailManager.Add(booster, Player.TwoDashesHairColor, 0.5f);
                level.ParticlesFG.Emit(BadelineBoost.P_Move, 1, booster.Center, Vector2.One * 4f);
            }
        };
        tween.OnComplete = delegate
        {
            stretch.Visible = false;
            if (finalBoost)
            {
                instance.dummy.Visible = true;
            }
            else
            {
                booster.Visible = true;
                sprite.Visible = true;
                booster.Collidable = true;
            }

            Audio.Play("event:/char/badeline/booster_reappear", booster.Position);
        };
        booster.Add(tween);
        Audio.Play("event:/char/badeline/booster_relocate", null, 0f);
        Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
        level.DirectionalShake(-Vector2.UnitY);
        level.Displacement.AddBurst(booster.Center, 0.4f, 8f, 32f, 0.5f);
        firstBoost = false;
        boosting = false;
    }*/
}
