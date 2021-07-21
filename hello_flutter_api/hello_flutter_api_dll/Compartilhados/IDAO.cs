using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDAO<F, T>
{
	string TableName { get; }

	SqlConnection Connection { get; set; }

	void ValidateModel(T model);

	void ValidateFilter(F filtro);

	string GetSqlSelect(F filtro);

	T FindOne(F filtro);

	List<T> FindAll(F filtro);

	T LoadObject(IDataReader dr);
}