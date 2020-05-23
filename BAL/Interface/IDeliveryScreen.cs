using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IDeliveryScreen
    {
        Task<SingleReturnResult<string>> AddDeliveryScreen(DeliveryScreenDto del);
        Task<ListReturnResult<AssignedJobDTO>> GetDeliveryAssignedJob();
    }
}
