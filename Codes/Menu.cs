using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text currencyUI;
    [SerializeField] private Animator anim;
    private bool isMenuOpen = true;

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
    }
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }
    public void SetSelected()
    {


    }
}
