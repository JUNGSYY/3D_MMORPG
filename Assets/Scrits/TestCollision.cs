using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown((0)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

            LayerMask mask = (LayerMask.GetMask("Monster", "Wall"));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                Debug.Log($"Raycast Camera @{hit.collider.gameObject.name}");
            }
        }
    }
}
