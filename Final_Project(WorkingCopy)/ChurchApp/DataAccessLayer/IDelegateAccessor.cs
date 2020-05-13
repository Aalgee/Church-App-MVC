using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Data access interfaces for delegate classes
    /// </summary>
    public interface IDelegateAccessor
    {
        int InsertDelegate(ElectionDelegate theDelegate);
        List<ElectionDelegate> SelectDelegatesByActive(bool active);
        ElectionDelegate SelectDelegateByID(int delegateID);
        ElectionDelegate SelectDelegateByPin(string pin);
        int UpdateDelegate(ElectionDelegate oldDelegate, ElectionDelegate newDelegate);
        int UpdateDelegatePin(int delegateID, string pin);
        int DeactivateDelegate(int delegateID);
        int ActivateDelegate(int delegateID);
    }
}
