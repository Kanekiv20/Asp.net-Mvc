using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank1;
namespace ServiceBank
{
    public interface IBankOperation
    {
        decimal Balence(string username);
        string Signup(BankUser user);
        string Login(BankUser user);
        string Transfer(string senderUsername, string receiverUsername, decimal amount);
    }
}
