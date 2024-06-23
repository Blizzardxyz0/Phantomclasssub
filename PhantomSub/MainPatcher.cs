using BepInEx;
using HarmonyLib;
using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Json.Attributes;
using Nautilus.Options.Attributes;
using Nautilus.Utility;

using System;
using System.Collections.Generic;
using UnityEngine;
using static FlexibleGridLayout;





namespace PhantomSub
{
    public static class Logger
    {
        public static void Log(string message)
        {
            UnityEngine.Debug.Log("[Carry]:" + message);
        }
        public static void Output(string msg)
        {
            BasicText message = new BasicText(500, 0);
            message.ShowMessage(msg, 5);
        }
    }
    [BepInPlugin("com.blizzard.subnautica.PhantomSub.mod", "PhantomSub", "2.1.1")]
    [BepInDependency("com.mikjaw.subnautica.vehicleframework.mod")]
    [BepInDependency("com.snmodding.nautilus")]

    public class MainPatcher : BaseUnityPlugin
    {
        public void Start()
        {
            var harmony = new Harmony("com.blizzard.subnautica.PhantomSub.mod");
            harmony.PatchAll();
            UWE.CoroutineHost.StartCoroutine(PhantomSub.Register());
        }
        

    }

    

    [FileName("Prawnsaves")]
    internal class SaveData : SaveDataCache
    {
        
        
        public List<Tuple<Vector3, bool>> AttachmentStatuses { get; set; }
    }


}