using System;
//using Celeste.Mod.Rug.Entities;
//using Celeste.Mod.Rug.Module;
//using MonoMod.Core.Utils;
//using System.Collections;
using System.Collections.Generic;
//using FMOD.Studio;
//using Celeste;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
using Monocle;
///using static MonoMod.InlineRT.MonoModRule;
//using MonoMod;
using System.Reflection;
using MonoMod.Cil;
//using Mono.Cecil.Cil;
//using static Celeste.Player;
//using System.Linq;
//using Celeste.Mod.Helpers.LegacyMonoMod;
///using MonoMod.RuntimeDetour;
//using Celeste.Mod;
//using IL.Celeste;
//using MonoMod.Utils;
using On.Celeste;
using System.Globalization;
//using FrostHelper;
using static MonoMod.InlineRT.MonoModRule;
using MonoMod.Utils;
using static Celeste.Level;
using Celeste.Mod.Rug.Entities;
using IL.MonoMod;
using Celeste.Mod.Entities;
using FrostHelper;
//using Celeste.Mod.Meta;
//using FrostHelper.ShaderImplementations;
//using FrostHelper.ModIntegration;
//using FrostHelper;
//using YamlDotNet.Core.Tokens;
//using System.Globalization;
//using static Celeste.GaussianBlur;
//using FrostHelper;

namespace Celeste.Mod.Rug;

public class RugHelperModule : EverestModule {
    // Only one alive module instance can exist at any given time.
    public static RugHelperModule Instance;

    public static readonly FrameworkType Framework;

    static RugHelperModule()
    {
        Framework = typeof(Game).Assembly.FullName!.Contains("FNA")
            ? FrameworkType.FNA
            : FrameworkType.XNA;
    }

    public RugHelperModule()
    {
        Instance = this;
    }

    // Check the next section for more information about mod settings, save data and session.
    // Those are optional: if you don't need one of those, you can remove it from the module.

    // If you need to store settings:
    public override Type SettingsType => typeof(RugModuleSettings);
    public static RugModuleSettings RugSettings => (RugModuleSettings)Instance._Settings;

    // If you need to store save data:
    public override Type SaveDataType => typeof(RugModuleSaveData);
    public static RugModuleSaveData SaveDat => (RugModuleSaveData)Instance._SaveData;

    //public override Type meta //=> typeof(RugHelperMeta);
    //public static RugHelperMeta


    // Set up any hooks, event handlers and your mod in general here.
    // Load runs before Celeste itself has initialized properly.

    public static Effect gradientShader;

    public override void Load()
    {
        //hook_OuiChapterSelect_get_area = new Hook((MethodBase)typeof(OuiChapterSelect).GetProperty("area", BindingFlags.Instance | BindingFlags.NonPublic).GetGetMethod(nonPublic: true), typeof(RugHelperModule).GetMethod("OnChapterSelectGetArea", BindingFlags.Static | BindingFlags.NonPublic));
        //Everest.Events.Player.OnSpawn += onPlayerSpawn;
        On.Celeste.OuiChapterPanel.Render += OuiChapterPanel_RenderHook;
        On.Celeste.AreaData.Load += AreaDataOnLoad;
        Everest.Content.OnUpdate += Content_OnUpdate;
        On.Celeste.Level.EnforceBounds += EnforceBounds;
        TheoWOWOWOWOWOWOWOWOWOWOWOWOWFuckHimPutHimIntoLimbo.P_Impact = new ParticleType
        {
            Color = Calc.HexToColor("cbdbfc"),
            Size = 1f,
            FadeMode = ParticleType.FadeModes.Late,
            DirectionRange = 1.7453293f,
            SpeedMin = 10f,
            SpeedMax = 20f,
            SpeedMultiplier = 0.1f,
            LifeMin = 0.3f,
            LifeMax = 0.8f
        };
        //gradientShader = GFX.LoadFx("test");
        //hook_OuiChapterSelect_get_area = new LegacyHook(typeof(OuiChapterSelect).GetProperty("area", BindingFlags.Instance | BindingFlags.NonPublic).GetGetMethod(nonPublic: true), typeof(RugHelperModule).GetMethod("OnChapterSelectGetArea", BindingFlags.Static | BindingFlags.NonPublic));
    }

