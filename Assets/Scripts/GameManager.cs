using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public CarMovement playerCar;
        public List<CarAIMovement> aiCarList;
        public GameObject gameOverPanel;
        public GameObject startPanel;
        public UIManager ui;
        public int winLaps;
        private int checkpointsCount;

        void Start()
        {
            ui.SetMaxLaps(winLaps);
            SetMovementEnabled(false);
            checkpointsCount = playerCar.collisionManager.checkpointList.Count;
        }

        private void SetMovementEnabled(bool enabled)
        {
            playerCar.enabled = enabled;
            foreach (CarAIMovement aiCar in aiCarList)
            {
                aiCar.SetMovementEnabled(enabled);
            }
        }

        public void OnPlay()
        {
            StartCoroutine(Play());
        }

        public void OnReplay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator Play()
        {
            yield return StartCoroutine(ui.CountDown());
            SetMovementEnabled(true);
            startPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            CheckGameOver();
            ui.SetLaps(playerCar.GetDoneCheckpointCount() / checkpointsCount);
            ui.SetSpeed(playerCar.GetSpeed());
            //ui.SetGear(playerCar.GetGear());
            //ui.SetStressIndicator(playerCar.GetStress() * 0.5f);
        }

        private void CheckGameOver()
        {
            if (playerCar.GetDoneCheckpointCount() == winLaps * checkpointsCount)
            {
                SetMovementEnabled(false);
                ui.Win();
            }
            else
            {
                foreach (CarAIMovement aiCar in aiCarList)
                {
                    if (aiCar.GetDoneCheckpointCount() == winLaps * checkpointsCount)
                    {
                        SetMovementEnabled(false);
                        ui.Loose();
                    }
                }
            }
        }
    }
}