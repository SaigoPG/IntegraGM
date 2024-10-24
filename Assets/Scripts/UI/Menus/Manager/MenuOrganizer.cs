using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOrganizer
{
    public void OrganizeMenus(MenuManager menuContainer, Dictionary<string, BaseMenu> dictionary)
    {
        foreach (Transform child in menuContainer.transform)
        {
            BaseMenu menu = child.GetComponent<BaseMenu>();
            if (menu != null) dictionary[menu.name] = menu;
        }
    }
}