    public static void EnforceBounds(On.Celeste.Level.orig_EnforceBounds orig, Level self, Player player)
    {
        TheoWOWOWOWOWOWOWOWOWOWOWOWOWFuckHimPutHimIntoLimbo entity2 = self.Tracker.GetEntity<TheoWOWOWOWOWOWOWOWOWOWOWOWOWFuckHimPutHimIntoLimbo>();
        if (entity2 == null)
        {
            orig(self, player);
        }
        else
        {
            //orig(self, player);
            Rectangle bounds = self.Bounds;
            Rectangle rectangle = new Rectangle((int)self.Camera.Left, (int)self.Camera.Top, 320, 180);
            DynamicData data = DynamicData.For(self);
            if (data.Get("transition") != null)
            {
                return;
            }
            if (self.CameraLockMode == CameraLockModes.FinalBoss && player.Left < (float)rectangle.Left)
            {
                player.Left = rectangle.Left;
                player.OnBoundsH();
            }
            else if (player.Left < (float)bounds.Left)
            {
                if (player.Top >= (float)bounds.Top && player.Bottom < (float)bounds.Bottom && self.Session.MapData.CanTransitionTo(self, player.Center + Vector2.UnitX * -8f))
                {
                    player.BeforeSideTransition();
                    data.Invoke("NextLevel", player.Center + Vector2.UnitX * -8f, -Vector2.UnitX);
                    return;
                }
                player.Left = bounds.Left;
                player.OnBoundsH();
            }
            TheoCrystal entity = self.Tracker.GetEntity<TheoCrystal>();
            //TheoWOWOWOWOWOWOWOWOWOWOWOWOWFuckHimPutHimIntoLimbo entity2 = self.Tracker.GetEntity<TheoWOWOWOWOWOWOWOWOWOWOWOWOWFuckHimPutHimIntoLimbo>();
            if (self.CameraLockMode == CameraLockModes.FinalBoss && player.Right > (float)rectangle.Right && rectangle.Right < bounds.Right - 4)
            {
                player.Right = rectangle.Right;
                player.OnBoundsH();
            }
            else if (entity != null && (player.Holding == null || !player.Holding.IsHeld) && player.Right > (float)(bounds.Right - 1))
            {
                player.Right = bounds.Right - 1;
            }
            else if (entity2 != null && (player.Holding == null || !player.Holding.IsHeld) && player.Right > (float)(bounds.Right - 1))
            {
                player.Right = bounds.Right - 1;
            }
            else if (player.Right > (float)bounds.Right)
            {
                if (player.Top >= (float)bounds.Top && player.Bottom < (float)bounds.Bottom && self.Session.MapData.CanTransitionTo(self, player.Center + Vector2.UnitX * 8f))
                {
                    player.BeforeSideTransition();
                    data.Invoke("NextLevel", player.Center + Vector2.UnitX * 8f, Vector2.UnitX);
                    return;
                }
                player.Right = bounds.Right;
                player.OnBoundsH();
            }
            if (self.CameraLockMode != 0 && player.Top < (float)rectangle.Top)
            {
                player.Top = rectangle.Top;
                player.OnBoundsV();
            }
            else if (player.CenterY < (float)bounds.Top)
            {
                if (self.Session.MapData.CanTransitionTo(self, player.Center - Vector2.UnitY * 12f))
                {
                    player.BeforeUpTransition();
                    data.Invoke("NextLevel", player.Center - Vector2.UnitY * 12f, -Vector2.UnitY);
                    return;
                }
                if (player.Top < (float)(bounds.Top - 24))
                {
                    player.Top = bounds.Top - 24;
                    player.OnBoundsV();
                }
            }
            if (self.CameraLockMode != 0 && rectangle.Bottom < bounds.Bottom - 4 && player.Top > (float)rectangle.Bottom)
            {
                if (SaveData.Instance.Assists.Invincible)
                {
                    player.Play("event:/game/general/assist_screenbottom");
                    player.Bounce(rectangle.Bottom);
                }
                else
                {
                    player.Die(Vector2.Zero);
                }
            }
            else if (player.Bottom > (float)bounds.Bottom && self.Session.MapData.CanTransitionTo(self, player.Center + Vector2.UnitY * 12f) && !self.Session.LevelData.DisableDownTransition)
            {
                if (!player.CollideCheck<Solid>(player.Position + Vector2.UnitY * 4f))
                {
                    player.BeforeDownTransition();
                    data.Invoke("NextLevel", player.Center + Vector2.UnitY * 12f, Vector2.UnitY);
                }
            }
            else if (player.Top > (float)bounds.Bottom && SaveData.Instance.Assists.Invincible)
            {
                player.Play("event:/game/general/assist_screenbottom");
                player.Bounce(bounds.Bottom);
            }
            else if (player.Top > (float)(bounds.Bottom + 4))
            {
                player.Die(Vector2.Zero);
            }
        }
    }

    internal static void Content_OnUpdate(ModAsset from, ModAsset to)
    {
        if (to.Format is ".meta")
        {
            try
            {
                AssetReloadHelper.Do("Reloading Meta", () =>
                {
                    //var effectName = to.PathVirtual.Substring("Effects/".Length, to.PathVirtual.Length - ".cso".Length - "Effects/".Length);
                    //if (from.TryDeserialize(out RugMeta meta) && VanillaEditMetadata.ContainsValue(meta))
                    //{
                    reload = true;
                    FallbackEffectDict.Clear();
                    //foreach (var i in AreaData.Areas)
                    //{
                    AreaDataOnLoad(AreaData.Areas);
                    //}
                    //}
                    //..if (FallbackEffectDict.Remove(effectName, out var effect))
                    //{ 
                    //if (!effect.IsDisposed)
                    //effect.Dispose();
                    //}

                    //Logger.Log(LogLevel.Info, "FrostHelper.ShaderHelper", $"Reloaded {effectName}");
                }//, () => {
                 //FrostModule.TryGetCurrentLevel()?.Reload();
                /*}*/);

            }
            catch (Exception e)
            {
                Logger.LogDetailed(e);
            }
        }
        else if (to.Format is "cso" or ".cso")
        {
            try
            {
                AssetReloadHelper.Do("Reloading Shader", () =>
                {
                    //var effectName = to.PathVirtual.Substring("Effects/".Length, to.PathVirtual.Length - ".cso".Length - "Effects/".Length);
                    //if (from.TryDeserialize(out RugMeta meta) && VanillaEditMetadata.ContainsValue(meta))
                    //{
                    reload = true;
                    reloadAcc = true;
                    FallbackEffectDict.Clear();
                    //foreach (var i in AreaData.Areas)
                    //{
                    AreaDataOnLoad(AreaData.Areas);
                    //}
                    //}
                    //..if (FallbackEffectDict.Remove(effectName, out var effect))
                    //{ 
                    //if (!effect.IsDisposed)
                    //effect.Dispose();
                    //}

                    //Logger.Log(LogLevel.Info, "FrostHelper.ShaderHelper", $"Reloaded {effectName}");
                }//, () => {
                 //FrostModule.TryGetCurrentLevel()?.Reload();
                /*}*/);

            }
            catch (Exception e)
            {
                Logger.LogDetailed(e);
            }

        }
    }

