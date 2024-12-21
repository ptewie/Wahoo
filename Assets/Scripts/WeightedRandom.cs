using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class WeightedRandom
{
    public static GameObject GetRandomItem(WeightedItem[] possibleItems)
    {
        float[] CDA = new float[possibleItems.Length];
        float cumulativeDensity = 0;
        for (int i = 0; i < possibleItems.Length; i++)
        {
            cumulativeDensity += possibleItems[i].weight;

            CDA[i] = cumulativeDensity;
        }

        float randomValue = Random.Range(0, cumulativeDensity); 

        int selectedIndex = System.Array.BinarySearch(CDA, randomValue);
        if (selectedIndex < 0)
        {
            
            selectedIndex = ~selectedIndex;
        }
        return possibleItems[selectedIndex].objectToDrop; 
    }

    [System.Serializable]
    public class WeightedItem
    {
        public GameObject objectToDrop;
        public float weight;
    }
}