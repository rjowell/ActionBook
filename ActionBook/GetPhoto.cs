using System;
using System.IO;
using System.Threading.Tasks;

namespace ActionBook
{
    public interface GetPhoto
    {
        Task<Stream> GetDevicePhoto();
    }
}
