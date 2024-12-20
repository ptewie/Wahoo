using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILivesDisplay : MonoBehaviour
{
    [Tooltip("The order of these lives icons matters. The first icon in the list should be when there is 1 life remaining. The last is when there are full lives.")]
    public Image[] lifeIcons;

    void Update()
    {
        // Start by turning off all the icons
        TurnOffAllIcons();
        // Then go through the icons and turn them on if we have more lives than the current icon index
        TurnOnIcons();
    }

    private void TurnOnIcons()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (i < GameManager.instance.player.lives)
            {
                lifeIcons[i].enabled = true;
            }
        }
    }

    private void TurnOffAllIcons()
    {
        for (int i = 0; i<lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = false;
        }
    }
}