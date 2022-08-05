using System.Reflection;
using BB;
using BB.UI;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Rewired;
using UnityEngine;

namespace BnBSellItems;

public static class Reference {
	public const string GUID = "at.derfl007.bnb.sellitems";
}

[BepInPlugin(Reference.GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin {
	public static readonly ManualLogSource LogSource = new("BnBSellItems");

	private void Awake() {
		// Plugin startup logic
		BepInEx.Logging.Logger.Sources.Add(LogSource);
		LogSource.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} is loaded!");
		var harmony = new Harmony(Reference.GUID);
		harmony.PatchAll();
	}
}