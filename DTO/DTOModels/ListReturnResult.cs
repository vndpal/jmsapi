using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class ListReturnResult<T>
    {
        public string Flag { get; set; }
        public string message { get; set; }
        public long returnId { get; set; }
        public List<T> result { get; set; }
    }
}
