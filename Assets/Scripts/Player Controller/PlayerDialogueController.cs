

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Yarn.Unity.Example
{
    public class PlayerDialogueController : MonoBehaviour
    {

        public float interactionRadius;

        /// Update is called once per frame
        void Update()
        {

            // Remove all player control when we're in dialogue
            //if (FindObjectOfType<DialogueRunner>().IsDialogueRunning == true)
            //{
            //    return;
            //}

            // Detect if we want to start a conversation
            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckForNearbyNPC();
            }
        }

        /// Find all DialogueParticipants
        /** Filter them to those that have a Yarn start node and are in range; 
         * then start a conversation with the first one
         */
        public void CheckForNearbyNPC()
        {
            var allParticipants = new List<NPC>(FindObjectsOfType<NPC>());
            var target = allParticipants.Find(delegate (NPC p) {
                return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
                (p.transform.TransformPoint(Vector3.zero) - this.transform.TransformPoint(Vector3.zero))// is in range?
                .magnitude <= interactionRadius;
            });
            if (target != null)
            {
                // Kick off the dialogue at this node.
                FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
            }
        }
    }
}