    public override void Unload()
    {
        //Everest.Events.Player.OnSpawn -= onPlayerSpawn;
        On.Celeste.OuiChapterPanel.Render -= OuiChapterPanel_RenderHook;
        On.Celeste.AreaData.Load -= AreaDataOnLoad;
        // On.Celeste.Dialog.Get -= DialogOnGet;
        //On.Celeste.Dialog.Clean -= DialogOnClean;

        //On.Celeste.BadelineOldsite.Added -= BadelineOldsiteOnAdded;
    }

    //DynamicData instanceData;

    protected virtual void RestartSpriteBatch(/*OuiChapterPanel self*/)
    {
        //if (instanceData == null || !instanceData.IsAlive)
        //{
            //instanceData = new DynamicData(typeof(GameplayRenderer));
        //}

       //MountainCamera cam = self.Overworld.Mountain.Camera;
        Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, (HiresRenderer.DrawToBuffer ? Matrix.Identity : Engine.ScreenMatrix));
        //if (Hires)
        //{
        //Draw.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, transformMatrix == null ? GameplayHudRenderer.GetMatrix() : transformMatrix.Value);
        //instanceData.Invoke("Begin");
        //var field = typeof(Camera).GetField("instace", BindingFlags.Instance | BindingFlags.NonPublic);
        //Camera instace = (Camera)field.GetValue();
        //Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, instance.Camera.Matrix);
        //}
        //else
        //{
        //GameplayRenderer.Begin();
        //}
    }

    private static Dictionary<string, Effect> FallbackEffectDict = new();
    private static Dictionary<string, Effect> FallbackEffectDictAcc = new();
    private static Dictionary<string, Effect> GetShaderHelperEffects() =>  FallbackEffectDict;
    private static Dictionary<string, Effect> GetShaderHelperEffectsAcc() => FallbackEffectDictAcc;

    private static Effect? _effect = null;
    private static string effectName = "";
    private static string effectNameAccent = "";
    private static bool reload;
    private static bool reloadAcc;
    private static Effect effect
    {
        get
        {
            //if (_effect != null) Logger.Log(LogLevel.Info, "meow", _effect.);

            if (effectName != "" && effectName != null && effectName != string.Empty)
            {
                if (!reload && GetShaderHelperEffects().TryGetValue(effectName, out var eff)) return eff;
                if (reload) reload = false;
                if (Everest.Content.TryGet($"Effects/{effectName}.cso", out var effectAsset, true))
                {
                    try
                    {
                        Effect effect = new Effect(Engine.Graphics.GraphicsDevice, effectAsset.Data);
                        GetShaderHelperEffects().Add(effectName, effect);
                        Logger.Log(LogLevel.Info, "RugHelper", "Loading shdaer: " + effectName.ToString());
                        return effect;
                    }
                    catch (Exception ex)
                    {  
                        Logger.Log(LogLevel.Error, "RugHelper", "Failed to load the shader " + effectName);
                        Logger.Log(LogLevel.Error, "RugHelper", "Exception: \n" + ex.ToString());
                    }
                }
                /*if (Engine.Graphics == null || Engine.Graphics.GraphicsDevice == null)
                {
                    throw new InvalidOperationException("Tried to obtain the shader too early!");
                }

                if (!Everest.Content.TryGet($"Effects/{effectName}.cso", out ModAsset effectAsset))
                {
                    throw new InvalidOperationException($"No Effects/{effectName}.cso file found!");
                }
                if (_effect != null)
                    return _effect == ShaderHelperIntegration.GetEffect(effectName); new Effect(Engine.Graphics.GraphicsDevice, effectAsset.Data) ? _effect : _effect = new Effect(Engine.Graphics.GraphicsDevice, effectAsset.Data);
                else
                    return _effect = new Effect(Engine.Graphics.GraphicsDevice, effectAsset.Data);
            }*/

            }
            return null;
        }
    }
    private static Effect effectAccent
    {
        get
        {
            //if (_effect != null) Logger.Log(LogLevel.Info, "meow", _effect.);

            if (effectNameAccent != "" && effectNameAccent != null && effectNameAccent != string.Empty)
            {
                if (!reloadAcc && GetShaderHelperEffectsAcc().TryGetValue(effectNameAccent, out var eff)) return eff;
                if (reloadAcc) reloadAcc = false;
                if (Everest.Content.TryGet($"Effects/{effectNameAccent}.cso", out var effectAsset, true))
                {
                    try
                    {
                        Effect effect = new Effect(Engine.Graphics.GraphicsDevice, effectAsset.Data);
                        if (!GetShaderHelperEffectsAcc().TryAdd(effectNameAccent, effect))
                        {
                            FallbackEffectDictAcc[effectNameAccent] = effect;
                        }
                        Logger.Log(LogLevel.Info, "RugHelper", "Loading accent shader: " + effectNameAccent.ToString());
                        return effect;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(LogLevel.Error, "RugHelper", "Failed to load the shader " + effectNameAccent);
                        Logger.Log(LogLevel.Error, "RugHelper", "Exception: \n" + ex.ToString());
                    }
                }
                else
                {
                    Logger.Log(LogLevel.Error, "RugHelper", "accent shader not found!");
                }
                /*if (Engine.Graphics == null || Engine.Graphics.GraphicsDevice == null)
                {
                    throw new InvalidOperationException("Tried to obtain the shader too early!");
                }

                if (!Everest.Content.TryGet($"Effects/{effectName}.cso", out ModAsset effectAsset))
                {
                    throw new InvalidOperationException($"No Effects/{effectName}.cso file found!");
                }
                if (_effect != null)
                    return _effect == ShaderHelperIntegration.GetEffect(effectName); new Effect(Engine.Graphics.GraphicsDevice, effectAsset.Data) ? _effect : _effect = new Effect(Engine.Graphics.GraphicsDevice, effectAsset.Data);
                else
                    return _effect = new Effect(Engine.Graphics.GraphicsDevice, effectAsset.Data);
            }*/

            }
            return null;
        }
    }

    // Optional, initialize anything after Celeste has initialized itself properly.
    public override void Initialize()
    {

    }

    public static Effect ApplyStandardParametersOverWorld(Effect effect, OuiChapterPanel self, Matrix? camera)
    {
        //var level = FrostModule.GetCurrentLevel() ?? throw new Exception("Not in a level when applying shader parameters! How did you...");


        effect.Parameters["DeltaTime"]?.SetValue(Engine.DeltaTime);
        effect.Parameters["Time"]?.SetValue(Engine.Scene.TimeActive);
        effect.Parameters["Dimensions"]?.SetValue(new Vector2(1920, 1920));
        effect.Parameters["CamPos"]?.SetValue(self.Overworld.Mountain.Camera.Position);
        effect.Parameters["ColdCoreMode"]?.SetValue(true);


        Viewport viewport = Engine.Graphics.GraphicsDevice.Viewport;

        Matrix projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);

        Matrix halfPixelOffset = Framework is FrameworkType.FNA
            ? Matrix.Identity
            : Matrix.CreateTranslation(-0.5f, -0.5f, 0f);

        effect.Parameters["TransformMatrix"]?.SetValue(halfPixelOffset * projection);

        effect.Parameters["ViewMatrix"]?.SetValue(camera ?? Matrix.Identity);
        effect.Parameters["Photosensitive"]?.SetValue(Settings.Instance.DisableFlashes);
        
        //effect.Parameters = parameters;
        
        return effect;
    }


    public void OuiChapterPanel_RenderHook(On.Celeste.OuiChapterPanel.orig_Render orig, OuiChapterPanel self)
    {
        orig(self);
        //Engine.Instance.GraphicsDevice.SetRenderTarget(GameplayBuffers.Gameplay);
        //Engine.Instance.
        //get current chapter
        DynamicData data = DynamicData.For(self);
        bool init = data.Get<bool>("initialized");
        if (!init) return;
        string chapter = data.Get<string>("chapter");
        string text = Dialog.Clean(AreaData.Get(self.Area).Name);

        Matrix transformMatrix = (HiresRenderer.DrawToBuffer ? Matrix.Identity : Engine.ScreenMatrix);
        MTexture titleTex = GFX.Gui[_ModAreaselectTexture("areaselect/title", self)];
        MTexture accentTex = GFX.Gui[_ModAreaselectTexture("areaselect/accent", self)];
        
        AreaData panelArea = AreaData.Get(self.Area);
        RugMeta rug = GetMetaForAreaData(panelArea);

        if (rug != null)
        {
            effectName = rug.Title.Effect;
            effectNameAccent = rug.TitleAccent.Effect;
        }
        else
        {
            effectNameAccent = "";
            effectName = "";
        }

        //effect.Parameters["sizeMult"]?.SetValue(2f);
        //effect.Parameters["speedMult"]?.SetValue(2.5f);
        //Logger.SetLogLevel("Rug", LogLevel.Info);
        //Logger.Log(LogLevel.Info, "Rug", $"checking name: {panelArea == null}, {rug == null}");
        if ((effectName != "" && effectName != null && effectName != string.Empty) || (effectNameAccent != "" && effectNameAccent != null && effectNameAccent != string.Empty))
        {
            if (effectName != "" && effectName != null && effectName != string.Empty)
            {
                //Logger.Log(LogLevel.Info, "Rug", "Attempting to get effect");
                if (effect != null)
                {
                    ApplyStandardParametersOverWorld(effect, self, transformMatrix);
                    // Logger.Log(LogLevel.Info, "Rug", "Applied standard parameters");
                    if (rug != null)
                    {
                        foreach (var keys in rug.Title.Parameters)
                        {
                            //Logger.Log(LogLevel.Info, "Rug", $"setting paramters {keys.paramName}");
                            //string[] key = keys.Split('=');
                            //if (key.Length == 2)
                            //{
                            //effect.Parameters[key[0]]?.SetValue(key[1]);
                            //}
                            //else if (key.Length == 3)
                            //{
                            switch (keys.paramType.ToLower())
                            {
                                case "float":
                                    //Logger.Log(LogLevel.Info, "Rug", $"set param {keys.paramName} as {keys.paramType}, with value {keys.paramValue}");
                                    effect.Parameters[keys.paramName]?.SetValue(float.Parse(keys.paramValue, CultureInfo.InvariantCulture));
                                    break;
                                case "bool":
                                    //Logger.Log(LogLevel.Info, "Rug", $"set param {keys.paramName} as {keys.paramType}, with value {keys.paramValue}");
                                    effect.Parameters[keys.paramName]?.SetValue(bool.Parse(keys.paramValue));
                                    break;
                                case "int":
                                    //Logger.Log(LogLevel.Info, "Rug", $"set param {keys.paramName} as {keys.paramType}, with value {keys.paramValue}");
                                    effect.Parameters[keys.paramName]?.SetValue(int.Parse(keys.paramValue));
                                    break;
                                case "string":
                                    //Logger.Log(LogLevel.Info, "Rug", $"set param {keys.paramName} as {keys.paramType}, with value {keys.paramValue}");
                                    effect.Parameters[keys.paramName]?.SetValue(keys.paramValue);
                                    break;
                                default:
                                    effect.Parameters[keys.paramName]?.SetValue(keys.paramValue);
                                    break;
                            }
                        }
                    }

                    Draw.SpriteBatch.End();
                    Draw.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, effect, transformMatrix);
                    //Logger.Log(LogLevel.Info, "meow", "meow");
                    //Draw.SpriteBatch.Draw(titleTex.Texture.Texture_Safe, dstRect, Color.White); //self.Position + new Vector2(_FixTitleLength(-60f, self), 0f), Vector2.Zero, self.Data.TitleBaseColor);
                    titleTex.Draw(self.Position + new Vector2(_FixTitleLength(-60f, self), 0f), Vector2.Zero, self.Data.TitleBaseColor);
                    //accentTex.Draw(self.Position + new Vector2(_FixTitleLength(-60f, self), 0f), Vector2.Zero, self.Data.TitleAccentColor);

                    Draw.SpriteBatch.End();
                    RestartSpriteBatch();
                }
            }
            if (effectNameAccent != "" && effectNameAccent != null && effectNameAccent != string.Empty)
            {
                //Logger.Log(LogLevel.Info, "Rug", "Attempting to get effect");
                if (effectAccent != null)
                {
                    ApplyStandardParametersOverWorld(effectAccent, self, transformMatrix);
                    // Logger.Log(LogLevel.Info, "Rug", "Applied standard parameters");
                    if (rug != null)
                    {
                        foreach (var keys in rug.Title.Parameters)
                        {
                            //Logger.Log(LogLevel.Info, "Rug", $"setting paramters {keys.paramName}");
                            //string[] key = keys.Split('=');
                            //if (key.Length == 2)
                            //{
                            //effect.Parameters[key[0]]?.SetValue(key[1]);
                            //}
                            //else if (key.Length == 3)
                            //{
                            switch (keys.paramType.ToLower())
                            {
                                case "float":
                                    //Logger.Log(LogLevel.Info, "Rug", $"set param {keys.paramName} as {keys.paramType}, with value {keys.paramValue}");
                                    effectAccent.Parameters[keys.paramName]?.SetValue(float.Parse(keys.paramValue, CultureInfo.InvariantCulture));
                                    break;
                                case "bool":
                                    //Logger.Log(LogLevel.Info, "Rug", $"set param {keys.paramName} as {keys.paramType}, with value {keys.paramValue}");
                                    effectAccent.Parameters[keys.paramName]?.SetValue(bool.Parse(keys.paramValue));
                                    break;
                                case "int":
                                    //Logger.Log(LogLevel.Info, "Rug", $"set param {keys.paramName} as {keys.paramType}, with value {keys.paramValue}");
                                    effectAccent.Parameters[keys.paramName]?.SetValue(int.Parse(keys.paramValue));
                                    break;
                                case "string":
                                    //Logger.Log(LogLevel.Info, "Rug", $"set param {keys.paramName} as {keys.paramType}, with value {keys.paramValue}");
                                    effectAccent.Parameters[keys.paramName]?.SetValue(keys.paramValue);
                                    break;
                                default:
                                    effectAccent.Parameters[keys.paramName]?.SetValue(keys.paramValue);
                                    break;
                            }
                        }
                    }

                    Draw.SpriteBatch.End();
                    Draw.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, effectAccent, GameplayHudRenderer.GetMatrix());
                    //Logger.Log(LogLevel.Info, "meow", "meow");
                    //Draw.SpriteBatch.Draw(titleTex.Texture.Texture_Safe, dstRect, Color.White); //self.Position + new Vector2(_FixTitleLength(-60f, self), 0f), Vector2.Zero, self.Data.TitleBaseColor);
                    //titleTex.Draw(self.Position + new Vector2(_FixTitleLength(-60f, self), 0f), Vector2.Zero, self.Data.TitleBaseColor);
                    accentTex.Draw(self.Position + new Vector2(_FixTitleLength(-60f, self), 0f), Vector2.Zero, self.Data.TitleAccentColor);

                    Draw.SpriteBatch.End();
                    RestartSpriteBatch();
                }
            }
            //}
            //DrawGradientOverlay(self.Position + new Vector2(_FixTitleLength(-60f, self), 0), titleTex, 0, 1); //accentTex);
            //DrawGradientOverlay(accentTex, self.Position + new Vector2(_FixTitleLength(-60f, self), 0), -50);
            if (self.Data.Interlude_Safe)
            {
                ActiveFont.Draw(text, self.Position + self.IconOffset + new Vector2(-100f, 0f), new Vector2(1f, 0.5f), Vector2.One * 1f, self.Data.TitleTextColor * 0.8f);
            }
            else
            {
                //self.chapter
                //Logger.Log(LogLevel.Info, "hmm", $"{chapter}");
                ActiveFont.Draw(chapter, self.Position + self.IconOffset + new Vector2(-100f, -2f), new Vector2(1f, 1f), Vector2.One * 0.6f, self.Data.TitleAccentColor * 0.8f);
                ActiveFont.Draw(text, self.Position + self.IconOffset + new Vector2(-100f, -18f), new Vector2(1f, 0f), Vector2.One * 1f, self.Data.TitleTextColor * 0.8f);
            }
            float light = data.Get<float>("spotlightAlpha");
            Vector2 lightpos = data.Get<Vector2>("spotlightPosition");
            float lightrad = data.Get<float>("spotlightRadius");
            if (light > 0f)
            {
                HiresRenderer.EndRender();
                SpotlightWipe.DrawSpotlight(lightpos, lightrad, Color.Black * light);
                HiresRenderer.BeginRender();
            }
        }

        //Vector2 pos = self.Position + new Vector2(_FixTitleLength(-60f, self), 0f);
        //Vector2 origin = Vector2.Zero;

        // draw your gradient
        //DrawGradient(titleTex, pos, origin);*/
    }

    private static float _FixTitleLength(float vanillaValue, OuiChapterPanel self)
    {
        float x = ActiveFont.Measure(Dialog.Clean(AreaData.Get(self.Area).Name)).X;
        return vanillaValue - Math.Max(0f, x + vanillaValue - 490f);
    }

    private string _ModAreaselectTexture(string textureName, OuiChapterPanel self)
    {
        string name = AreaData.Areas[self.Area.ID].Name;
        string text = textureName.Replace("areaselect/", $"areaselect/{name}_");
        if (GFX.Gui.Has(text))
        {
            textureName = text;
            return textureName;
        }
        string arg = SaveData.Instance?.LevelSet ?? "Celeste";
        string text2 = textureName.Replace("areaselect/", $"areaselect/{arg}/");
        if (GFX.Gui.Has(text2))
        {
            textureName = text2;
            return textureName;
        }
        return textureName;
    }

    /*private void modDashLength(ILContext il)
    {
        ILCursor cursor = new ILCursor(il);

        // jump where 0.3 or 0.15f are loaded (those are dash times)
        while (cursor.TryGotoNext(instr => instr.MatchLdfld("Celeste.AreaData", "TitleBaseColor")))
        {

            Logger.Log("Rug/Color", $"Applying gradient overlay at {cursor.Index} in {cursor.Method.FullName}");

            // Move to the MTexture.Draw call
            if (cursor.TryGotoNext(instr => instr.MatchCallvirt<Monocle.MTexture>("Draw")))
            {

                // We'll insert instructions *after* the original Draw
                cursor.Index++;
                cursor.Emit(OpCodes.Pop);
                // Inject a delegate to draw the gradient overlay
                cursor.EmitDelegate<Action<Monocle.MTexture, OuiChapterPanel>>(DrawGradientOverlay);
            }
        }
        while (cursor.TryGotoNext(instr => instr.MatchLdfld("Celeste.AreaData", "TitleBaseColor")))
        {
            //Logger.Log("ExtendedVariantMode/DashLength", $"Applying dash length to constant at {cursor.Index} in CIL code for {cursor.Method.FullName}");
            Logger.Log("Rug/Color", $"Applying UpdateHue at {cursor.Index} in {cursor.Method.FullName}");

            cursor.Index++;         // move after ldfld
            cursor.Emit(OpCodes.Pop);
            cursor.EmitDelegate<Func<Color>>(hue);
            //cursor.Emit(OpCodes.Mul);
        }
    }*/

    private static void DrawGradientOverlay(Vector2 pos, MTexture tex, int offset = 0, int pixelSize = 1)
    {
        int width = tex.Width;
        int height = tex.Height;
        for (int y = 0; y < height; y += pixelSize)
        {
            float ty = y / (float)(height - pixelSize);
            for (int x = 0; x < width; x += pixelSize)
            {
                Rectangle lineRect = new Rectangle(x, y, pixelSize, pixelSize);
                MTexture texture = tex.GetSubtexture(lineRect);
                float tx = x / (float)(width - pixelSize);
                Color lineColor = hue(new Vector2(tx, ty), 2f, 1f);
                lineColor = new Color(lineColor.R + offset, lineColor.G + offset, lineColor.B + offset, lineColor.A);
                texture.Draw(pos + new Vector2(x, y), Vector2.Zero, lineColor);
            }
        }
    }

    private static Color GetGradientColor(float tx, float ty, Vector2 origin, Vector2 target, Color startColor, Color endColor)
    {
        // Normalize relative to origin, target
        float t = ((tx - origin.X) * target.X + (ty - origin.Y) * target.Y) /
                  (target.X * target.X + target.Y * target.Y);
        t = MathHelper.Clamp(t, 0f, 1f);
        return Color.Lerp(startColor, endColor, t);
    }

    private static Color hue(Vector2 pos, float sizeMult = 1f, float speedMult = 1f)
    {
        // pos.X and pos.Y should be in 0..1 across the texture
        float cycle = (pos.X * sizeMult + pos.Y * sizeMult + Engine.Scene.TimeActive * 0.2f * speedMult) % 1f;
        return Calc.HsvToColor(0.4f + Calc.YoYo(cycle) * 0.4f, 0.4f, 0.9f);
    }

    /*private static void OuiRender(On.Celeste.OuiChapterPanel.orig_Render orig, OuiChapterPanel self)
    {
        var field = typeof(OuiChapterPanel).GetField("initialized", BindingFlags.Instance | BindingFlags.NonPublic);
        bool init = (bool)field.GetValue(self);
        if (!init) return;
        field = typeof(OuiChapterPanel).GetField("OptionsRenderPosition", BindingFlags.Instance | BindingFlags.NonPublic);
        Vector2 optionsRenderPosition = (Vector2)field.GetValue(self);
        //Vector2 optionsRenderPosition = self.OptionsRenderPosition;
        for (int i = 0; i < self.options.Count; i++)
        {
            if (!self.options[i].OnTopOfUI)
            {
                self.options[i].Render(optionsRenderPosition, self.option == i, self.wiggler, self.modeAppearWiggler);
            }
        }
        bool flag = false;
        if (self.RealStats.Modes[(int)self.Area.Mode].Completed)
        {
            int mode = (int)self.Area.Mode;
            foreach (EntityData goldenberry in AreaData.Areas[self.Area.ID].Mode[mode].MapData.Goldenberries)
            {
                EntityID item = new EntityID(goldenberry.Level.Name, goldenberry.ID);
                if (self.RealStats.Modes[mode].Strawberries.Contains(item))
                {
                    flag = true;
                    break;
                }
            }
        }
        MTexture mTexture = GFX.Gui[(!flag) ? "areaselect/cardtop" : "areaselect/cardtop_golden"];
        mTexture.Draw(self.Position + new Vector2(0f, -32f));
        MTexture mTexture2 = GFX.Gui[(!flag) ? "areaselect/card" : "areaselect/card_golden"];
        self.card = mTexture2.GetSubtexture(0, mTexture2.Height - (int)self.height, mTexture2.Width, (int)self.height, self.card);
        self.card.Draw(self.Position + new Vector2(0f, -32 + mTexture.Height));
        for (int j = 0; j < self.options.Count; j++)
        {
            if (self.options[j].OnTopOfUI)
            {
                self.options[j].Render(optionsRenderPosition, self.option == j, self.wiggler, self.modeAppearWiggler);
            }
        }
        ActiveFont.Draw(self.options[self.option].Label, optionsRenderPosition + new Vector2(0f, -140f), Vector2.One * 0.5f, Vector2.One * (1f + self.wiggler.Value * 0.1f), Color.Black * 0.8f);
        if (self.selectingMode)
        {
            self.strawberries.Position = self.contentOffset + new Vector2(0f, 170f) + self.strawberriesOffset;
            self.deaths.Position = self.contentOffset + new Vector2(0f, 170f) + self.deathsOffset;
            self.heart.Position = self.contentOffset + new Vector2(0f, 170f) + self.heartOffset;
            MethodInfo baseRender = typeof(OuiTitleScreen).BaseType
            .GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            baseRender.Invoke(self, null);
        }
        if (!self.selectingMode)
        {
            Vector2 center = self.Position + new Vector2(self.contentOffset.X, 340f);
            for (int num = self.options.Count - 1; num >= 0; num--)
            {
                self.DrawCheckpoint(center, self.options[num], num);
            }
        }
        GFX.Gui["areaselect/title"].Draw(self.Position + new Vector2(-60f, 0f), Vector2.Zero, self.Data.TitleBaseColor);
        GFX.Gui["areaselect/accent"].Draw(self.Position + new Vector2(-60f, 0f), Vector2.Zero, self.Data.TitleAccentColor);
        string text = Dialog.Clean(AreaData.Get(self.Area).Name);
        if (self.Data.Interlude)
        {
            ActiveFont.Draw(text, self.Position + self.IconOffset + new Vector2(-100f, 0f), new Vector2(1f, 0.5f), Vector2.One * 1f, self.Data.TitleTextColor * 0.8f);
        }
        else
        {
            ActiveFont.Draw(self.chapter, self.Position + self.IconOffset + new Vector2(-100f, -2f), new Vector2(1f, 1f), Vector2.One * 0.6f, self.Data.TitleAccentColor * 0.8f);
            ActiveFont.Draw(text, self.Position + self.IconOffset + new Vector2(-100f, -18f), new Vector2(1f, 0f), Vector2.One * 1f, self.Data.TitleTextColor * 0.8f);
        }
        if (self.spotlightAlpha > 0f)
        {
            HiresRenderer.EndRender();
            SpotlightWipe.DrawSpotlight(self.spotlightPosition, self.spotlightRadius, Color.Black * self.spotlightAlpha);
            HiresRenderer.BeginRender();
        }
    }
    */

    private static Color UpdateHue(Vector2 pos)
    {
        return GetHue(new Vector2(0, 0));
        //return color
    }

    private static Color GetHue(Vector2 position)
    {
        float num = 280f;
        float value = (position.Length() + Engine.Scene.TimeActive * 50f) % num / num;
        return Calc.HsvToColor(0.4f + Calc.YoYo(value) * 0.4f, 0.4f, 0.9f);
    }


    // disabled until i bring back colorful followers
    /*public static void onPlayerSpawn(Player player)
    {
        if (Settings.ColorfulFieldTrip)
        {
            int i = 0;
            while (i < 35)
            {
                string variant = BadelineHairColors.Colors_Index[i % 8];
                ChangedFollower.SpawnColorfulBadelineFriendo(player.SceneAs<Level>(), variant);
                i++;
            }
        }
    }*/

    private static Dictionary<AreaData, RugMeta> VanillaEditMetadata = new Dictionary<AreaData, RugMeta>();

    private static void AreaDataOnLoad(List<AreaData> areas)
    {
        //orig();
        foreach (AreaData map in areas)
        {
            //Logger.Log(LogLevel.Info, "RugMeta", "Maps/" + map.Mode[0].Path + ".Rug.meta.yaml");
            //if ($"Maps/{map.Mode[0].Path}.Rug.meta.yaml" == "Maps/Examples/2.Rug.meta.yaml")
            //{
            /*Logger.Log(LogLevel.Info, "RugMeta", $"Loaded meta test: {YamlHelper.Serializer.Serialize(new RugMeta
            {
                Effect = "Cool/Em3",
                Parameters = new[] {
                new RugMetaPage { paramName = "speedMult", paramType = "float", paramValue = "1" },
                new RugMetaPage { paramName = "sizeMult", paramType = "float", paramValue = "1" }
            }
            })}");*/
            //}
            if (Everest.Content.TryGet("Maps/" + map.Mode[0].Path + ".Rug.meta",
                out ModAsset metadata) && metadata.TryDeserialize(out RugMeta meta))
            {
                Logger.Log(LogLevel.Warn, "RugMeta", "Meta found: " + meta.ToString() + " " + meta.Title.Effect.ToString() + " " + meta.Title.Parameters.ToString());
                foreach (var i in meta.Title.Parameters)
                {
                    Logger.Log(LogLevel.Info, "RugMeta Parameters", $"{i.paramName}, {i.paramType}, {i.paramValue}");
                }
                //string uh;Parameters:
                //-paramName: speedMult
                //paramType: float
                //paramValue: 1
                //- paramName: sizeMult
                //paramType: float
                //paramValue: 1

                //if (metadata.TryDeserialize<string>(out uh))
                //Logger.Log(LogLevel.Info, "RugMeta", "Meta Raw Content:\n" + uh);
                //if (!VanillaEditMetadata.ContainsKey(map))
                //VanillaEditMetadata.Add(map, meta);
                //else
                //Logger.Log(LogLevel.Info, "Rug", $"trying to set value of {map.SID}");
                VanillaEditMetadata[map] = meta;
            }
            else
            {
                //VanillaEditMetadata.Remove(map);
            }
        }
    }

    private static void AreaDataOnLoad(On.Celeste.AreaData.orig_Load orig)
    {
        orig();
        foreach (AreaData map in AreaData.Areas)
        {
            //Logger.Log(LogLevel.Info, "RugMeta", "Maps/" + map.Mode[0].Path + ".Rug.meta.yaml");
            //if ($"Maps/{map.Mode[0].Path}.Rug.meta.yaml" == "Maps/Examples/2.Rug.meta.yaml")
            //{
            /*Logger.Log(LogLevel.Info, "RugMeta", $"Loaded meta test: {YamlHelper.Serializer.Serialize(new RugMeta
            {
                Effect = "Cool/Em3",
                Parameters = new[] {
                new RugMetaPage { paramName = "speedMult", paramType = "float", paramValue = "1" },
                new RugMetaPage { paramName = "sizeMult", paramType = "float", paramValue = "1" }
            }
            })}");*/
            //}
            if (Everest.Content.TryGet("Maps/" + map.Mode[0].Path + ".Rug.meta",
                out ModAsset metadata) && metadata.TryDeserialize(out RugMeta meta))
            {
                if (meta.Title != null)
                {
                    Logger.Log(LogLevel.Warn, "RugMeta", "Meta found: " + meta.ToString() + " " + meta.Title.Effect.ToString() + " " + meta.Title.Parameters.ToString());
                    foreach (var i in meta.Title.Parameters)
                    {
                        Logger.Log(LogLevel.Info, "RugMeta Parameters", $"{i.paramName}, {i.paramType}, {i.paramValue}");
                    }
                }
                //string uh;Parameters:
                //-paramName: speedMult
                //paramType: float
                //paramValue: 1
                //- paramName: sizeMult
                //paramType: float
                //paramValue: 1

                //if (metadata.TryDeserialize<string>(out uh))
                //Logger.Log(LogLevel.Info, "RugMeta", "Meta Raw Content:\n" + uh);
                //if (!VanillaEditMetadata.ContainsKey(map))
                //VanillaEditMetadata.Add(map, meta);
                //else
                //Logger.Log(LogLevel.Info, "Rug", $"trying to set value of {map.SID}");
                VanillaEditMetadata[map] = meta;
            }
            else
            {
                //VanillaEditMetadata.Remove(map);
            }
        }
    }

    private static RugMeta GetMetaForAreaData(AreaData area)
    {
        //Logger.Log(LogLevel.Info, "Rug", $"trying to get value of {area.SID}");
        if (VanillaEditMetadata.ContainsKey(area))
        {
            //Logger.Log(LogLevel.Info, "Rug", $"trying to get value of {area.SID}");
            return VanillaEditMetadata[area];
        }
        return null;
    }

    private static RugMeta CurrentMeta => GetMetaForAreaData(AreaData.Get(
        ((Engine.Scene as Level ?? (Engine.Scene as LevelLoader)?.Level)?.Session?.Area).GetValueOrDefault()));
}

