using System.Reflection;
using System.Text;

namespace HomeWork_7;

internal class Program
{
	public static string ObjectToString(object o)
	{
		StringBuilder stringBuilder = new();
		Type t = o.GetType();
		var fieldsInfo = t.GetFields();
		foreach (var fi in fieldsInfo)
		{
			CustomNameAttribute attr = fi.GetCustomAttribute<CustomNameAttribute>();
			if (attr != null)
			{
				string fieldName = attr.Name;
				object fieldValue = fi.GetValue(o);
				stringBuilder.Append(fieldName + ':' + fieldValue + '\n');
			}
		}
		return stringBuilder.ToString();
	}

	public static void StringToObject(string data, object o)
	{
		string[] newLines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string nl in newLines)
		{
			string[] keyVal = nl.Split(':');
			string fieldName = keyVal[0];
			string fieldValue = keyVal[1];
			Type t=o.GetType();
			var fieldsInfo=t.GetFields();
			foreach(FieldInfo fi in fieldsInfo)
			{
				CustomNameAttribute attr = fi.GetCustomAttribute<CustomNameAttribute>();
				if (attr.Name ==fieldName)
				{
					Type fieldType = fi.FieldType;
					object modValue = Convert.ChangeType(fieldValue,fieldType);
					fi.SetValue(o, modValue);
					break;
				}
			}
		}
	}

	static void Main(string[] args)
	{
		MyClass obj1 = new();
		string dataObj = ObjectToString(obj1);
		Console.WriteLine(dataObj);

		MyClass obj2 = new();
		StringToObject(dataObj, obj2);
		Console.WriteLine(obj2.I);
	}
}
