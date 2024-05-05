using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        private int maxLaps;
        public Text laps;
        public Text speed;
        public Text gear;
        public Image stress;

        public void SetLaps(int lapsValue)
        {
            laps.text = lapsValue + "/" + maxLaps;
        }
        public void SetSpeed(float speedValue)
        {
            float truncatedSpeed = (float)Math.Truncate(speedValue * 100) / 100;
            speed.text = truncatedSpeed + " Km/H";
        }
        public void SetGear(int gearValue)
        {
            gear.text = gearValue.ToString();
        }

        internal void SetMaxLaps(int laps)
        {
            maxLaps = laps;
        }

        internal void SetStressIndicator(float stressValue)
        {
            stress.fillAmount = stressValue;
        }
    }
}