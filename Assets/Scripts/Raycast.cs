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

    void Start()
    {
        if (hitText != null)
        {
            hitText.text = "";
        }
    }

    void Update()
    {
        if (mainCamera == null || renderTexture == null)
        {
            Debug.LogError("Ana kamera veya Render Texture atanmad�!");
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
            GameObject hitObject = hit.collider.gameObject;

            // Nesne etiketi kontrol�
            if (hitText != null)
            {
                if (hitObject.CompareTag("Silinebilir"))
                {
                    hitText.text = $"Envantere ekle: {hitObject.name}";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Destroy(hitObject);
                    }
                }
                else if (hitObject.CompareTag("Tasinabilir"))
                {
                    hitText.text = $"Al: {hitObject.name}";
                    // Ta��ma i�lemi `PickUp` scripti ile zaten kontrol ediliyor.
                }
                else if (hitObject.CompareTag("Okunabilir"))
                {
                    hitText.text = $"Oku: {hitObject.name}";
                    NoteController noteController = hitObject.GetComponent<NoteController>();
                    if (noteController != null && Input.GetKeyDown(KeyCode.T))
                    {
                        if (currentNoteController == noteController)
                        {
                            currentNoteController.HideNote();
                            currentNoteController = null;
                        }
                        else
                        {
                            if (currentNoteController != null)
                            {
                                currentNoteController.HideNote();
                            }
                            currentNoteController = noteController;
                            currentNoteController.ShowNote();
                        }
                    }
                }
                else
                {
                    hitText.text = hitObject.name;
                }
            }
        }
        else
        {
            if (hitText != null)
            {
                hitText.text = "";
            }

            // E�er raycast ba�ka bir yere gidiyorsa notu kapat
            if (currentNoteController != null)
            {
                currentNoteController.HideNote();
                currentNoteController = null;
            }
        }
    }
}
