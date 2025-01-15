using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public Transform leftDoor; // Sol kap�n�n Transform bile�eni
    public Transform rightDoor; // Sa� kap�n�n Transform bile�eni
    public Vector3 leftClosedLocalPosition; // Sol kap�n�n kapal� yerel pozisyonu
    public Vector3 rightClosedLocalPosition; // Sa� kap�n�n kapal� yerel pozisyonu
    public Vector3 leftOpenLocalPosition; // Sol kap�n�n a��k yerel pozisyonu
    public Vector3 rightOpenLocalPosition; // Sa� kap�n�n a��k yerel pozisyonu
    public float doorSpeed = 2f; // Kap�lar�n hareket h�z�
    public float doorCloseDelay = 2f; // Kap�lar�n kapanma gecikmesi

    private bool isClosing = false; // Kap�lar�n kapan�p kapanmad���n� takip eder

    void Update()
    {
        if (isClosing)
        {
            // Kap�lar� kapal� yerel pozisyona do�ru hareket ettir
            leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftClosedLocalPosition, doorSpeed * Time.deltaTime);
            rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightClosedLocalPosition, doorSpeed * Time.deltaTime);

            // Kap�lar kapal� yerel pozisyona ula�t���nda isClosing bayra��n� false yap
            if (leftDoor.localPosition == leftClosedLocalPosition && rightDoor.localPosition == rightClosedLocalPosition)
            {
                isClosing = false;
                Invoke("OpenDoors", doorCloseDelay); // Belirli bir s�re sonra kap�lar� a�
            }
        }
    }

    public void CloseDoors()
    {
        isClosing = true; // Kap�lar� kapatmaya ba�la
    }

    private void OpenDoors()
    {
        // Kap�lar� a��k yerel pozisyona do�ru hareket ettir
        leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftOpenLocalPosition, doorSpeed * Time.deltaTime);
        rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightOpenLocalPosition, doorSpeed * Time.deltaTime);
    }
}