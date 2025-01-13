using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>(); // Envanterdeki ��eleri tutan liste
    public InventoryUI inventoryUI; // Envanter UI referans�
    private bool inventoryVisible = false; // Envanterin g�r�n�r olup olmad���n� takip eder
    private List<GameObject> inventoryObjects = new List<GameObject>(); // Inventory tag'ine sahip t�m nesneleri saklayan liste

    void Start()
    {
        // Oyun ba��nda Inventory tag'ine sahip t�m nesneleri bul ve listeye ekle
        inventoryObjects.AddRange(GameObject.FindGameObjectsWithTag("Inventory"));

        // Ba�lang��ta t�m inventory nesnelerini kapat
        foreach (GameObject obj in inventoryObjects)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        // M tu�una bas�ld���nda envanter nesnelerinin g�r�n�rl���n� de�i�tir
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleInventoryVisibility();
        }
    }

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

        // Envantere eklenen objeyi g�r�nmez yap
        item.SetActive(false);

        Debug.Log("Envantere eklendi: " + item.name); // Konsola bilgi yazd�r
        inventoryUI.UpdateInventoryUI(); // Envanter UI'sini g�ncelle
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
                items.Remove(existingItem); // Miktar s�f�rsa ��eyi envanterden ��kar
                item.SetActive(true); // Sahnede g�r�n�r yap
                Debug.Log("Envanterden ��kar�ld�: " + item.name);
            }

            inventoryUI.UpdateInventoryUI(); // Envanter UI'sini g�ncelle
        }
    }

    public List<InventoryItem> GetItems()
    {
        return items; // Envanterdeki ��elerin listesini d�nd�r
    }

    private void ToggleInventoryVisibility()
    {
        // inventoryVisible de�i�kenini tersine �evir
        inventoryVisible = !inventoryVisible;

        // Listedeki t�m nesnelerin g�r�n�rl�k durumunu de�i�tir
        foreach (GameObject obj in inventoryObjects)
        {
            obj.SetActive(inventoryVisible);
        }
    }
}
