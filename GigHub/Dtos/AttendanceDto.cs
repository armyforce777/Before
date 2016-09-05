using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Dtos
{
    //it is architectural pattern to send data across process
    public class AttendanceDto
    {
        public int GigId { get; set; }
    }
}