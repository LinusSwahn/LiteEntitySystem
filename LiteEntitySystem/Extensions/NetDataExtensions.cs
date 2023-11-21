using System;
using System.Runtime.CompilerServices;
using LiteNetLib.Utils;
#if UNITY_2021_2_OR_NEWER
using UnityEngine;
#endif

namespace LiteEntitySystem.Extensions
{
    public static class NetDataExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Put<T>(this NetDataWriter writer, T e) where T : unmanaged, Enum
        {
            switch (sizeof(T))
            {
                case 1: writer.Put(*(byte*)&e); break;
                case 2: writer.Put(*(short*)&e); break;
                case 4: writer.Put(*(int*)&e); break;
                case 8: writer.Put(*(long*)&e); break;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Get<T>(this NetDataReader reader, out T result) where T : unmanaged, Enum
        {
            var e = default(T);
            switch (sizeof(T))
            {
                case 1: (*(byte*)&e) = reader.GetByte(); break;
                case 2: (*(short*)&e) = reader.GetShort(); break;
                case 4: (*(int*)&e) = reader.GetInt(); break;
                case 8: (*(long*)&e) = reader.GetLong(); break;
            }
            result = e;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T Get<T>(this NetDataReader reader) where T : unmanaged, Enum
        {
            var e = default(T);
            switch (sizeof(T))
            {
                case 1: (*(byte*)&e) = reader.GetByte(); break;
                case 2: (*(short*)&e) = reader.GetShort(); break;
                case 4: (*(int*)&e) = reader.GetInt(); break;
                case 8: (*(long*)&e) = reader.GetLong(); break;
            }
            return e;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<byte> AsReadOnlySpan(this NetDataReader reader)
        {
            return new ReadOnlySpan<byte>(reader.RawData, reader.Position, reader.AvailableBytes);
        }
    }
}