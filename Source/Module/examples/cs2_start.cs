
// -- meant to be for an example mod, maybe ill add it later -- //

/*using System.Collections;
using Microsoft.Xna.Framework;
using Monocle;
using Celeste.Mod.Partline;
using Celeste.Mod.Partline.Entities;
using System;
using IL.Celeste;
using On.Celeste;
using System.Collections.Generic;
using Celeste.Mod.Partline.Module;
using Celeste.Mod.IsaGrabBag;

namespace Celeste.Mod.carpet.Module;

public class cs2_start : CutsceneEntity
{

    private UniversalBadelineDummy purple;
    private UniversalBadelineDummy yellow;
    private UniversalBadelineDummy orange;
    private UniversalBadelineDummy red;
    private UniversalBadelineDummy cyan;
    private UniversalBadelineDummy pink;
    private UniversalBadelineDummy green;
    private UniversalBadelineDummy blue;

    private Player player;
    private int ZoomIndex = 1;
    private Level _level;
    private bool shake = false;

    public cs2_start(Player player)
        : base(fadeInOnSkip: true, endingChapterAfter: false)
    {
        this.player = player;

    }

    public override void OnBegin(Level level)
    {
        //level.RegisterAreaComplete();
        List<UniversalBadelineDummy> cool = base.Scene.Entities.FindAll<UniversalBadelineDummy>();
        foreach (UniversalBadelineDummy i in cool)
        {
            BadelineVariants color = new BadelineVariants();
            //BadelineHairColors colorHairColors = new BadelineHairColors();

            if (Enum.TryParse(i.variant, true, out color))
            {
                Sprite = new BadelineSpriteModule($"{BadelineHairColors.Names[(int)color]}");
            }
        Add(new Coroutine(Cutscene(level)));
    }

    private IEnumerator Cutscene(Level level)
    {
        //Console.WriteLine(player.Position);
        _level = level;
        Add(new Coroutine(Shake()));
        player.StateMachine.State = 11;
        player.StateMachine.Locked = true;
        yield return 1f;
        level.Add(badeline = new BadelineDummy(player.Center));
        badeline.Appear(level, silent: true);
        badeline.FloatSpeed = 80f;
        badeline.Sprite.Scale.X = -1f;
        Audio.Play("event:/char/badeline/maddy_split", player.Center);
        yield return badeline.FloatTo(player.Position + new Vector2(24f, -20f), -1, faceDirection: false);
        yield return Textbox.Say("Something_Inspirational", B_R_E_A_T_H, breathMadelinebreath);
        player.DummyAutoAnimate = true;
        yield return 0.2f;
        badeline.Sprite.Scale.X = 1f;
        Add(new Coroutine(badeline.FloatTo(player.Position + new Vector2(200f, -15f), 1, faceDirection: true)));
        yield return player.DummyWalkToExact((int)player.Position.X + 95);
        yield return 0.25f;
        Add(new Coroutine(split(level)));
        Audio.Play("event:/char/badeline/maddy_split", player.Center);
        player.DummyAutoAnimate = false;
        player.Sprite.Play("faint");
        yield return 0.95f;
        player.Sprite.Play("deep_sleep");
        yield return level.ZoomTo(new Vector2(160f, 100f), 1.25f, 1f);
        //_level.ZoomSnap(new Vector2(160f, 110f), 1.25f);
        yield return Textbox.Say("Split_Start", BadelineTurnsLeft, Look_at_baddie, BadelineTurnsRight, ZoomIn, ZoomIn, ZoomIn, ZoomIn, BadelineFacesThem, ShakeStart, ShakeStop, BadelineTurnsRightAlt, SadelineConfrontBaddie, HadelineScream, HadelineIdle, BadelineTurnsLeftAlt, breathMadelinebreath);
        sadeline.Sprite.Scale = new Vector2(-1, 1);
        yield return BadelineGoInside();
        //Audio.Play("event:/char/madeline/backpack_drop", player.Position);
        yield return SceneAs<Level>().ZoomBack(0.5f);
        EndCutscene(level,false);
    }

    private IEnumerator B_R_E_A_T_H()
    {
        player.DummyAutoAnimate = false;
        player.Sprite.Play("tired");
        yield return null;
    }

    private IEnumerator breathMadelinebreath()
    {
        yield return 3.5f;
        player.Sprite.Play("duck");
    }

    private IEnumerator Shake()
    {
        while (true)
        {
            while (shake)
            {
                _level.Shake();
                Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
                yield return 0.2f;
            }
            yield return null;
        }
    }
    private IEnumerator ShakeStart()
    {
        yield return null;
        shake = true;
    }
    private IEnumerator ShakeStop()
    {
        yield return 0.01f;
        shake = false;
    }
    private IEnumerator SadelineConfrontBaddie()
    {
        yield return 0.1f;
        sadeline.Sprite.Scale.X = 1f;
        yield return sadeline.FloatTo(badeline.Position - new Vector2(12f, 0f));
    }
    private IEnumerator BadelineTurnsLeft()
    {
        yield return 0.1f;
        badeline.Sprite.Scale.X = -1f;
        yield return 0.1f;
    }
    private IEnumerator HadelineScream()
    {
        hadeline.Sprite.Play("angry");
        yield return 0.01f;
    }
    private IEnumerator HadelineIdle()
    {
        hadeline.Sprite.Play("fallSlow");
        yield return 0.01f;
    }
    private IEnumerator BadelineTurnsRight()
    {
        yield return 0.1f;
        badeline.Sprite.Scale.X = 1f;
        yield return badeline.FloatTo(badeline.Position + new Vector2(12f, -10f));
    }
    private IEnumerator BadelineTurnsRightAlt()
    {
        yield return 0.1f;
        badeline.Sprite.Scale.X = 1f;
        yield return badeline.FloatTo(badeline.Position + new Vector2(5f, 0));
    }
    private IEnumerator BadelineTurnsLeftAlt()
    {
        yield return 0.1f;
        badeline.Sprite.Scale.X = -1f;
        Add(new Coroutine(sadeline.FloatTo(sadeline.Position - new Vector2(9f, -3), null, false)));
        yield return badeline.FloatTo(badeline.Position - new Vector2(5f, 0));
    }
    private IEnumerator BadelineFacesThem()
    {
        yield return 0.1f;
        badeline.Sprite.Scale.X = -1f;
        yield return badeline.FloatTo(badeline.Position - new Vector2(12f, -10f));
    }
    private IEnumerator ZoomIn()
    {
        switch (ZoomIndex)
        {
            default:
                //yield return _level.ZoomTo(new Vector2(160f, 110f), 1.25f, 1f);
                _level.ZoomSnap(new Vector2(160f, 110f), 1.25f);
                break;
            case 1:
                _level.ZoomSnap(new Vector2(235f, 120f), 2.5f);
                _level.Shake();
                Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
                break;
            case 2:
                _level.ZoomSnap(new Vector2(235f, 120f), 3.5f);
                _level.Shake();
                Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
                break;
            case 3:
                _level.ZoomSnap(new Vector2(235f, 120f), 4.5f);
                _level.Shake();
                Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
                break;
        }
        ZoomIndex += 1;
        yield return null;
    }

    private IEnumerator BadelineGoInside()
    {
        yield return badeline.FloatTo(player.Position, 1);
        yield return 0.3f;
        yield return Textbox.Say("instructions");
        badeline.Vanish();
        player.DummyAutoAnimate = true;
        player.StateMachine.State = 0;
        player.StateMachine.Locked = false;
        player.Dashes = 1;
        _level.Session.Inventory.Dashes = 1;
        yield return 0.1f;
        yield return Textbox.Say("lets_go");
        var dummies = new object[] { sadeline, nadeline, dadeline, ladeline, cadeline, radeline, hadeline };
        foreach (var obj in dummies)
        {
            var appearMethod = obj.GetType().GetMethod("Vanish");
            appearMethod?.Invoke(obj, new object[] { });
            yield return 0.25f;
        }
        yield return 0.2f;
        yield return Textbox.Say("What");
    }

    private IEnumerator split(Level level)
    {
        player.Dashes = 0;
        level.Session.Inventory.Dashes = 0;
        level.Add(cadeline = new CadelineDummy(player.Center));
        level.Add(dadeline = new DadelineDummy(player.Center));
        level.Add(hadeline = new HadelineDummy(player.Center));
        level.Add(ladeline = new LadelineDummy(player.Center));
        level.Add(nadeline = new NadelineDummy(player.Center));
        level.Add(radeline = new RadelineDummy(player.Center));
        level.Add(sadeline = new SadelineDummy(player.Center));
        var dummies = new object[] { cadeline, dadeline, hadeline, ladeline, nadeline, radeline, sadeline };

        // Iterate through each dummy object
        foreach (var obj in dummies)
        {
            // Use reflection to call the Appear method and set FloatSpeed dynamically
            var appearMethod = obj.GetType().GetMethod("Appear");
            appearMethod?.Invoke(obj, new object[] { level, true }); // Call the Appear method

            var floatSpeedProperty = obj.GetType().GetProperty("FloatSpeed");
            if (floatSpeedProperty != null && floatSpeedProperty.CanWrite)
            {
                floatSpeedProperty.SetValue(obj, 80f); // Set the FloatSpeed for each dummy
            }
        }
        Audio.SetMusic("event:/music/lvl2/evil_madeline");
        Add(new Coroutine(cadeline.FloatTo(player.Position + new Vector2(-240000f, -210000f), 1, faceDirection: true)));
        Add(new Coroutine(dadeline.FloatTo(player.Position + new Vector2(-68f, -22f), 1, faceDirection: true)));
        Add(new Coroutine(hadeline.FloatTo(player.Position + new Vector2(540000f, -270000f), -1, faceDirection: false)));
        Add(new Coroutine(nadeline.FloatTo(player.Position + new Vector2(20f, -18f), -1, faceDirection: false)));
        Add(new Coroutine(ladeline.FloatTo(player.Position + new Vector2(-440000f, -240000f), 1, faceDirection: true)));
        Add(new Coroutine(radeline.FloatTo(player.Position + new Vector2(-20f, -20f), -1, faceDirection: false)));
        yield return sadeline.FloatTo(player.Position + new Vector2(68f, -25f), -1, faceDirection: false);
    }
    private IEnumerator Look_at_baddie()
    {
        dadeline.Sprite.Scale.X = 1f;
        yield return 0.3f;
        sadeline.Sprite.Scale.X = 1f;
        yield return 0.3f;
        radeline.Sprite.Scale.X = 1f;
        yield return 0.3f;
        nadeline.Sprite.Scale.X = 1f;
        yield return 0.3f;
        ladeline.Sprite.Scale.X = 1f;
        yield return 0.3f;
        hadeline.Sprite.Scale.X = 1f;
        yield return 0.3f;
        cadeline.Sprite.Scale.X = 1f;
        yield return 0.3f;
    }

    private IEnumerator TurnToLeft()
    {
        yield return 0.1f;
        player.Facing = Facings.Left;
        yield return 0.05f;
        badeline.Sprite.Scale.X = -1f;
        yield return 0.1f;
    }

    public override void OnEnd(Level level)
    {
        if (WasSkipped)
        {
            Audio.SetMusic("event:/music/lvl2/evil_madeline");
            // Ensure Badeline and dummies are removed if they exist
            badeline?.RemoveSelf();  // Clean up Badeline

            // Check and remove all the other dummies (Sadeline, Dadeline, etc.)
            if (sadeline != null)
            {
                var dummies = new object[] { sadeline, nadeline, dadeline, ladeline, cadeline, radeline, hadeline };
                foreach (var obj in dummies)
                {
                    // Dynamically call Vanish on each dummy if it's still there
                    var vanishMethod = obj.GetType().GetMethod("RemoveSelf");
                    vanishMethod?.Invoke(obj, new object[] { });
                }
            }
            // Ensure player is restored fully after skipping
            player.Dashes = 1;                   // Restore player's dashes
            level.Session.Inventory.Dashes = 1;
            if (player != null)
            {
                player.Position = new Vector2(-505, 168);
                if (player != null)
                {
                    while (!player.OnGround() && player.Y < (float)level.Bounds.Bottom)
                    {
                        player.Y++;
                    }
                }
                player.StateMachine.Locked = false;
                player.StateMachine.State = 0;
            }
            SceneAs<Level>().ResetZoom();
        }
        else
        {
            badeline?.RemoveSelf();  // Clean up Badeline
            var dummies = new object[] { sadeline, nadeline, dadeline, ladeline, cadeline, radeline, hadeline };
            foreach (var obj in dummies)
            {
                // Dynamically call Vanish on each dummy if it's still there
                var vanishMethod = obj.GetType().GetMethod("RemoveSelf");
                vanishMethod?.Invoke(obj, new object[] { });
            }
            player.Dashes = 1;                   // Restore player's dashes
            level.Session.Inventory.Dashes = 1;
            if (player != null)
            {
                player.Position = new Vector2(-505, 168);
                if (player != null)
                {
                    while (!player.OnGround() && player.Y < (float)level.Bounds.Bottom)
                    {
                        player.Y++;
                    }
                }
                player.StateMachine.Locked = false;
                player.StateMachine.State = 0;
            }
            //level.ZoomBack(1f);
            //Add(new Coroutine(SceneAs<Level>().ZoomBack(0.5f)));
        }

    }
}
*/