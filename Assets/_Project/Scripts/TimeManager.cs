using System;
using UnityEngine;

namespace _Project.Scripts
{
   public class TimeManager : MonoBehaviour
   {
      private bool _isPaused;
      public bool IsPaused => _isPaused;

      public event Action OnGamePaused;
      public event Action OnGameResumed;

      public void Pause()
      {
         if (_isPaused)
         {
            return;
         }
         _isPaused = true;
         OnGamePaused?.Invoke();
      }

      public void Resume()
      {
         if (!_isPaused)
         {
            return;
         }
      
         _isPaused = false;
         OnGameResumed?.Invoke();
      }
   }
}
