using System;
using System.Collections.Generic;
using System.Text;

namespace Threading_Example
{
    public class Account
    {
        public double _balance;
        public int _id;

        public Account(double balance, int id)
        {
            _balance = balance;
            _id = id;
        }

        public void Withdraw(double amount)
        {
            _balance -= amount;
        }

        public void deposit(double amount)
        {
            _balance += amount;
        }
    }
}
