using BepInEx;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using VehicleFramework;
using VehicleFramework.Engines;
using VehicleFramework.Patches;
using VehicleFramework.StorageComponents;
using VehicleFramework.UpgradeModules;
using VehicleFramework.VehicleParts;
using VehicleFramework.VehicleTypes;





namespace PhantomSub
{
    public partial class PhantomSub : Submarine
    {
        public static GameObject model = null;
        public static Atlas.Sprite pingSprite = null;
        public static Atlas.Sprite crafterSprite = null;



        public static void GetAssets()
        {
            string modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(modPath, "assets/phantom"));
            if (myLoadedAssetBundle == null)
            {
                Logger.Log("Failed to load AssetBundle!");
                return;
            }

            System.Object[] arr = myLoadedAssetBundle.LoadAllAssets();
            foreach (System.Object obj in arr)
            {

                if (obj.ToString().Contains("SpriteAtlas"))
                {
                    SpriteAtlas thisAtlas = (SpriteAtlas)obj;

                    Sprite ping = thisAtlas.GetSprite("PingSprite");
                    pingSprite = new Atlas.Sprite(ping);

                    Sprite ping3 = thisAtlas.GetSprite("CrafterSprite");
                    crafterSprite = new Atlas.Sprite(ping3);
                }
                else if (obj.ToString().Contains("vehicle"))
                {
                    model = (GameObject)obj;
                }
                // load the asset bundle
                
               
                else
                {
                    //Logger.Log(obj.ToString());
                }
            }
        }

        public static Dictionary<TechType, int> GetRecipe()
        {
            Dictionary<TechType, int> recipe = new Dictionary<TechType, int>();
            recipe.Add(TechType.PlasteelIngot, 2);
            recipe.Add(TechType.Lubricant, 1);
            recipe.Add(TechType.EnameledGlass, 2);
            recipe.Add(TechType.AdvancedWiringKit, 1);
            recipe.Add(TechType.Diamond, 4);
            recipe.Add(TechType.Lead, 2);
            recipe.Add(TechType.Kyanite, 3);


            return recipe;
        }
        public static IEnumerator Register()
        {
            GetAssets();
            ModVehicle PhantomSub = model.EnsureComponent<PhantomSub>() as ModVehicle;
            PhantomSub.name = "Phantom";

            yield return UWE.CoroutineHost.StartCoroutine(VehicleRegistrar.RegisterVehicle(PhantomSub));

        }


        public override string vehicleDefaultName
        {
            get
            {
                Language main = Language.main;
                if (!(main != null))
                {
                    return "Phantom Submarine";
                }
                return main.Get("phantomdefaultname");
            }
        }




        public override GameObject VehicleModel
        {
            get
            {
                return model;
            }
        }


        public override GameObject StorageRootObject
        {
            get
            {
                return transform.Find("StorageRoot").gameObject;
            }
        }

        public override GameObject ModulesRootObject
        {
            get
            {
                return transform.Find("ModulesRoot").gameObject;
            }
        }

        /*public GameObject subRoot 
        {
            get
            {
                return transform.Find("SubRoot").gameObject;
            
            }
        
        
        }*/

