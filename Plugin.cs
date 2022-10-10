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
	public const string PLUGIN_NAME = "BnBSellItems";
	public const string PLUGIN_VERSION = "1.0.1";
}

[BepInPlugin(Reference.GUID, Reference.PLUGIN_NAME, Reference.PLUGIN_VERSION)]
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