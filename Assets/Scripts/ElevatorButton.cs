using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public ElevatorDoor elevatorDoor; // ElevatorDoor scriptine referans
    public float interactionDistance = 2f; // Etkile�im mesafesi
    private GameObject player; // Oyuncu referans�

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Oyuncu referans�n� bul
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // E tu�una bas�ld���nda kap�lar� kapat veya a�
                elevatorDoor.CloseDoors();
            }
        }
    }
}