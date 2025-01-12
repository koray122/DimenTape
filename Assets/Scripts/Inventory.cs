using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>(); // Envanterdeki ��eleri tutan liste

    public void AddItem(GameObject item)
    {
        // Envanterde ayn� isimde bir ��e olup olmad���n� kontrol et
        InventoryItem existingItem = items.Find(i => i.item.name == item.name);
        if (existingItem != null)
        {
            existingItem.quantity++; // Ayn� isimde ��e varsa miktar�n� art�r
        }
        else
        {
            items.Add(new InventoryItem(item, 1)); // Ayn� isimde ��e yoksa yeni bir ��e ekle
        }
        item.SetActive(false); // Envantere eklenen objeyi sahnede g�r�nmez yap
        Debug.Log("Envantere eklendi: " + item.name); // Konsola envantere eklendi�ini yazd�r
    }

    public void RemoveItem(GameObject item)
    {
        // Envanterde ayn� isimde bir ��e olup olmad���n� kontrol et
        InventoryItem existingItem = items.Find(i => i.item.name == item.name);
        if (existingItem != null)
        {
            existingItem.quantity--; // Ayn� isimde ��e varsa miktar�n� azalt
            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem); // Miktar s�f�r veya daha az ise ��eyi envanterden ��kar
                item.SetActive(true); // Envanterden ��kar�lan objeyi sahnede g�r�n�r yap
                Debug.Log("Envanterden ��kar�ld�: " + item.name); // Konsola envanterden ��kar�ld���n� yazd�r
            }
        }
    }

    public List<InventoryItem> GetItems()
    {
        return items; // Envanterdeki ��elerin listesini d�nd�r
    }
}