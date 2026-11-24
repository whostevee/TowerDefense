using System;

namespace _Project.Scripts.Back
{
    public class Wallet
    {
        public int Money { get; private set; }
        public event Action OnMoneyChanged;

        public void AddMoney(int money)
        {
            Money += money;
            OnMoneyChanged?.Invoke();
        }
        
        public void SpendMoney(int price)
        {
            if (Money < price)
            {
                return;
            }
            Money -= price;
            OnMoneyChanged?.Invoke();
        }
    }
}