using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TdMarzPay.Models.Responses;

namespace TdMarzPay.Interfaces
{
    public interface ISendMoney
    {
       /// <summary>
       /// Send money to another Party
       /// </summary>
       void SendMoney();
       ///<summary>
       ///get services for sending money
       ///</summary>
       void SendMoneyServices();
       ///<summary>
       ///get Detials of a send money service
       ///</summary>
       void GetServiceDetails();
        
    }
}
