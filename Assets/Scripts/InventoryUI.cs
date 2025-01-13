using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro i�in gerekli namespace

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI itemNamesText; // Envanterdeki ��elerin isimlerini g�sterecek TextMeshPro bile�eni
    public TextMeshProUGUI itemQuantitiesText; // Envanterdeki ��elerin miktarlar�n� g�sterecek TextMeshPro bile�eni
    public Inventory inventory; // Envanter referans�

    void Start()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory referans� atanmad�!"); // Inventory referans�n�n atan�p atanmad���n� kontrol et
        }
        UpdateInventoryUI(); // Envanter UI'sini g�ncelle
    }

    public void UpdateInventoryUI()
    {
        Debug.Log("Updating Inventory UI"); // Debug mesaj� ekleyin

        // Envanterdeki ��elerin isimlerini ve miktarlar�n� toplamak i�in stringler
        string itemNames = "";
        string itemQuantities = "";

        // Envanterdeki ��eleri UI'de g�ster
        foreach (InventoryItem inventoryItem in inventory.GetItems())
        {
            itemNames += inventoryItem.item.name + "\n"; // Nesnenin ismini ekle
            itemQuantities += inventoryItem.quantity.ToString() + "\n"; // Nesnenin miktar�n� ekle
        }

        // TextMeshPro bile�enlerini g�ncelle
        itemNamesText.text = itemNames;
        itemQuantitiesText.text = itemQuantities;
    }
}