using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaultLifeAdmin.Service.Rules
{
    public enum RuleType
    {
        NOTIFY_PARTICIPANTS,
        PREPARE_GAME,
        START_GAME,
        RESOLVE_WINNERS,
        RESOLVE_ACTUAL_WINNERS,
        START_NEW_GAME

    }
}