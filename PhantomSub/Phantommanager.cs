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
    public class Phantommanager  : MonoBehaviour
    {
        public static Phantommanager main
        {
            get
            {
                return Player.main.gameObject.EnsureComponent<Phantommanager>();
            }
        }
        public List<PhantomSub> AllPrawns = new List<PhantomSub>();
        public PhantomSub FindNearestPhantom(Vector3 mount)
        {
            float ComputeDistance(PhantomSub cc)
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
            PhantomSub nearestContainer = null;
            foreach (PhantomSub cont in AllPrawns)
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
            //nearestContainer = AllCricketContainers.OrderBy(x => ComputeDistance(x)).FirstOrDefault();
            return nearestContainer;
        }
        public void RegisterPhantom(PhantomSub cont)
        {
            AllPrawns.Add(cont);
        }
        public void DeregisterPhantom(PhantomSub cont)
        {
            AllPrawns.Remove(cont);
        }
    }
}