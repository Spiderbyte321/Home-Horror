using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
        [SerializeField] private TutorialCollisionBox[] playerTriggerColisions;

        [SerializeField] private GameObject wallSegment;
        [SerializeField] private GameObject basementDoor;
        [SerializeField] private GameObject[] basementJumpscares;
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
        }

        private void OnDisable()
        {
                TutorialCollisionBox.TutorialBoxTriggeredAction -= PlayerEplored;
                TutorialBasementDoor.TutorialBasementDoorAction -= InformGame;
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
        }

        private void InformGame()
        { 
                PlayerEnteredBasementAction?.Invoke();
        }


        private void RevealBasementDoor()//Trigger event once we fade back in
        {
                //Play sound effect
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
}
