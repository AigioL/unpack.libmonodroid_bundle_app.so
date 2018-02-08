// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Kaitai.Elf
{
    internal enum Endian
    {
        Le = 1,
        Be = 2,
    }

    internal enum ShType
    {
        NullType = 0,
        Progbits = 1,
        Symtab = 2,
        Strtab = 3,
        Rela = 4,
        Hash = 5,
        Dynamic = 6,
        Note = 7,
        Nobits = 8,
        Rel = 9,
        Shlib = 10,
        Dynsym = 11,
        InitArray = 14,
        FiniArray = 15,
        PreinitArray = 16,
        Group = 17,
        SymtabShndx = 18,
        SunwCapchain = 1879048175,
        SunwCapinfo = 1879048176,
        SunwSymsort = 1879048177,
        SunwTlssort = 1879048178,
        SunwLdynsym = 1879048179,
        SunwDof = 1879048180,
        SunwCap = 1879048181,
        SunwSignature = 1879048182,
        SunwAnnotate = 1879048183,
        SunwDebugstr = 1879048184,
        SunwDebug = 1879048185,
        SunwMove = 1879048186,
        SunwComdat = 1879048187,
        SunwSyminfo = 1879048188,
        SunwVerdef = 1879048189,
        SunwVerneed = 1879048190,
        SunwVersym = 1879048191,
        SparcGotdata = 1879048192,
        Amd64Unwind = 1879048193,
    }

    internal enum OsAbi
    {
        SystemV = 0,
        HpUx = 1,
        Netbsd = 2,
        Gnu = 3,
        Solaris = 6,
        Aix = 7,
        Irix = 8,
        Freebsd = 9,
        Tru64 = 10,
        Modesto = 11,
        Openbsd = 12,
        Openvms = 13,
        Nsk = 14,
        Aros = 15,
        Fenixos = 16,
        Cloudabi = 17,
        Openvos = 18,
    }

    internal enum Machine
    {
        NotSet = 0,
        Sparc = 2,
        X86 = 3,
        Mips = 8,
        Powerpc = 20,
        Arm = 40,
        Superh = 42,
        Ia64 = 50,
        X8664 = 62,
        Aarch64 = 183,
    }

    internal enum Bits
    {
        B32 = 1,
        B64 = 2,
    }

    internal enum PhType
    {
        NullType = 0,
        Load = 1,
        Dynamic = 2,
        Interp = 3,
        Note = 4,
        Shlib = 5,
        Phdr = 6,
        Tls = 7,
        GnuEhFrame = 1685382480,
        GnuStack = 1685382481,
        GnuRelro = 1685382482,
        Hios = 1879048191,
    }

    internal enum ObjType
    {
        Relocatable = 1,
        Executable = 2,
        Shared = 3,
        Core = 4,
    }

    internal sealed class Elf : KaitaiStruct
    {
        internal static Elf FromFile(string fileName)
        {
            return new Elf(new KaitaiStream(fileName));
        }

        internal Elf(KaitaiStream p__io, KaitaiStruct p__parent = null, Elf p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }

        private void _read()
        {
            _magic = m_io.EnsureFixedContents(new byte[] { 127, 69, 76, 70 });
            _bits = ((Bits)m_io.ReadU1());
            _endian = ((Endian)m_io.ReadU1());
            _eiVersion = m_io.ReadU1();
            _abi = ((OsAbi)m_io.ReadU1());
            _abiVersion = m_io.ReadU1();
            _pad = m_io.ReadBytes(7);
            _header = new EndianElf(m_io, this, m_root);
        }

        internal sealed class EndianElf : KaitaiStruct
        {
            internal static EndianElf FromFile(string fileName)
            {
                return new EndianElf(new KaitaiStream(fileName));
            }

            private bool? m_isLe;

            internal EndianElf(KaitaiStream p__io, Elf p__parent = null, Elf p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_programHeaders = false;
                f_sectionHeaders = false;
                f_strings = false;
                _read();
            }

            private void _read()
            {
                switch (M_Root.Endian)
                {
                    case Endian.Le:
                        {
                            m_isLe = (bool)(true);
                            break;
                        }
                    case Endian.Be:
                        {
                            m_isLe = (bool)(false);
                            break;
                        }
                }

                if (m_isLe == null)
                {
                    throw new Exception("Unable to decide on endianness");
                }
                else if (m_isLe == true)
                {
                    _readLE();
                }
                else
                {
                    _readBE();
                }
            }

            private void _readLE()
            {
                _eType = (ObjType)m_io.ReadU2le();
                _machine = (Machine)m_io.ReadU2le();
                _eVersion = m_io.ReadU4le();
                switch (M_Root.Bits)
                {
                    case Bits.B32:
                        {
                            _entryPoint = m_io.ReadU4le();
                            break;
                        }
                    case Bits.B64:
                        {
                            _entryPoint = m_io.ReadU8le();
                            break;
                        }
                }
                switch (M_Root.Bits)
                {
                    case Bits.B32:
                        {
                            _programHeaderOffset = m_io.ReadU4le();
                            break;
                        }
                    case Bits.B64:
                        {
                            _programHeaderOffset = (long)m_io.ReadU8le();
                            break;
                        }
                }
                switch (M_Root.Bits)
                {
                    case Bits.B32:
                        {
                            _sectionHeaderOffset = m_io.ReadU4le();
                            break;
                        }
                    case Bits.B64:
                        {
                            _sectionHeaderOffset = (long)m_io.ReadU8le();
                            break;
                        }
                }
                _flags = m_io.ReadBytes(4);
                _eEhsize = m_io.ReadU2le();
                _programHeaderEntrySize = m_io.ReadU2le();
                _qtyProgramHeader = m_io.ReadU2le();
                _sectionHeaderEntrySize = m_io.ReadU2le();
                _qtySectionHeader = m_io.ReadU2le();
                _sectionNamesIdx = m_io.ReadU2le();
            }

            private void _readBE()
            {
                _eType = (ObjType)m_io.ReadU2be();
                _machine = (Machine)m_io.ReadU2be();
                _eVersion = m_io.ReadU4be();
                switch (M_Root.Bits)
                {
                    case Bits.B32:
                        {
                            _entryPoint = m_io.ReadU4be();
                            break;
                        }
                    case Bits.B64:
                        {
                            _entryPoint = m_io.ReadU8be();
                            break;
                        }
                }
                switch (M_Root.Bits)
                {
                    case Bits.B32:
                        {
                            _programHeaderOffset = m_io.ReadU4be();
                            break;
                        }
                    case Bits.B64:
                        {
                            _programHeaderOffset = (long)m_io.ReadU8be();
                            break;
                        }
                }
                switch (M_Root.Bits)
                {
                    case Bits.B32:
                        {
                            _sectionHeaderOffset = m_io.ReadU4be();
                            break;
                        }
                    case Bits.B64:
                        {
                            _sectionHeaderOffset = (long)m_io.ReadU8be();
                            break;
                        }
                }
                _flags = m_io.ReadBytes(4);
                _eEhsize = m_io.ReadU2be();
                _programHeaderEntrySize = m_io.ReadU2be();
                _qtyProgramHeader = m_io.ReadU2be();
                _sectionHeaderEntrySize = m_io.ReadU2be();
                _qtySectionHeader = m_io.ReadU2be();
                _sectionNamesIdx = m_io.ReadU2be();
            }

            internal sealed class ProgramHeader : KaitaiStruct
            {
                internal static ProgramHeader FromFile(string fileName)
                {
                    return new ProgramHeader(new KaitaiStream(fileName));
                }

                private bool? m_isLe;

                internal ProgramHeader(KaitaiStream p__io, Elf.EndianElf p__parent = null, Elf p__root = null, bool? isLe = null) : base(p__io)
                {
                    M_Parent = p__parent;
                    m_root = p__root;
                    m_isLe = isLe;
                    _read();
                }

                private void _read()
                {
                    if (m_isLe == null)
                    {
                        throw new Exception("Unable to decide on endianness");
                    }
                    else if (m_isLe == true)
                    {
                        _readLE();
                    }
                    else
                    {
                        _readBE();
                    }
                }

                private void _readLE()
                {
                    _type = (PhType)m_io.ReadU4le();
                    if (M_Root.Bits == Bits.B64)
                    {
                        _flags64 = m_io.ReadU4le();
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _offset = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _offset = m_io.ReadU8le();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _vaddr = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _vaddr = m_io.ReadU8le();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _paddr = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _paddr = m_io.ReadU8le();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _filesz = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _filesz = m_io.ReadU8le();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _memsz = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _memsz = m_io.ReadU8le();
                                break;
                            }
                    }
                    if (M_Root.Bits == Bits.B32)
                    {
                        _flags32 = m_io.ReadU4le();
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _align = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _align = m_io.ReadU8le();
                                break;
                            }
                    }
                }

                private void _readBE()
                {
                    _type = (PhType)m_io.ReadU4be();
                    if (M_Root.Bits == Bits.B64)
                    {
                        _flags64 = m_io.ReadU4be();
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _offset = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _offset = m_io.ReadU8be();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _vaddr = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _vaddr = m_io.ReadU8be();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _paddr = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _paddr = m_io.ReadU8be();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _filesz = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _filesz = m_io.ReadU8be();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _memsz = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _memsz = m_io.ReadU8be();
                                break;
                            }
                    }
                    if (M_Root.Bits == Bits.B32)
                    {
                        _flags32 = m_io.ReadU4be();
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _align = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _align = m_io.ReadU8be();
                                break;
                            }
                    }
                }

                private PhType _type;
                private uint? _flags64;
                private ulong _offset;
                private ulong _vaddr;
                private ulong _paddr;
                private ulong _filesz;
                private ulong _memsz;
                private uint? _flags32;
                private ulong _align;
                private readonly Elf m_root;
                internal PhType Type => _type;
                internal uint? Flags64 => _flags64;
                internal ulong Offset => _offset;
                internal ulong Vaddr => _vaddr;
                internal ulong Paddr => _paddr;
                internal ulong Filesz => _filesz;
                internal ulong Memsz => _memsz;
                internal uint? Flags32 => _flags32;
                internal ulong Align => _align;
                internal Elf M_Root => m_root;
                internal EndianElf M_Parent { get; }
            }

            internal sealed class SectionHeader : KaitaiStruct
            {
                internal static SectionHeader FromFile(string fileName)
                {
                    return new SectionHeader(new KaitaiStream(fileName));
                }

                private readonly bool? m_isLe;

                internal SectionHeader(KaitaiStream p__io, EndianElf p__parent = null, Elf p__root = null, bool? isLe = null) : base(p__io)
                {
                    m_parent = p__parent;
                    m_root = p__root;
                    m_isLe = isLe;
                    f_body = false;
                    f_name = false;
                    _read();
                }

                private void _read()
                {
                    if (m_isLe == null)
                    {
                        throw new Exception("Unable to decide on endianness");
                    }

                    if (m_isLe == true)
                    {
                        _readLE();
                    }
                    else
                    {
                        _readBE();
                    }
                }

                private void _readLE()
                {
                    _nameOffset = m_io.ReadU4le();
                    _type = (ShType)m_io.ReadU4le();
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _flags = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _flags = m_io.ReadU8le();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _addr = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _addr = m_io.ReadU8le();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _offset = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _offset = (long)m_io.ReadU8le();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _size = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _size = m_io.ReadU8le();
                                break;
                            }
                    }
                    _linkedSectionIdx = m_io.ReadU4le();
                    _info = m_io.ReadBytes(4);
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _align = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _align = m_io.ReadU8le();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _entrySize = m_io.ReadU4le();
                                break;
                            }
                        case Bits.B64:
                            {
                                _entrySize = m_io.ReadU8le();
                                break;
                            }
                    }
                }

                private void _readBE()
                {
                    _nameOffset = m_io.ReadU4be();
                    _type = (ShType)m_io.ReadU4be();
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _flags = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _flags = m_io.ReadU8be();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _addr = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _addr = m_io.ReadU8be();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _offset = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _offset = (long)m_io.ReadU8be();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _size = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _size = m_io.ReadU8be();
                                break;
                            }
                    }
                    _linkedSectionIdx = m_io.ReadU4be();
                    _info = m_io.ReadBytes(4);
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _align = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _align = m_io.ReadU8be();
                                break;
                            }
                    }
                    switch (M_Root.Bits)
                    {
                        case Bits.B32:
                            {
                                _entrySize = m_io.ReadU4be();
                                break;
                            }
                        case Bits.B64:
                            {
                                _entrySize = m_io.ReadU8be();
                                break;
                            }
                    }
                }

                private bool f_body;
                private byte[] _body;

                internal byte[] Body
                {
                    get
                    {
                        if (f_body)
                            return _body;
                        KaitaiStream io = M_Root.M_Io;
                        long _pos = io.Pos;
                        io.Seek(Offset);
                        if (m_isLe == true)
                        {
                            _body = io.ReadBytes(Size);
                        }
                        else
                        {
                            _body = io.ReadBytes(Size);
                        }
                        io.Seek(_pos);
                        f_body = true;
                        return _body;
                    }
                }

                private bool f_name;
                private string _name;

                internal string Name
                {
                    get
                    {
                        if (f_name)
                            return _name;
                        KaitaiStream io = M_Root.Header.Strings.M_Io;
                        long _pos = io.Pos;
                        io.Seek(NameOffset);
                        if (m_isLe == true)
                        {
                            _name = System.Text.Encoding.GetEncoding("ASCII").GetString(io.ReadBytesTerm(0, false, true, true));
                        }
                        else
                        {
                            _name = System.Text.Encoding.GetEncoding("ASCII").GetString(io.ReadBytesTerm(0, false, true, true));
                        }
                        io.Seek(_pos);
                        f_name = true;
                        return _name;
                    }
                }

                private uint _nameOffset;
                private ShType _type;
                private ulong _flags;
                private ulong _addr;
                private long _offset;
                private ulong _size;
                private uint _linkedSectionIdx;
                private byte[] _info;
                private ulong _align;
                private ulong _entrySize;
                private readonly Elf m_root;
                private readonly EndianElf m_parent;
                internal uint NameOffset => _nameOffset;
                internal ShType Type => _type;
                internal ulong Flags => _flags;
                internal ulong Addr => _addr;
                internal long Offset => _offset;
                internal ulong Size => _size;
                internal uint LinkedSectionIdx => _linkedSectionIdx;
                internal byte[] Info => _info;
                internal ulong Align => _align;
                internal ulong EntrySize => _entrySize;
                internal Elf M_Root => m_root;
                internal EndianElf M_Parent => m_parent;
            }

            internal sealed class StringsStruct : KaitaiStruct
            {
                internal static StringsStruct FromFile(string fileName)
                {
                    return new StringsStruct(new KaitaiStream(fileName));
                }

                private bool? m_isLe;

                internal StringsStruct(KaitaiStream p__io, Elf.EndianElf p__parent = null, Elf p__root = null, bool? isLe = null) : base(p__io)
                {
                    m_parent = p__parent;
                    m_root = p__root;
                    m_isLe = isLe;
                    _read();
                }

                private void _read()
                {
                    if (m_isLe == null)
                    {
                        throw new Exception("Unable to decide on endianness");
                    }
                    else if (m_isLe == true)
                    {
                        _readLE();
                    }
                    else
                    {
                        _readBE();
                    }
                }

                private void _readLE()
                {
                    _entries = new List<string>();
                    {
                        var i = 0;
                        while (!m_io.IsEof)
                        {
                            _entries.Add(System.Text.Encoding.GetEncoding("ASCII").GetString(m_io.ReadBytesTerm(0, false, true, true)));
                            i++;
                        }
                    }
                }

                private void _readBE()
                {
                    _entries = new List<string>();
                    {
                        var i = 0;
                        while (!m_io.IsEof)
                        {
                            _entries.Add(System.Text.Encoding.GetEncoding("ASCII").GetString(m_io.ReadBytesTerm(0, false, true, true)));
                            i++;
                        }
                    }
                }

                private List<string> _entries;
                private Elf m_root;
                private Elf.EndianElf m_parent;
                internal List<string> Entries => _entries;
                internal Elf M_Root => m_root;
                internal Elf.EndianElf M_Parent => m_parent;
            }

            private bool f_programHeaders;
            private List<ProgramHeader> _programHeaders;

            internal List<ProgramHeader> ProgramHeaders
            {
                get
                {
                    if (f_programHeaders)
                        return _programHeaders;
                    long _pos = m_io.Pos;
                    m_io.Seek(ProgramHeaderOffset);
                    if (m_isLe == true)
                    {
                        __raw_programHeaders = new List<byte[]>((int)(QtyProgramHeader));
                        _programHeaders = new List<ProgramHeader>((int)(QtyProgramHeader));
                        for (var i = 0; i < QtyProgramHeader; i++)
                        {
                            __raw_programHeaders.Add(m_io.ReadBytes(ProgramHeaderEntrySize));
                            var io___raw_programHeaders = new KaitaiStream(__raw_programHeaders[__raw_programHeaders.Count - 1]);
                            _programHeaders.Add(new ProgramHeader(io___raw_programHeaders, this, m_root, m_isLe));
                        }
                    }
                    else
                    {
                        __raw_programHeaders = new List<byte[]>((int)(QtyProgramHeader));
                        _programHeaders = new List<ProgramHeader>((int)(QtyProgramHeader));
                        for (var i = 0; i < QtyProgramHeader; i++)
                        {
                            __raw_programHeaders.Add(m_io.ReadBytes(ProgramHeaderEntrySize));
                            var io___raw_programHeaders = new KaitaiStream(__raw_programHeaders[__raw_programHeaders.Count - 1]);
                            _programHeaders.Add(new ProgramHeader(io___raw_programHeaders, this, m_root, m_isLe));
                        }
                    }
                    m_io.Seek(_pos);
                    f_programHeaders = true;
                    return _programHeaders;
                }
            }

            private bool f_sectionHeaders;
            private List<SectionHeader> _sectionHeaders;

            internal List<SectionHeader> SectionHeaders
            {
                get
                {
                    if (f_sectionHeaders)
                        return _sectionHeaders;
                    long _pos = m_io.Pos;
                    m_io.Seek(SectionHeaderOffset);
                    if (m_isLe == true)
                    {
                        __raw_sectionHeaders = new List<byte[]>((int)(QtySectionHeader));
                        _sectionHeaders = new List<SectionHeader>((int)(QtySectionHeader));
                        for (var i = 0; i < QtySectionHeader; i++)
                        {
                            __raw_sectionHeaders.Add(m_io.ReadBytes(SectionHeaderEntrySize));
                            var io___raw_sectionHeaders = new KaitaiStream(__raw_sectionHeaders[__raw_sectionHeaders.Count - 1]);
                            _sectionHeaders.Add(new SectionHeader(io___raw_sectionHeaders, this, m_root, m_isLe));
                        }
                    }
                    else
                    {
                        __raw_sectionHeaders = new List<byte[]>((int)(QtySectionHeader));
                        _sectionHeaders = new List<SectionHeader>((int)(QtySectionHeader));
                        for (var i = 0; i < QtySectionHeader; i++)
                        {
                            __raw_sectionHeaders.Add(m_io.ReadBytes(SectionHeaderEntrySize));
                            var io___raw_sectionHeaders = new KaitaiStream(__raw_sectionHeaders[__raw_sectionHeaders.Count - 1]);
                            _sectionHeaders.Add(new SectionHeader(io___raw_sectionHeaders, this, m_root, m_isLe));
                        }
                    }
                    m_io.Seek(_pos);
                    f_sectionHeaders = true;
                    return _sectionHeaders;
                }
            }

            private bool f_strings;
            private StringsStruct _strings;

            internal StringsStruct Strings
            {
                get
                {
                    if (f_strings)
                        return _strings;
                    long _pos = m_io.Pos;
                    m_io.Seek(SectionHeaders[SectionNamesIdx].Offset);
                    if (m_isLe == true)
                    {
                        __raw_strings = m_io.ReadBytes(SectionHeaders[SectionNamesIdx].Size);
                        var io___raw_strings = new KaitaiStream(__raw_strings);
                        _strings = new StringsStruct(io___raw_strings, this, m_root, m_isLe);
                    }
                    else
                    {
                        __raw_strings = m_io.ReadBytes(SectionHeaders[SectionNamesIdx].Size);
                        var io___raw_strings = new KaitaiStream(__raw_strings);
                        _strings = new StringsStruct(io___raw_strings, this, m_root, m_isLe);
                    }
                    m_io.Seek(_pos);
                    f_strings = true;
                    return _strings;
                }
            }

            private ObjType _eType;
            private Machine _machine;
            private uint _eVersion;
            private ulong _entryPoint;
            private long _programHeaderOffset;
            private long _sectionHeaderOffset;
            private byte[] _flags;
            private ushort _eEhsize;
            private ushort _programHeaderEntrySize;
            private ushort _qtyProgramHeader;
            private ushort _sectionHeaderEntrySize;
            private ushort _qtySectionHeader;
            private ushort _sectionNamesIdx;
            private readonly Elf m_root;
            private readonly Elf m_parent;
            private List<byte[]> __raw_programHeaders;
            private List<byte[]> __raw_sectionHeaders;
            private byte[] __raw_strings;
            internal ObjType EType => _eType;
            internal Machine Machine => _machine;
            internal uint EVersion => _eVersion;
            internal ulong EntryPoint => _entryPoint;
            internal long ProgramHeaderOffset => _programHeaderOffset;
            internal long SectionHeaderOffset => _sectionHeaderOffset;
            internal byte[] Flags => _flags;
            internal ushort EEhsize => _eEhsize;
            internal ushort ProgramHeaderEntrySize => _programHeaderEntrySize;
            internal ushort QtyProgramHeader => _qtyProgramHeader;
            internal ushort SectionHeaderEntrySize => _sectionHeaderEntrySize;
            internal ushort QtySectionHeader => _qtySectionHeader;
            internal ushort SectionNamesIdx => _sectionNamesIdx;
            internal Elf M_Root => m_root;
            internal Elf M_Parent => m_parent;
            internal List<byte[]> M_RawProgramHeaders => __raw_programHeaders;
            internal List<byte[]> M_RawSectionHeaders => __raw_sectionHeaders;
            internal byte[] M_RawStrings => __raw_strings;
        }

        private byte[] _magic;
        private Bits _bits;
        private Endian _endian;
        private byte _eiVersion;
        private OsAbi _abi;
        private byte _abiVersion;
        private byte[] _pad;
        private EndianElf _header;
        private Elf m_root;
        private KaitaiStruct m_parent;

        /// <summary>
        /// File identification, must be 0x7f + &quot;ELF&quot;.
        /// </summary>
        internal byte[] Magic => _magic;

        /// <summary>
        /// File class: designates target machine word size (32 or 64
        /// bits). The size of many integer fields in this format will
        /// depend on this setting.
        /// </summary>
        internal Bits Bits => _bits;

        /// <summary>
        /// Endianness used for all integers.
        /// </summary>
        internal Endian Endian => _endian;

        /// <summary>
        /// ELF header version.
        /// </summary>
        internal byte EiVersion => _eiVersion;

        /// <summary>
        /// Specifies which OS- and ABI-related extensions will be used
        /// in this ELF file.
        /// </summary>
        internal OsAbi Abi => _abi;

        /// <summary>
        /// Version of ABI targeted by this ELF file. Interpretation
        /// depends on `abi` attribute.
        /// </summary>
        internal byte AbiVersion => _abiVersion;

        internal byte[] Pad => _pad;
        internal EndianElf Header => _header;
        internal Elf M_Root => m_root;
        internal KaitaiStruct M_Parent => m_parent;
    }
}