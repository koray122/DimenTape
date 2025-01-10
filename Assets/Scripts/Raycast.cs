using UnityEngine;
using TMPro; // TextMeshPro i�in gerekli namespace

public class Raycast : MonoBehaviour
{
    public TextMeshProUGUI hitText; // TextMeshProUGUI referans�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ba�lang��ta hitText'in bo� oldu�undan emin olun
        if (hitText != null)
        {
            hitText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ekran�n ortas�ndan bir ray olu�tur
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // E�er ray bir objeye �arparsa
        if (Physics.Raycast(ray, out hit))
        {
            // �arp�lan objenin ismini TextMeshPro'ya yazd�r
            if (hitText != null)
            {
                hitText.text = hit.collider.gameObject.name;
            }
        }
        else
        {
            // E�er hi�bir objeye �arpmazsa, TextMeshPro'yu temizle
            if (hitText != null)
            {
                hitText.text = "";
            }
        }
    }
}
