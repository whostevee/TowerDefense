using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class MoneyManager : MonoBehaviour
    {
        public int Money { get; private set; }
        public event Action OnMoneyChanged;

        public void Add(int amount)
        {
            Money += amount;
            OnMoneyChanged?.Invoke();
        }

        public bool TrySpend(int amount)
        {
            if (Money < amount)
            {
                return false;
            }

            Money -= amount;
            OnMoneyChanged?.Invoke();
            return true;
        }
    }
}