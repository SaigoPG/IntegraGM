using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private MenuOrganizer menuOrganizer;
    [SerializeField] private Dictionary<string, BaseMenu> menus = new Dictionary<string, BaseMenu>();
    private void Awake()
    {
        menuOrganizer = new MenuOrganizer();
    }

    private void Start()
    {
        menuOrganizer.OrganizeMenus(this, menus);
    }

    public void Transition(string menuName)
    {
        if (!menus.ContainsKey(menuName))
        {
            Debug.LogWarning("El menu " + menuName + " no se encuentra en la lista");
            return;
        }
        foreach (BaseMenu menu in menus.Values)
        {
            if (menu.activated)
            {
                menu.FadeOut();
                break;
            }
        }
        menus[menuName].FadeIn();
    }

    public void CloseAll()
    {
        foreach (BaseMenu menu in menus.Values)
        {
            if (menu.activated)
            {
                menu.FadeOut();
            }
        }
    }
}
