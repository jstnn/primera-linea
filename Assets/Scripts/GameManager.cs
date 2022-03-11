using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //Declare which states we'd like use
    public enum States
    {
        Init,
        Countdown,
        Play,
        Win,
        Lose
    }

    public int startingPlayers = 1;
    public Transform startPoint;
    public GameObject prefab;
    public List<Player> players = new List<Player>();

    private StateMachine<States> fsm;
    private CameraController camera;
    private SemihOrhan.WaveOne.Demo.DisplayStats displayStats;
    private bool endedGame = false;

    private void Awake()
    {
        fsm = StateMachine<States>.Initialize(this, States.Init);
    }

    void Start() {
        camera = GameObject.Find("GameController").GetComponent<CameraController>();
    }

    void Update() {
        if (!displayStats)
            displayStats = GameObject.Find("Canvas").GetComponent<SemihOrhan.WaveOne.Demo.DisplayStats>();
    }

    public void StartGame() {
        Debug.Log("PRESSED START");
        
        fsm.ChangeState(States.Countdown);
    }

    public void RemovePlayer(GameObject deadPlayer)
    {
        Debug.Log(deadPlayer);
        players.Remove(deadPlayer.GetComponent<Player>());
    }

    private bool isEverybodyDead() {
     for ( int i = 0; i < players.Count; ++i ) {
        if ( players[ i ].isDead == false ) {
            return false;
        }
    }
        endedGame=true;
        return true;
    }

    private void Init_Enter()
    {
        Debug.Log("START GAME");
        Debug.Log("Waiting for start button to be pressed");
        if (displayStats)
            displayStats.SetCanvas("Init");
    }

    private void Init_Update()
    {
        if (!displayStats) {
            displayStats = GameObject.Find("Canvas").GetComponent<SemihOrhan.WaveOne.Demo.DisplayStats>();
            displayStats.SetCanvas("Init");
        }
    }

    //We can return a coroutine, this is useful animations and the like
    private IEnumerator Countdown_Enter()
    {
        displayStats.SetCountdown("Preparad@?");
        displayStats.SetCanvas("Countdown");
        displayStats.SetCountdown("Starting in 3...");
        yield return new WaitForSeconds(0.5f);
        displayStats.SetCountdown("Starting in 2...");
        yield return new WaitForSeconds(0.5f);
        displayStats.SetCountdown("Starting in 1...");
        yield return new WaitForSeconds(0.5f);

        fsm.ChangeState(States.Play);

    }

    private void Play_Enter()
    {
        displayStats.SetCanvas("Play");
        Debug.Log("FIGHT!");
        for (int i = 0; i < startingPlayers; i++)
        {
            GameObject rioter = Instantiate(prefab, startPoint.position, Quaternion.identity) as GameObject;
            players.Add(rioter.GetComponent<Player>());
        }
    }

    private void Play_Update()
    {
        if (endedGame) return;
        if (!endedGame && isEverybodyDead()) {
            fsm.ChangeState(States.Lose);
        }
        displayStats.SetHealth(players[0].health);
    }

    void Play_Exit()
    {
        Debug.Log("Game Over");
    }

    void Lose_Enter()
    {
        displayStats.SetCanvas("Lose");
        Debug.Log("Lost");
    }

    void Win_Enter()
    {
        displayStats.SetCanvas("Win");
        Debug.Log("Won");
    }

    // void OnGUI()
    // {
    //     //Example of polling state 
    //     var state = fsm.State;

    //     GUILayout.BeginArea(new Rect(50, 50, 120, 40));

    //     if (state == States.Init && GUILayout.Button("Start"))
    //     {
    //         fsm.ChangeState(States.Countdown);
    //     }
    //     if (state == States.Countdown)
    //     {
    //         GUILayout.Label("Look at Console");
    //     }
    //     if (state == States.Play)
    //     {
    //         if (GUILayout.Button("Force Win"))
    //         {
    //             fsm.ChangeState(States.Win);
    //         }

    //         GUILayout.Label("Health: " + Mathf.Round(players[0].health).ToString());
    //     }
    //     if (state == States.Win || state == States.Lose)
    //     {
    //         if (GUILayout.Button("Play Again"))
    //         {
    //             fsm.ChangeState(States.Countdown);
    //         }
    //     }

    //     GUILayout.EndArea();
    // }
}
