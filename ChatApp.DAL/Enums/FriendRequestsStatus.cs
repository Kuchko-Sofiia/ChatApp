﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DAL.Enums
{
    public enum FriendRequestsStatus
    {
        Unknown = 0,
        Pending = 1,
        Accepted = 2,
        Declined = 3,
        Blocked = 4
    }
}
