using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace Xciles.Uncommon.MvvmCross.Plugins.ProtectedStore.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IProtectedStore>(new ProtectedStore());
        }
    }
}
