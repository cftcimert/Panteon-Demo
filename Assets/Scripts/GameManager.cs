using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameMode gameMode;
    [HideInInspector] public bool isGame;
    PlayerMovement playerMovement;
    MenuController menuController;
    Ranking playerRank;
    bool isStartGame;

    void Start()
    {
        gameMode = GameMode.Running;
        menuController = GetComponent<MenuController>();
        playerRank = GameObject.FindGameObjectWithTag("Player").GetComponent<Ranking>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!isStartGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }
        else
        {
            menuController.SetRankText(playerRank.Rank);
        }
    }

    private void StartGame()
    {
        isGame = true;
        isStartGame = true;
        menuController.ActivedGamePanel();
        playerMovement.StartRunAnimation();
        StartOpponents();
    }

    public IEnumerator FinishGame()
    {
        isGame = false;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    public IEnumerator WinGame()
    {
        menuController.CreateStars(Camera.main.transform.position + Vector3.forward * 3);
        isGame = false;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    private void StartOpponents()
    {
        GameObject[] runners = GameObject.FindGameObjectsWithTag("Opponent");
        for (int i = 0; i < runners.Length; i++)
        {
            runners[i].GetComponent<NavMeshAgent>().enabled = true;
            runners[i].GetComponent<OpponentMovement>().enabled = true;
        }
    }
}

public enum GameMode
{
    Running,
    Shooting
}
