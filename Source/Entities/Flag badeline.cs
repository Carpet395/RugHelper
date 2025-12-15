using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.Rug.Entities;

[CustomEntity("Rug/FlagBadeline")]
public class FlagBadeline : FinalBoss
{

    // -- just badeline with a flag, camera lock mode is disabled cause when there are two badelines like this a small missmatch in flags and its game over for the camera -- //

    string flag;
    bool needTo;

    //[MethodImpl(MethodImplOptions.NoInlining)]
    public FlagBadeline(Vector2 position, Vector2[] nodes, int patternIndex, float cameraYPastMax, bool dialog, bool startHit, bool cameraLockY, string flag, bool needTo)
    : base(position, nodes, patternIndex, cameraYPastMax, dialog, startHit, cameraLockY)
    {
        this.needTo = needTo;
        this.flag = flag;
    }

    /*public FlagBadeline(EntityData data, Vector2 offset)
        : this(data.Position + offset, data.NodesOffset(offset), data.Int("patternIndex"), data.Float("cameraPastY"), data.Bool("dialog"), data.Bool("startHit"), data.Bool("cameraLockY"), data.String("flag"), data.Bool("state"))
    {
    }*/

    public override void Added(Scene scene)
    {
        (scene as Level).CameraLockMode = Level.CameraLockModes.None;
        if ((scene as Level).Session.GetFlag(flag) == needTo)
            base.Added(scene);
        else
            base.RemoveSelf();
        //SceneAs<Level>().CameraLockMode = Level.CameraLockModes.None;
        //base.Added(scene);
    }

    public override void Awake(Scene scene)
    {
        (scene as Level).CameraLockMode = Level.CameraLockModes.None;
        if ((scene as Level).Session.GetFlag(flag) == needTo)
            base.Awake(scene);
        else
            base.RemoveSelf();
    }

    public FlagBadeline(EntityData data, Vector2 offset)
    : base(data, offset)
    {
        //Logger.Log(LogLevel.Info, "meow meow,", "meow meow, meow meow meow");
        flag = data.String("flag");
        needTo = data.Bool("state");
    }

    public override void Update()
    {
        (Engine.Scene as Level).CameraLockMode = Level.CameraLockModes.None;
        if ((Engine.Scene as Level).Session.GetFlag(flag) == needTo)
            base.Update();
    }
}
