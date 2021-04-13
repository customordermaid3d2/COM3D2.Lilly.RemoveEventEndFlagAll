using BepInEx;
using COM3D2.Lilly.Plugin;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.RemoveFlag
{
    [BepInPlugin("COM3D2.Lilly.RemoveFlag", "COM3D2.Lilly.RemoveFlag", "1.0")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    [BepInProcess("COM3D2x64.exe")]
    public class Lilly : BaseUnityPlugin
    {
        public static CheatUtill? cheatUtill;

        public Lilly()
        {
            cheatUtill = new CheatUtill();

            actions += cheatUtill.OnGui;
        }

        public void Awake()
        {

        }

        public static event Action actions;
        public static Harmony harmonyMaid;

        public void OnEnable()
        {
            MyLog.LogMessage("OnEnable");

            SceneManager.sceneLoaded += this.OnSceneLoaded;


            //HarmonyUtill.SetHarmonyPatchAll();
            harmonyMaid=Harmony.CreateAndPatchAll(typeof(MaidManagementMainPatch));
        }

        public static Scene scene;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Lilly.scene = scene;
            MyLog.LogMessage("OnSceneLoaded"
                , scene.buildIndex
                , scene.rootCount
                , scene.name
                , scene.isLoaded
                , scene.isDirty
                , scene.IsValid()
                , scene.path
                , mode
                );
            GearMenu.SetButton();
        }

        public void OnGUI()
        {
            actions();
        }

        public void OnDisable()
        {
            MyLog.LogMessage("OnDisable");

            SceneManager.sceneLoaded -= this.OnSceneLoaded;

            harmonyMaid.UnpatchSelf();
        }

    }
}
