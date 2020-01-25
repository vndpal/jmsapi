using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class SingleReturnResult<T>
    {
        public string Flag { get; set; }
        public string message { get; set; }
        public long returnId { get; set; }
        public T result { get; set; }
    }
}
