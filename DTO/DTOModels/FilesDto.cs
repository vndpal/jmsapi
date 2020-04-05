using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class FilesDto
    {
        public int Id { get; set; }

        public string filetype { get; set; }

        public string fileGroup { get; set; }

        public int refId { get; set; }

        public byte[] filedata { get; set; }

        public string filename { get; set; }

        public string contentType { get; set; }

        public DateTime uploadedDate { get; set; }

        public int uploadedBy { get; set; }
    }
}
