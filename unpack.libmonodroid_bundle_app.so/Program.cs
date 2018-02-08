using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using Elf = Kaitai.Elf.Elf;

namespace unpack.libmonodroid_bundle_app.so
{
    /// <summary>
    /// 😄🔒💻📶📱🖥⌚🎮📺💾
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            TryMain();
            void TryMain()
            {
                try
                {
                    Main();
                }
                catch (Exception ex)
                {
                    var index = byte.MinValue;
                    while (ex != null)
                    {
                        if (index++ > sbyte.MaxValue) break;
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        ex = ex.InnerException;
                    }
                }
                Console.ReadLine();
            }
            void Main()
            {
                // ReSharper disable PossibleNullReferenceException
                var entryAssembly = Assembly.GetEntryAssembly();
                var title = entryAssembly.FullName.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(title)) Console.Title = title;
                var rootPath = Path.GetDirectoryName(entryAssembly.Location);
                if (rootPath == null) throw new ArgumentNullException(nameof(rootPath));
                var sofilepaths = Directory.GetFiles(rootPath).Where(x =>
                    Path.GetExtension(x).Equals(GetSoFileExtension(), StringComparison.OrdinalIgnoreCase)).ToArray();
                var sofilepath =
                    sofilepaths.FirstOrDefault(x => Path.GetFileName(x).Equals(GetSoDefaultFileName(), StringComparison.OrdinalIgnoreCase)) ??
                    sofilepaths.FirstOrDefault();
                if (sofilepath == null) ReadLineAndExit("Can not find the .so file.");
                // ReSharper disable once AssignNullToNotNullAttribute
                var bytes = File.ReadAllBytes(sofilepath);
                var elf = Elf.FromFile(sofilepath);
                var rodata = elf.Header.SectionHeaders.FirstOrDefault(x => x.Name.Equals(".rodata"));
                if (rodata == null) ReadLineAndExit(".rodata not found.");
                var packedFiles = new List<string>();
                var addr = (uint)rodata.Addr;
                while (true)
                {
                    //up to 16 bytes of alignment
                    uint i;
                    for (i = 0; i < 16; i++)
                        if (bytes[addr + i] != 0)
                            break;

                    if (i == 16)
                        break; //We found all the files
                    addr += i;

                    var name = GetString(bytes, addr);
                    if (string.IsNullOrWhiteSpace(name))
                        break;

                    //We only care about dlls
                    if (!name.EndsWith(".dll"))
                        break;

                    packedFiles.Add(name);
                    addr += (uint)name.Length + 1u;
                }
                var data = elf.Header.SectionHeaders.FirstOrDefault(x => x.Name.Equals(".data"));
                if (data == null) ReadLineAndExit(".data not found.");
                int ixGzip = 0;
                addr = (uint)data.Offset;
                var output = Path.Combine(rootPath, "assemblies");
                if (!Directory.Exists(output)) Directory.CreateDirectory(output);
                Console.WriteLine($"output:{output}");
                foreach (var item in packedFiles)
                {
                    ixGzip = findNextGZIPIndex(bytes, ixGzip);
                    if (ixGzip > 0)
                    {
                        var ptr = ixGzip;
                        var length = GetBigEndianUInt32(bytes, addr + 8);
                        var compressedbytes = new byte[length];
                        if (ptr + length <= bytes.LongLength)
                        {
                            Array.Copy(bytes, ptr, compressedbytes, 0, length);
                            try
                            {
                                var decompbytes = Decompress(compressedbytes);
                                var path = Path.Combine(output, item);
                                File.WriteAllBytes(path, decompbytes);
                                Console.WriteLine($"file:{item}");
                                addr += 0x10;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Failed to decompress file: {item} {e.Message}.");
                            }
                        }
                    }
                }
                TryOpenDir(output);
                // ReSharper restore PossibleNullReferenceException
            }
            void ReadLineAndExit(string writeLine = null)
            {
                if (writeLine != null) Console.WriteLine(writeLine);
                Console.ReadLine();
                Environment.Exit(0);
            }
            string GetSoFileExtension() => ".so";
            string GetSoDefaultFileName() => "libmonodroid_bundle_app" + GetSoFileExtension();
            string GetString(byte[] bytes, uint address)
            {
                int maxLength = 255;
                for (int i = (int)address; i < address + maxLength; i++)
                {
                    if (bytes[i] == 0)
                    {
                        maxLength = i - (int)address;
                        break;
                    }
                }
                var buffer = new byte[maxLength];
                Array.Copy(bytes, address, buffer, 0, maxLength);
                return Encoding.ASCII.GetString(buffer);
            }
            int findNextGZIPIndex(byte[] bytes, int ixGzip)
            {
                for (var j = ixGzip + 2; j < bytes.Length; j++)
                {
                    if (bytes[j - 1] == 0x1f && bytes[j] == 0x8b)
                    {
                        ixGzip = j - 1;
                        return ixGzip;
                    }
                }
                return 0;
            }
            byte[] Decompress(byte[] data)
            {
                using (var compressedStream = new MemoryStream(data))
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                using (var resultStream = new MemoryStream())
                {
                    zipStream.CopyTo(resultStream);
                    return resultStream.ToArray();
                }
            }
            uint GetBigEndianUInt32(byte[] bytes, uint address)
            {
                var byte1 = (uint)bytes[(int)address + 3] << 24;
                var byte2 = (uint)bytes[(int)address + 2] << 16;
                var byte3 = (uint)bytes[(int)address + 1] << 8;
                var byte4 = bytes[(int)address];
                return byte1 + byte2 + byte3 + byte4;
            }
            void TryOpenDir(string dirpath)
            {
                try
                {
                    Process.Start(dirpath);
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}