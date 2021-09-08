using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace System.Data.SQLiteCipher
{
    /// <summary>
    /// �����ַ����Ĵ����͹���ʹ��
    /// <see cref="SqliteConnection" />.
    /// </summary>
    public class SqliteConnectionStringBuilder : DbConnectionStringBuilder
    {
        /// <summary>
        /// Ĭ�ϰ汾
        /// </summary>
        public static SqliteVersion DefaultVersion { get; set; }

        private const string ProviderKeyword = "Provider";
        private const string DriverKeyword = "Driver";
        private const string DataSourceKeyword = "Data Source";
        private const string DataSourceNoSpaceKeyword = "DataSource";
        private const string ModeKeyword = "Mode";
        private const string CacheKeyword = "Cache";
        private const string FilenameKeyword = "Filename";
        private const string PasswordKeyword = "Password";
        private const string ForeignKeysKeyword = "Foreign Keys";
        private const string RecursiveTriggersKeyword = "Recursive Triggers";
        private const string VersionKeyword = "Version";

#if NET40
        private static readonly List<String> _validKeywords;
        private static readonly Dictionary<string, SqliteConnKeywords> _keywords;
#else
        private static readonly IReadOnlyList<string> _validKeywords;
        private static readonly IReadOnlyDictionary<string, SqliteConnKeywords> _keywords;
#endif

        static SqliteConnectionStringBuilder()
        {
            var keyValues = Enum.GetNames(typeof(SqliteConnKeywords));

#if NETFrame
            DefaultVersion = SqliteVersion.V3;
#else
            DefaultVersion = SqliteVersion.V4;
#endif
            var validKeywords = new string[keyValues.Length];
            validKeywords[(int)SqliteConnKeywords.DataSource] = DataSourceKeyword;
            validKeywords[(int)SqliteConnKeywords.Mode] = ModeKeyword;
            validKeywords[(int)SqliteConnKeywords.Cache] = CacheKeyword;
            validKeywords[(int)SqliteConnKeywords.Password] = PasswordKeyword;
            validKeywords[(int)SqliteConnKeywords.ForeignKeys] = ForeignKeysKeyword;
            validKeywords[(int)SqliteConnKeywords.RecursiveTriggers] = RecursiveTriggersKeyword;
            validKeywords[(int)SqliteConnKeywords.Version] = VersionKeyword;
            validKeywords[(int)SqliteConnKeywords.Provider] = ProviderKeyword;
            validKeywords[(int)SqliteConnKeywords.Driver] = DriverKeyword;
#if NET40
            _validKeywords = validKeywords.ToList();
#else
            _validKeywords = validKeywords;
#endif
            _keywords = new Dictionary<string, SqliteConnKeywords>(8, StringComparer.OrdinalIgnoreCase)
            {
                [DataSourceKeyword] = SqliteConnKeywords.DataSource,
                [ModeKeyword] = SqliteConnKeywords.Mode,
                [CacheKeyword] = SqliteConnKeywords.Cache,
                [PasswordKeyword] = SqliteConnKeywords.Password,
                [ForeignKeysKeyword] = SqliteConnKeywords.ForeignKeys,
                [RecursiveTriggersKeyword] = SqliteConnKeywords.RecursiveTriggers,
                [VersionKeyword] = SqliteConnKeywords.Version,
                [ProviderKeyword] = SqliteConnKeywords.Provider,
                [DriverKeyword] = SqliteConnKeywords.Driver,
                // aliases
                [FilenameKeyword] = SqliteConnKeywords.DataSource,
                [DataSourceNoSpaceKeyword] = SqliteConnKeywords.DataSource
            };
        }

        /// <summary>
        /// Ĭ�Ϲ���
        /// </summary>
        public SqliteConnectionStringBuilder() { }

        /// <summary>
        /// �����ַ�������
        /// </summary>
        /// <param name="connectionString">�����ַ���,����Ϊ��</param>
        public SqliteConnectionStringBuilder(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private string _dataSource = string.Empty;
        /// <summary>
        /// ����Դ
        /// </summary>
        public virtual string DataSource
        {
            get => _dataSource;
            set => base[DataSourceKeyword] = _dataSource = value;
        }

        private SqliteOpenMode _mode = SqliteOpenMode.ReadWriteCreate;
        /// <summary>
        /// ���ݴ�ģʽ
        /// </summary>
        public virtual SqliteOpenMode Mode
        {
            get => _mode;
            set => base[ModeKeyword] = _mode = value;
        }
        private SqliteVersion _version = DefaultVersion;
        /// <summary>
        /// �汾
        /// </summary>
        public virtual SqliteVersion Version
        {
            get => _version;
            set => base[VersionKeyword] = _version = value;
        }
        private string _provider;
        /// <summary>
        /// �ṩ��
        /// </summary>
        public virtual String Provider
        {
            get => _provider;
            set => base[ProviderKeyword] = _provider = value;
        }
        private string _driver;
        /// <summary>
        /// ����
        /// </summary>
        public virtual string Driver
        {
            get => _driver;
            set => base[DriverKeyword] = _driver = value;
        }
        private SqliteCacheMode _cache = SqliteCacheMode.Default;
        /// <summary>
        /// ����ģʽ
        /// </summary>
        /// <seealso href="http://sqlite.org/sharedcache.html">SQLite Shared-Cache Mode</seealso>
        public virtual SqliteCacheMode Cache
        {
            get => _cache;
            set => base[CacheKeyword] = _cache = value;
        }
        private string _password = string.Empty;
        /// <summary>
        /// ��������,
        /// ����:SQLite�ⲻ֧�ּ���ʱ�������á�ָ��ʱ��<c>PRAGMA key</c>�ڴ����Ӻ��������͡�
        /// </summary>
        public string Password
        {
            get => _password;
            set => base[PasswordKeyword] = _password = value;
        }
        private bool? _foreignKeys;
        /// <summary>
        /// �Ƿ��������Լ��
        /// true: <c>PRAGMA foreign_keys = 1</c> ������ʱ����;
        /// false: <c>PRAGMA foreign_keys = 0</c> ������ʱ����;
        /// null: ������ָ��,ʹ��Ĭ��ֵ
        /// </summary>
        public bool? ForeignKeys
        {
            get => _foreignKeys;
            set => base[ForeignKeysKeyword] = _foreignKeys = value;
        }
        private bool _recursiveTriggers;
        /// <summary>
        /// ���õݹ鴥����
        /// true:<c>PRAGMA recursive_triggers</c>�ڴ�����ʱ����
        /// fasle:������
        /// </summary>
        public bool RecursiveTriggers
        {
            get => _recursiveTriggers;
            set => base[RecursiveTriggersKeyword] = _recursiveTriggers = value;
        }

        /// <summary>
        /// ��ȡ�����ַ����еĹؼ����б�
        /// </summary>
        public override ICollection Keys => new ReadOnlyCollection<string>(_validKeywords.ToList());

        /// <summary>
        /// ��ȡ�����ַ����е�ֵ�б�
        /// </summary>
        public override ICollection Values
        {
            get
            {
                var values = new object[_validKeywords.Count];
                for (var i = 0; i < _validKeywords.Count; i++)
                {
                    values[i] = GetAt((SqliteConnKeywords)i);
                }

                return new ReadOnlyCollection<object>(values);
            }
        }

        /// <summary>
        /// ָ����������ֵ
        /// </summary>
        /// <param name="keyword">��</param>
        /// <returns>ֵ</returns>
        public override object this[string keyword]
        {
            get => GetAt(GetIndex(keyword));
            set
            {
                if (value == null)
                {
                    Remove(keyword);

                    return;
                }

                switch (GetIndex(keyword))
                {
                    case SqliteConnKeywords.DataSource:
                        DataSource = Convert.ToString(value, CultureInfo.InvariantCulture);
                        return;

                    case SqliteConnKeywords.Mode:
                        Mode = ConvertToEnum<SqliteOpenMode>(value);
                        return;

                    case SqliteConnKeywords.Cache:
                        Cache = ConvertToEnum<SqliteCacheMode>(value);
                        return;

                    case SqliteConnKeywords.Password:
                        Password = Convert.ToString(value, CultureInfo.InvariantCulture);
                        return;

                    case SqliteConnKeywords.ForeignKeys:
                        ForeignKeys = ConvertToNullableBoolean(value);
                        return;

                    case SqliteConnKeywords.RecursiveTriggers:
                        RecursiveTriggers = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
                        return;

                    case SqliteConnKeywords.Version:
                        Version = ConvertToEnum<SqliteVersion>(value);
                        return;

                    case SqliteConnKeywords.Provider:
                        Provider = Convert.ToString(value, CultureInfo.InvariantCulture);
                        return;

                    case SqliteConnKeywords.Driver:
                        Driver = Convert.ToString(value, CultureInfo.InvariantCulture);
                        return;

                    default:
                        Debug.Assert(false, "Unexpected keyword: " + keyword);
                        return;
                }
            }
        }

        private static TEnum ConvertToEnum<TEnum>(object value)
            where TEnum : struct
        {
            if (value is string stringValue)
            {
                return (TEnum)Enum.Parse(typeof(TEnum), stringValue, ignoreCase: true);
            }
            if (value is not TEnum enumValue)
            {
                if (value.GetType().IsEnum)
                {
                    throw new ArgumentException(Resources.ConvertFailed(value.GetType(), typeof(TEnum)));
                }
                else
                {
                    enumValue = (TEnum)Enum.ToObject(typeof(TEnum), value);
                }
            }
            if (!Enum.IsDefined(typeof(TEnum), enumValue))
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, Resources.InvalidEnumValue(typeof(TEnum), enumValue));
            }
            return enumValue;
        }

        private static bool? ConvertToNullableBoolean(object value)
        {
            return value == null || (value is string stringValue && stringValue.Length == 0)
                ? null
                : (bool?)Convert.ToBoolean(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// ��������ַ���
        /// </summary>
        public override void Clear()
        {
            base.Clear();

            for (var i = 0; i < _validKeywords.Count; i++)
            {
                Reset((SqliteConnKeywords)i);
            }
        }

        /// <summary>
        /// �ж��Ƿ�����ؼ���
        /// </summary>
        /// <param name="keyword">�ؼ���</param>
        /// <returns>��ΪTrue,��ΪFalse</returns>
        public override bool ContainsKey(string keyword) => _keywords.ContainsKey(keyword);

        /// <summary>
        /// �Ƴ�ָ���Ĺؼ���
        /// </summary>
        /// <param name="keyword">�ؼ���</param>
        /// <returns>���ΪTrue,������ΪFalse</returns>
        public override bool Remove(string keyword)
        {
            if (_keywords.TryGetValue(keyword, out var index) && base.Remove(_validKeywords[(int)index]))
            {
                Reset(index);

                return true;
            }
            return false;
        }

        /// <summary>
        ///     Determines whether the specified key should be serialized into the connection string.
        /// </summary>
        /// <param name="keyword">The key to check.</param>
        /// <returns><see langword="true" /> if it should be serialized; otherwise, <see langword="false" />. </returns>
        public override bool ShouldSerialize(string keyword)
            => _keywords.TryGetValue(keyword, out var index) && base.ShouldSerialize(_validKeywords[(int)index]);

        /// <summary>
        ///     Gets the value of the specified key if it is used.
        /// </summary>
        /// <param name="keyword">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true" /> if the key was used; otherwise, <see langword="false" />. </returns>
        public override bool TryGetValue(string keyword, out object value)
        {
            if (!_keywords.TryGetValue(keyword, out var index))
            {
                value = null;

                return false;
            }

            value = GetAt(index);

            return true;
        }

        private object GetAt(SqliteConnKeywords index)
        {
            switch (index)
            {
                case SqliteConnKeywords.DataSource: return DataSource;
                case SqliteConnKeywords.Mode: return Mode;
                case SqliteConnKeywords.Cache: return Cache;
                case SqliteConnKeywords.Password: return Password;
                case SqliteConnKeywords.ForeignKeys: return ForeignKeys;
                case SqliteConnKeywords.RecursiveTriggers: return RecursiveTriggers;
                case SqliteConnKeywords.Version: return Version;
                case SqliteConnKeywords.Provider: return Provider;
                case SqliteConnKeywords.Driver: return Driver;
                default: Debug.Assert(false, "Unexpected keyword: " + index); return null;
            }
        }

        private static SqliteConnKeywords GetIndex(string keyword)
            => !_keywords.TryGetValue(keyword, out var index)
                ? throw new ArgumentException(Resources.KeywordNotSupported(keyword))
                : index;

        private void Reset(SqliteConnKeywords index)
        {
            switch (index)
            {
                case SqliteConnKeywords.DataSource:
                    _dataSource = string.Empty;
                    return;

                case SqliteConnKeywords.Mode:
                    _mode = SqliteOpenMode.ReadWriteCreate;
                    return;

                case SqliteConnKeywords.Cache:
                    _cache = SqliteCacheMode.Default;
                    return;

                case SqliteConnKeywords.Password:
                    _password = string.Empty;
                    return;

                case SqliteConnKeywords.ForeignKeys:
                    _foreignKeys = null;
                    return;

                case SqliteConnKeywords.RecursiveTriggers:
                    _recursiveTriggers = false;
                    return;

                case SqliteConnKeywords.Driver:
                    _driver = string.Empty;
                    return;
                case SqliteConnKeywords.Provider:
                    _provider = string.Empty;
                    return;
                case SqliteConnKeywords.Version:
                    _version = DefaultVersion;
                    return;

                default:
                    Debug.Assert(false, "Unexpected keyword: " + index);
                    return;
            }
        }
    }
}
