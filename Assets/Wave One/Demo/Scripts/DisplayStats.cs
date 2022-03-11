using UnityEngine;
using UnityEngine.UI;

namespace SemihOrhan.WaveOne.Demo
{
#pragma warning disable 0649
    public class DisplayStats : MonoBehaviour
    {
        public GameObject playCanvas;
        public GameObject loseCanvas;
        public GameObject winCanvas;
        public GameObject initCanvas;
        public GameObject countdownCanvas;

        [SerializeField] private Text totalEnemiesText;
        [SerializeField] private Text deployedEnemiesText;
        [SerializeField] private Text AliveEnemiesText;
        [SerializeField] private Text AlivePlayersText;
        [SerializeField] private Text CountdownText;
        [SerializeField] private Text HealthText;

        private int totalEnemiesAmount;
        private int deployedEnemiesAmount;
        private int aliveEnemiesAmount;
        private int healthAmount;
        private int alivePlayersAmount;

        void Start(){
            SetCanvas("Init");
        }

        public void SetTotalEnemies(int amount)
        {
            totalEnemiesAmount += amount;
            totalEnemiesText.text = "Total Enemies: " + totalEnemiesAmount;
        }

        public void SetDeployedEnemies(int amount)
        {
            deployedEnemiesAmount += amount;
            deployedEnemiesText.text = "Deployed Enemies: " + deployedEnemiesAmount;
        }

        public void SetAliveEnemies(int amount)
        {
            aliveEnemiesAmount += amount;
            AliveEnemiesText.text = "Alive Enemies: " + aliveEnemiesAmount;
        }

        public void SetHealth(int amount)
        {
            healthAmount = amount;
            HealthText.text = "Health: " + healthAmount;
        }

        public void SetCountdown(string text)
        {
            CountdownText.text = text;
        }

        public void SetAlivePlayers(int amount)
        {
            alivePlayersAmount = amount;
            AlivePlayersText.text = "Alive Players: " + alivePlayersAmount;
        }

        public void SetCanvas(string canvas) {
            switch (canvas)
            {
                case "Play":
                    playCanvas.gameObject.SetActive(true);
                    loseCanvas.gameObject.SetActive(false);
                    winCanvas.gameObject.SetActive(false);
                    initCanvas.gameObject.SetActive(false);
                    countdownCanvas.gameObject.SetActive(false);
                    break;
                case "Lose":
                    playCanvas.gameObject.SetActive(false);
                    loseCanvas.gameObject.SetActive(true);
                    winCanvas.gameObject.SetActive(false);
                    initCanvas.gameObject.SetActive(false);
                    countdownCanvas.gameObject.SetActive(false);
                    break;
                case "Win":
                    playCanvas.gameObject.SetActive(false);
                    loseCanvas.gameObject.SetActive(false);
                    winCanvas.gameObject.SetActive(true);
                    initCanvas.gameObject.SetActive(false);
                    countdownCanvas.gameObject.SetActive(false);
                    break;
                case "Init":
                    playCanvas.gameObject.SetActive(false);
                    loseCanvas.gameObject.SetActive(false);
                    winCanvas.gameObject.SetActive(false);
                    initCanvas.gameObject.SetActive(true);
                    countdownCanvas.gameObject.SetActive(false);
                    break;
                case "Countdown":
                    playCanvas.gameObject.SetActive(false);
                    loseCanvas.gameObject.SetActive(false);
                    winCanvas.gameObject.SetActive(false);
                    initCanvas.gameObject.SetActive(false);
                    countdownCanvas.gameObject.SetActive(true);
                    break;
                default:
                    playCanvas.gameObject.SetActive(false);
                    loseCanvas.gameObject.SetActive(false);
                    winCanvas.gameObject.SetActive(false);
                    initCanvas.gameObject.SetActive(true);
                    countdownCanvas.gameObject.SetActive(false);
                    break;
            }
        }
    }
}