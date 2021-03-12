using UnityEngine;
using System.Collections.Generic;

public class RankingController : MonoBehaviour
{
    Ranking[] runners;

    void Start()
    {
        runners = FindObjectsOfType<Ranking>();
        SortRunners();
    }

    void Update()
    {
        SortRunners();
    }

    void SortRunners()
    {
        for (int i = 0; i < runners.Length - 1; i++)
        {
            // if the runner has finished the race, doesn't check.
            if (runners[i].isFinish)
                continue;

            for (int j = i + 1; j < runners.Length; j++)
            {
                if (runners[i].transform.position.z < runners[j].transform.position.z)
                {
                    Replace(ref runners[i], ref runners[j]);
                    runners[i].Rank = i + 1;
                    runners[j].Rank = j + 1;
                }
            }
        }
    }

    void Replace(ref Ranking greatValue, ref Ranking smallValue)
    {
        Ranking temporary = greatValue;
        greatValue = smallValue;
        smallValue = temporary;
    }
}
