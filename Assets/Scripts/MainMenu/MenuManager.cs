using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text primaryText;
    [SerializeField] private Image primaryImage;
    [SerializeField] private TMP_Text secondaryText;
    [SerializeField] private Image secondaryImage;

    [Space]
    [SerializeField] private MenuItem[] menuItems;
    private int currentItem = 0;

    public Animator menuAnim;
    public Animator charAnim;

    void Start()
    {
        if (menuItems.Length > 0)
        {
            UpdatePrimaryPanel();
            UpdateSecondaryPanel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (menuItems.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Swipe(1);
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
                Swipe(-1);
            
        }
    }

    private void Swipe(int direction)
    {
        charAnim.SetTrigger("UsePhone");
        menuAnim.SetInteger("Dir", direction);
        menuAnim.SetTrigger("Swipe");

        currentItem += direction;
        if (currentItem >= menuItems.Length)
            currentItem = 0;
        else if (currentItem < 0)
            currentItem = menuItems.Length - 1;
    }

    public void UpdatePrimaryPanel()
    {
        primaryText.text = menuItems[currentItem].name;
        primaryImage.sprite = menuItems[currentItem].image;
    }

    public void UpdateSecondaryPanel()
    {
        secondaryText.text = menuItems[currentItem].name;
        secondaryImage.sprite = menuItems[currentItem].image;
    }
}
