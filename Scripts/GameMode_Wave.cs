using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMode_Wave : MonoBehaviour
{
    public TextMeshProUGUI m_CurrentRound;
    public TextMeshProUGUI m_RoundTimer;

    public float RoundLength;
    float m_Timer;
    public float m_round;
    public float m_intermissionTime;



    public bool isSetupDone = false;


    public List<GameObject> enemySpawners = new List<GameObject>();
    static public List<GameObject> activeEnemies = new List<GameObject>();

    /// <summary>
    /// Sequence flow.
    /// 1: Start Game
    /// 2: Coroutine (Round Start Sequence) Starts
    /// 3: Once Coroutine finishes, bool(isSetupDone) is changed to true && RoundStart() is called.
    /// 4: Round timer is Started, Round is incrememented by 1, and RoundSystem shows the current round.
    /// 5: Once the timer hits 0, EndRound() is called.
    ///     - Sets all spawners inactive.
    ///     - Changes isSetupDone to false.
    ///     - Waits for a certain amount of time(intermission float) as to give the player time between rounds.
    /// 6: Step 2 Repeats.
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isSetupDone)
        {
            RoundTimer();
            foreach (GameObject objects in enemySpawners)
            {
                objects.SetActive(true);
            }

        }
    }


    public void StartGame()
    {
        StartCoroutine(RoundStartSequence());
        Debug.Log("Game Started");


    }

    public void StartRound()
    {
        TimerReset();
        IncrementRound();
        RoundSystem();
    }
    public void EndRound()
    {
        foreach (GameObject objects in enemySpawners)
        {
            objects.SetActive(false);
        }
        isSetupDone = false;
        CleanUp();
        Debug.Log("Round Over + Intermission Starting");
        m_CurrentRound.SetText("Intermission");
        new WaitForSeconds(m_intermissionTime);
        m_CurrentRound.SetText("");
        m_RoundTimer.SetText("");
        StartCoroutine(RoundStartSequence());

    }

    public void RestartGame()
    {
        m_round = 0;
        CleanUp();
        StartGame();
    }

    public void RoundSystem()
    {
        m_CurrentRound.SetText("Round " + m_round);
        Debug.Log("Round Text Updated");
    }

    public void IncrementRound()
    {
        m_round++;
        Debug.Log("Round Incremented");
    }

    public void CleanUp()
    {

        foreach (GameObject enemy in activeEnemies)
        {
            Destroy(enemy);
        }
        Debug.Log("Clean Up Finished");
    }

    public void RoundTimer()
    {

        if (m_Timer <= 0)
        {
            m_Timer = 0;
            m_RoundTimer.SetText(":");
            EndRound();
        }
        else
        {
            // Countdown 1 second.
            m_Timer = m_Timer - 1 * Time.deltaTime;
            // Convert the seconds into minutes.
            string minutes = Mathf.Floor(m_Timer / 60).ToString("00");
            //Show seconds correctly.
            string seconds = (m_Timer % 60).ToString("00");
            // Display the timer.
            m_RoundTimer.SetText(minutes + ":" + seconds);

        }
    }

    public void TimerReset()
    {
        m_Timer = RoundLength;
        Debug.Log("Timer Reset");
    }

    public void PlayerDied()
    {

    }

    public IEnumerator RoundStartSequence()
    {
        m_CurrentRound.SetText("Get Ready!");
        yield return new WaitForSeconds(5f);
        m_CurrentRound.SetText("3");
        yield return new WaitForSeconds(1f);
        m_CurrentRound.SetText("2");
        yield return new WaitForSeconds(1f);
        m_CurrentRound.SetText("1");
        yield return new WaitForSeconds(1);
        m_CurrentRound.SetText("");
        isSetupDone = true;
        Debug.Log("Setup Complete.");
        StartRound();
    }
}
