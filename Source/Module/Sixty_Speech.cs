using System;
using System.Collections;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using YamlDotNet.Core.Tokens;

namespace Celeste.Mod.Rug.Module;

public enum Characters
{
    Magenta,
    Gold,
    Sea
}

public class Sixty_Speech : Entity
{

    // -- very very cool thingy, ask @carpet about it -- //

    public enum States
    {
        OpenAll,
        CLosed3,
        CLosed2,
        CLosedAll
    }

    public const string Flag = "badeline_connection";

    private Player player;

    private float fade;

    private bool Skip_Blink = false;

    private float pictureFade;

    private float pictureGlow;

    private MTexture picture;

    private bool waitForKeyPress;

    public bool Visible = false;

    private Vector2 ogCenter;

    private float timer = 0;
    private float timer_Alt = 0;
    private float offset = 0;

    private EventInstance sfx;

    public string State;

    private bool Stop = false;

    public bool InBlink = false;

    private bool first = true;

    private float Volume = 0f;

    private Characters character = Characters.Magenta;

    public Sixty_Speech(Player player, string State, Characters character = Characters.Magenta)
    {
        this.character = character;
        base.Tag = Tags.HUD;
        this.State = State;
        this.player = player;
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);
        Add(new Coroutine(Cutscene(SceneAs<Level>())));
    }

    private IEnumerator Cutscene(Level level)
    {
        Volume = Audio.MusicVolume;
        while ((fade += Engine.DeltaTime) < 0.75f)
        {
            Audio.MusicVolume -= 0.1f;
            if (Audio.MusicVolume <= 0) { Audio.MusicVolume = 0f; break; }
            yield return 0.1f;
        }
        yield return SetState(State);
        Visible = true;
        yield return WaitForEnd();
    }

    private IEnumerator PictureFade(float to, float duration = 1f)
    {
        while ((pictureFade = Calc.Approach(pictureFade, to, Engine.DeltaTime / duration)) != to)
        {
            float height = (float)Math.Sin(timer_Alt * 0.3f * MathHelper.TwoPi) * 25 + ogCenter.Y - 75;
            picture.Center = new Vector2(picture.Center.X, height);
            yield return null;
        }
    }

    private IEnumerator WaitForEnd()
    {
        waitForKeyPress = true;
        while (!Stop)
        {
            yield return null;
        }

        waitForKeyPress = false;
    }

    private IEnumerator ENDFORFUCKSAKE()
    {
        while ((fade -= Engine.DeltaTime) < 0)
        {
            Audio.MusicVolume += 0.1f;
            yield return 0.1f;
        }
        Audio.MusicVolume = Volume;
    }

    public IEnumerator Close()
    {
        Add(new Coroutine(ENDFORFUCKSAKE()));
        yield return PictureFade(0, 0.5f);
        Stop = true;
    }

    public IEnumerator SetState(string State, bool silent = false)
    {
        States states;
        if (Enum.TryParse(State, true, out states))
        {
            picture = GFX.Portraits[$"{character}_{states}"];
            if (first)
            {
                timer_Alt = 3.3f;
                ogCenter = picture.Center;
                first = false;
            }
            //Audio.SfxVolume = 0.3f;
            sfx = Audio.Play("event:/game/06_reflection/hug_image_1", player.Position + new Vector2(5000,5000));
            //Audio.SfxVolume = 1f;
            float height = (float)Math.Sin(timer_Alt * 0.3f * MathHelper.TwoPi) * 50 + ogCenter.Y - 75;
            picture.Center = new Vector2(picture.Center.X, height);
            if (!silent)
            {
                this.State = State;
            }
            Skip_Blink = true;
            yield return PictureFade(0.25f);
        }
    }

    public override void Update()
    {
        timer += Engine.DeltaTime;
        if (picture != null)
        {
            if (!first)
            {
                timer_Alt += Engine.DeltaTime;
                float height = (float)Math.Sin(timer_Alt * 0.3f * MathHelper.TwoPi) * 50 + ogCenter.Y - 75;
                picture.Center = new Vector2(picture.Center.X, height);
            }
            Random random = new Random();
            if (Scene.OnInterval(random.Range(2.25f,2.75f)))
            {
                if (!Skip_Blink)
                {
                    Add(new Coroutine(Blink()));
                }
                else
                {
                    Skip_Blink = false;
                }
            }
        }
        base.Update();
    }

    public IEnumerator Blink()
    {
        InBlink = true;
        yield return SetState("ClosedAll", true);
        yield return 0.2f;
        yield return SetState(State, true);
        InBlink = false;
    }

    public override void Render()
    {
        if (!(fade > 0f))
        {
            return;
        }
        Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * Ease.CubeOut(fade) * 0.8f);
        if (picture != null && pictureFade > 0f)
        {
            float num = Ease.CubeOut(pictureFade);
            Vector2 position = new Vector2(960f, 540f);
            float scale = 1f + (1f - num) * 0.025f;
            picture.DrawCentered(position, Color.White * Ease.CubeOut(pictureFade), scale, 0f);
            if (pictureGlow > 0f)
            {
                GFX.Portraits["hug-light2a"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
                GFX.Portraits["hug-light2b"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
                GFX.Portraits["hug-light2c"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
                HiresRenderer.EndRender();
                HiresRenderer.BeginRender(BlendState.Additive);
                GFX.Portraits["hug-light2a"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
                GFX.Portraits["hug-light2b"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
                GFX.Portraits["hug-light2c"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
                HiresRenderer.EndRender();
                HiresRenderer.BeginRender();
            }
        }
    }
}
