using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDAOSaveDelete<T>
{
    string Save(T model);

    string Delete(T model);
}

