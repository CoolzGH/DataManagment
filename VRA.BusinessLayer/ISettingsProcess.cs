using System;
using System.Collections.Generic;
using System.Text;

namespace VRA.BusinessLayer
{
    public interface ISettingsProcess
    {
        string GetSettings();
        bool SetSettings(string server, string db, string user, string password);
    }
}
