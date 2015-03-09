using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace Uncommon.MvvmCross.Plugins.ProtectedStore.Touch
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IProtectedStore>(new ProtectedStore());
        }
    }
}
