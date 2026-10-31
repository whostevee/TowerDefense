using System;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "RestartEvent", menuName = "_Project/Events/RestartEvent")]
    public class RestartEvent : ScriptableObject
    {
        public event Action OnRestart;
        public bool SkipMenu {get; set;}

        public void Restart()
        {
            OnRestart?.Invoke();
        }
    }
}