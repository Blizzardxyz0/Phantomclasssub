using HarmonyLib;
using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static VFXParticlesPool;

namespace PhantomSub
{
    [HarmonyPatch(typeof(Exosuit))]
    internal class Exosuitregisterpatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        public static void Postfix(Exosuit __instance)
        {
            PrawnManager.main.RegisterPrawn(__instance);

        }
        
        

        



    }
   
   
    

}
