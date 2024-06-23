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
using static VFXParticlesPool;

namespace PhantomSub
{
    public partial class PhantomSub : Submarine
    {
        public Transform PrawnMountPoint
        {
            get
            {
                return transform.Find("PrawnMountPoint");
            }
        }
        public Transform Dockexit
        {
            get
            {
                return transform.Find("Dockexit");
            }
        }
       









        public Exosuit currentMount;
        public bool playerinside;
        
        public bool detachflag = false;
        public int rotationcount = 0;
        
        public override void Update()
        {
            base.Update();
            Exosuit container = PrawnManager.main.FindNearestPrawn(PrawnMountPoint.transform.position);

           

          
           
            if (!IsPowered())
            {
                return;
            }
           

            
            if (detachflag == true && Vector3.Distance(PrawnMountPoint.position, container.transform.position) > 5)
            {
                detachflag = false;

            }
            
            if (currentMount is null)
            {
                ShowAttachmentStatus();

            }
            
            if (currentMount is null)
            {
                if (container != null)
                {
                    if (container.rotationDirty == true)
                    {
                        rotationcount++;
                        if (rotationcount == 500)
                        {
                            container.rotationDirty = false;
                            rotationcount = 0;
                        }
                    }
                }
            }
            
            

            if (currentMount is null && detachflag == false)
            {
                TryAttachContainer();
            }
            if (currentMount != null && detachflag == false)
                {
                currentMount.transform.position = PrawnMountPoint.position;
                currentMount.transform.rotation = PrawnMountPoint.rotation;

            
                }
          
            


        }

        public override void Awake()
        {
            base.Awake();
            Phantommanager.main.RegisterPhantom(this);
            playerinside = false;
        }
        
        

        

        public void TryAttachContainer()
        {
            if (currentMount != null)
            {
                return;
            }
            Exosuit container = PrawnManager.main.FindNearestPrawn(PrawnMountPoint.transform.position);
            if (container == null) {
                return;
            
            }
            if (!ValidateAttachment(container))
            {
               
                return;
            }
            AttachContainer(container);
            

        }
        public bool ValidateAttachment(Exosuit container)
        {
            if (container is null)
            {
                return false;
            }
            if (Vector3.Distance(PrawnMountPoint.position, container.transform.position) < 5 && detachflag == false)
            {
                return true;
            }

            return false;
                
            
            
        }
        public void AttachContainer(Exosuit bloederroboter)
        {

            // Set the parent of the container to be "this" object
            
            //bloederroboter.transform.SetParent(this.transform);

            // Set the world position and rotation of the container to match PrawnMountPoint
            bloederroboter.transform.localPosition = PrawnMountPoint.position;
            bloederroboter.transform.localRotation = PrawnMountPoint.rotation;
            
            bloederroboter.liveMixin.shielded = true;
            //bloederroboter.collisionModel.SetActive(false);
            bloederroboter.useRigidbody.isKinematic = true;
            bloederroboter.crushDamage.enabled = false;
            bloederroboter.UpdateCollidersForDocking(true);

            currentMount = bloederroboter;
            if (playerinside == false) 
            {
                Player.main.rigidBody.velocity = Vector3.zero;
                
                Player.main.ToNormalMode(false);
                Player.main.rigidBody.angularVelocity = Vector3.zero;
                Player.main.ExitLockedMode(false, false);
                Player.main.SetPosition(Dockexit.position);
                Player.main.ExitSittingMode();
                
                IsPlayerDry = true;
                Player.main.SetScubaMaskActive(false);
                playerinside = true;
                try
                {
                    foreach (GameObject window in CanopyWindows)
                    {
                        window?.SetActive(false);
                    }
                }
                catch (Exception)
                {
                    //It's okay if the vehicle doesn't have a canopy
                }
                Player.main.SetCurrentSub(GetComponent<SubRoot>());
                NotifyStatus(PlayerStatus.OnPlayerEntry); 
            }
            
            
           
         }
        public void TryDetachContainer()
        {
            if (currentMount == null)
            {
                return;
            }
            /*string msg = "Faileduifhae9oiwfhieuoshfoierhio";
            Nautilus.Utility.BasicText message = new Nautilus.Utility.BasicText(500, 0);
            message.ShowMessage(msg, 200 * Time.deltaTime);*/
            /*currentMount.transform.SetParent(null);
            LargeWorldStreamer.main.cellManager.RegisterGlobalEntity(currentMount);*/


            detachflag = true;
            Logger.Log("1");
            //currentMount.collisionModel.SetActive(true);
            Logger.Log("2");
            currentMount.rotationDirty = true;
            Logger.Log("3");
            currentMount.liveMixin.shielded = true;
            Logger.Log("4");
            currentMount.useRigidbody.velocity = Vector3.zero;
            Logger.Log("5");

            currentMount.useRigidbody.isKinematic = false;
            Logger.Log("6");
            currentMount.crushDamage.enabled = true;
            Logger.Log("7");
            currentMount.UpdateCollidersForDocking(false);
            Logger.Log("8");

            //
            currentMount = null;
            
            /*string ms2g = "FailedDeatach45555555555555555555555555555555555555555555555555555555555555555555";
            Nautilus.Utility.BasicText message2 = new Nautilus.Utility.BasicText(500, 0);
            message2.ShowMessage(ms2g, 20 * Time.deltaTime);*/



        }
        

        public void ShowAttachmentStatus()
        {
            Exosuit container = PrawnManager.main.FindNearestPrawn(PrawnMountPoint.transform.position);
            if (container is null)
            {
                /*string msg = "Failed";
                Nautilus.Utility.BasicText message = new Nautilus.Utility.BasicText(500, 0);
                message.ShowMessage(msg, 2 * Time.deltaTime);*/
                return;
            }
            float distance = Vector3.Distance(container.transform.position, PrawnMountPoint.position);
            if (distance > 10000)
            {
                return;
            }
            string distanceString = distance.ToString();
            if (distance > 5)
            {
                /*string msg = "Container is " + distanceString + " meters away.";
                Nautilus.Utility.BasicText message = new Nautilus.Utility.BasicText(500, 0);
                message.ShowMessage(msg, 2 * Time.deltaTime);*/
            }
            else if (!ValidateAttachment(container))
            {
                
            }
            

        }
        public override void Start()
        {
            base.Start();
            var handtarget = transform.Find("prawndocked").gameObject.EnsureComponent<Prawnhandtarget>();
            
            

        }
        public override void PlayerExit()
        {
            base.PlayerExit();
            playerinside = false;

        }
        public override void PlayerEntry()
        {
            base.PlayerEntry();
            playerinside = true;
            


        }
    }
}