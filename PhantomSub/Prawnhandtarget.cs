using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PhantomSub
{
    public class Prawnhandtarget : HandTarget, IHandTarget
    {

        public Transform PrawnMountPoint
        {
            get
            {
                return transform.Find("PrawnMountPoint");
            }
        }
        void IHandTarget.OnHandClick(GUIHand hand)
        {
            if (GameInput.GetButtonDown(GameInput.Button.LeftHand))
            {
                PhantomSub closest = Phantommanager.main.FindNearestPhantom(this.transform.position);
                Exosuit container = closest.currentMount;
                if (container != null)
                {
                    /*Nautilus.Utility.BasicText message = new Nautilus.Utility.BasicText(500, 0);
                    string msg = "HandtargetFailed";
                    message.ShowMessage(msg, 200 * Time.deltaTime);*/
                    closest.PlayerExit();
                    container.EnterVehicle(Player.main, true);
                    closest.playerinside = false;

                }

            }

        }
        void IHandTarget.OnHandHover(GUIHand hand)
        {
            HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            string displayString = "Enter Docked Vehicle";
            HandReticle.main.SetTextRaw(HandReticle.TextType.Hand, displayString);


        }
    }

    /*public class BedAnimationSupplier : MonoBehaviour
    {
        [SerializeField] Bed bed;
        private IEnumerator Start()
        {
            var request = CraftData.GetPrefabForTechTypeAsync(TechType.NarrowBed);
            yield return request;
            var bedPrefab = request.GetResult().GetComponent<Bed>();
            bed.animator = bedPrefab.animator;
            bed.leftLieDownCinematicController = bedPrefab.leftLieDownCinematicController;
            bed.rightLieDownCinematicController = bedPrefab.rightLieDownCinematicController;
            bed.leftStandUpCinematicController = bedPrefab.leftStandUpCinematicController;
            bed.rightStandUpCinematicController = bedPrefab.rightStandUpCinematicController;
            bed.cinematicController = bedPrefab.cinematicController;
            bed.currentStandUpCinematicController = bedPrefab.currentStandUpCinematicController;
            bed.leftAnimPosition = bedPrefab.leftAnimPosition;
            bed.rightAnimPosition = bedPrefab.rightAnimPosition;
            bed.handText = bedPrefab.handText;
            bed.triggerType = bedPrefab.triggerType;
            bed.volumeTriggerType = bedPrefab.volumeTriggerType;
            bed.cinematicController = bedPrefab.cinematicController;
            bed.secureInventory = bedPrefab.secureInventory;
            bed.restoreActiveQuickSlot = bedPrefab.restoreActiveQuickSlot;
            bed.onCinematicStart = bedPrefab.onCinematicStart;
            bed.onCinematicEnd = bedPrefab?.onCinematicEnd;
            bed.debug = bedPrefab.debug;


        }
    }*/

    
}