using DecoratorExample.Decorator.Abstracts;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorExample.Decorator.Concretes;

public class DataSourceCompressionDecorator : DataSourceDecorator
{
    public DataSourceCompressionDecorator(IDataSource dataSource) : base(dataSource)
    {
    }

    public override void WriteData(string data)
    {
        base.WriteData(Compress(data));
    }

    public override string? ReadData() => Decompress(base.ReadData());

    public static string Compress(string uncompressedString)
    {
        byte[] compressedBytes;

        using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(uncompressedString)))
        {
            using var compressedStream = new MemoryStream();

            using (var compressorStream = new DeflateStream(compressedStream, CompressionLevel.Fastest, true))
            {
                uncompressedStream.CopyTo(compressorStream);
            }

            compressedBytes = compressedStream.ToArray();
        }

        return Convert.ToBase64String(compressedBytes);
    }


    public static string Decompress(string? compressedString)
    {
        byte[] decompressedBytes;

        var compressedStream = new MemoryStream(Convert.FromBase64String(compressedString));

        using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
        {
            using (var decompressedStream = new MemoryStream())
            {
                decompressorStream.CopyTo(decompressedStream);

                decompressedBytes = decompressedStream.ToArray();
            }
        }

        return Encoding.UTF8.GetString(decompressedBytes);
    }
}
