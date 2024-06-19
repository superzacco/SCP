using UnityEngine;

public static class ZaccoUtil
{
    public static bool PercentChance(int percentageInt)
    {
        int i = Random.Range(1, 101);

        if (i <= percentageInt)
            return true;
        else    
            return false;    
    } 
}
