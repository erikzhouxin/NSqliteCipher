using System;
using System.Text;

namespace SQLitePCL.Raw.Core
{
    // TODO consider Length property, which returns sp.Length - 1, but what about null
    // TODO consider way to get span, not included the zero terminator, but what about null

    /// <summary>
    /// A raw string representation suitable for passing into many core SQLite apis. These will normally be pointers to
    /// utf8 encoded bytes, with a trailing <c>\0</c> terminator.  <see langword="null"/> strings can be represented as
    /// well as empty strings.
    /// </summary>
    public readonly ref struct Utf8z
    {
        // this span will contain a zero terminator byte
        // if sp.Length is 0, it represents a null string
        // if sp.Length is 1, the only byte must be zero, and it is an empty string
#if NET40
        readonly byte[] sp;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ref readonly byte GetPinnableReference()
        {
            if (sp != null && sp.Length > 0)
            {
                return ref sp[0];
            }
            unsafe
            {
                byte* ptr = default;
                return ref *ptr;
            }
        }
        Utf8z(byte[] a)
        {
            // no check here.  anything that calls this
            // constructor must make assurances about the
            // zero terminator.
            sp = a;
        }
        /// <summary>
        /// Creates a new instance of <see cref="Utf8z"/> which directly points at the memory pointed to by <paramref
        /// name="span"/>. The span must contain a valid <see cref="Encoding.UTF8"/> encoded block of memory that
        /// terminates with a <c>\0</c> byte.  The span passed in must include the <c>\0</c> terminator.
        /// <para/>
        /// Both <see langword="null"/> and empty strings can be created here.  To create a <see langword="null"/> string,
        /// pass in an empty <see cref="ReadOnlySpan{T}"/>.  To create an empty string, pass in a span with length 1, that
        /// only contains a <c>\0</c>
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if <c>span.Length > 0</c> and <c>span[^1]</c> is not <c>\0</c>.
        /// </exception>
        public static Utf8z FromSpan(byte[] span)
        {
            if (
                (span.Length > 0)
                && (span[span.Length - 1] != 0)
                )
            {
                throw new ArgumentException("zero terminator required");
            }
            return new Utf8z(span);
        }

        public static Utf8z FromString(string s)
        {
            if (s == null)
            {
                return new Utf8z(new byte[] { });
            }
            else
            {
                return new Utf8z(s.ToUtf8WithZ());
            }
        }

#else
        readonly ReadOnlySpan<byte> sp;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ref readonly byte GetPinnableReference()
        {
            return ref sp.GetPinnableReference();
        }
        Utf8z(ReadOnlySpan<byte> a)
        {
            // no check here.  anything that calls this
            // constructor must make assurances about the
            // zero terminator.
            sp = a;
        }
        /// <summary>
        /// Creates a new instance of <see cref="Utf8z"/> which directly points at the memory pointed to by <paramref
        /// name="span"/>. The span must contain a valid <see cref="Encoding.UTF8"/> encoded block of memory that
        /// terminates with a <c>\0</c> byte.  The span passed in must include the <c>\0</c> terminator.
        /// <para/>
        /// Both <see langword="null"/> and empty strings can be created here.  To create a <see langword="null"/> string,
        /// pass in an empty <see cref="ReadOnlySpan{T}"/>.  To create an empty string, pass in a span with length 1, that
        /// only contains a <c>\0</c>
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if <c>span.Length > 0</c> and <c>span[^1]</c> is not <c>\0</c>.
        /// </exception>
        public static Utf8z FromSpan(ReadOnlySpan<byte> span)
        {
            if (
                (span.Length > 0)
                && (span[span.Length - 1] != 0)
                )
            {
                throw new ArgumentException("zero terminator required");
            }
            return new Utf8z(span);
        }

        public static Utf8z FromString(string s)
        {
            if (s == null)
            {
                return new Utf8z(ReadOnlySpan<byte>.Empty);
            }
            else
            {
                return new Utf8z(s.ToUtf8WithZ());
            }
        }

#endif
        unsafe static long my_strlen(byte* p)
        {
            var q = p;
            while (*q != 0)
            {
                q++;
            }
            return q - p;
        }

#if NET40
        unsafe static byte[] find_zero_terminator(byte* p)
        {
            var len = (int)my_strlen(p);
            var re = new byte[len + 1];
            for (int i = 0; i < len; i++)
            {
                re[i] = p[i];
            }
            return re;
        }
#else
        unsafe static ReadOnlySpan<byte> find_zero_terminator(byte* p)
        {
            var len = (int)my_strlen(p);
            return new ReadOnlySpan<byte>(p, len + 1);
        }
#endif
        /// <summary>
        /// Creates a new instance of <see cref="Utf8z"/> which directly points at the memory pointed to by <paramref
        /// name="p"/>. The pointer must either be <see langword="null"/> or point to a valid <see
        /// cref="Encoding.UTF8"/> encoded block of memory that terminates with a <c>\0</c> byte.
        /// </summary>
        unsafe public static Utf8z FromPtr(byte* p)
        {
            if (p == null)
            {
#if NET40
                return new Utf8z(new byte[] { });
#else
                return new Utf8z(ReadOnlySpan<byte>.Empty);
#endif
            }
            else
            {
                return new Utf8z(find_zero_terminator(p));
            }
        }

#if NET40
        // TODO maybe remove this and just use FromSpan?

        /// <summary>
        /// Creates a new instance of <see cref="Utf8z"/> which directly points at the memory pointed to by <paramref
        /// name="p"/> with length <paramref name="len"/>. The pointer must be to a valid <see cref="Encoding.UTF8"/>
        /// encoded block of memory that terminates with a <c>\0</c> byte.  The <paramref name="len"/> value refers to
        /// the number of bytes in the utf8 encoded value <em>not</em> including the <c>\0</c> byte terminator.
        /// <para/>
        /// <paramref name="p"/> can be <see langword="null"/>, in which case <paramref name="len"/> is ignored
        /// and a new <see cref="Utf8z"/> instance is created that represents <see langword="null"/>.  Note that this
        /// different from a pointer to a single <c>\0</c> byte and a length of one.  That would represent an empty <see
        /// cref="Utf8z"/> string.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="p"/> is not <see langword="null"/> and <c>p[len]</c> is not <c>\0</c>.
        /// </exception>
        unsafe public static Utf8z FromPtrLen(byte* p, int len)
        {
            if (p == null)
            {
                return new Utf8z(new byte[] { });
            }
            else
            {
                // the given len does NOT include the zero terminator
                var re = new byte[len + 1];
                for (int i = 0; i < len; i++)
                {
                    re[i] = p[i];
                }
                return FromSpan(re);
            }
        }

        unsafe public static Utf8z FromIntPtr(IntPtr p)
        {
            if (p == IntPtr.Zero)
            {
                return new Utf8z(new byte[] { });
            }
            else
            {
                return new Utf8z(find_zero_terminator((byte*)(p.ToPointer())));
            }
        }
#else
        // TODO maybe remove this and just use FromSpan?

        /// <summary>
        /// Creates a new instance of <see cref="utf8z"/> which directly points at the memory pointed to by <paramref
        /// name="p"/> with length <paramref name="len"/>. The pointer must be to a valid <see cref="Encoding.UTF8"/>
        /// encoded block of memory that terminates with a <c>\0</c> byte.  The <paramref name="len"/> value refers to
        /// the number of bytes in the utf8 encoded value <em>not</em> including the <c>\0</c> byte terminator.
        /// <para/>
        /// <paramref name="p"/> can be <see langword="null"/>, in which case <paramref name="len"/> is ignored
        /// and a new <see cref="utf8z"/> instance is created that represents <see langword="null"/>.  Note that this
        /// different from a pointer to a single <c>\0</c> byte and a length of one.  That would represent an empty <see
        /// cref="utf8z"/> string.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="p"/> is not <see langword="null"/> and <c>p[len]</c> is not <c>\0</c>.
        /// </exception>
        unsafe public static Utf8z FromPtrLen(byte* p, int len)
        {
            if (p == null)
            {
                return new Utf8z(ReadOnlySpan<byte>.Empty);
            }
            else
            {
                // the given len does NOT include the zero terminator
                var sp = new ReadOnlySpan<byte>(p, len + 1);
                return FromSpan(sp);
            }
        }

        unsafe public static Utf8z FromIntPtr(IntPtr p)
        {
            if (p == IntPtr.Zero)
            {
                return new Utf8z(ReadOnlySpan<byte>.Empty);
            }
            else
            {
                return new Utf8z(find_zero_terminator((byte*) (p.ToPointer())));
            }
        }
#endif
        public string utf8_to_string()
        {
            if (sp.Length == 0)
            {
                return null;
            }

            unsafe
            {
                fixed (byte* q = sp)
                {
                    return Encoding.UTF8.GetString(q, sp.Length - 1);
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="Encoding.UTF8"/> encoded bytes for the provided <paramref name="value"/>.  The array
        /// will include a trailing <c>\0</c> character.  The length of the array will <see cref="Encoding.UTF8"/>'s
        /// <see cref="Encoding.GetByteCount(string)"/><c>+1</c> (for the trailing <c>\0</c> byte).  These bytes are
        /// suitable to use with <see cref="FromSpan"/> using the extension <see
        /// cref="MemoryExtensions.AsSpan{T}(T[])"/> or <see cref="FromPtr(byte*)"/> or <see cref="FromPtrLen(byte*,
        /// int)"/>.  Note that for <see cref="FromPtrLen(byte*, int)"/> the length provided should not include the
        /// trailing <c>\0</c> terminator.
        /// </summary>
        public static byte[] GetZeroTerminatedUTF8Bytes(string value)
        {
            return InnerCaller.ToUtf8WithZ(value);
        }
    }
}
