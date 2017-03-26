using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Cats.Services
{
    /*
    class AzureService
    {
    }
    */

    public class AzureService<T>
    {
        IMobileServiceClient Client;
        IMobileServiceTable<T> Table;

        //construtor
        public AzureService()
        {
            string MyAppServiceURL = "http://CatasAppPablo.azurewebsites.net";
            Client = new MobileServiceClient(MyAppServiceURL);
            Table = Client.GetTable<T>();
        }

        //pegar dados da tabela IEnumerable
        public Task<IEnumerable<T>> GetTable()
        {
            return Table.ToEnumerableAsync();
        }
    }
}
