using UnityEngine;
using System.Collections.Generic;
using HandyButtons;

public class LevelObjectEditor : MonoBehaviour
{
    private List<GameObject> childObjects = new List<GameObject>();

    [Button]
    void Disabler()
    {
        // Get all child objects of the current GameObject
        foreach (Transform child in transform)
        {
            childObjects.Add(child.gameObject);
        }
        
        // Iterate through all child objects
        foreach (GameObject child in childObjects)
        {
            // Disable the SpriteRenderer component if it exists
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }

            // Disable all children of the child GameObject
            foreach (Transform grandChild in child.transform)
            {
                if (grandChild.name == "Lock")
                {
                    grandChild.GetComponent<SpriteRenderer>().enabled = false;
                }
                grandChild.gameObject.SetActive(false);
            }
        }
    }
}

