using ECommons.EzIpcManager;
using ECommons.Reflection;

namespace HuntTrainAssistant.Services;

#nullable disable
public class NavmeshIPC
{
    public const string Name = "vnavmesh";
    public const string Repo = "https://puni.sh/api/repository/veyn";
    public NavmeshIPC() => EzIPC.Init(this, Name);
    public static bool Installed => DalamudReflector.TryGetDalamudPlugin(Name, out _, false, true);

    [EzIPC("Nav.%m")] public readonly Func<bool> IsReady;
    [EzIPC("Nav.%m")] public readonly Func<float> BuildProgress;
    [EzIPC("Nav.%m")] public readonly Func<bool> Reload;
    [EzIPC("Nav.%m")] public readonly Func<bool> Rebuild;
    [EzIPC("Nav.%m")] public readonly Func<Vector3, Vector3, bool, Vector3> Pathfind;

    [EzIPC("SimpleMove.%m")] public readonly Func<Vector3, bool, bool> PathfindAndMoveTo;
    [EzIPC("SimpleMove.%m")] public readonly Func<bool> PathfindInProgress;

    [EzIPC("Path.%m")] public readonly Action Stop;
    [EzIPC("Path.%m")] public readonly Func<bool> IsRunning;

    [EzIPC("Query.Mesh.%m")] public readonly Func<Vector3, float, float, Vector3?> NearestPoint;
    [EzIPC("Query.Mesh.%m")] public readonly Func<Vector3, bool, float, Vector3?> PointOnFloor;
}
