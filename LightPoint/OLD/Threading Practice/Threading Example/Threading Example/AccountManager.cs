using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Threading_Example
{
    public class AccountManager
    {
        public Account _fromAccount;
        public Account _toAccount;
        public double _amount;

        public AccountManager(Account fromAccount, Account toAccount, double amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _amount = amount;
        }

        public void Transfer()
        {
            object _lock1, _lock2;
            if(_fromAccount._id < _toAccount._id)
            {
                _lock1 = _fromAccount;
                _lock2 = _toAccount;
            }
            else
            {
                _lock1 = _toAccount;
                _lock2 = _fromAccount;
            }

            Console.WriteLine(Thread.CurrentThread.Name + " is trying to acquire lock on " + ((Account)_lock1)._id.ToString());
            lock (_lock1)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " acquired lock on " + ((Account)_lock1)._id.ToString());
                Console.WriteLine(Thread.CurrentThread.Name + " went into sleep for 1 second" );
                Thread.Sleep(2000);
                Console.WriteLine(Thread.CurrentThread.Name + " back in action and is trying to acquire lock on " + ((Account)_lock2)._id.ToString());
                lock (_lock2)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " acquired lock on " + ((Account)_lock2)._id.ToString());
                    _fromAccount.Withdraw(_amount);
                    _toAccount.deposit(_amount);
                    Console.WriteLine(Thread.CurrentThread.Name + " Transfered " + _amount + " from " + _fromAccount._id.ToString() + " to " + _toAccount._id.ToString());
                }
            }
        }
    }
}
