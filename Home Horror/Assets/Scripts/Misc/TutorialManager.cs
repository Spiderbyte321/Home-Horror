using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
        [SerializeField] private TutorialCollisionBox[] playerTriggerColisions;

        private Dictionary<TutorialCollisionBox, bool> CheckPlayerCollisions = new Dictionary<TutorialCollisionBox, bool>();

        public delegate void OnPlayerExplored();

        public static event OnPlayerExplored PlayerExploredAction;

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
}
