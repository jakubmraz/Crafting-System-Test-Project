using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private CraftingSystem craftingScreen;

    public void ShowCraftingScreen()
    {
        craftingScreen.gameObject.SetActive(true);
    }
}
