using UnityEngine;

namespace _Project.Scripts
{
   public class TimeManager : MonoBehaviour
   {
      private bool IsPaused { get; set; }

      public void Pause()
      {
         if (IsPaused)
         {
            return;
         }
         IsPaused = true;
         Time.timeScale = 0f;
      }

      public void Resume()
      {
         if (!IsPaused)
         {
            return;
         }
      
         IsPaused = false;
         Time.timeScale = 1f;
      }
   }
}
