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
   
    internal class Exosuitenterpatch {

        [HarmonyPostfix]
        [HarmonyPatch("EnterVehicle")]
        public static void Postfix(Exosuit __instance)
        {
            PhantomSub closest = Phantommanager.main.FindNearestPhantom(__instance.transform.position);

            if (closest != null)
            {
                /*string msg2 = "patch1";
                Nautilus.Utility.BasicText message = new Nautilus.Utility.BasicText(500, 0);
                message.ShowMessage(msg2, 200 * Time.deltaTime);*/
                //if (Vector3.Distance(__instance.transform.position, closest.transform.position) < 10)
                //{
                if (closest.currentMount == __instance)
                    {
                    /*string msg = "patch2";
                    Nautilus.Utility.BasicText message1 = new Nautilus.Utility.BasicText(500, 0);
                    message1.ShowMessage(msg, 200 * Time.deltaTime);*/
                    closest.TryDetachContainer();

                    }
                //}
            }
        }




    }
   
    

}
