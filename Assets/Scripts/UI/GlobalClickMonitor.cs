using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalClickMonitor : MonoBehaviour
{
    private const string towerPlacementTag = "TowerNode";
    private TowerNode currentNode;

    private void Update()
    {
        Vector2 position = new Vector2();
        var inputReceived = false;
        if (Input.GetMouseButton(0))
        {
            position = Input.mousePosition;
            inputReceived = true;
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                position = Input.GetTouch(0).position;
                inputReceived = true;
            }
        }

        if (inputReceived)
        {
            position = Camera.main.ScreenToWorldPoint(position);
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
            if (hit.collider != null)
            {
                var hitObject = hit.collider.gameObject;
                switch (hitObject.tag)
                {
                    case towerPlacementTag:
                        HandleTowerTagSelection(hitObject);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void HandleTowerTagSelection(GameObject hitObject)
    {
        var node = hitObject.GetComponent<TowerNode>();
        if (node != null)
        {
            if (currentNode != null)
                currentNode.OnCloseRequested();
        }

        currentNode = node;
        node.OnNodeSelected();
    }
}
