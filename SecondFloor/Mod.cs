using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace SageSecondFloor
{
    public class SetEverythingOnFire : GenericSystemBase, IModSystem
    {
        public const string MOD_GUID = "com.sage.secondfloor";
        public const string MOD_NAME = "Second Floor";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "Sage";
        private EntityQuery Appliances;

        struct CHasBeenSetOnFire : IModComponent { }

        protected override void Initialise()
        {
            base.Initialise();
            Appliances = GetEntityQuery(new QueryHelper()
                    .All(typeof(CAppliance))
                    .None(
                        typeof(CFire),
                        typeof(CIsOnFire),
                        typeof(CFireImmune),
                        typeof(CHasBeenSetOnFire)
                    ));
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnUpdate()
        {
            var appliances = Appliances.ToEntityArray(Allocator.TempJob);
            foreach (var appliance in appliances)
            {
                EntityManager.AddComponent<CIsOnFire>(appliance);
                EntityManager.AddComponent<CHasBeenSetOnFire>(appliance);
            }
            appliances.Dispose();
        }
    }

    #region Logging
    public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
    public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
    public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
    public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
    public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
    public static void LogError(object _log) { LogError(_log.ToString()); }
    #endregion
}
