using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    public TextMeshProUGUI hitText; // TextMeshPro referans�
    public Camera mainCamera; // Ana kamera referans�
    public RenderTexture renderTexture; // Kullan�lan Render Texture
    public float rayLength = 100f; // Ray'in maksimum uzunlu�u
    public Color rayColor = Color.red; // Ray'in rengi

    private NoteController currentNoteController; // Aktif NoteController referans�
    private Inventory inventory; // Envanter referans�

    void Start()
    {
        if (hitText != null)
        {
            hitText.text = ""; // Ba�lang��ta hitText'in bo� oldu�undan emin olun
        }

        // Inventory bile�enini bul
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory bile�eni bulunamad�!"); // Inventory bile�eni bulunamazsa hata mesaj� ver
        }
    }

    void Update()
    {
        if (mainCamera == null || renderTexture == null)
        {
            Debug.LogError("Ana kamera veya Render Texture atanmad�!"); // Ana kamera veya Render Texture atanmad�ysa hata mesaj� ver
            return;
        }

        // Render Texture'un boyutlar�na g�re ekran ortas�n� hesapla
        Vector3 renderTextureCenter = new Vector3(renderTexture.width / 2, renderTexture.height / 2, 0);

        // Render Texture boyutlar�na g�re bir ray olu�tur
        Ray ray = mainCamera.ScreenPointToRay(renderTextureCenter);
        RaycastHit hit;

        // Debug.DrawRay ile ray'i �iz
        Debug.DrawRay(ray.origin, ray.direction * rayLength, rayColor);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            GameObject hitObject = hit.collider.gameObject; // Ray'in �arpt��� objeyi al

            // Nesne etiketi kontrol�
            if (hitText != null)
            {
                if (hitObject.CompareTag("Silinebilir"))
                {
                    hitText.text = $"Envantere ekle: {hitObject.name}"; // Silinebilir nesneye �arpt�ysa hitText'i g�ncelle
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        inventory.AddItem(hitObject); // E tu�una bas�ld���nda nesneyi envantere ekle
                    }
                }
                else if (hitObject.CompareTag("Tasinabilir"))
                {
                    hitText.text = $"Al: {hitObject.name}"; // Ta��nabilir nesneye �arpt�ysa hitText'i g�ncelle
                    // Ta��ma i�lemi `PickUp` scripti ile zaten kontrol ediliyor.
                }
                else if (hitObject.CompareTag("Okunabilir"))
                {
                    hitText.text = $"Oku: {hitObject.name}"; // Okunabilir nesneye �arpt�ysa hitText'i g�ncelle
                    NoteController noteController = hitObject.GetComponent<NoteController>();
                    if (noteController != null && Input.GetKeyDown(KeyCode.T))
                    {
                        if (currentNoteController == noteController)
                        {
                            currentNoteController.HideNote(); // Ayn� notu tekrar okudu�unda notu gizle
                            currentNoteController = null;
                        }
                        else
                        {
                            if (currentNoteController != null)
                            {
                                currentNoteController.HideNote(); // Ba�ka bir not okunuyorsa �nceki notu gizle
                            }
                            currentNoteController = noteController;
                            currentNoteController.ShowNote(); // Yeni notu g�ster
                        }
                    }
                }
                else
                {
                    hitText.text = hitObject.name; // Di�er nesneler i�in hitText'i g�ncelle
                }
            }
        }
        else
        {
            if (hitText != null)
            {
                hitText.text = ""; // Ray hi�bir �eye �arpmad�ysa hitText'i temizle
            }

            // E�er raycast ba�ka bir yere gidiyorsa notu kapat
            if (currentNoteController != null)
            {
                currentNoteController.HideNote(); // Aktif notu gizle
                currentNoteController = null;
            }
        }
    }
}