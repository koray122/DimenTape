using UnityEngine;

[System.Serializable] // Bu s�n�f�n Unity taraf�ndan serile�tirilebilir oldu�unu belirtir
public class InventoryItem
{
    public GameObject item; // Envanterdeki ��eyi temsil eden GameObject
    public int quantity; // Bu ��eden envanterde ka� tane oldu�unu belirten miktar

    // Constructor: Yeni bir InventoryItem olu�turur
    public InventoryItem(GameObject item, int quantity)
    {
        this.item = item; // ��eyi ayarlar
        this.quantity = quantity; // Miktar� ayarlar
    }
}