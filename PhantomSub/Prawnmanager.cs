using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using VehicleFramework;
using VehicleFramework.VehicleParts;
using VehicleFramework.VehicleTypes;

namespace PhantomSub
{
    public class PrawnManager : MonoBehaviour
    {
        public static PrawnManager main
        {
            get
            {
                return Player.main.gameObject.EnsureComponent<PrawnManager>();
            }
        }
        public List<Exosuit> AllPrawns = new List<Exosuit>();
        public Exosuit FindNearestPrawn(Vector3 mount)
        {
            float ComputeDistance(Exosuit cc)
            {
                try
                {
                    return Vector3.Distance(mount, cc.transform.position);
                }
                catch
                {
                    return 9999;
                }
            }
            Exosuit nearestContainer = null;
            foreach (Exosuit cont in AllPrawns)
            {
                if (cont is null)
                {
                    continue;
                }
                if (nearestContainer == null || (ComputeDistance(cont) < ComputeDistance(nearestContainer)))
                {
                   
                        nearestContainer = cont;
                    
                }
                
            }
            
            return nearestContainer;
        }
        public void RegisterPrawn(Exosuit cont)
        {
            AllPrawns.Add(cont);
        }
        public void DeregisterPrawn(Exosuit cont)
        {
            AllPrawns.Remove(cont);
        }
    }
}