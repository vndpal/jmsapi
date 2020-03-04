using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Common
{
    public class Enums
    {

        public enum DepartmentType
        {
            HR=1,
            CAD=2,
            WAX=3,
            Casting=4,
            Filling=5,
            Polish=6,
            QC=7,
            DiamondFitter=8
        }

        public enum EmployeeType
        {
            Contract=1,
            Salaried=2
        }

        public enum AddresAndIdentity
        {
            AadharCard=1,
            PanCard=2,
            DrivingLic=3,
            RationCard=4
        }
    }
}
