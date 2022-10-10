using System.Reflection;
using BB.Base;
using BB.InventoryWrapper;
using BB.UI;
using FMODUnity;
using HarmonyLib;
using MoreMountains.InventoryEngine;
using UnityEngine;

namespace BnBSellItems;

[HarmonyPatch(typeof(InventoryTrashSlot), "ThrowItem")]
public class InventoryTrashSlotThrowItemPatcher {
	public static void Postfix(InventoryItem item) {
		if (item == null || !item.CanDeleteObject) return;
		var purse = ServiceLocator.Resolve<Purse>();
		purse.Delta(item.Quantity * item.price, item.currency, $"Gained {item.Quantity * item.price} {item.currency.ToString()} from selling {item.name}");
	}
}

[HarmonyPatch(typeof(InventoryTrashSlot), "RemoveItemFromSlot")]
public class InventoryTrashSlotRemoveItemFromSlotPatcher {
	public static void Postfix(InventoryTrashSlot __instance) {
		// var inventory = (Inventory)AccessTools.Property(typeof(Inventory), "_inventory").GetValue(__instance);
		// var trashInventory = (inventory == null) ? (InventoryManager.Instance.MainTrash) : inventory;
		// var storedItem = trashInventory.GetFirstItem();
		var methodInfo = AccessTools.PropertyGetter(typeof(InventoryTrashSlot), "StoredItem");
		var storedItem = (InventoryItem)methodInfo.Invoke(__instance, null);
		if (storedItem == null) {
			Plugin.LogSource.LogInfo("storedItem = null");
			return;
		}
		var purse = ServiceLocator.Resolve<Purse>();
		purse.Delta(-(storedItem.Quantity * storedItem.price), storedItem.currency,
			$"Lost {storedItem.Quantity * storedItem.price} {storedItem.currency.ToString()} from re-buying {storedItem.name}");
	}
}