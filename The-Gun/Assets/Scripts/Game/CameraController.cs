using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController player;

    Vector3 offset;
    Vector3 centerPos;

    float distance;

    int floorMask;

    [Header("Camera settings")]
    [SerializeField] [Range(1f, 20f)] float range = 10f;
    [SerializeField] [Range(0.01f, 0.5f)] float sensetive = 0.25f;


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        offset = transform.position - player.transform.position;
        floorMask = LayerMask.GetMask("Floor");
    }

    void LateUpdate()
    {
        if (Input.GetButton("Fire3"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // todo make attach to floor mask

            if (Physics.Raycast(ray, out hit, 100, floorMask))
            {
                centerPos = new Vector3(hit.point.x - player.transform.position.x, 0, hit.point.z - player.transform.position.z);
                centerPos.Normalize();
                distance = Vector3.Distance(player.transform.position, hit.point) * sensetive;
                distance = Mathf.Clamp(distance, 0f, range);
            }
        }
        else
        {
            distance = 0f;
        }
        Vector3 newCamPos = player.transform.position + centerPos * distance + offset;
        transform.position = newCamPos;
    }
}
