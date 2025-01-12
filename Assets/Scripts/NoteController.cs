using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NoteController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode closeKey; // Not kapama tu�u
    [Space(10)]
    [SerializeField] private GameObject player; // Oyuncu objesi (CharacterController i�ermeli)

    [Header("UI Text")]
    [SerializeField] private GameObject noteCanvas; // Not UI paneli
    [SerializeField] private TMP_Text noteTextAreaUI; // Notun yaz�laca�� UI Text alan�

    [Space(10)]
    [SerializeField][TextArea] private string noteText; // G�sterilecek not metni

    [Space(10)]
    [SerializeField] private UnityEvent openEvent; // Not a��ld���nda �al��t�r�lacak event
    private bool isOpen = false; // Notun a��k olup olmad���n� takip eder

    public void ShowNote()
    {
        // Notu g�ster
        noteTextAreaUI.text = noteText; // Not metnini UI Text alan�na yaz
        noteCanvas.SetActive(true); // Not UI panelini aktif et
        openEvent.Invoke(); // Not a��ld���nda �al��t�r�lacak event'i tetikle
        DisablePlayer(true); // Oyuncu hareketini devre d��� b�rak
        isOpen = true; // Notun a��k oldu�unu belirt
    }

    public void HideNote()
    {
        DisableNote(); // Notu kapat
    }

    private void DisableNote()
    {
        // Notu kapat
        noteCanvas.SetActive(false); // Not UI panelini devre d��� b�rak
        DisablePlayer(false); // Oyuncu hareketini etkinle�tir
        isOpen = false; // Notun kapal� oldu�unu belirt
    }

    private void DisablePlayer(bool disable)
    {
        // Oyuncunun hareketini devre d��� b�rak/etkinle�tir
        var characterController = player.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = !disable; // CharacterController'� etkinle�tir/devre d��� b�rak
        }
        else
        {
            Debug.LogWarning("Player object does not have a CharacterController component!"); // Oyuncu objesinde CharacterController yoksa uyar� ver
        }
    }

    private void Update()
    {
        // Not a��ksa ve kapama tu�una bas�l�rsa notu kapat
        if (isOpen && Input.GetKeyDown(closeKey))
        {
            DisableNote(); // Notu kapat
        }
    }
}