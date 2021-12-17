using System;
using Server;
using Server.Network;

namespace Server.Misc
{
    public class ExpansionArt
    {
        public static void Initialize()
        {
            Server.Network.SupportedFeatures.Value = FeatureFlags.ExpansionTOL;

        }
    }
}