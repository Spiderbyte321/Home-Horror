using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
        [SerializeField] private TutorialCollisionBox[] playerTriggerColisions;

        [SerializeField] private GameObject wallSegment;
        [SerializeField] private GameObject basementDoor;
        [SerializeField] private GameObject[] basementJumpscares;
        [SerializeField] private GameObject hiddenLetter;
        private Dictionary<TutorialCollisionBox, bool> CheckPlayerCollisions = new Dictionary<TutorialCollisionBox, bool>();

        public delegate void OnPlayerExplored();

        public static event OnPlayerExplored PlayerExploredAction;

        public delegate void OnPLayerAwoken();

        public static event OnPLayerAwoken PlayerAwokenAction;

        public delegate void OnPLayerEnteredBasement();

        public static event OnPLayerEnteredBasement PlayerEnteredBasementAction;

        private void Awake()
        {
                for(int i=0;i<playerTriggerColisions.Length;i++)
                {
                        
                        CheckPlayerCollisions.Add(playerTriggerColisions[i],false);
                }
        }


        private void OnEnable()
        {
                TutorialCollisionBox.TutorialBoxTriggeredAction += PlayerEplored;
                TutorialBasementDoor.TutorialBasementDoorAction += InformGame;
                DayEndController.OnBedInteracted += RevealBasementDoor;
        }

        private void OnDisable()
        {
                TutorialCollisionBox.TutorialBoxTriggeredAction -= PlayerEplored;
                TutorialBasementDoor.TutorialBasementDoorAction -= InformGame;
                DayEndController.OnBedInteracted -= RevealBasementDoor;
        }


        private void PlayerEplored(TutorialCollisionBox collidedBox)
        {
                CheckPlayerCollisions[collidedBox] = true;

                foreach (KeyValuePair<TutorialCollisionBox,bool> box in CheckPlayerCollisions)
                { 
                        if(!box.Value) 
                                return;
                }
                
                PlayerExploredAction?.Invoke();
                hiddenLetter.SetActive(true);
        }

        private void InformGame()
        { 
                PlayerEnteredBasementAction?.Invoke();
                StartCoroutine(JumpscareDelay());
        }


        private void RevealBasementDoor()//Trigger event once we fade back in
        {
                wallSegment.SetActive(false);
                basementDoor.SetActive(true);
                PlayerAwokenAction?.Invoke();
        }

        private void PrimeBasementJumpScares()
        {
                foreach (GameObject scare in basementJumpscares)
                { 
                        scare.SetActive(true);
                }
        }


        private IEnumerator JumpscareDelay()
        {
                yield return new WaitForSeconds(20);
                PrimeBasementJumpScares();
        }
}
