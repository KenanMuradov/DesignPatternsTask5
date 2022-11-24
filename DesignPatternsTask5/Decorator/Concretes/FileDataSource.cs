using DecoratorExample.Decorator.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorExample.Decorator.Concretes;

public class FileDataSource : IDataSource
{
    private string _filePath;

    public FileDataSource(string filePath) => _filePath = filePath;
    public string? ReadData()
    {
        FileInfo file = new FileInfo(_filePath);

        if (file.Exists)
            return File.ReadAllText(file.FullName);

        return null;
    }

    public void WriteData(string data) => File.WriteAllText(_filePath, data);

    public string GetFilePath() => _filePath;
}
