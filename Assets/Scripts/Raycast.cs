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
            if (hitText != null)
            {
                hitText.text = hit.collider.gameObject.name;
            }

            // NoteController kontrol�
            NoteController noteController = hit.collider.GetComponent<NoteController>();
            if (noteController != null)
            {
                // E�er bir NoteController'a raycast ediliyorsa
                if (Input.GetKeyDown(KeyCode.T))
                {
                    // Notu a� veya kapat
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

            // E�er ray'in �arpt��� nesne "Silinebilir" etikete sahipse
            if (Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("Silinebilir"))
            {
                // �arp�lan nesneyi sahneden yok et
                Destroy(hit.collider.gameObject);
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
