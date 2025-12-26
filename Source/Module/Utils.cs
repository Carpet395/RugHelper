using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.Rug.Module;

//utils components
public class SineWave01 : Component
{
    public float Frequency = 1f;

    public float Rate = 1f;

    public Action<float> OnUpdate;

    public bool UseRawDeltaTime;

    public float counter;

    public float Value
    {
        get;
        set;
    }

    public float ValueOverTwo
    {
        get;
        set;
    }

    public float TwoValue
    {
        get;
        set;
    }

    public float Counter
    {
        get
        {
            return counter;
        }
        set
        {
            counter = (float)(((double)value + 25.132741928100586) % 25.132741928100586);
            Value = (float)Math.Sin(counter) + 1f / 2;
            ValueOverTwo = (float)Math.Sin(counter / 2f);
            TwoValue = (float)Math.Sin(counter * 2f);
        }
    }

    public SineWave01()
        : base(active: true, visible: false)
    {
    }

    public SineWave01(float frequency, float offset = 0f)
        : this()
    {
        Frequency = frequency;
        Counter = offset;
    }

    public override void Update()
    {
        Counter += (float)(6.2831854820251465 * (double)Frequency * (double)Rate * (double)(UseRawDeltaTime ? Engine.RawDeltaTime : Engine.DeltaTime));
        if (OnUpdate != null)
        {
            OnUpdate(Value);
        }
    }

    public float ValueOffset(float offset)
    {
        return (float)Math.Sin(counter + offset);
    }

    public SineWave01 Randomize()
    {
        Counter = (float)((double)Calc.Random.NextFloat() * 6.2831854820251465 * 2.0);
        return this;
    }

    public void Reset()
    {
        Counter = 0f;
    }

    public void StartUp()
    {
        Counter = MathF.PI / 2f;
    }

    public void StartDown()
    {
        Counter = 4.712389f;
    }

    public SineWave01(float frequency)
        : this(frequency, 0f)
    {
    }
}

// utils functions
public static class RandUtils
{
    public static Level ToLevel(this Scene scene)
    {
        return scene as Level;
    }
    public static Level ToLevel()
    {
        return Engine.Scene as Level;
    }

    public static IEnumerator PanCamera(this Camera cam, Vector2 to, float duration, Ease.Easer ease = null)
    {
        to += new Vector2(-160f, -120f);
        if (ease == null)
        {
            ease = Ease.CubeInOut;
        }

        Vector2 from = cam.Position;
        for (float t = 0f; t < 1f; t += Engine.DeltaTime / duration)
        {
            yield return null;
            cam.Position = from + (to - from) * ease(Math.Min(t, 1f));
        }
    }

}

public static class ColorUtils
{

}

public static class Vector2Utils
{

    public static Vector2 GetAverage(this Vector2 a, Vector2 b)
    {
        return Vector2.Lerp(a, b, 0.5f);
    }

    public static Vector2 ScreenToFocusPoint(this Vector2 vector)
    {
        return vector / 6;
    }
    public static Vector2 WorldToFocusPoint(this Vector2 vector, int offsetX = 0, int offsetY = 0)
    {
        Level level = (Engine.Scene as Level);
        if (level != null)
        {
            return level.WorldToScreen(vector) / 6 + new Vector2(offsetX, offsetY);
        }
        else
        {
            throw new Exception("You aren't in a level dumbass");
        }
    }
}