        public override List<VehiclePilotSeat> PilotSeats
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehiclePilotSeat>();
                VehicleFramework.VehicleParts.VehiclePilotSeat vps = new VehicleFramework.VehicleParts.VehiclePilotSeat();
                Transform mainSeat = transform.Find("PilotSeat");
                vps.Seat = mainSeat.gameObject;
                vps.SitLocation = mainSeat.Find("SitLocation").gameObject;
                vps.LeftHandLocation = mainSeat;
                vps.RightHandLocation = mainSeat;
                vps.ExitLocation = mainSeat.Find("ExitLocation");
                // TODO exit location
                list.Add(vps);
                return list;
            }
        }

        public override List<VehicleFloodLight> FloodLights
        {
            get
            {
                return null;

            }

        }
        public override List<GameObject> NavigationPortLights
        {
            get
            {
                return null;

            }

        }
        public override List<GameObject> NavigationStarboardLights
        {
            get
            {
                return null;

            }

        }
        public override List<GameObject> NavigationPositionLights
        {
            get
            {
                return null;

            }

        }
        public override List<GameObject> NavigationWhiteStrobeLights
        {
            get
            {
                return null;

            }

        }
        public override List<GameObject> NavigationRedStrobeLights
        {
            get
            {
                return null;

            }

        }
        public override List<GameObject> TetherSources
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("TetherSources"))
                {
                    list.Add(child.gameObject);
                }
                return list;

            }
        }
        public override GameObject ControlPanel
        {
            get
            {
                return null;

            }

        }

        public override List<VehicleStorage> InnateStorages
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleStorage>();

                Transform innate1 = transform.Find("Storage/Locker1");
                Transform innate2 = transform.Find("Storage/Locker2");
                Transform innate3 = transform.Find("Storage/Locker3");
                Transform innate4 = transform.Find("Storage/Locker4");
                Transform innate5 = transform.Find("Storage/Locker5");
                Transform innate6 = transform.Find("Storage/Locker6");
                Transform innate7 = transform.Find("Storage/Locker7");
                Transform innate8 = transform.Find("Storage/Locker8");


                VehicleFramework.VehicleParts.VehicleStorage IS1 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS1.Container = innate1.gameObject;
                IS1.Height = 6;
                IS1.Width = 4;
                list.Add(IS1);
                VehicleFramework.VehicleParts.VehicleStorage IS2 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS2.Container = innate2.gameObject;
                IS2.Height = 6;
                IS2.Width = 4;
                list.Add(IS2);
                VehicleFramework.VehicleParts.VehicleStorage IS3 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS3.Container = innate3.gameObject;
                IS3.Height = 6;
                IS3.Width = 4;
                list.Add(IS3);
                VehicleFramework.VehicleParts.VehicleStorage IS4 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS4.Container = innate4.gameObject;
                IS4.Height = 6;
                IS4.Width = 4;
                list.Add(IS4);
                VehicleFramework.VehicleParts.VehicleStorage IS5 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS5.Container = innate5.gameObject;
                IS5.Height = 6;
                IS5.Width = 4;
                list.Add(IS5); VehicleFramework.VehicleParts.VehicleStorage IS6 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS6.Container = innate6.gameObject;
                IS6.Height = 6;
                IS6.Width = 4;
                list.Add(IS6);
                VehicleFramework.VehicleParts.VehicleStorage IS7 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS7.Container = innate7.gameObject;
                IS7.Height = 6;
                IS7.Width = 4;
                list.Add(IS7);
                VehicleFramework.VehicleParts.VehicleStorage IS8 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS8.Container = innate8.gameObject;
                IS8.Height = 6;
                IS8.Width = 4;
                list.Add(IS8);

                return list;
            }
        }

        public override List<VehicleHatchStruct> Hatches
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleHatchStruct>();

                VehicleFramework.VehicleParts.VehicleHatchStruct interior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform intHatch = transform.Find("Hatches/TopHatch/InsideHatch");
                interior_vhs.Hatch = intHatch.gameObject;
                interior_vhs.EntryLocation = intHatch.Find("Entry");
                interior_vhs.ExitLocation = intHatch.Find("Exit");
                interior_vhs.SurfaceExitLocation = intHatch.Find("SurfaceExit");

                VehicleFramework.VehicleParts.VehicleHatchStruct exterior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform extHatch = transform.Find("Hatches/TopHatch/OutsideHatch");
                exterior_vhs.Hatch = extHatch.gameObject;
                exterior_vhs.EntryLocation = interior_vhs.EntryLocation;
                exterior_vhs.ExitLocation = interior_vhs.ExitLocation;
                exterior_vhs.SurfaceExitLocation = interior_vhs.SurfaceExitLocation;

                list.Add(interior_vhs);
                list.Add(exterior_vhs);




                return list;
            }
        }




        public override List<VehicleStorage> ModularStorages
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleStorage>();
                return list;
            }
        }



        public override List<VehicleBattery> Batteries
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleBattery>();

                VehicleFramework.VehicleParts.VehicleBattery vb1 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb1.BatterySlot = transform.Find("Batteries/Battery1").gameObject;
                vb1.BatteryProxy = null;
                list.Add(vb1);

                VehicleFramework.VehicleParts.VehicleBattery vb2 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb2.BatterySlot = transform.Find("Batteries/Battery2").gameObject;
                vb2.BatteryProxy = null;
                list.Add(vb2);

                VehicleFramework.VehicleParts.VehicleBattery vb3 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb3.BatterySlot = transform.Find("Batteries/Battery3").gameObject;
                vb3.BatteryProxy = null;
                list.Add(vb3);

                VehicleFramework.VehicleParts.VehicleBattery vb4 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb4.BatterySlot = transform.Find("Batteries/Battery4").gameObject;
                vb4.BatteryProxy = null;
                list.Add(vb4);

                VehicleFramework.VehicleParts.VehicleBattery vb5 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb5.BatterySlot = transform.Find("Batteries/Battery5").gameObject;
                vb5.BatteryProxy = null;
                list.Add(vb5);

                VehicleFramework.VehicleParts.VehicleBattery vb6 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb6.BatterySlot = transform.Find("Batteries/Battery6").gameObject;
                vb6.BatteryProxy = null;
                list.Add(vb6);

                return list;
            }
        }

        public override List<VehicleBattery> BackupBatteries
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleBattery>();
                return null;
            }
        }

        public override List<VehicleFloodLight> HeadLights
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleFloodLight>();

                list.Add(new VehicleFramework.VehicleParts.VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/headlights/headlight1").gameObject,
                    Angle = 70,
                    Color = Color.white,
                    Intensity = 1.3f,
                    Range = 200f
                });
                list.Add(new VehicleFramework.VehicleParts.VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/headlights/headlight2").gameObject,
                    Angle = 70,
                    Color = Color.white,
                    Intensity = 1.3f,
                    Range = 200f
                });


                return list;
            }
        }


        public override GameObject SteeringWheelLeftHandTarget
        {
            get
            {
                return transform.Find("SteeringWheelLeftHandTarget").gameObject;

            }
        }
        public override GameObject SteeringWheelRightHandTarget
        {
            get
            {
                return transform.Find("SteeringWheelRightHandTarget").gameObject;

            }
        }


        /*public override void PlayerEntry()
        {
            base.PlayerEntry();



        /*/

        public override void BeginPiloting()
        {
            base.BeginPiloting();

            Player.main.armsController.ikToggleTime = 1;
            Player.main.armsController.SetWorldIKTarget(SteeringWheelLeftHandTarget.transform, SteeringWheelRightHandTarget.transform);

            NotifyStatus(PlayerStatus.OnPilotBegin);
        }
        public override void StopPiloting()
        {
            base.StopPiloting();
        }


        public override List<GameObject> WaterClipProxies
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("WaterClipProxies"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }

        public override List<GameObject> CanopyWindows
        {
            get
            {
                var list = new List<GameObject>();
                list.Add(transform.Find("Model/BackCanopy").gameObject);
                list.Add(transform.Find("Model/InnerCanopy").gameObject);
                list.Add(transform.Find("Model/OuterCanopy").gameObject);
                return list;
                
                
            }
        }




        public override GameObject CollisionModel
        {
            get
            {
                return transform.Find("Collider").gameObject;

            }
        }



        public override List<VehicleUpgrades> Upgrades
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleUpgrades>();
                VehicleFramework.VehicleParts.VehicleUpgrades vu = new VehicleFramework.VehicleParts.VehicleUpgrades();
                vu.Interface = transform.Find("UpgradesInterface").gameObject;
                vu.Flap = vu.Interface;
                vu.AnglesClosed = Vector3.zero;
                vu.AnglesOpened = new Vector3(0, 0, 0);

                vu.ModuleProxies = null;

                list.Add(vu);
                return list;
            }
        }

        public override ModVehicleEngine Engine

        {
            get
            {
                return gameObject.EnsureComponent<PhantomSubEngine>();

            }

        }

        public override Dictionary<TechType, int> Recipe
        {

            get
            {
                Dictionary<TechType, int> recipe = new Dictionary<TechType, int>();
                recipe.Add(TechType.PlasteelIngot, 2);
                recipe.Add(TechType.Lubricant, 1);
                recipe.Add(TechType.EnameledGlass, 2);
                recipe.Add(TechType.AdvancedWiringKit, 1);
                recipe.Add(TechType.Diamond, 4);
                recipe.Add(TechType.Lead, 2);
                recipe.Add(TechType.Kyanite, 3);

                return recipe;

            }
        }

        public override Atlas.Sprite PingSprite
        {
            get
            {
                return pingSprite;

            }

        }
        public override Atlas.Sprite CraftingSprite

        {
            get 
            {

                return crafterSprite;
            
            }
        }

        public override string Description
        {
            get
            {
                return "The Phantom is a medium size submarine designed to navigate dangerous territory.";

            }

        }

        public override string EncyclopediaEntry
        {
            get
            {
                string ency = "The Phantom is a medium size submarine designed to navigate dangerous territory.";
                ency += "Its heavily armoured hull allows it to withstand the heaviest of leviathan attacks.\n";
                ency += "- The Alterra Phantom is powered by Blizzard Corp software -\n";
                ency += "\nIt features:\n";
                ency += "- Forward observation deck with manual control seat\n";
                ency += "- External dock for transportation of smaller vehicles (Prawn Suit compatible)\n";
                ency += "- Preinstalled fabricator module\n";
                ency += "- Extensive storage capacity\n";
                ency += "- Kyanite reinforced plasteel hull to protect against creature attacks\n";
                ency += "- Onboard AI for threat detection and ease of operation\n";
                ency += "- Automatic attitude adjustment capabilities\n";
                ency += "- Some customization options\n";
                ency += "\nRatings:\n";
                ency += "- Top Speed: ??m/s \n";
                ency += "- Acceleration: ??m/s/s \n";
                ency += "- Power: 6 replaceable power cells\n";
                ency += "- Dimensions: 12m x 5m x 15m\n";
                ency += "- Persons: 1-2\n";
                ency += "\nNB The Phantom does NOT feature:\n";
                ency += "- Habitation quarters: An Alterra SleepSack (not included) is recommended for comfort on long missions";
                ency += "where you need to sleep on the floor\n";
                ency += "- Silent running: Unlike the popular Alterra Cyclops, the Phantom does not feature silent running modes\n";
                ency += "- Invisibility: Although the name suggests it, this submarine is not invisible\n";
                ency += "- Upgrade fabrication methods: A separate moonpool with a vehicle upgrade console is required to fabricate";
                ency += "upgrades for this submarine\n";
                ency += "\n'The Alterra Phantom: For when you need a strong sub.'";
                
                
                return ency;
                

            }

        }

        public override int BaseCrushDepth
        {
            get
            {
                return 900;

            }

        }

        public override int MaxHealth
        {
            get
            {
                return 5000;

            }

        }

        public override int Mass
        {
            get
            {
                return 4000;

            }

        }

        public override int NumModules
        {
            get
            {
                return 8;

            }

        }

        public override bool HasArms
        {
            get
            {
                return false;

            }

        }

        public override GameObject BoundingBox
        {
            get
            {
                return transform.Find("BoundingBox").gameObject;

            }

        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

        }

        public override GameObject Fabricator
        {
            get
            {
                return transform.Find("Fabricator-Location").gameObject;
            }
        }

        
        public override void SubConstructionBeginning()
        {
            base.SubConstructionBeginning();



        }

        public override void SubConstructionComplete()
        {
            base.SubConstructionComplete();
        }


       


    }





}









;