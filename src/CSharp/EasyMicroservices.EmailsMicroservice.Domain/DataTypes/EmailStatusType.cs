using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.DataTypes
{
    public enum EmailStatusType
    {
        /// <summary>
        /// value is none, Never use the None to return values
        /// </summary>
        None = 0,
        /// <summary>
        /// error value is default
        /// </summary>
        Default = 1,
        /// <summary>
        /// for the filter values from web admin panel you can sent all for types
        /// </summary>
        All = 2,
        /// <summary>
        /// there is other error that is not in the types
        /// </summary>
        Other = 3,
        /// <summary>
        /// the error type is uknown to us
        /// </summary>
        Unknown = 4,
        /// <summary>
        /// there is nothing to show or validate error
        /// </summary>
        Nothing = 5,
        Queue = 6,
        Sending = 7,
        Sent = 8,
        Exception = 9,
        Canceled = 10,

    }
}
