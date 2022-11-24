using DecoratorExample.Decorator.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorExample.Decorator.Concretes;

public class DataSourceDecorator : IDataSource
{
    protected IDataSource _dataSource { get; set; }

    public DataSourceDecorator(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }


    public virtual void WriteData(string data)
    {
        _dataSource.WriteData(data);
    }

    public virtual string? ReadData()
    {
        return _dataSource.ReadData();
    }
}
