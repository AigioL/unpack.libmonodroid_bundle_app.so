#if NET35

// ReSharper disable once CheckNamespace
namespace System.IO
{
    /// <summary>
    /// CopyTo .NET Framework 3.5
    /// </summary>
    public static class StreamExtension
    {
        public static void CopyTo(this Stream source, Stream destination) => CopyTo(source, destination, 81920);

        public static void CopyTo(this Stream source, Stream destination, int bufferSize)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize), "ArgumentOutOfRange_NeedPosNum");
            if (!source.CanRead && !source.CanWrite)
                throw new ObjectDisposedException(nameof(source), "ObjectDisposed_StreamClosed");
            if (!destination.CanRead && !destination.CanWrite)
                throw new ObjectDisposedException(nameof(destination), "ObjectDisposed_StreamClosed");
            if (!source.CanRead)
                throw new NotSupportedException(nameof(source) + "_NotSupported_UnreadableStream");
            if (!destination.CanWrite)
                throw new NotSupportedException(nameof(destination) + "_NotSupported_UnwritableStream");
            InternalCopyTo(source, destination, bufferSize);
        }

        private static void InternalCopyTo(Stream source, Stream destination, int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int count;
            while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
                destination.Write(buffer, 0, count);
        }
    }
}

#endif