using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inex_inter.Domain.Repositories.Abstract;






namespace Inex_inter.Domain
{
    public class DataManager
    {
        public ITextFieldsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }

        public DataManager(ITextFieldsRepository textFieldsRepository,IServiceItemsRepository serviceItemsRepository)
        {
            TextFields = textFieldsRepository;
            ServiceItems = serviceItemsRepository;
        }
    }
}
