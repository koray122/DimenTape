using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHold : MonoBehaviour
{
    public Transform PlayerTransform;
    public float range = 3f;
    public float Go = 100f;
    public Camera Camera;
    public string pickUpTag = "PickUp"; // Ta��nabilir nesneler i�in kullan�lacak tag
    private bool isHolding = false; // Nesnenin al�n�p al�nmad���n� takip eden de�i�ken
    private GameObject heldObject; // Ta��nan nesneyi takip eden de�i�ken

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (isHolding)
            {
                Drop();
            }
            else
            {
                StartPickUp();
            }
        }
    }

    void StartPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // Tag kontrol�
            if (hit.transform.CompareTag(pickUpTag))
            {
                PickUp(hit.transform.gameObject);
            }
        }
    }

    void PickUp(GameObject obj)
    {
        heldObject = obj;
        heldObject.transform.SetParent(PlayerTransform);
        MeshCollider meshCollider = heldObject.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.enabled = false; // MeshCollider'� kapat
        }
        isHolding = true; // Nesne al�nd�
    }

    void Drop()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null);
            MeshCollider meshCollider = heldObject.GetComponent<MeshCollider>();
            if (meshCollider != null)
            {
                meshCollider.enabled = true; // MeshCollider'� a�
            }
            heldObject = null;
        }
        isHolding = false; // Nesne b�rak�ld�
    }
}