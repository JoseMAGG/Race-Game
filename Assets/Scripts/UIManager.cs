using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        private int maxLaps;
        public Text laps;
        public Text speed;
        //public Text gear;
        //public Image stress;
        public Text startText;
        public Image startPanel;
        public Text gameOverText;
        public GameObject gameOverPanel;

        public void SetLaps(int lapsValue)
        {
            laps.text = lapsValue + "/" + maxLaps;
        }
        public void SetSpeed(float speedValue)
        {
            float truncatedSpeed = (float)Math.Truncate(speedValue * 100) / 100;
            speed.text = truncatedSpeed + " Km/H";
        }
        //public void SetGear(int gearValue)
        //{
        //    gear.text = gearValue.ToString();
        //}
        //internal void SetStressIndicator(float stressValue)
        //{
        //    stress.fillAmount = stressValue;
        //}
        internal void SetMaxLaps(int laps)
        {
            maxLaps = laps;
        }

        internal IEnumerator CountDown()
        {
            startPanel.color = new Color(0, 0, 0, 0.3f);
            int count = 3;
            while (count > 0)
            {
                startText.text = count.ToString();
                yield return new WaitForSeconds(1);
                count--;
            }
        }

        internal void Win()
        {
            gameOverText.text = "HAS GANADO";
            gameOverPanel.SetActive(true);
        }

        internal void Loose()
        {
            gameOverText.text = "HAS PERDIDO";
            gameOverPanel.SetActive(true);
        }
    }
}