public class RugMeta //: IMeta
{
    public RugMetaBook Title { get; set; } = new();
    public RugMetaBook TitleAccent { get; set; } = new();
}

public class RugMetaBook //: IMeta
{
    public String Effect { get; set; } = "";
    public RugMetaPage[] Parameters { get; set; } = new RugMetaPage[0];
}
public class RugMetaPage
{
    public String paramName { get; set; } = null;
    public String paramType { get; set; } = null;
    public String paramValue { get; set; } = null;
}


public class GameplayHudRenderer : Renderer
{
    public static BitTag GameplayHud = null!;
    public static readonly GameplayHudRenderer Instance = new();

    public override void Render(Scene scene)
    {
        Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, GetMatrix());
        scene.Entities.RenderOnly(GameplayHud);
        Draw.SpriteBatch.End();
    }

    public static Matrix GetMatrix()
    {
        Matrix matrix = Engine.ScreenMatrix;
        if (SaveData.Instance.Assists.MirrorMode)
        {
            matrix *= Matrix.CreateTranslation(-Engine.Viewport.Width, 0f, 0f);
            matrix *= Matrix.CreateScale(-1f, 1f, 1f);
        }
        return matrix;
    }

    internal static void ILLevelRender(ILContext il)
    {
        ILCursor cursor = new(il);
        if (!cursor.TryGotoNext(MoveType.AfterLabel, i => i.MatchLdarg0(), i => i.MatchLdfld(typeof(Level), nameof(Level.SubHudRenderer))))
        {
            throw new InvalidOperationException("Cannot find this.SubHudRenderer!");
        }

        cursor.EmitLdarg0();
        cursor.EmitDelegate(RenderForScene);
    }

    private static void RenderForScene(Scene scene)
    {
        Instance.Render(scene);
    }

    internal static void ILGameplayRenderer(ILContext il)
    {
        ILCursor cursor = new(il);

        if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchCallOrCallvirt(typeof(EntityList), "RenderExcept")))
        {
            throw new InvalidOperationException("Cannot find scene.Entities.RenderExcept(int32)!");
        }

        cursor.EmitLdsfld(typeof(GameplayHudRenderer).GetField(nameof(GameplayHud))!);
        cursor.EmitCall(typeof(BitTag).GetMethod("op_Implicit")!);
        cursor.EmitOr();
    }
}


