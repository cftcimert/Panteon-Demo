using UnityEngine;

public class Ranking : MonoBehaviour
{
    private int rank;
    [HideInInspector] public bool isFinish;

    public int Rank
    {
        get { return rank; }
        set 
        {
            if (value < 1)
            {
                rank = 1;
            }
            else
            {
                rank = value;
            }
        }
    }
}
