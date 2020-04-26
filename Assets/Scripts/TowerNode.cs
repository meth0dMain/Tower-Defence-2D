using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoxCollider2D))]
public class TowerNode : MonoBehaviour
{
    [SerializeField] private TowerNodeUIController selectionUI;

    private GameObject placedTower;

    public GameObject PlacedTower => placedTower;

    public void Awake()
    {
        Assert.IsNotNull(selectionUI);
    }

    private void Start()
    {
        selectionUI.gameObject.SetActive(false);
        selectionUI.CloseButtonPressed += OnCloseRequested;
        selectionUI.PurchasedTower += OnTowerPurchased;
    }

    private void ToggleSelectionUI()
    {
        var uiGameobject = selectionUI.gameObject;
        uiGameobject.SetActive(!uiGameobject.activeSelf);
    }

    private void OnTowerPurchased(object sender, EventArgTemplate<TowerConfiguration> purchasedTower)
    {
        if (placedTower != null)
            Destroy(placedTower);
        var prefab = purchasedTower.Attachment.TowerPrefab;
        placedTower = Instantiate(prefab);
        placedTower.transform.parent = transform;
        placedTower.transform.localPosition = new Vector2(0, 0);
        OnCloseRequested();
    }

    public void OnCloseRequested()
    {
        selectionUI.gameObject.SetActive(false);
    }

    public void OnNodeSelected()
    {
        ToggleSelectionUI();
    }
}
