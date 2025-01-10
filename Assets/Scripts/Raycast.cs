using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    public TextMeshProUGUI hitText; // TextMeshPro referans�
    public Camera mainCamera; // Ana kamera referans�
    public RenderTexture renderTexture; // Kullan�lan Render Texture
    public float rayLength = 100f; // Ray'in maksimum uzunlu�u
    public Color rayColor = Color.red; // Ray'in rengi

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
                hitText.text = "�arp�lan obje: " + hit.collider.gameObject.name;
            }
        }
        else
        {
            if (hitText != null)
            {
                hitText.text = "";
            }
        }
    }
}
