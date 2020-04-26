using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerNodeUIController : MonoBehaviour
{
    [SerializeField] private List<TowerPurchaseButtonCotroller> towerPurchaseButtons = new List<TowerPurchaseButtonCotroller>();
    
    public event EventHandler<EventArgTemplate<TowerConfiguration>> PurchasedTower;
    public event Action CloseButtonPressed;

    private void Start()
    {
        foreach (var buttor in towerPurchaseButtons)
        {
            buttor.TowerPurchased += OnTowerPurchased;
        }
    }

    private void OnTowerPurchased(object sender, EventArgTemplate<TowerConfiguration> purchasedTower)
    {
        SafeEventHandler.SafelyBroadcastEvent<TowerConfiguration>(ref PurchasedTower, purchasedTower.Attachment, this);
    }
    
    public void OnCloseSelected()
    {
        SafeEventHandler.SafelyBroadcastAction(ref CloseButtonPressed);
    }
}
