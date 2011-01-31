using System;

namespace NabaztagNet
{
    public interface INabaztagMessage
    {
        Uri GenerateUrl(Nabaztag nab);
    }
}
