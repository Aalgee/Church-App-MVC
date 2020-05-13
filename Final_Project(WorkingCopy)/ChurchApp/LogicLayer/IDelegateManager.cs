using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
namespace LogicLayer
{
    /// <summary>
    /// Interface for the delegate manager methods
    /// 
    /// </summary>
    public interface IDelegateManager
    {
        bool AddDelegate(ElectionDelegate electionDelegate);
        List<ElectionDelegate> RetrieveDelegatesByActive(bool active);
        ElectionDelegate RetrieveDelegateByID(int delegateID);
        ElectionDelegate RetrieveDelegateByPin(string pin);
        bool EditDelegate(ElectionDelegate oldDelegate, ElectionDelegate newDelegate);
        bool EditDelegatePin(int delegateID, string pin);
        bool DeactivateDelegate(int delegateID);
        bool ActivateDelegate(int delegateID);
    }
}
