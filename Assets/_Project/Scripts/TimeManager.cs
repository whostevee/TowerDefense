using UnityEngine;

namespace _Project.Scripts
{
   public class TimeManager : MonoBehaviour
   {
      private bool _isPaused;

      public void Pause()
      {
         if (_isPaused)
         {
            return;
         }
         _isPaused = true;
         Time.timeScale = 0f;
      }

      public void Resume()
      {
         if (!_isPaused)
         {
            return;
         }
      
         _isPaused = false;
         Time.timeScale = 1f;
      }
   }
}
