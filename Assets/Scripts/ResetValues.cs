using UnityEngine;

public static class ResetValues
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResetStars()
    {
        ActivateStar.activatedAllStars = false;
        ActivateStar.activatedStarCount = 0;
        ActivateStar.enemyCanSpawn = false;
    }
}
